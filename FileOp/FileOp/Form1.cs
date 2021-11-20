using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace FileOp
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

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
				FileReader.Ins().ReadFileByLine(FileSystem.Ins().m_strFileName, FileSystem.Ins().AddFileStringLine);
				this.FileContentBox.Text = FileSystem.Ins().m_strFileContent;
			}
		}

		private void BtnTakePlaceAll_Click(object sender, EventArgs e)
		{
			FileSystem.Ins().SetOperatedKeyWords(this.FindKeyBox.Text);
			int nBlockCount = 7;
			int nSize = FileSystem.Ins().AverageStringInThread(nBlockCount);
			FileSystem.Ins().ConstructOpContentString(nSize, nBlockCount);
		}

		private void btnTakePlace_Click(object sender, EventArgs e)
		{

		}
	}
}
