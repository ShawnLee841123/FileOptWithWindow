using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public delegate void OnJobsDoneEvent();
public delegate void OnCreateThread(int nCount);
public delegate void OnInitialProcessEvent();

public class ThreadPool: Singleton<ThreadPool>
{
	public int threadCount { get; protected set; }

	protected List<ThreadBase> m_Pool;

	protected object lockObject;

	public OnJobsDoneEvent pJobsDone;
	public OnCreateThread pCreateEvent;
	public OnInitialProcessEvent pInitialEvent;

	public ThreadPool()
	{
		threadCount = 0;
		m_Pool = new List<ThreadBase>();
		m_Pool.Clear();
		pCreateEvent = CreateWorkThreads;
		pInitialEvent = InitialReplaceProcess;
		pJobsDone = null;
	}

	virtual public bool CreateThreads(int nThreadCount)
	{
		if (threadCount > 0)
		{
			return true;
		}

		threadCount = nThreadCount;
		if (null != pCreateEvent)
		{
			pCreateEvent(nThreadCount);
		}
		
		return true;
	}

	virtual public bool InitialWorkProcess()
	{
		if (null != pInitialEvent)
		{
			pInitialEvent();
		}
		
		return true;
	}

	virtual public bool StartWork()
	{
		if (m_Pool.Count <= 0)
		{
			return false;
		}

		for(int i = 0; i < threadCount; i++)
		{
			m_Pool[i].StartThread();
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

	public bool CheckAllThreadJobsDone()
	{
		bool bRet = true;
		for(int i = 0; i < threadCount; i++)
		{
			if (!m_Pool[i].Enable)
			{
				bRet &= true;
			}
			else
			{
				bRet &= false;
			}
		}

		return bRet;
	}

	public bool JobsDone()
	{
		if (!CheckAllThreadJobsDone())
		{
			return false;
		}

		if (null != pJobsDone)
		{
			pJobsDone();
		}
		
		return true;
	}

	#region replace work about
	public void CreateWorkThreads(int nThreadCount)
	{
		threadCount = nThreadCount;
		for (int i = 0; i < threadCount; i++)
		{
			WorkThread tempThread = new WorkThread();
			tempThread.SetThreadIndex(m_Pool.Count);
			m_Pool.Add(tempThread);
		}
	}

	public void InitialReplaceProcess()
	{
		FileSystem.Ins().PreFileOp(threadCount);
		Dictionary<int, string> FullTextContent = FileSystem.Ins().m_dicThreadContent;

		int BlockCount = 0;
		for (int i = 0; i < threadCount; i++)
		{
			((WorkThread)m_Pool[i]).SetFindKeyWords(FileSystem.Ins().m_strKeyWords);
			((WorkThread)m_Pool[i]).SetReplaceWords(FileSystem.Ins().m_strReplaceWords);
			((WorkThread)m_Pool[i]).SetWorkingContent(FullTextContent[i]);
			m_Pool[i].SleepTime = 100;

			BlockCount += ((WorkThread)m_Pool[i]).ElementCount;
		}

		pJobsDone = OnAllThreadJobsDone;
		FileSystem.Ins().SetContentBlockCount(BlockCount);
		FileSystem.Ins().UpdateProcess();
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
	#endregion
}

