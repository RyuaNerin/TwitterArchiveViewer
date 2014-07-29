namespace TwitterArchiveViewer
{
	partial class frmMain
	{
		/// <summary>
		/// 필수 디자이너 변수입니다.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 사용 중인 모든 리소스를 정리합니다.
		/// </summary>
		/// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form 디자이너에서 생성한 코드

		/// <summary>
		/// 디자이너 지원에 필요한 메서드입니다.
		/// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
			this.lsvDate = new System.Windows.Forms.ListView();
			this.lsvDate_Date = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.lsvDate_Tweets = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.lsvTweet = new System.Windows.Forms.ListView();
			this.lsvTweet_Date = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.lsvTweet_user = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.lsvTweet_Text = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.lsvTweet_source = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.cmsUrls = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.tsmWeb = new System.Windows.Forms.ToolStripMenuItem();
			this.tssUrls = new System.Windows.Forms.ToolStripSeparator();
			this.imgList = new System.Windows.Forms.ImageList(this.components);
			this.lblCount = new System.Windows.Forms.Label();
			this.grbSearch = new System.Windows.Forms.GroupBox();
			this.btnSearchHelp = new System.Windows.Forms.Button();
			this.btnSearch = new System.Windows.Forms.Button();
			this.txtSearch = new System.Windows.Forms.TextBox();
			this.bgwOpen = new System.ComponentModel.BackgroundWorker();
			this.lblPayload = new System.Windows.Forms.Label();
			this.bgwLoad = new System.ComponentModel.BackgroundWorker();
			this.bgwSearch = new System.ComponentModel.BackgroundWorker();
			this.sts = new System.Windows.Forms.StatusStrip();
			this.tsdFilter = new System.Windows.Forms.ToolStripDropDownButton();
			this.chkFilterMy = new System.Windows.Forms.ToolStripMenuItem();
			this.chkFilterMention = new System.Windows.Forms.ToolStripMenuItem();
			this.chkFilterRetweet = new System.Windows.Forms.ToolStripMenuItem();
			this.chkFilterLink = new System.Windows.Forms.ToolStripMenuItem();
			this.chkFilterImage = new System.Windows.Forms.ToolStripMenuItem();
			this.prg = new System.Windows.Forms.ToolStripProgressBar();
			this.lbl = new System.Windows.Forms.ToolStripStatusLabel();
			this.bgwRefresh = new System.ComponentModel.BackgroundWorker();
			this.btnAnalytics = new System.Windows.Forms.Button();
			this.bgwAnalytics = new System.ComponentModel.BackgroundWorker();
			this.cmsUrls.SuspendLayout();
			this.grbSearch.SuspendLayout();
			this.sts.SuspendLayout();
			this.SuspendLayout();
			// 
			// lsvDate
			// 
			this.lsvDate.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lsvDate_Date,
            this.lsvDate_Tweets});
			this.lsvDate.FullRowSelect = true;
			this.lsvDate.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.lsvDate.HideSelection = false;
			this.lsvDate.LabelWrap = false;
			this.lsvDate.Location = new System.Drawing.Point(12, 105);
			this.lsvDate.Name = "lsvDate";
			this.lsvDate.Size = new System.Drawing.Size(168, 273);
			this.lsvDate.TabIndex = 1;
			this.lsvDate.UseCompatibleStateImageBehavior = false;
			this.lsvDate.View = System.Windows.Forms.View.Details;
			this.lsvDate.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lsvDate_MouseDoubleClick);
			// 
			// lsvDate_Date
			// 
			this.lsvDate_Date.Text = "";
			this.lsvDate_Date.Width = 70;
			// 
			// lsvDate_Tweets
			// 
			this.lsvDate_Tweets.Text = "";
			this.lsvDate_Tweets.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// lsvTweet
			// 
			this.lsvTweet.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.lsvTweet_Date,
            this.lsvTweet_user,
            this.lsvTweet_Text,
            this.lsvTweet_source});
			this.lsvTweet.ContextMenuStrip = this.cmsUrls;
			this.lsvTweet.FullRowSelect = true;
			this.lsvTweet.HideSelection = false;
			this.lsvTweet.LabelWrap = false;
			this.lsvTweet.Location = new System.Drawing.Point(186, 87);
			this.lsvTweet.Name = "lsvTweet";
			this.lsvTweet.ShowGroups = false;
			this.lsvTweet.Size = new System.Drawing.Size(428, 291);
			this.lsvTweet.StateImageList = this.imgList;
			this.lsvTweet.TabIndex = 1;
			this.lsvTweet.UseCompatibleStateImageBehavior = false;
			this.lsvTweet.View = System.Windows.Forms.View.Details;
			this.lsvTweet.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lsvTweets_MouseDoubleClick);
			// 
			// lsvTweet_Date
			// 
			this.lsvTweet_Date.Text = "작성일";
			this.lsvTweet_Date.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.lsvTweet_Date.Width = 150;
			// 
			// lsvTweet_user
			// 
			this.lsvTweet_user.Text = "작성자";
			this.lsvTweet_user.Width = 120;
			// 
			// lsvTweet_Text
			// 
			this.lsvTweet_Text.Text = "트윗내용";
			this.lsvTweet_Text.Width = 300;
			// 
			// lsvTweet_source
			// 
			this.lsvTweet_source.Text = "사용한 앱";
			this.lsvTweet_source.Width = 80;
			// 
			// cmsUrls
			// 
			this.cmsUrls.Font = new System.Drawing.Font("맑은 고딕", 9F);
			this.cmsUrls.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmWeb,
            this.tssUrls});
			this.cmsUrls.Name = "cmsUrls";
			this.cmsUrls.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.cmsUrls.Size = new System.Drawing.Size(139, 32);
			this.cmsUrls.Opening += new System.ComponentModel.CancelEventHandler(this.cmsUrls_Opening);
			// 
			// tsmWeb
			// 
			this.tsmWeb.Name = "tsmWeb";
			this.tsmWeb.Size = new System.Drawing.Size(138, 22);
			this.tsmWeb.Text = "웹에서 보기";
			this.tsmWeb.Click += new System.EventHandler(this.tsmWeb_Click);
			// 
			// tssUrls
			// 
			this.tssUrls.Name = "tssUrls";
			this.tssUrls.Size = new System.Drawing.Size(135, 6);
			// 
			// imgList
			// 
			this.imgList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgList.ImageStream")));
			this.imgList.TransparentColor = System.Drawing.Color.Transparent;
			this.imgList.Images.SetKeyName(0, "");
			// 
			// lblCount
			// 
			this.lblCount.AutoSize = true;
			this.lblCount.Location = new System.Drawing.Point(572, 69);
			this.lblCount.Name = "lblCount";
			this.lblCount.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.lblCount.Size = new System.Drawing.Size(42, 15);
			this.lblCount.TabIndex = 2;
			this.lblCount.Text = "0 트윗";
			this.lblCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// grbSearch
			// 
			this.grbSearch.Controls.Add(this.btnSearchHelp);
			this.grbSearch.Controls.Add(this.btnSearch);
			this.grbSearch.Controls.Add(this.txtSearch);
			this.grbSearch.Location = new System.Drawing.Point(12, 12);
			this.grbSearch.Name = "grbSearch";
			this.grbSearch.Size = new System.Drawing.Size(602, 51);
			this.grbSearch.TabIndex = 3;
			this.grbSearch.TabStop = false;
			this.grbSearch.Text = "검색";
			// 
			// btnSearchHelp
			// 
			this.btnSearchHelp.Location = new System.Drawing.Point(6, 20);
			this.btnSearchHelp.Name = "btnSearchHelp";
			this.btnSearchHelp.Size = new System.Drawing.Size(29, 21);
			this.btnSearchHelp.TabIndex = 3;
			this.btnSearchHelp.Text = "?";
			this.btnSearchHelp.UseVisualStyleBackColor = true;
			this.btnSearchHelp.Click += new System.EventHandler(this.btnSearchHelp_Click);
			// 
			// btnSearch
			// 
			this.btnSearch.Location = new System.Drawing.Point(544, 22);
			this.btnSearch.Name = "btnSearch";
			this.btnSearch.Size = new System.Drawing.Size(52, 23);
			this.btnSearch.TabIndex = 2;
			this.btnSearch.Text = "검색";
			this.btnSearch.UseVisualStyleBackColor = true;
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
			// 
			// txtSearch
			// 
			this.txtSearch.Location = new System.Drawing.Point(41, 20);
			this.txtSearch.Name = "txtSearch";
			this.txtSearch.Size = new System.Drawing.Size(497, 23);
			this.txtSearch.TabIndex = 0;
			this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
			// 
			// bgwOpen
			// 
			this.bgwOpen.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwOpen_DoWork);
			this.bgwOpen.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwOpen_RunWorkerCompleted);
			// 
			// lblPayload
			// 
			this.lblPayload.AutoSize = true;
			this.lblPayload.Location = new System.Drawing.Point(186, 69);
			this.lblPayload.Name = "lblPayload";
			this.lblPayload.Size = new System.Drawing.Size(169, 15);
			this.lblPayload.TabIndex = 4;
			this.lblPayload.Text = "2014.06.30 00:00:00 - 0 트윗";
			// 
			// bgwLoad
			// 
			this.bgwLoad.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwLoad_DoWork);
			this.bgwLoad.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwLoad_RunWorkerCompleted);
			// 
			// bgwSearch
			// 
			this.bgwSearch.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwSearch_DoWork);
			this.bgwSearch.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwSearch_RunWorkerCompleted);
			// 
			// sts
			// 
			this.sts.Font = new System.Drawing.Font("맑은 고딕", 9F);
			this.sts.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsdFilter,
            this.prg,
            this.lbl});
			this.sts.Location = new System.Drawing.Point(0, 385);
			this.sts.Name = "sts";
			this.sts.Size = new System.Drawing.Size(634, 22);
			this.sts.TabIndex = 5;
			this.sts.Text = "statusStrip1";
			// 
			// tsdFilter
			// 
			this.tsdFilter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.tsdFilter.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.chkFilterMy,
            this.chkFilterMention,
            this.chkFilterRetweet,
            this.chkFilterLink,
            this.chkFilterImage});
			this.tsdFilter.Image = ((System.Drawing.Image)(resources.GetObject("tsdFilter.Image")));
			this.tsdFilter.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsdFilter.Margin = new System.Windows.Forms.Padding(0, 2, 10, 0);
			this.tsdFilter.Name = "tsdFilter";
			this.tsdFilter.Size = new System.Drawing.Size(56, 20);
			this.tsdFilter.Text = "필터링";
			// 
			// chkFilterMy
			// 
			this.chkFilterMy.Checked = true;
			this.chkFilterMy.CheckOnClick = true;
			this.chkFilterMy.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkFilterMy.Name = "chkFilterMy";
			this.chkFilterMy.Size = new System.Drawing.Size(190, 22);
			this.chkFilterMy.Text = "내 트윗 표시";
			// 
			// chkFilterMention
			// 
			this.chkFilterMention.Checked = true;
			this.chkFilterMention.CheckOnClick = true;
			this.chkFilterMention.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkFilterMention.Name = "chkFilterMention";
			this.chkFilterMention.Size = new System.Drawing.Size(190, 22);
			this.chkFilterMention.Text = "멘션 표시";
			// 
			// chkFilterRetweet
			// 
			this.chkFilterRetweet.Checked = true;
			this.chkFilterRetweet.CheckOnClick = true;
			this.chkFilterRetweet.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkFilterRetweet.Name = "chkFilterRetweet";
			this.chkFilterRetweet.Size = new System.Drawing.Size(190, 22);
			this.chkFilterRetweet.Text = "리트윗 표시";
			// 
			// chkFilterLink
			// 
			this.chkFilterLink.Checked = true;
			this.chkFilterLink.CheckOnClick = true;
			this.chkFilterLink.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkFilterLink.Name = "chkFilterLink";
			this.chkFilterLink.Size = new System.Drawing.Size(190, 22);
			this.chkFilterLink.Text = "링크가 첨부된 트윗";
			// 
			// chkFilterImage
			// 
			this.chkFilterImage.Checked = true;
			this.chkFilterImage.CheckOnClick = true;
			this.chkFilterImage.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkFilterImage.Name = "chkFilterImage";
			this.chkFilterImage.Size = new System.Drawing.Size(190, 22);
			this.chkFilterImage.Text = "이미지가 첨부된 트윗";
			// 
			// prg
			// 
			this.prg.Name = "prg";
			this.prg.Size = new System.Drawing.Size(100, 16);
			this.prg.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			// 
			// lbl
			// 
			this.lbl.Margin = new System.Windows.Forms.Padding(5, 3, 0, 2);
			this.lbl.Name = "lbl";
			this.lbl.Size = new System.Drawing.Size(105, 17);
			this.lbl.Text = "불러오는중 : 0 / 0";
			// 
			// bgwRefresh
			// 
			this.bgwRefresh.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwRefresh_DoWork);
			// 
			// btnAnalytics
			// 
			this.btnAnalytics.Location = new System.Drawing.Point(12, 69);
			this.btnAnalytics.Name = "btnAnalytics";
			this.btnAnalytics.Size = new System.Drawing.Size(168, 30);
			this.btnAnalytics.TabIndex = 6;
			this.btnAnalytics.Text = "트윗 분석";
			this.btnAnalytics.UseVisualStyleBackColor = true;
			this.btnAnalytics.Click += new System.EventHandler(this.btnAnalytics_Click);
			// 
			// bgwAnalytics
			// 
			this.bgwAnalytics.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgwAnalytics_DoWork);
			this.bgwAnalytics.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgwAnalytics_RunWorkerCompleted);
			// 
			// frmMain
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(634, 407);
			this.Controls.Add(this.btnAnalytics);
			this.Controls.Add(this.sts);
			this.Controls.Add(this.lblPayload);
			this.Controls.Add(this.grbSearch);
			this.Controls.Add(this.lblCount);
			this.Controls.Add(this.lsvTweet);
			this.Controls.Add(this.lsvDate);
			this.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
			this.MinimumSize = new System.Drawing.Size(642, 441);
			this.Name = "frmMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "TweetArchiveViewer";
			this.Shown += new System.EventHandler(this.frmMain_Shown);
			this.Resize += new System.EventHandler(this.frmMain_Resize);
			this.cmsUrls.ResumeLayout(false);
			this.grbSearch.ResumeLayout(false);
			this.grbSearch.PerformLayout();
			this.sts.ResumeLayout(false);
			this.sts.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListView lsvDate;
		private System.Windows.Forms.ListView lsvTweet;
		private System.Windows.Forms.ColumnHeader lsvDate_Date;
		private System.Windows.Forms.ColumnHeader lsvDate_Tweets;
		private System.Windows.Forms.ImageList imgList;
		private System.Windows.Forms.ColumnHeader lsvTweet_Date;
		private System.Windows.Forms.ColumnHeader lsvTweet_user;
		private System.Windows.Forms.ColumnHeader lsvTweet_Text;
		private System.Windows.Forms.Label lblCount;
		private System.Windows.Forms.GroupBox grbSearch;
		private System.Windows.Forms.TextBox txtSearch;
		private System.Windows.Forms.Button btnSearch;
		private System.Windows.Forms.Button btnSearchHelp;
		private System.ComponentModel.BackgroundWorker bgwOpen;
		private System.Windows.Forms.Label lblPayload;
		private System.ComponentModel.BackgroundWorker bgwLoad;
		private System.ComponentModel.BackgroundWorker bgwSearch;
		private System.Windows.Forms.StatusStrip sts;
		private System.Windows.Forms.ToolStripStatusLabel lbl;
		private System.Windows.Forms.ToolStripProgressBar prg;
		private System.ComponentModel.BackgroundWorker bgwRefresh;
		private System.Windows.Forms.ContextMenuStrip cmsUrls;
		private System.Windows.Forms.ToolStripMenuItem tsmWeb;
		private System.Windows.Forms.ToolStripSeparator tssUrls;
		private System.Windows.Forms.Button btnAnalytics;
		private System.ComponentModel.BackgroundWorker bgwAnalytics;
		private System.Windows.Forms.ColumnHeader lsvTweet_source;
		private System.Windows.Forms.ToolStripDropDownButton tsdFilter;
		private System.Windows.Forms.ToolStripMenuItem chkFilterMy;
		private System.Windows.Forms.ToolStripMenuItem chkFilterRetweet;
		private System.Windows.Forms.ToolStripMenuItem chkFilterImage;
		private System.Windows.Forms.ToolStripMenuItem chkFilterMention;
		private System.Windows.Forms.ToolStripMenuItem chkFilterLink;
	}
}

