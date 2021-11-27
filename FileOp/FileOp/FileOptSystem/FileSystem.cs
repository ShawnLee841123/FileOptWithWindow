using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

public class FileSystem : Singleton<FileSystem>
{
	#region Variable
	protected object lockObject;
	#region

	#endregion

	#region Base File Info
	public string m_strFileName { get; protected set; }				//	file name
	public string m_strFileContent { get; protected set; }          //	file content
	public string m_strTempContent { get; protected set; }
	public int m_nContentStringSize { get; protected set; }			//	base file size
	public int m_uFileLines { get; protected set; }					//	base file lines count number
	public List<string> m_listFileLines { get; protected set; }     //	base file lines content

	public string OutFileName { get; protected set; }
	#endregion

	#region File Op
	public int BlockCount { get; protected set; }
	public int CurFinished { get; protected set; }
	public string m_strKeyWords { get; protected set; }
	public string m_strReplaceWords { get; protected set; }
	public Dictionary<int, string> m_dicThreadContent { get; protected set; }
	#endregion

	#endregion

	#region Function
	public FileSystem()
	{
		lockObject = new object();
		ResetFile();
	}

	public void ResetFile()
	{
		m_strFileName = "";
		m_strFileContent = "";
		m_nContentStringSize = 0;
		m_uFileLines = 0;
		m_strKeyWords = "";
		if (null == m_listFileLines)
			m_listFileLines = new List<string>();

		if (null == m_dicThreadContent)
			m_dicThreadContent = new Dictionary<int, string>();

		BlockCount = 0;
		CurFinished = 0;
	}

	public void SetFileName(string strFileName)
	{
		if (!CheckStringValid(strFileName))
			return;

		m_strFileName = strFileName;
		m_uFileLines = 0;
		m_listFileLines.Clear();
	}

	public bool SetFileContent(string strContent)
	{
		if (!CheckStringValid(strContent))
			return false;

		m_strFileContent = strContent;
		return true;
	}

	public void AddFileStringLine(string strContent)
	{
		m_strFileContent += strContent;
		m_listFileLines.Add(strContent);
		m_uFileLines++;
	}

	#region File Content Op
	public bool SwitchCatchContent()
	{
		if (!CheckStringValid(m_strTempContent))
		{
			return false;
		}

		m_strFileContent = m_strTempContent;
		m_strTempContent = "";
		return true;
	}

	public void SetOperatedKeyWords(string strKey)
	{
		m_strKeyWords = strKey;
	}

	public void SetOperatedReplaceWords(string strKey)
	{
		m_strReplaceWords = strKey;
	}

	public bool SetJobsDoneContent(string strContent)
	{
		if (!CheckStringValid(strContent))
		{
			return false;
		}

		m_strTempContent = strContent;
		return true;
	}

	public bool PreFileOp(int threadCount)
	{
		if (!CheckStringValid(m_strKeyWords))
			return true;

		if (m_uFileLines > 1)
		{
			return true;
		}

		if (m_nContentStringSize > 1)
		{
			return true;
		}

		int nSize = FileSystem.Ins().AverageStringInThread(threadCount);
		Dictionary<int, string> FullTextContent = new Dictionary<int, string>();
		ConstructOpContentString(nSize, threadCount, ref FullTextContent);
		m_dicThreadContent = FullTextContent;

		return true;
	}

	//	Calculate how many lines need to be operated in each thread 
	public int AverageLinesInThread(int nThreadCount)
	{
		int linesAOp = m_uFileLines / nThreadCount;
		return linesAOp;
	}

	//	Calculate how many characters need to be operated in eatch thread
	public int AverageStringInThread(int nBlockCount)
	{
		int nSize = m_strFileContent.Length / nBlockCount;
		

		return nSize;
	}
	public bool ConstructOpContentString(int nSize, int nBlockCount, ref Dictionary<int, string> dicContet)
	{
		dicContet.Clear();
		for (int i = 0; i < nBlockCount; i++)
		{
			string strTemp = "";
			if (i < nBlockCount - 1)
			{
				strTemp = m_strFileContent.Substring((i * nSize), nSize);
			}
			else
			{
				strTemp = m_strFileContent.Substring(i * nSize);
			}

			dicContet.Add(i, strTemp);
		}

		int nKeyWordsLen = m_strKeyWords.Length;
		//	string end contained part of key word
		for(int i = 0; i < nBlockCount; i++)
		{
			string strContent = dicContet[i];
			string strBegin = strContent.Substring(0, nKeyWordsLen);
			int nEndPos = -1;
			if (CheckStringBeginHaveKeyWords(strBegin, ref nEndPos))
			{
				if (i > 0)
				{
					string strLastContent = dicContet[i - 1];
					int nContentLen = strLastContent.Length;
					string strEnd = strLastContent.Substring(nContentLen - nKeyWordsLen);
					int nBeginPos = -1;
					if (CheckStringEndHaveKeyWords(strEnd, ref nBeginPos))
					{
						string strKeyContent = strContent.Substring(0, nEndPos);
						string strTempContent = strContent.Substring(nEndPos);
						dicContet[i] = strTempContent;
						dicContet[i - 1] += strKeyContent;
					}	
				}
			}
		}

		return true;
	}

	public bool CheckStringBeginHaveKeyWords(string strBegin, ref int nEndPos)
	{
		int nKeyWordsLen = m_strKeyWords.Length;
		for(int i = 1; i < nKeyWordsLen; i++)
		{
			//	Get keywords character from end point in keys
			string subKey = m_strKeyWords.Substring(nKeyWordsLen - i);
			//	get keywords character from begin string
			string subBeg = strBegin.Substring(0, i);
			
			if (subKey == subBeg)
			{
				nEndPos = i;
				return true;
			}
		}
		return false;
	}

	public bool CheckStringEndHaveKeyWords(string strEnd, ref int nBeginPos)
	{
		int nKeyWordsLen = m_strKeyWords.Length;
		for (int i = 1; i < nKeyWordsLen; i++)
		{
			string subKey = m_strKeyWords.Substring(0, i);
			string subEnd = strEnd.Substring(nKeyWordsLen - i);
			if (subEnd == subKey)
			{
				nBeginPos = nKeyWordsLen - i;
				return true;
			}
		}

		return false;
	}

	#endregion

	#region Display
	public void SetContentBlockCount(int nBlockCount)
	{
		BlockCount = nBlockCount;
	}

	public void AddCurrentFinishedBlock()
	{
		lock (lockObject)
		{
			CurFinished++;
			UpdateProcess();
		}
	}

	public void UpdateProcess()
	{
		float percent = ((float)(CurFinished)) / ((float)(BlockCount)) * 100.0f;
		FileOp.Program.MyWindow.ProgressBar.Value = (int)percent;
		FileOp.Program.MyWindow.ProgressValue.Text = string.Format("{0}%", (int)percent);
	}
	#endregion

	#endregion
}

