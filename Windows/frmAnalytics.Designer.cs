namespace TwitterArchiveViewer
{
	partial class frmAnalytics
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
			this.tbc = new System.Windows.Forms.TabControl();
			this.tbc_Details = new System.Windows.Forms.TabPage();
			this.lsvResult = new System.Windows.Forms.ListView();
			this.lsvResult_Key = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.lsvResult_Value = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.tbc_timeZone = new System.Windows.Forms.TabPage();
			this.lsvTimeZone = new System.Windows.Forms.ListView();
			this.lsvTimeZone_Time = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.lsvTimeZone_Rate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.lsvTimeZone_RateT = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.lsvTimeZone_Count = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.tbcDay = new System.Windows.Forms.TabControl();
			this.tbc_Client = new System.Windows.Forms.TabPage();
			this.lsvSource = new System.Windows.Forms.ListView();
			this.lsvSource_Name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.lsvSource_Percent = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.lsvSource_Count = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.tbc_Mention = new System.Windows.Forms.TabPage();
			this.lsvMention = new System.Windows.Forms.ListView();
			this.lsvMention_Name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.lsvMention_Percent = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.lsvMention_Count = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.tbc_Keyword = new System.Windows.Forms.TabPage();
			this.lsvKeyword = new System.Windows.Forms.ListView();
			this.lsvKeyword_Name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.lsvKeyword_rate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.lsvKeyword_count = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.tbc.SuspendLayout();
			this.tbc_Details.SuspendLayout();
			this.tbc_timeZone.SuspendLayout();
			this.tbc_Client.SuspendLayout();
			this.tbc_Mention.SuspendLayout();
			this.tbc_Keyword.SuspendLayout();
			this.SuspendLayout();
			// 
			// tbc
			// 
			this.tbc.Controls.Add(this.tbc_Details);
			this.tbc.Controls.Add(this.tbc_timeZone);
			this.tbc.Controls.Add(this.tbc_Client);
			this.tbc.Controls.Add(this.tbc_Mention);
			this.tbc.Controls.Add(this.tbc_Keyword);
			this.tbc.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbc.Location = new System.Drawing.Point(0, 0);
			this.tbc.Name = "tbc";
			this.tbc.SelectedIndex = 0;
			this.tbc.Size = new System.Drawing.Size(372, 264);
			this.tbc.TabIndex = 1;
			// 
			// tbc_Details
			// 
			this.tbc_Details.Controls.Add(this.lsvResult);
			this.tbc_Details.Location = new System.Drawing.Point(4, 24);
			this.tbc_Details.Margin = new System.Windows.Forms.Padding(0);
			this.tbc_Details.Name = "tbc_Details";
			this.tbc_Details.Size = new System.Drawing.Size(364, 236);
			this.tbc_Details.TabIndex = 0;
			this.tbc_Details.Text = "요약보기";
			this.tbc_Details.UseVisualStyleBackColor = true;
			// 
			// lsvResult
			// 
			this.lsvResult.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lsvResult_Key,
            this.lsvResult_Value});
			this.lsvResult.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lsvResult.FullRowSelect = true;
			this.lsvResult.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lsvResult.HideSelection = false;
			this.lsvResult.Location = new System.Drawing.Point(0, 0);
			this.lsvResult.Name = "lsvResult";
			this.lsvResult.Size = new System.Drawing.Size(364, 236);
			this.lsvResult.TabIndex = 1;
			this.lsvResult.UseCompatibleStateImageBehavior = false;
			this.lsvResult.View = System.Windows.Forms.View.Details;
			// 
			// lsvResult_Key
			// 
			this.lsvResult_Key.Text = "설명";
			this.lsvResult_Key.Width = 130;
			// 
			// lsvResult_Value
			// 
			this.lsvResult_Value.Text = "";
			this.lsvResult_Value.Width = 200;
			// 
			// tbc_timeZone
			// 
			this.tbc_timeZone.Controls.Add(this.lsvTimeZone);
			this.tbc_timeZone.Controls.Add(this.tbcDay);
			this.tbc_timeZone.Location = new System.Drawing.Point(4, 24);
			this.tbc_timeZone.Margin = new System.Windows.Forms.Padding(0);
			this.tbc_timeZone.Name = "tbc_timeZone";
			this.tbc_timeZone.Size = new System.Drawing.Size(364, 236);
			this.tbc_timeZone.TabIndex = 1;
			this.tbc_timeZone.Text = "트윗 시간대";
			this.tbc_timeZone.UseVisualStyleBackColor = true;
			// 
			// lsvTimeZone
			// 
			this.lsvTimeZone.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lsvTimeZone_Time,
            this.lsvTimeZone_Rate,
            this.lsvTimeZone_RateT,
            this.lsvTimeZone_Count});
			this.lsvTimeZone.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lsvTimeZone.FullRowSelect = true;
			this.lsvTimeZone.HideSelection = false;
			this.lsvTimeZone.Location = new System.Drawing.Point(0, 19);
			this.lsvTimeZone.Name = "lsvTimeZone";
			this.lsvTimeZone.Size = new System.Drawing.Size(364, 217);
			this.lsvTimeZone.TabIndex = 4;
			this.lsvTimeZone.UseCompatibleStateImageBehavior = false;
			this.lsvTimeZone.View = System.Windows.Forms.View.Details;
			// 
			// lsvTimeZone_Time
			// 
			this.lsvTimeZone_Time.Text = "시간대";
			this.lsvTimeZone_Time.Width = 130;
			// 
			// lsvTimeZone_Rate
			// 
			this.lsvTimeZone_Rate.Text = "비율";
			this.lsvTimeZone_Rate.Width = 70;
			// 
			// lsvTimeZone_RateT
			// 
			this.lsvTimeZone_RateT.Text = "전체비율";
			this.lsvTimeZone_RateT.Width = 70;
			// 
			// lsvTimeZone_Count
			// 
			this.lsvTimeZone_Count.Text = "트윗 수";
			this.lsvTimeZone_Count.Width = 80;
			// 
			// tbcDay
			// 
			this.tbcDay.Dock = System.Windows.Forms.DockStyle.Top;
			this.tbcDay.ItemSize = new System.Drawing.Size(43, 20);
			this.tbcDay.Location = new System.Drawing.Point(0, 0);
			this.tbcDay.Name = "tbcDay";
			this.tbcDay.SelectedIndex = 0;
			this.tbcDay.Size = new System.Drawing.Size(364, 19);
			this.tbcDay.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
			this.tbcDay.TabIndex = 3;
			this.tbcDay.SelectedIndexChanged += new System.EventHandler(this.tbcDay_SelectedIndexChanged);
			// 
			// tbc_Client
			// 
			this.tbc_Client.Controls.Add(this.lsvSource);
			this.tbc_Client.Location = new System.Drawing.Point(4, 24);
			this.tbc_Client.Margin = new System.Windows.Forms.Padding(0);
			this.tbc_Client.Name = "tbc_Client";
			this.tbc_Client.Size = new System.Drawing.Size(364, 236);
			this.tbc_Client.TabIndex = 2;
			this.tbc_Client.Text = "사용한 앱";
			this.tbc_Client.UseVisualStyleBackColor = true;
			// 
			// lsvSource
			// 
			this.lsvSource.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lsvSource_Name,
            this.lsvSource_Percent,
            this.lsvSource_Count});
			this.lsvSource.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lsvSource.FullRowSelect = true;
			this.lsvSource.HideSelection = false;
			this.lsvSource.Location = new System.Drawing.Point(0, 0);
			this.lsvSource.Name = "lsvSource";
			this.lsvSource.Size = new System.Drawing.Size(364, 236);
			this.lsvSource.TabIndex = 3;
			this.lsvSource.UseCompatibleStateImageBehavior = false;
			this.lsvSource.View = System.Windows.Forms.View.Details;
			// 
			// lsvSource_Name
			// 
			this.lsvSource_Name.Text = "앱 이름";
			this.lsvSource_Name.Width = 130;
			// 
			// lsvSource_Percent
			// 
			this.lsvSource_Percent.Text = "비율";
			this.lsvSource_Percent.Width = 70;
			// 
			// lsvSource_Count
			// 
			this.lsvSource_Count.Text = "트윗 수";
			this.lsvSource_Count.Width = 80;
			// 
			// tbc_Mention
			// 
			this.tbc_Mention.Controls.Add(this.lsvMention);
			this.tbc_Mention.Location = new System.Drawing.Point(4, 24);
			this.tbc_Mention.Margin = new System.Windows.Forms.Padding(0);
			this.tbc_Mention.Name = "tbc_Mention";
			this.tbc_Mention.Size = new System.Drawing.Size(364, 236);
			this.tbc_Mention.TabIndex = 3;
			this.tbc_Mention.Text = "멘션 빈도";
			this.tbc_Mention.UseVisualStyleBackColor = true;
			// 
			// lsvMention
			// 
			this.lsvMention.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lsvMention_Name,
            this.lsvMention_Percent,
            this.lsvMention_Count});
			this.lsvMention.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lsvMention.FullRowSelect = true;
			this.lsvMention.HideSelection = false;
			this.lsvMention.Location = new System.Drawing.Point(0, 0);
			this.lsvMention.Name = "lsvMention";
			this.lsvMention.Size = new System.Drawing.Size(364, 236);
			this.lsvMention.TabIndex = 5;
			this.lsvMention.UseCompatibleStateImageBehavior = false;
			this.lsvMention.View = System.Windows.Forms.View.Details;
			// 
			// lsvMention_Name
			// 
			this.lsvMention_Name.Text = "유저 ID";
			this.lsvMention_Name.Width = 130;
			// 
			// lsvMention_Percent
			// 
			this.lsvMention_Percent.Text = "비율";
			this.lsvMention_Percent.Width = 70;
			// 
			// lsvMention_Count
			// 
			this.lsvMention_Count.Text = "멘션 수";
			this.lsvMention_Count.Width = 80;
			// 
			// tbc_Keyword
			// 
			this.tbc_Keyword.Controls.Add(this.lsvKeyword);
			this.tbc_Keyword.Location = new System.Drawing.Point(4, 24);
			this.tbc_Keyword.Margin = new System.Windows.Forms.Padding(0);
			this.tbc_Keyword.Name = "tbc_Keyword";
			this.tbc_Keyword.Size = new System.Drawing.Size(364, 236);
			this.tbc_Keyword.TabIndex = 4;
			this.tbc_Keyword.Text = "키워드 빈도";
			this.tbc_Keyword.UseVisualStyleBackColor = true;
			// 
			// lsvKeyword
			// 
			this.lsvKeyword.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lsvKeyword_Name,
            this.lsvKeyword_rate,
            this.lsvKeyword_count});
			this.lsvKeyword.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lsvKeyword.FullRowSelect = true;
			this.lsvKeyword.HideSelection = false;
			this.lsvKeyword.Location = new System.Drawing.Point(0, 0);
			this.lsvKeyword.Name = "lsvKeyword";
			this.lsvKeyword.Size = new System.Drawing.Size(364, 236);
			this.lsvKeyword.TabIndex = 6;
			this.lsvKeyword.UseCompatibleStateImageBehavior = false;
			this.lsvKeyword.View = System.Windows.Forms.View.Details;
			// 
			// lsvKeyword_Name
			// 
			this.lsvKeyword_Name.Text = "키워드";
			this.lsvKeyword_Name.Width = 130;
			// 
			// lsvKeyword_rate
			// 
			this.lsvKeyword_rate.Text = "비율";
			this.lsvKeyword_rate.Width = 70;
			// 
			// lsvKeyword_count
			// 
			this.lsvKeyword_count.Text = "사용 수";
			this.lsvKeyword_count.Width = 80;
			// 
			// frmAnalytics
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(372, 264);
			this.Controls.Add(this.tbc);
			this.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(380, 290);
			this.Name = "frmAnalytics";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "트윗 분석 결과";
			this.tbc.ResumeLayout(false);
			this.tbc_Details.ResumeLayout(false);
			this.tbc_timeZone.ResumeLayout(false);
			this.tbc_Client.ResumeLayout(false);
			this.tbc_Mention.ResumeLayout(false);
			this.tbc_Keyword.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tbc;
		private System.Windows.Forms.TabPage tbc_Details;
		private System.Windows.Forms.ColumnHeader lsvResult_Key;
		private System.Windows.Forms.ColumnHeader lsvResult_Value;
		private System.Windows.Forms.TabPage tbc_timeZone;
		private System.Windows.Forms.ListView lsvResult;
		private System.Windows.Forms.TabPage tbc_Client;
		private System.Windows.Forms.ListView lsvSource;
		private System.Windows.Forms.ColumnHeader lsvSource_Name;
		private System.Windows.Forms.ColumnHeader lsvSource_Percent;
		private System.Windows.Forms.ColumnHeader lsvSource_Count;
		private System.Windows.Forms.TabPage tbc_Mention;
		private System.Windows.Forms.ListView lsvMention;
		private System.Windows.Forms.ColumnHeader lsvMention_Name;
		private System.Windows.Forms.ColumnHeader lsvMention_Percent;
		private System.Windows.Forms.ColumnHeader lsvMention_Count;
		private System.Windows.Forms.TabPage tbc_Keyword;
		private System.Windows.Forms.ListView lsvKeyword;
		private System.Windows.Forms.ColumnHeader lsvKeyword_Name;
		private System.Windows.Forms.ColumnHeader lsvKeyword_rate;
		private System.Windows.Forms.ColumnHeader lsvKeyword_count;
		private System.Windows.Forms.TabControl tbcDay;
		private System.Windows.Forms.ListView lsvTimeZone;
		private System.Windows.Forms.ColumnHeader lsvTimeZone_Time;
		private System.Windows.Forms.ColumnHeader lsvTimeZone_Rate;
		private System.Windows.Forms.ColumnHeader lsvTimeZone_Count;
		private System.Windows.Forms.ColumnHeader lsvTimeZone_RateT;



	}
}