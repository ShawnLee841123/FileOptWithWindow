using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;


public class ReadThread: ThreadBase
{
	public string FileName { get; protected set; }

	public string FileContent { get; protected set; }

	public int WaitLines { get; protected set; }
	public ReadThread()
	{
		Reset();
	}

	public void Reset()
	{
		if (null == lockObj)
		{
			lockObj = new object();
		}

		FileName = "";
		FileContent = "";
		if (m_th != null)
			m_th.Join();

		m_th = null;
		WaitLines = 150;
	}

	public void SetFileName(string strValue)
	{
		FileName = strValue;
	}

	public void BeginRead(string strValue)
	{
		SetFileName(strValue);
		m_th = new Thread(ReadFile);
		m_th.Start();
	}

	public Encoding GetCoding(string strLine)
	{
		Encoding code = Encoding.Default;
		FileSystem.CodeType eCode = (FileSystem.CodeType)FileSystem.Ins().GetStringCode(strLine);
		switch (eCode)
		{
			case FileSystem.CodeType.CT_ASCII:
				{
					code = Encoding.ASCII;
				}
				break;
			case FileSystem.CodeType.CT_UTF8:
				{
					code = Encoding.UTF8;
				}
				break;
			case FileSystem.CodeType.CT_GB:
				{
					code = Encoding.GetEncoding("GB2312");
				}
				break;
			case FileSystem.CodeType.CT_GBK:
				{
					code = Encoding.GetEncoding("GBK");
				}
				break;
			case FileSystem.CodeType.CT_BIG5:
				{
					code = Encoding.GetEncoding("Big5");
				}
				break;
			default:
				break;
		}

		return code;
	}

	public void ReadFile()
	{
		
		using (FileStream pFileStream = new FileStream(FileName, FileMode.Open))
		{
			StreamReader pReader = new StreamReader(pFileStream);
			if (null == pReader)
			{
				return;
			}

			long lByteCount = pFileStream.Length;
			long lCurRead = 0;
			int ReadLine = 0;
			
			Encoding code = Encoding.Default;
			lock (lockObj)
			{
				string strCurLine = "";
				while (!pReader.EndOfStream)
				{
					strCurLine = pReader.ReadLine();
					if (code == Encoding.Default)
					{
						code = GetCoding(strCurLine);
					}

					FileContent += strCurLine;
					int curByte = code.GetBytes(strCurLine).Length;
					lCurRead += curByte;

					int percent = (int)((lCurRead * 100) / lByteCount);
					Program.MyWindow.Invoke(Program.MyWindow.UpdateValue, percent);

					if (ReadLine >= WaitLines)
					{
						ReadLine = 0;
						Thread.Sleep(3);
					}
				}
			}
		}

		FileSystem.Ins().SetFileContent(FileContent);
		m_th.Join();
		return;
	}
}

