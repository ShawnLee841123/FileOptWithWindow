using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


public class ThreadBase
{
	protected Thread m_th;

	public int SleepTime;	//	million second

	public int nIndex { get; protected set; }
	public bool Enable { get; protected set; }

	public ThreadBase()
	{
	}

	public void SetThreadEnable(bool bEnable)
	{
		Enable = bEnable;
	}

	public void SetThreadIndex(int id)
	{
		nIndex = id;
	}

	public void StartThread()
	{
		m_th = new Thread(ThreadTick);
		Enable = true;
		m_th.Start();
	}

	protected void ThreadTick()
	{
		while (Enable)
		{
			Enable = TickProcess();
			Thread.Sleep(SleepTime);
		}

		m_th.Join();
	}

	virtual protected bool TickProcess()
	{
		return true;
	}
}

