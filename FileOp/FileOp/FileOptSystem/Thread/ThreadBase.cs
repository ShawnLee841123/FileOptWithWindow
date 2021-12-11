using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

public delegate bool WorkThreadTickEvent();
public class ThreadBase
{
	protected Thread m_th;

	public int SleepTime;   //	million second

	protected object lockObj;

	public WorkThreadTickEvent TickEvent;
	public int nIndex { get; protected set; }
	public bool Enable { get; protected set; }

	public ThreadBase()
	{
		TickEvent = null;
		lockObj = new object();
	}

	public void SetThreadEnable(bool bEnable)
	{
		Enable = bEnable;
	}

	public void SetThreadIndex(int id)
	{
		nIndex = id;
	}

	virtual public void StartThread()
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

		ThreadPool.Ins().JobsDone();
		m_th.Join();
		m_th = null;
	}

	virtual protected bool TickProcess()
	{
		string strOut = string.Format("Thread[{0}]: Working.", nIndex);
		System.Console.WriteLine(strOut);
		bool bRet = false;
		if (null != TickEvent)
		{
			bRet = TickEvent();
		}
		return bRet;
	}

	public bool CheckStringValid(string strValue)
	{
		if (null == strValue)
			return false;

		if (strValue.Length <= 0)
			return false;

		return true;
	}

	public bool CheckListValid<T1>(List<T1> listValue)
	{
		if (null == listValue)
			return false;

		if (listValue.Count <= 0)
			return false;

		return true;
	}

	public bool CheckArrayValid<T1>(T1[] arrValue)
	{
		if (null == arrValue)
			return false;

		if (arrValue.Length <= 0)
			return false;

		return true;
	}
}

