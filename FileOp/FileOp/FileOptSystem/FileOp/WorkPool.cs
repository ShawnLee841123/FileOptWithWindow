using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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

	public bool InitialWorkProcess()
	{
		for(int i =0; i < threadCount; i++)
		{
			((WorkThread)m_Pool[i]).SetFindKeyWords(FileSystem.Ins().m_strKeyWords);
			((WorkThread)m_Pool[i]).SetReplaceWords(FileSystem.Ins().m_strReplaceWords);
		}

		return true;
	}
}

