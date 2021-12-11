using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

public class FileSystem : Singleton<FileSystem>
{
	public enum CodeType
	{
		CT_ASCII = 0,
		CT_UTF8,
		CT_GB,
		CT_GBK,
		CT_BIG5,

		CT_MAX
	}
	#region Variable
	protected object lockObject;
	#region

	#endregion

	#region Base File Info
	public string m_strFileName { get; protected set; }             //	file name
	public string m_strFileContent { get; protected set; }          //	file content
	public string m_strTempContent { get; protected set; }
	public int m_nContentStringSize { get; protected set; }         //	base file size
	public int m_uFileLines { get; protected set; }                 //	base file lines count number
	public List<string> m_listFileLines { get; protected set; }     //	base file lines content

	public List<string> m_listFinishedLines;
	//public List<char> m_CharContent { get; protected set; }
	//public List<char> m_tempCharContent { get; protected set; }
	public string OutFileName { get; protected set; }
	#endregion

	#region File Op
	public int BlockCount { get; protected set; }
	public int CurFinished { get; protected set; }
	public string m_strKeyWords { get; protected set; }
	public string m_strReplaceWords { get; protected set; }

	public string m_strLineFlag { get; protected set; }

	public Dictionary<int, string> m_dicThreadContent { get; protected set; }

	public CodeType m_eFileCodeType { get; protected set; }
	//public Dictionary<int, List<char>> m_dicCharThreadContent { get; protected set; }

	public ReadThread m_pReader;
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
		else
			m_listFileLines.Clear();

		if (null == m_dicThreadContent)
			m_dicThreadContent = new Dictionary<int, string>();
		else
			m_dicThreadContent.Clear();

		if (null == m_listFinishedLines)
		{
			m_listFinishedLines = new List<string>();
		}
		else
		{
			m_listFinishedLines.Clear();
		}

		if (null == m_pReader)
		{
			m_pReader = new ReadThread();
		}
		else
		{
			m_pReader.Reset();
		}

		BlockCount = 0;
		CurFinished = 0;
		m_eFileCodeType = CodeType.CT_ASCII;
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
		
