using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

public delegate void UpdateProgressValue(int nValue);
public delegate void ResetDisplayText(string strText);
public delegate void SetShowText(string strText);
public partial class Form1 : Form
{
	public Form1()
	{
		InitializeComponent();
		UpdateValue = this.UpdateShowValue;
		UpdateText = this.UpdateShowText;
		UpdateFileContent = this.SetEditBoxContent;
	}

	public UpdateProgressValue UpdateValue;
	public ResetDisplayText UpdateText;
	public SetShowText UpdateFileContent;
	public int DefaultThreadCount = 1;

	private void btnFind_Click(object sender, EventArgs e)
	{

	}

	private void btnOpenFile_Click(object sender, EventArgs e)
	{
		OpenFileDialog openFileDialog = new OpenFileDialog();
		openFileDialog.InitialDirectory = "e:\\";//注意这里写路径时要用c:\\而不是c:\
		openFileDialog.Filter = "文本文件|*.*|文本文件|*.txt|所有文件|*.*";
		openFileDialog.RestoreDirectory = true;
		openFileDialog.FilterIndex = 1;
		if (openFileDialog.ShowDialog() == DialogResult.OK)
		{
			FileSystem.Ins().SetFileName(openFileDialog.FileName);
			this.FileName.Text = FileSystem.Ins().m_strFileName;
			//FileReader.Ins().ReadFileByLine(FileSystem.Ins().m_strFileName, FileSystem.Ins().AddFileStringLine);
			//this.FileContentBox.Text = FileSystem.Ins().m_strFileContent;
			int nBlockCount = Convert.ToInt32(ThreadCountText.Text);
			nBlockCount = nBlockCount > DefaultThreadCount ? nBlockCount : DefaultThreadCount;
			FileSystem.Ins().SetContentBlockCount(nBlockCount);
			FileSystem.Ins().ReadFile();
		}
	}

	private void BtnTakePlaceAll_Click(object sender, EventArgs e)
	{
		FileSystem.Ins().SwitchCatchContent();

		string strKeyString = this.FindKeyBox.Text;
		if (this.ExtraMode.Checked)
		{
			if (strKeyString.Contains("\\r"))
			{
				strKeyString = strKeyString.Replace("\\r", "\r");
			}

			if (strKeyString.Contains("\\n"))
			{
				strKeyString = strKeyString.Replace("\\n", "\n");
			}
		}

		char[] arrKeyBytes = strKeyString.ToCharArray();
		byte[] arrKeyFileCodeByte = FileSystem.Ins().m_efileEncodeType.GetBytes(arrKeyBytes);
		string strKeyFileCodeString = FileSystem.Ins().m_efileEncodeType.GetString(arrKeyFileCodeByte);
		FileSystem.Ins().SetOperatedKeyWords(strKeyFileCodeString);

		string strReplaceString = this.ReplaceBox.Text;
		if (this.ExtraMode.Checked)
		{
			if (strReplaceString.Contains("\\r"))
			{
				strReplaceString = strReplaceString.Replace("\\r", "\r");
			}

			if (strReplaceString.Contains("\\n"))
			{
				strReplaceString = strReplaceString.Replace("\\n", "\n");
			}
		}

		char[] arrReplaceBytes = strReplaceString.ToCharArray();
		byte[] arrReplaceFileCodeByte = FileSystem.Ins().m_efileEncodeType.GetBytes(arrReplaceBytes);
		string strReplaceFileCodeString = FileSystem.Ins().m_efileEncodeType.GetString(arrReplaceFileCodeByte);

		FileSystem.Ins().SetOperatedReplaceWords(strReplaceFileCodeString);
		int nBlockCount = Convert.ToInt32(ThreadCountText.Text);
		nBlockCount = nBlockCount > DefaultThreadCount ? nBlockCount : DefaultThreadCount;
		//int nSize = FileSystem.Ins().AverageStringInThread(nBlockCount);
		//Dictionary<int, string> tempContent = new Dictionary<int, string>();
		//FileSystem.Ins().ConstructOpContentString(nSize, nBlockCount, ref tempContent);
		ThreadPool.Ins().CreateThreads(nBlockCount);
		ThreadPool.Ins().InitialWorkProcess();
		ThreadPool.Ins().StartWork();
	}

	private void btnTakePlace_Click(object sender, EventArgs e)
	{
		#region test code


		//ThreadPool.Ins().CreateThreads(12);
		//ThreadPool.Ins().StartWork();
		int codingType = FileSystem.Ins().GetStringCode(this.FindKeyBox.Text);
		int nFileCodeType = FileSystem.Ins().GetStringCode(FileSystem.Ins().m_strFileContent);
		this.FileContentBox.Text = string.Format("Editbox coding Page[{0}], File content coding page[{1}]", codingType, nFileCodeType);
		#endregion
	}

	private void btnSave_Click(object sender, EventArgs e)
	{
		FileSystem.Ins().SwitchCatchContent();
		string strFileName = FileSystem.Ins().m_strFileName;
		string[] arrFileName = strFileName.Split(new char[] { '.' });
		string outName = arrFileName[0] + "(1)." + arrFileName[arrFileName.Length - 1];

		string strLineFlag = FileSystem.Ins().m_strLineFlag;
		if (FileSystem.Ins().CheckStringValid(strLineFlag))
		{
			string[] arrFileLines = FileSystem.Ins().m_listFinishedLines.ToArray();
			if (FileSystem.Ins().CheckArrayValid(arrFileLines))
			{
				//FileWriter.Ins().WriteFileInLines(outName, arrFileLines);
				FileWriter.Ins().WriteFileInLinesWithEncodingType(outName, arrFileLines, FileSystem.Ins().m_efileEncodeType);
				return;
			}
		}

		FileWriter.Ins().WriteFileWithEncodingType(outName, FileSystem.Ins().m_strFileContent, FileSystem.Ins().m_efileEncodeType);
	}

	private void btnSaveLineFlag_Click(object sender, EventArgs e)
	{
		FileSystem.Ins().SwitchCatchContent();

		char[] arrLineFlagBytes = this.LineFlagInput.Text.ToCharArray();
		byte[] arrLineFlagFileCodeByte = FileSystem.Ins().m_efileEncodeType.GetBytes(arrLineFlagBytes);
		string strLineFlagFileCodeString = FileSystem.Ins().m_efileEncodeType.GetString(arrLineFlagFileCodeByte);
		FileSystem.Ins().SetLineFlag(strLineFlagFileCodeString);
		int nBlockCount = Convert.ToInt32(ThreadCountText.Text);
		nBlockCount = nBlockCount > DefaultThreadCount ? nBlockCount : DefaultThreadCount;
		ThreadPool.Ins().CreateThreads(nBlockCount);
		ThreadPool.Ins().SplitLinerWorkProcess();
		ThreadPool.Ins().StartWork();
	}

	private void ExtraMode_CheckedChanged(object sender, EventArgs e)
	{

	}
}

