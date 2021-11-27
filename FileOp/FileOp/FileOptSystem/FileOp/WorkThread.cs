using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class WorkThread: ThreadBase
{
	public List<string> ElementList = new List<string>();

	public string FindKey { get; protected set; }
	public string ReplaceWords { get; protected set; }
	public int CurIndex { get; protected set; }
	public int ElementCount { get; protected set; }

	public int BlockSize { get; protected set; }

	protected object lockObj = new object();

	public WorkThread()
	{
		Reset();
	}

	public void Reset()
	{
		
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

			FindKey = "";
			ReplaceWords = "";
			CurIndex = 0;
			BlockSize = 1024;
		}
	}
	override protected bool TickProcess()
	{
		if (!CheckStringValid(FindKey))
		{
			return false;
		}

		if (!CheckStringValid(ReplaceWords))
		{
			return true;
		}

		if (CurIndex >= ElementCount)
		{
			return false;
		}

		lock (lockObj)
		{
			ElementList[CurIndex].Replace(FindKey, ReplaceWords);
			CurIndex++;
		}
		
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
			FindKey = Words;
		}

		return true;
	}

	public bool SetReplaceWords(string Words)
	{
		if (!CheckStringValid(Words))
		{
			return false;
		}

		lock(lockObj)
		{
			ReplaceWords = Words;
		}
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

		for (int i = 0; i < blockCount; i++)
		{
			string strTempString = tempString.Substring(i * BlockSize, BlockSize);
			ElementList.Add(strTempString);
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
			strContent += ElementList[i];
		}

		return true;
	}
}

