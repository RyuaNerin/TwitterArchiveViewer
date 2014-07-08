namespace TwitterArchiveViewer
{
	partial class frmOpen
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.txtPath = new System.Windows.Forms.TextBox();
			this.txtOpen = new System.Windows.Forms.Button();
			this.chkLoadAll = new System.Windows.Forms.CheckBox();
			this.btnOpen = new System.Windows.Forms.Button();
			this.fbd = new System.Windows.Forms.FolderBrowserDialog();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.txtPath);
			this.groupBox1.Controls.Add(this.txtOpen);
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(340, 51);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "아카이브 위치";
			// 
			// txtPath
			// 
			this.txtPath.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append;
			this.txtPath.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories;
			this.txtPath.BackColor = System.Drawing.SystemColors.Window;
			this.txtPath.Location = new System.Drawing.Point(6, 22);
			this.txtPath.Name = "txtPath";
			this.txtPath.Size = new System.Drawing.Size(255, 23);
			this.txtPath.TabIndex = 1;
			// 
			// txtOpen
			// 
			this.txtOpen.Location = new System.Drawing.Point(267, 22);
			this.txtOpen.Name = "txtOpen";
			this.txtOpen.Size = new System.Drawing.Size(67, 23);
			this.txtOpen.TabIndex = 0;
			this.txtOpen.Text = "선택";
			this.txtOpen.UseVisualStyleBackColor = true;
			this.txtOpen.Click += new System.EventHandler(this.txtOpen_Click);
			// 
			// chkLoadAll
			// 
			this.chkLoadAll.AutoSize = true;
			this.chkLoadAll.Location = new System.Drawing.Point(12, 69);
			this.chkLoadAll.Name = "chkLoadAll";
			this.chkLoadAll.Size = new System.Drawing.Size(340, 19);
			this.chkLoadAll.TabIndex = 0;
			this.chkLoadAll.Text = "모든 트윗 미리 읽기 (많은 메모리, 여는시간 김, 빠른 검색)";
			this.chkLoadAll.UseVisualStyleBackColor = true;
			// 
			// btnOpen
			// 
			this.btnOpen.Location = new System.Drawing.Point(263, 95);
			this.btnOpen.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.btnOpen.Name = "btnOpen";
			this.btnOpen.Size = new System.Drawing.Size(89, 34);
			this.btnOpen.TabIndex = 0;
			this.btnOpen.Text = "열기";
			this.btnOpen.UseVisualStyleBackColor = true;
			this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
			// 
			// fbd
			// 
			this.fbd.Description = "트위터 아카이브 폴더 위치를 선택해주세요";
			// 
			// frmOpen
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(364, 142);
			this.Controls.Add(this.chkLoadAll);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.btnOpen);
			this.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmOpen";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "아카이브 선택";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button txtOpen;
		internal System.Windows.Forms.TextBox txtPath;
		internal System.Windows.Forms.CheckBox chkLoadAll;
		private System.Windows.Forms.Button btnOpen;
		private System.Windows.Forms.FolderBrowserDialog fbd;
	}
}