using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileOp;

public class WorkPool: ThreadPool
{
	override public bool CreateThreads(int nThreadCount)
	{
		threadCount = nThreadCount;
		for (int i = 0; i < threadCount; i++)
		{
			WorkThread tempThread = new WorkThread();
			tempThread.SetThreadIndex(m_Pool.Count);
			m_Pool.Add(tempThread);
		}

		return true;
	}

	override public bool InitialWorkProcess()
	{
		FileSystem.Ins().PreFileOp(threadCount);
		Dictionary<int, string> FullTextContent = FileSystem.Ins().m_dicThreadContent;
		//int nSize = FileSystem.Ins().AverageStringInThread(threadCount);
		//FileSystem.Ins().ConstructOpContentString(nSize, threadCount, ref FullTextContent);
		int BlockCount = 0;
		for (int i =0; i < threadCount; i++)
		{
			((WorkThread)m_Pool[i]).SetFindKeyWords(FileSystem.Ins().m_strKeyWords);
			((WorkThread)m_Pool[i]).SetReplaceWords(FileSystem.Ins().m_strReplaceWords);
			((WorkThread)m_Pool[i]).SetWorkingContent(FullTextContent[i]);

			BlockCount += ((WorkThread)m_Pool[i]).ElementCount;
		}

		pJobsDone = OnAllThreadJobsDone;
		FileSystem.Ins().SetContentBlockCount(BlockCount);
		FileSystem.Ins().UpdateProcess();
		return true;
	}

	public void OnAllThreadJobsDone()
	{
		string strContent = "";
		for (int i = 0; i < threadCount; i++)
		{
			((WorkThread)m_Pool[i]).GetJobsDone(ref strContent);
		}

		FileSystem.Ins().SetJobsDoneContent(strContent);
		FileSystem.Ins().SwitchCatchContent();
	}
}

