
namespace FileOp
{
	partial class Form1
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
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.btnFind = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.btnTakePlace = new System.Windows.Forms.Button();
			this.btnTakePlaceAll = new System.Windows.Forms.Button();
			this.FileContentBox = new System.Windows.Forms.TextBox();
			this.btnOpenFile = new System.Windows.Forms.Button();
			this.FileName = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(104, 13);
			this.textBox1.Name = "FindKey";
			this.textBox1.Size = new System.Drawing.Size(265, 21);
			this.textBox1.TabIndex = 0;
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
			this.label2.Location = new System.Drawing.Point(562, 15);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(83, 12);
			this.label2.TabIndex = 3;
			this.label2.Text = "TakePlaceText";
			// 
			// textBox2
			// 
			this.textBox2.Location = new System.Drawing.Point(654, 13);
			this.textBox2.Name = "TakePlace";
			this.textBox2.Size = new System.Drawing.Size(317, 21);
			this.textBox2.TabIndex = 4;
			// 
			// btnTakePlace
			// 
			this.btnTakePlace.Location = new System.Drawing.Point(987, 13);
			this.btnTakePlace.Name = "btnTakePlace";
			this.btnTakePlace.Size = new System.Drawing.Size(75, 23);
			this.btnTakePlace.TabIndex = 5;
			this.btnTakePlace.Text = "TakePlace";
			this.btnTakePlace.UseVisualStyleBackColor = true;
			this.btnTakePlace.Click += new System.EventHandler(this.btnTakePlace_Click);
			// 
			// btnTakePlaceAll
			// 
			this.btnTakePlaceAll.Location = new System.Drawing.Point(1096, 13);
			this.btnTakePlaceAll.Name = "btnTakePlaceAll";
			this.btnTakePlaceAll.Size = new System.Drawing.Size(75, 23);
			this.btnTakePlaceAll.TabIndex = 6;
			this.btnTakePlaceAll.Text = "TakePlaceAll";
			this.btnTakePlaceAll.UseVisualStyleBackColor = true;
			this.btnTakePlaceAll.Click += new System.EventHandler(this.btnTakePlaceAll_Click);
			// 
			// FileContentBox
			// 
			this.FileContentBox.Location = new System.Drawing.Point(3, 72);
			this.FileContentBox.Multiline = true;
			this.FileContentBox.Name = "FileContent";
			this.FileContentBox.Size = new System.Drawing.Size(1231, 624);
			this.FileContentBox.TabIndex = 7;
			
			// 
			// btnOpenFile
			// 
			this.btnOpenFile.Location = new System.Drawing.Point(29, 41);
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
			this.FileName.Location = new System.Drawing.Point(111, 47);
			this.FileName.Name = "FileName";
			this.FileName.Size = new System.Drawing.Size(41, 12);
			this.FileName.TabIndex = 9;
			this.FileName.Text = "label3";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1236, 708);
			this.Controls.Add(this.FileName);
			this.Controls.Add(this.btnOpenFile);
			this.Controls.Add(this.FileContentBox);
			this.Controls.Add(this.btnTakePlaceAll);
			this.Controls.Add(this.btnTakePlace);
			this.Controls.Add(this.textBox2);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.btnFind);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textBox1);
			this.Name = "Form1";
			this.Text = "FileOp";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnFind;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.Button btnTakePlace;
		private System.Windows.Forms.Button btnTakePlaceAll;
		private System.Windows.Forms.TextBox FileContentBox;
		private System.Windows.Forms.Button btnOpenFile;
		private System.Windows.Forms.Label FileName;
	}
}

