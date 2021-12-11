using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

public class WorkThread: ThreadBase
{
	public List<string> ElementList = new List<string>();
	public List<string> FinishList = new List<string>();
	public string FindKey { get; protected set; }
	public string ReplaceWords { get; protected set; }

	public string SplitLineFlag { get; protected set; }
	public List<char> m_CharFindKey { get; protected set; }
	public List<char> m_CharReplaceWords { get; protected set; }
	public int CurIndex { get; protected set; }
	public int ElementCount { get; protected set; }

	public int BlockSize { get; protected set; }

	public WorkThread()
	{
		Reset();
	}

	public void Reset()
	{
		if (null == lockObj)
		{
			lockObj = new object();
		}

		lock(lockObj)
		{
			if (null != ElementList)
			{
				ElementList.Clear();
			}
			else
			{
				ElementList = new List<string>();
			}

			if (null != FinishList)
			{
				FinishList.Clear();
			}
			else
			{
				FinishList = new List<string>();
			}

			if (null != m_CharFindKey)
			{
				m_CharFindKey.Clear();
				
			}
			else
			{
				m_CharFindKey = new List<char>();
			}

			if (null != m_CharReplaceWords)
			{
				m_CharReplaceWords.Clear();
			}
			else
			{
				m_CharReplaceWords = new List<char>();
			}


			FindKey = "";
			ReplaceWords = "";
			SplitLineFlag = "";
			CurIndex = 0;
			BlockSize = 1024;
		}
	}
	override public void StartThread()
	{
		if (!CheckStringValid(SplitLineFlag))
		{
			TickEvent = ReplaceTickProcess;
		}
		else
		{
			TickEvent = SplitLinesTickProcess;
		}

		m_th = new Thread(ThreadTick);
		Enable = true;
		m_th.Start();
	}

	override protected bool TickProcess()
	{
		lock (lockObj)
		{
			bool bRet = false;
			if (null != TickEvent)
			{
				bRet = TickEvent();
			}
			return bRet;
		}

		return true;
	}

	public bool ReplaceTickProcess()
	{
		if (!CheckStringValid(FindKey))
		{
			return false;
		}

		if (null == ReplaceWords)
		{
			return false;
		}

		if (CurIndex >= ElementCount)
		{
			return false;
		}

		
		FinishList.Add(ElementList[CurIndex].Replace(FindKey, ReplaceWords));
		CurIndex++;
		FileSystem.Ins().AddCurrentFinishedBlock();
		
		return true;
	}

	public bool SplitLinesTickProcess()
	{
		if (!CheckStringValid(SplitLineFlag))
		{
			return false;
		}

		if (CurIndex >= ElementCount)
		{
			return false;
		}

		string[] arrLines = ElementList[CurIndex].Split(new string[] { SplitLineFlag }, 0);
		if (FinishList.Count > 0)
		{
			FinishList[FinishList.Count - 1] += arrLines[0];
			for (int i = 1; i < arrLines.Length; i++)
			{
				FinishList.Add(arrLines[i]);
			}
		}
		else
		{
			for (int i = 0; i < arrLines.Length; i++)
			{
				FinishList.Add(arrLines[i]);
			}
		}
		
		
		CurIndex++;
		FileSystem.Ins().AddCurrentFinishedBlock();

		return true;
	}

	public bool ReplaceCharContent(string strContent, ref string strOut)
	{
		byte[] arrContent = System.Text.Encoding.Default.GetBytes(strContent);
		List<char> tempRet = new List<char>();
		bool bStart = false;
		int nStartPos = 0;
		
		for (int i = 0; i < arrContent.Length; i++)
		{
			while(bStart)
			{
				int nEndPos = 0;
				for (int j = 1; j < m_CharFindKey.Count; j++)
				{
					if (arrContent[nStartPos + j] != m_CharFindKey[j])
					{
						nEndPos = j;
						break;
					}
				}

				if (nEndPos < m_CharFindKey.Count - 1)
				{
					bStart = false;
					tempRet.Add((char)arrContent[nStartPos]);
				}
				else
				{
					for(int k = 0; k < m_CharReplaceWords.Count; k++)
					{
						tempRet.Add(m_CharReplaceWords[k]);
					}

					bStart = false;
					i = nStartPos + m_CharFindKey.Count;
				}
			}

			if ((char)arrContent[i] == m_CharFindKey[0])
			{
				if (i + m_CharFindKey.Count < arrContent.Length)
				{
					bStart = true;
					nStartPos = i;
				}
			}

			tempRet.Add((char)arrContent[i]);
		}

		strOut = System.Text.Encoding.Default.GetString(arrContent);
		return true;
	}

