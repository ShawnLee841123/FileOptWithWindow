using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class ThreadPool: Singleton<ThreadPool>
{
	public int threadCount { get; protected set; }

	protected List<ThreadBase> m_Pool;

	public ThreadPool()
	{
		threadCount = 0;
	}

	virtual public bool CreateThreads(int nThreadCount)
	{
		threadCount = nThreadCount;
		for(int i = 0; i < threadCount; i++)
		{
			ThreadBase tempThread = new ThreadBase();
			tempThread.SetThreadIndex(m_Pool.Count);
			m_Pool.Add(tempThread);
		}

		return true;
	}

	virtual public bool StopWork()
	{
		for(int i = 0; i < threadCount; i++)
		{
			m_Pool[i].SetThreadEnable(false);
		}

		return true;
	}

	virtual public bool DestroyThreads()
	{
		for(int i = 0; i < threadCount; i++)
		{
			m_Pool[i] = null;
		}

		m_Pool.Clear();
		return true;
	}
}

