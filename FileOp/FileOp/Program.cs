using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileOp
{
	static class Program
	{
		/// <summary>
		/// 应用程序的主入口点。
		/// </summary>
		public static Form1 MyWindow;
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			MyWindow = new Form1();
			Application.Run(MyWindow);
		}
	}
}