	public bool SetFindKeyWords(string Words)
	{
		if (!CheckStringValid(Words))
		{
			return false;
		}
		lock(lockObj)
		{
			//FileSystem.CodeType codeType = FileSystem.Ins().m_eFileCodeType;
			//string strTemp = "";
			//if (!FileSystem.Ins().GetDestString(Words, (int)codeType, ref strTemp))
			//{
			//	return false;
			//}

			FindKey = Words;
			byte[] arrContent = System.Text.Encoding.Default.GetBytes(FindKey);
			for (int i = 0; i < arrContent.Length; i++)
			{
				m_CharFindKey.Add((char)arrContent[i]);
			}
		}

		return true;
	}

	public bool SetReplaceWords(string Words)
	{
		if (null == Words)
		{
			return false;
		}

		lock(lockObj)
		{
			if (Words.Contains("\\"))
			{
				
			}
			if (Words.Length > 0)
			{
				//FileSystem.CodeType codeType = FileSystem.Ins().m_eFileCodeType;
				//string strTemp = "";
				//if (!FileSystem.Ins().GetDestString(Words, (int)codeType, ref strTemp))
				//{
				//	return false;
				//}

				ReplaceWords = Words;
				byte[] arrContent = System.Text.Encoding.Default.GetBytes(ReplaceWords);
				for (int i = 0; i < arrContent.Length; i++)
				{
					m_CharFindKey.Add((char)arrContent[i]);
				}
			}
		}
		return true;
	}

	public bool SetSplitLineFlag(string Words)
	{
		if (!CheckStringValid(Words))
		{
			return false;
		}
		SplitLineFlag = Words;
		return true;
	}

	public bool SetStringElementList(List<string> eleList)
	{
		if (!CheckListValid(eleList))
		{
			return false;
		}

		lock(lockObj)
		{
			ElementList = eleList;
			CurIndex = 0;
			ElementCount = ElementList.Count;
		}
		return true;
	}

	public bool SetWorkingContent(string strContent)
	{
		if (!CheckStringValid(strContent))
		{
			return false;
		}

		string tempString = strContent;
		int ContentSize = strContent.Length;
		int blockCount = (ContentSize / BlockSize) + 1;

		Dictionary<int, string> FullTextContent = new Dictionary<int, string>();
		FileSystem.Ins().ConstructOpContentString(BlockSize, blockCount, SplitLineFlag, strContent, ref FullTextContent);

		foreach(KeyValuePair<int, string> var in FullTextContent)
		{
			ElementList.Add(var.Value);
		}

		CurIndex = 0;
		ElementCount = ElementList.Count;

		return true;
	}

	public bool GetJobsDone(ref string strContent)
	{
		if (ElementCount <= 0)
		{
			return false;
		}

		for(int i = 0; i < ElementCount; i++)
		{
			strContent += FinishList[i];
		}

		return true;
	}

	public bool GetSplitDone(ref List<string> listOut)
	{
		if (null == listOut)
		{
			return false;
		}

		if (FinishList.Count <= 0)
		{
			return true;
		}

		if (listOut.Count > 0)
		{
			listOut[listOut.Count - 1] += FinishList[0];
		}

		for(int i = 1; i < FinishList.Count; i++)
		{
			listOut.Add(FinishList[i]);
		}

		return true;
	}
}

