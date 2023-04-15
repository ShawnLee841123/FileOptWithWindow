

public partial class Form1
{
	/// <summary>
	/// 必需的设计器变量。
	/// </summary>
	private System.ComponentModel.IContainer components = null;

	/// <summary>
	/// 清理所有正在使用的资源。
	/// </summary>
	/// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
	protected override void Dispose(bool disposing)
	{
		if (disposing && (components != null))
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	#region Windows 窗体设计器生成的代码

	

	/// <summary>
	/// 设计器支持所需的方法 - 不要修改
	/// 使用代码编辑器修改此方法的内容。
	/// </summary>
	private void InitializeComponent()
	{
			this.FindKeyBox = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.btnFind = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.ReplaceBox = new System.Windows.Forms.TextBox();
			this.btnTakePlace = new System.Windows.Forms.Button();
			this.btnTakePlaceAll = new System.Windows.Forms.Button();
			this.FileContentBox = new System.Windows.Forms.TextBox();
			this.btnOpenFile = new System.Windows.Forms.Button();
			this.FileName = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.ProgressBar = new System.Windows.Forms.ProgressBar();
			this.ProgressValue = new System.Windows.Forms.Label();
			this.btnSave = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.btnSaveLineFlag = new System.Windows.Forms.Button();
			this.LineFlagInput = new System.Windows.Forms.TextBox();
			this.ThreadCountText = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// FindKeyBox
			// 
			this.FindKeyBox.Location = new System.Drawing.Point(104, 13);
			this.FindKeyBox.Name = "FindKeyBox";
			this.FindKeyBox.Size = new System.Drawing.Size(265, 21);
			this.FindKeyBox.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(27, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(71, 12);
			this.label1.TabIndex = 1;
			this.label1.Text = "FindKeyText";
			// 
			// btnFind
			// 
			this.btnFind.Location = new System.Drawing.Point(376, 10);
			this.btnFind.Name = "btnFind";
			this.btnFind.Size = new System.Drawing.Size(75, 23);
			this.btnFind.TabIndex = 2;
			this.btnFind.Text = "Find";
			this.btnFind.UseVisualStyleBackColor = true;
			this.btnFind.Click += new System.EventHandler(this.btnFind_Click);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(471, 15);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(83, 12);
			this.label2.TabIndex = 3;
			this.label2.Text = "TakePlaceText";
			// 
			// ReplaceBox
			// 
			this.ReplaceBox.Location = new System.Drawing.Point(563, 13);
			this.ReplaceBox.Name = "ReplaceBox";
			this.ReplaceBox.Size = new System.Drawing.Size(317, 21);
			this.ReplaceBox.TabIndex = 4;
			// 
			// btnTakePlace
			// 
			this.btnTakePlace.Location = new System.Drawing.Point(896, 13);
			this.btnTakePlace.Name = "btnTakePlace";
			this.btnTakePlace.Size = new System.Drawing.Size(75, 23);
			this.btnTakePlace.TabIndex = 5;
			this.btnTakePlace.Text = "TakePlace";
			this.btnTakePlace.UseVisualStyleBackColor = true;
			this.btnTakePlace.Click += new System.EventHandler(this.btnTakePlace_Click);
			// 
			// btnTakePlaceAll
			// 
			this.btnTakePlaceAll.Location = new System.Drawing.Point(1005, 13);
			this.btnTakePlaceAll.Name = "btnTakePlaceAll";
			this.btnTakePlaceAll.Size = new System.Drawing.Size(75, 23);
			this.btnTakePlaceAll.TabIndex = 6;
			this.btnTakePlaceAll.Text = "TakePlaceAll";
			this.btnTakePlaceAll.UseVisualStyleBackColor = true;
			this.btnTakePlaceAll.Click += new System.EventHandler(this.BtnTakePlaceAll_Click);
			// 
			// FileContentBox
			// 
			this.FileContentBox.Location = new System.Drawing.Point(3, 101);
			this.FileContentBox.Multiline = true;
			this.FileContentBox.Name = "FileContentBox";
			this.FileContentBox.Size = new System.Drawing.Size(1231, 595);
			this.FileContentBox.TabIndex = 7;
			// 
			// btnOpenFile
			// 
			this.btnOpenFile.Location = new System.Drawing.Point(29, 67);
			this.btnOpenFile.Name = "btnOpenFile";
			this.btnOpenFile.Size = new System.Drawing.Size(75, 23);
			this.btnOpenFile.TabIndex = 8;
			this.btnOpenFile.Text = "OpenFile";
			this.btnOpenFile.UseVisualStyleBackColor = true;
			this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
			// 
			// FileName
			// 
			this.FileName.AutoSize = true;
			this.FileName.Location = new System.Drawing.Point(111, 73);
			this.FileName.Name = "FileName";
			this.FileName.Size = new System.Drawing.Size(41, 12);
			this.FileName.TabIndex = 9;
			this.FileName.Text = "label3";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(470, 75);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(53, 12);
			this.label3.TabIndex = 10;
			this.label3.Text = "Progress";
			// 
			// ProgressBar
			// 
			this.ProgressBar.Location = new System.Drawing.Point(544, 76);
			this.ProgressBar.Name = "ProgressBar";
			this.ProgressBar.Size = new System.Drawing.Size(424, 10);
			this.ProgressBar.TabIndex = 11;
			this.ProgressBar.Value = 30;
			// 
			// ProgressValue
			// 
			this.ProgressValue.AutoSize = true;
			this.ProgressValue.Location = new System.Drawing.Point(992, 74);
			this.ProgressValue.Name = "ProgressValue";
			this.ProgressValue.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.ProgressValue.Size = new System.Drawing.Size(23, 12);
			this.ProgressValue.TabIndex = 12;
			this.ProgressValue.Text = "30%";
			// 
			// btnSave
			// 
			this.btnSave.Location = new System.Drawing.Point(1107, 13);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(75, 23);
			this.btnSave.TabIndex = 13;
			this.btnSave.Text = "SaveFile";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(27, 43);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(53, 12);
			this.label4.TabIndex = 14;
			this.label4.Text = "LineFlag";
			// 
			// btnSaveLineFlag
			// 
			this.btnSaveLineFlag.Location = new System.Drawing.Point(376, 40);
			this.btnSaveLineFlag.Name = "btnSaveLineFlag";
			this.btnSaveLineFlag.Size = new System.Drawing.Size(110, 23);
			this.btnSaveLineFlag.TabIndex = 15;
			this.btnSaveLineFlag.Text = "ReplaceLineFlag";
			this.btnSaveLineFlag.UseVisualStyleBackColor = true;
			this.btnSaveLineFlag.Click += new System.EventHandler(this.btnSaveLineFlag_Click);
			// 
			// LineFlagInput
			// 
			this.LineFlagInput.Location = new System.Drawing.Point(104, 41);
			this.LineFlagInput.Name = "LineFlagInput";
			this.LineFlagInput.Size = new System.Drawing.Size(265, 21);
			this.LineFlagInput.TabIndex = 16;
			// 
			// ThreadCountText
			// 
			this.ThreadCountText.Location = new System.Drawing.Point(621, 43);
			this.ThreadCountText.Name = "ThreadCountText";
			this.ThreadCountText.Size = new System.Drawing.Size(40, 21);
			this.ThreadCountText.TabIndex = 17;
			this.ThreadCountText.Text = "10";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(529, 45);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(77, 12);
			this.label5.TabIndex = 18;
			this.label5.Text = "Thread Count";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1236, 708);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.ThreadCountText);
			this.Controls.Add(this.LineFlagInput);
			this.Controls.Add(this.btnSaveLineFlag);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.ProgressValue);
			this.Controls.Add(this.ProgressBar);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.FileName);
			this.Controls.Add(this.btnOpenFile);
			this.Controls.Add(this.FileContentBox);
			this.Controls.Add(this.btnTakePlaceAll);
			this.Controls.Add(this.btnTakePlace);
			this.Controls.Add(this.ReplaceBox);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.btnFind);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.FindKeyBox);
			this.Name = "Form1";
			this.Text = "FileOp";
			this.ResumeLayout(false);
			this.PerformLayout();

	}

	public void UpdateShowValue(int nValue)
	{
		if (nValue >= 100)
			nValue = 100;

		ProgressBar.Value = nValue;
		ProgressValue.Text = string.Format("{0}%", nValue);
	}

	public void UpdateShowText(string strValue)
	{
		FileContentBox.Text = strValue;
	}

	public void SetEditBoxContent(string strText)
	{
		FileContentBox.Text = strText;
	}
	#endregion

	private System.Windows.Forms.TextBox FindKeyBox;
	private System.Windows.Forms.Label label1;
	private System.Windows.Forms.Button btnFind;
	private System.Windows.Forms.Label label2;
	private System.Windows.Forms.TextBox ReplaceBox;
	private System.Windows.Forms.Button btnTakePlace;
	private System.Windows.Forms.Button btnTakePlaceAll;
	public System.Windows.Forms.TextBox FileContentBox;
	private System.Windows.Forms.Button btnOpenFile;
	private System.Windows.Forms.Label FileName;
	private System.Windows.Forms.Label label3;
	public System.Windows.Forms.ProgressBar ProgressBar;
	public System.Windows.Forms.Label ProgressValue;
	private System.Windows.Forms.Button btnSave;
	private System.Windows.Forms.Label label4;
	private System.Windows.Forms.Button btnSaveLineFlag;
	private System.Windows.Forms.TextBox LineFlagInput;
	private System.Windows.Forms.TextBox ThreadCountText;
	private System.Windows.Forms.Label label5;
}


