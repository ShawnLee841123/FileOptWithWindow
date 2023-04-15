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
			nBlockCount = nBlockCount > 10 ? nBlockCount : 10;
			FileSystem.Ins().SetContentBlockCount(nBlockCount);
			FileSystem.Ins().ReadFile();
		}
	}

	private void BtnTakePlaceAll_Click(object sender, EventArgs e)
	{
		FileSystem.Ins().SwitchCatchContent();
		FileSystem.Ins().SetOperatedKeyWords(this.FindKeyBox.Text);
		FileSystem.Ins().SetOperatedReplaceWords(this.ReplaceBox.Text);
		int nBlockCount = Convert.ToInt32(ThreadCountText.Text);
		nBlockCount = nBlockCount > 10 ? nBlockCount : 10;
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
				FileWriter.Ins().WriteFileInLines(outName, arrFileLines);
				return;
			}
		}

		FileWriter.Ins().WriteFile(outName, FileSystem.Ins().m_strFileContent);
	}

	private void btnSaveLineFlag_Click(object sender, EventArgs e)
	{
		FileSystem.Ins().SwitchCatchContent();
		FileSystem.Ins().SetLineFlag(this.LineFlagInput.Text);
		int nBlockCount = Convert.ToInt32(ThreadCountText.Text);
		nBlockCount = nBlockCount > 10 ? nBlockCount : 10;
		ThreadPool.Ins().CreateThreads(nBlockCount);
		ThreadPool.Ins().SplitLinerWorkProcess();
		ThreadPool.Ins().StartWork();
	}
}