		Program.MyWindow.Invoke(Program.MyWindow.UpdateFileContent, m_strFileContent);
		return true;
	}

	public bool ReadFile(string strFileName = "")
	{
		string strReadName = strFileName;
		if (strReadName.Equals(""))
		{
			strReadName = m_strFileName;
		}

		if (null != m_pReader)
		{
			m_pReader.BeginRead(strReadName);
		}
			
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

		lock (lockObject)
		{
			m_strFileContent = m_strTempContent;
			m_nContentStringSize = m_strFileContent.Length;
			m_strTempContent = "";
			BlockCount = 0;
			CurFinished = 0;
			m_dicThreadContent.Clear();
		}

		return true;
	}

	public void SetOperatedKeyWords(string strKey)
	{
		m_strKeyWords = strKey;
	}

	public void SetLineFlag(string strFlag)
	{
		if (!CheckStringValid(strFlag))
		{
			return;
		}

		m_strLineFlag = strFlag;
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
		Program.MyWindow.Invoke(Program.MyWindow.UpdateText, m_strTempContent);
		return true;
	}

	public bool PreFileOp(int threadCount, string strKeyWords)
	{
		if (!CheckStringValid(m_strKeyWords))
			return false;

		//if (m_uFileLines > 1)
		//{
		//	return true;
		//}

		//if (m_nContentStringSize > 1)
		//{
		//	return true;
		//}
		BlockCount = 0;
		CurFinished = 0;
		m_dicThreadContent.Clear();

		m_eFileCodeType = (CodeType)GetStringCode(m_strFileContent);
		int nSize = FileSystem.Ins().AverageStringInThread(threadCount);
		Dictionary<int, string> FullTextContent = new Dictionary<int, string>();
		ConstructOpContentString(nSize, threadCount, strKeyWords, m_strFileContent, ref FullTextContent);
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
	public bool ConstructOpContentString(int nSize, int nBlockCount, string strKeyWords, string strContentIn, ref Dictionary<int, string> dicContet)
	{
		dicContet.Clear();
		for (int i = 0; i < nBlockCount; i++)
		{
			string strTemp = "";
			if (i < nBlockCount - 1)
			{
				strTemp = strContentIn.Substring((i * nSize), nSize);
			}
			else
			{
				strTemp = strContentIn.Substring(i * nSize);
			}

			dicContet.Add(i, strTemp);
		}

		int nKeyWordsLen = strKeyWords.Length;
		//	string end contained part of key word
		for(int i = 0; i < nBlockCount; i++)
		{
			string strContent = dicContet[i];
			string strBegin = strContent.Substring(0, nKeyWordsLen);
			int nEndPos = -1;
			if (CheckStringBeginHaveKeyWords(strBegin, strKeyWords, ref nEndPos))
			{
				if (i > 0)
				{
					string strLastContent = dicContet[i - 1];
					int nContentLen = strLastContent.Length;
					string strEnd = strLastContent.Substring(nContentLen - nKeyWordsLen);
					int nBeginPos = -1;
					if (CheckStringEndHaveKeyWords(strEnd, strKeyWords, ref nBeginPos))
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

	public bool CheckStringBeginHaveKeyWords(string strBegin, string strKeyWords, ref int nEndPos)
	{
		int nKeyWordsLen = strKeyWords.Length;
		for(int i = 1; i < nKeyWordsLen; i++)
		{
			//	Get keywords character from end point in keys
			string subKey = strKeyWords.Substring(nKeyWordsLen - i);
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

	public bool CheckStringEndHaveKeyWords(string strEnd, string strKeyWords, ref int nBeginPos)
	{
		int nKeyWordsLen = strKeyWords.Length;
		for (int i = 1; i < nKeyWordsLen; i++)
		{
			string subKey = strKeyWords.Substring(0, i);
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
		lock (lockObject)
		{
			float percent = ((float)(CurFinished)) / ((float)(BlockCount)) * 100.0f;
			Program.MyWindow.Invoke(Program.MyWindow.UpdateValue, (int)percent);
		}
	}
	#endregion

	#region string code
	public int GetStringCode(string strValue)
	{
		if (CheckStringIsUTF8Code(strValue))
		{
			return (int)CodeType.CT_UTF8;
		}
		
		if(CheckStringIsGBCode(strValue))
		{
			return (int)CodeType.CT_GB;
		}

		if (CheckStringIsGBKCode(strValue))
		{
			return (int)CodeType.CT_GBK;
		}

		if (CheckStringIsBig5Code(strValue))
		{
			return (int)CodeType.CT_BIG5;
		}

		return (int)CodeType.CT_ASCII;
	}

	public bool GetDestString(string strIn, int nCodeType, ref string strOut)
	{
		if (!CheckStringValid(strIn))
		{
			return false;
		}

		int nInType = GetStringCode(strIn);
		CodeType eType = (CodeType)nInType;
		CodeType eOutType = (CodeType)nCodeType;
		Encoding codingIn;
		Encoding codingOut;
		byte[] arrIn;
		switch(eType)
		{
			case CodeType.CT_UTF8:
				{
					codingIn = Encoding.UTF8;
					arrIn = Encoding.UTF8.GetBytes(strIn);
				}
				break;
			case CodeType.CT_GB:
				{
					codingIn = Encoding.GetEncoding("GB2312");
					arrIn = Encoding.GetEncoding("GB2312").GetBytes(strIn);
				}
				break;
			case CodeType.CT_GBK:
				{
					codingIn = Encoding.GetEncoding("GBK");
					arrIn = Encoding.GetEncoding("GBK").GetBytes(strIn);
				}
				break;
			case CodeType.CT_BIG5:
				{
					codingIn = Encoding.GetEncoding("Big5");
					arrIn = Encoding.GetEncoding("Big5").GetBytes(strIn);
				}
				break;
			default:
				return false;
		}

		//switch (eOutType)
		//{
		//	case CodeType.CT_UTF8:
		//		{
		//			strOut = Encoding.UTF8.GetString(arrIn);
		//		}
		//		break;
		//	case CodeType.CT_GB:
		//		{
		//			strOut = Encoding.GetEncoding("GB2312").GetString(arrIn);
		//		}
		//		break;
		//	case CodeType.CT_GBK:
		//		{
		//			strOut = Encoding.GetEncoding("GBK").GetString(arrIn);
		//		}
		//		break;
		//	case CodeType.CT_BIG5:
		//		{
		//			strOut = Encoding.GetEncoding("Big5").GetString(arrIn);
		//		}
		//		break;
		//	default:
		//		return false;
		//}

		//return true;

		byte[] arrOut;// = Encoding.Convert(eType, eOutType, arrIn);
		switch (eOutType)
		{
			case CodeType.CT_UTF8:
				{
					codingOut = Encoding.UTF8;
					arrOut = Encoding.Convert(codingIn, Encoding.UTF8, arrIn);
				}
				break;
			case CodeType.CT_GB:
				{
					codingOut = Encoding.GetEncoding("GB2312");
					arrOut = Encoding.Convert(codingIn, Encoding.GetEncoding("GB2312"), arrIn);
					//arrOut = Encoding.GetEncoding("GB2312").GetString(arrIn);
				}
				break;
			case CodeType.CT_GBK:
				{
					codingOut = Encoding.GetEncoding("GBK");
					arrOut = Encoding.Convert(codingIn, Encoding.GetEncoding("GBK"), arrIn);
					//arrOut = Encoding.GetEncoding("GBK").GetString(arrIn);
				}
				break;
			case CodeType.CT_BIG5:
				{
					codingOut = Encoding.GetEncoding("Big5");
					arrOut = Encoding.Convert(codingIn, Encoding.GetEncoding("Big5"), arrIn);
					//arrOut = Encoding.GetEncoding("Big5").GetString(arrIn);
				}
				break;
			default:
				return false;
		}

		strOut = codingOut.GetString(arrOut);
		return true;
	}
	
	public bool CheckStringIsGBCode(string strValue)
	{
		byte[] bytes = Encoding.GetEncoding("GB2312").GetBytes(strValue);
		if (bytes.Length <= 1) // if there is only one byte, it is ASCII code or other code
		{
			return false;
		}
		else
		{
			byte byte1 = bytes[0];
			byte byte2 = bytes[1];
			if (byte1 >= 176 && byte1 <= 247 && byte2 >= 160 && byte2 <= 254)    //判断是否是GB2312
			{
				return true;
			}
		}

		return false;
	}

	public bool CheckStringIsGBKCode(string strValue)
	{
		byte[] bytes = Encoding.GetEncoding("GBK").GetBytes(strValue.ToString());
		if (bytes.Length <= 1) // if there is only one byte, it is ASCII code
		{
			return false;
		}
		else
		{
			byte byte1 = bytes[0];
			byte byte2 = bytes[1];
			if (byte1 >= 129 && byte1 <= 254 && byte2 >= 64 && byte2 <= 254)     //判断是否是GBK编码
			{
				return true;
			}
		}

		return false;
	}

	public bool CheckStringIsBig5Code(string strValue)
	{
		byte[] bytes = Encoding.GetEncoding("Big5").GetBytes(strValue.ToString());
		if (bytes.Length <= 1) // if there is only one byte, it is ASCII code
		{
			return false;
		}
		else
		{
			byte byte1 = bytes[0];
			byte byte2 = bytes[1];
			if ((byte1 >= 129 && byte1 <= 254) && ((byte2 >= 64 && byte2 <= 126) || (byte2 >= 161 && byte2 <= 254)))     //判断是否是Big5编码
			{
				return true;
			}
		}

		return false;
	}

	public bool CheckStringIsUTF8Code(string strValue)
	{
		byte[] bytes = Encoding.UTF8.GetBytes(strValue.ToString());
		for (int i = 0; i < bytes.Length; i++)
		{
			if ((bytes[i] & 0xE0) == 0xC0) // 110x xxxx 10xx xxxx
			{
				if ((bytes[i + 1] & 0x80) != 0x80)
				{
					return false;
				}
			}
			else if ((bytes[i] & 0xF0) == 0xE0) // 1110 xxxx 10xx xxxx 10xx xxxx
			{
				if ((bytes[i + 1] & 0x80) != 0x80 || (bytes[i + 2] & 0x80) != 0x80)
				{
					return false;
				}
			}
			else if ((bytes[i] & 0xF8) == 0xF0) // 1111 0xxx 10xx xxxx 10xx xxxx 10xx xxxx
			{
				if ((bytes[i + 1] & 0x80) != 0x80 || (bytes[i + 2] & 0x80) != 0x80 || (bytes[i + 3] & 0x80) != 0x80)
				{
					return false;
				}
			}
		}

		return true;
	}
	#endregion

	#endregion
}

