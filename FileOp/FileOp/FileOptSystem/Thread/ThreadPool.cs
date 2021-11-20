using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class ThreadPool: Singleton<ThreadPool>
{
	public int threadCount { get; protected set; }

	public ThreadPool()
	{
		threadCount = 0;
	}
}

