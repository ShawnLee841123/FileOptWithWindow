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
			openFileDialog.InitialDirectory = "c:\\";//注意这里写路径时要用c:\\而不是c:\
			openFileDialog.Filter = "文本文件|*.*|C#文件|*.cs|所有文件|*.*";
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

		private void btnTakePlaceAll_Click(object sender, EventArgs e)
		{

		}

		private void btnTakePlace_Click(object sender, EventArgs e)
		{

		}
	}
}
