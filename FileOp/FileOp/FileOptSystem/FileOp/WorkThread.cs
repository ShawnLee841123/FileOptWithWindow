using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class WorkThread: ThreadBase
{

	override protected bool TickProcess()
	{
		return true;
	}
}

