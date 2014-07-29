using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Linq;
using System.Windows.Forms;
using ComputerBeacon.Json;
using WinTaskbar;

namespace TwitterArchiveViewer
{
	internal partial class frmMain : Form
	{
		List<TweetInfo> m_lstTweet = new List<TweetInfo>();

		Taskbar m_taskProg;

		public frmMain()
		{
			InitializeComponent();

			this.Text = Application.ProductName;
			
			this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);

			this.m_taskProg = new Taskbar(this);
			this.m_taskProg.ProgressBar.Maximum = this.prg.Maximum;
		}

		private void btnSearchHelp_Click(object sender, EventArgs e)
		{
			frmHelp frm = new frmHelp();
			frm.Show(this);
		}

		private void lsvDate_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (!this.grbSearch.Enabled) return;

			ListViewItem item = this.lsvDate.GetItemAt(e.X, e.Y);
			if (item != null)
			{
				int index = item.Index;

				this.bgwLoad.RunWorkerAsync(index);
			}
		}

		private void lsvTweets_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (!this.grbSearch.Enabled) return;

			ListViewItem item = this.lsvTweet.GetItemAt(e.X, e.Y);
			if (item != null)
			{
				TweetInfo info = (TweetInfo)item.Tag;

				Process.Start("explorer", String.Format("\"https://twitter.com/{0}/status/{1}\"", info.username, info.id));
			}
		}

		private void frmMain_Resize(object sender, EventArgs e)
		{
			this.grbSearch.Width	= this.Width	- 40;
			this.txtSearch.Width	= this.Width	- 145;
			this.prg.Width			= this.Width	- 40;
			this.lsvDate.Height		= this.Height	- 168;
			this.lsvTweet.Width		= this.Width	- 214;
			this.lsvTweet.Height	= this.Height	- 150;
			this.btnSearch.Left		= this.Width	- 98;

			this.lblCount.Left		= this.Width - this.lblCount.Width - 28;
		}

		private void txtSearch_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
				this.btnSearch_Click(null, null);
		}

		private void btnSearch_Click(object sender, EventArgs e)
		{
			this.grbSearch.Enabled = false;

			this.bgwSearch.RunWorkerAsync(this.txtSearch.Text.Trim());
		}

		private void tsmWeb_Click(object sender, EventArgs e)
		{
			try
			{
				TweetInfo info = (TweetInfo)this.lsvTweet.SelectedItems[0].Tag;
				Process.Start("explorer", String.Format("\"https://twitter.com/{0}/status/{1}\"", info.username, info.id));
			}
			catch { }
		}

		List<ToolStripMenuItem> m_lstUrls = new List<ToolStripMenuItem>();
		private void cmsUrls_Opening(object sender, CancelEventArgs e)
		{
			e.Cancel = true;

			if (this.lsvTweet.SelectedIndices.Count == 1)
			{
				this.m_lstUrls.ForEach((fitem) => { this.cmsUrls.Items.Remove(fitem); fitem.Dispose(); });
				this.m_lstUrls.Clear();

				try
				{
					TweetInfo info = (TweetInfo)this.lsvTweet.SelectedItems[0].Tag;

					if (info.urls == null)
					{
						this.tssUrls.Visible = false;
					}
					else
					{
						this.tssUrls.Visible = true;

						ToolStripMenuItem item;
						foreach (string url in info.urls)
						{
							item = new ToolStripMenuItem(url);
							item.Click += (fsender, fe) => Process.Start("explorer", String.Format("\"{0}\"", url));

							this.cmsUrls.Items.Add(item);
							this.m_lstUrls.Add(item);
						}
					}
				}
				catch { }

				e.Cancel = false;
			}
		}

		//////////////////////////////////////////////////////////////////////////

		private void frmMain_Shown(object sender, EventArgs e)
		{
			using (frmOpen frm = new frmOpen())
			{
				if (frm.ShowDialog() != DialogResult.OK)
				{
					this.Close();
					return;
				}

				this.bgwOpen.RunWorkerAsync();
			}
		}

		bool m_isRunning = false;
		private void Running()
		{
			if (this.InvokeRequired)
			{
				this.Invoke(new dv(this.Running));
			}
			else
			{
				this.grbSearch.Enabled = this.lsvDate.Enabled = this.lsvTweet.Enabled = this.btnAnalytics.Enabled = false;
				this.lsvTweet.Items.Clear();
				this.lblCount.Text = "0 트윗";
				this.frmMain_Resize(null, null);

				this.m_isRunning = true;

				if (!this.bgwRefresh.IsBusy)
					this.bgwRefresh.RunWorkerAsync();
			}
		}
		private void Running2()
		{
			if (this.InvokeRequired)
			{
				this.Invoke(new dv(this.Running2));
			}
			else
			{
				this.grbSearch.Enabled = this.lsvDate.Enabled = this.lsvTweet.Enabled = this.btnAnalytics.Enabled = false;
				this.frmMain_Resize(null, null);

				this.m_isRunning = true;

				if (!this.bgwRefresh.IsBusy)
					this.bgwRefresh.RunWorkerAsync();
			}
		}
		private void Wait()
		{
			this.m_isRunning = false;
			if (this.bgwRefresh.IsBusy)
				Thread.Sleep(50);
		}
		private void End()
		{
			this.grbSearch.Enabled = this.lsvDate.Enabled = this.lsvTweet.Enabled = this.btnAnalytics.Enabled = true;
		}

		string	m_prg_text	= null;
		bool	m_prg_done	= false;
		private void bgwRefresh_DoWork(object sender, DoWorkEventArgs e)
		{
			this.Invoke(new dv(delegate() { this.m_taskProg.ProgressBar.State = TaskbarProgressBarState.Normal; }));

			while (this.m_isRunning && !this.Disposing && !this.IsDisposed)
			{
				try
				{
					this.Invoke(new dv(
						delegate()
						{
							this.prg.Value = this.m_taskProg.ProgressBar.Value = (int)(ArchiveReader.Current.Index * 100f / ArchiveReader.Current.Count);
							this.lbl.Text =
								this.m_prg_done ?
									this.m_prg_text :
									String.Format(this.m_prg_text, ArchiveReader.Current.Index, ArchiveReader.Current.Count);
						}));

				}
				catch { }
			}

			try
			{
				this.Invoke(new dv(
					delegate()
					{
						this.prg.Value = this.m_taskProg.ProgressBar.Value = 100;
						this.m_taskProg.ProgressBar.State = TaskbarProgressBarState.NoProgress;
						this.lbl.Text = this.m_prg_text;
					}));
			}
			catch { }

			GC.Collect();
		}

		private delegate void dv();

		private void bgwOpen_DoWork(object sender, DoWorkEventArgs e)
		{
			this.Running();

			try
			{
				this.m_prg_done = false;
				this.m_prg_text = "트윗 불러오는중 {0} / {1}";
				ArchiveReader.Current.Index = 0;
				ArchiveReader.Current.Load();

				this.m_prg_done = true;
				this.m_prg_text = "트윗 불러오기 완료";
			}
			catch (Exception ex)
			{
				e.Result = ex.Message;
				return;
			}
		}
		private void bgwOpen_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			this.End();

			if (e.Result != null)
			{
				MessageBox.Show(this, (string)e.Result, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Hand);
				this.Close();
			}
			else
			{
				this.lblPayload.Text =
					String.Format(
						"{0} - {1} 트윗",
						ArchiveReader.Current.CreatedAt.ToString("yyyy.MM.dd HH:mm"),
						ArchiveReader.Current.Count);

				ListViewItem item;
				this.lsvDate.Items.Clear();

				for (int i = 0; i < ArchiveReader.Current.MonthlyInfos.Length; i++)
				{
					MonthlyInfo info = ArchiveReader.Current.MonthlyInfos[i];

					item = new ListViewItem();
					item.Text = String.Format("{0:0000} {1:00}", info.year, info.month);
					item.SubItems.Add(info.count.ToString());
					this.lsvDate.Items.Add(item);
				}

				this.lsvDate.Items[0].Selected = true;
				this.bgwLoad.RunWorkerAsync(0);
			}
		}

		private void bgwLoad_DoWork(object sender, DoWorkEventArgs e)
		{
			this.Running();

			int monthIndex = (int)e.Argument;
			int start = ArchiveReader.Current.IndexStart[monthIndex];
			int end = ArchiveReader.Current.IndexEnd[monthIndex];

			try
			{
				this.m_prg_done = false;
				this.m_prg_text = "트윗 불러오는중 {0} / {1}";

				ArchiveReader.Current.Index = start;

				this.m_lstTweet.Clear();
				for (int i = start; i < end; i++)
					this.m_lstTweet.Add(ArchiveReader.Current[i]);

				this.m_lstTweet.Sort(delegate(TweetInfo a, TweetInfo b) { return a.date.CompareTo(b.date) * -1; });
			}
			catch (Exception ex)
			{
				e.Result = ex.Message;
			}

			this.m_prg_done = true;
			this.m_prg_text = "트윗 조회 완료";

			this.Wait();
		}
		private void bgwLoad_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			this.End();

			if (e.Result != null)
			{
				MessageBox.Show(this, (string)e.Result, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Hand);
				this.Close();
			}
			else
			{
				this.AddItem();
			}
		}

		private void bgwSearch_DoWork(object sender, DoWorkEventArgs e)
		{
			this.Running();

			TweetSearch search = null;

			try
			{
				search = new TweetSearch((string)e.Argument);
			}
			catch
			{
				e.Result = "잘못된 검색 문자열입니다. 다시 한번 확인해주세요";
			}

			if (search != null)
			{
				try
				{
					this.m_prg_done = false;
					this.m_prg_text = "트윗 검색중 {0} / {1}";

					ArchiveReader.Current.Index = 0;

					TweetInfo info;

					this.m_lstTweet.Clear();
					for (int i = 0; i < ArchiveReader.Current.Count; i++)
					{
						info = ArchiveReader.Current[i];
						if (search.Check(info))
							this.m_lstTweet.Add(info);
					}

					this.m_lstTweet.Sort(delegate(TweetInfo a, TweetInfo b) { return a.date.CompareTo(b.date) * -1; });
				}
				catch (Exception ex)
				{
					e.Result = ex.Message;
				}
			}

			this.m_prg_done = true;
			this.m_prg_text = "트윗 조회 완료";

			this.Wait();
		}
		private void bgwSearch_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			this.End();

			if (e.Result is String)
			{
				MessageBox.Show(this, (string)e.Result, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Hand);
				this.Close();
			}
			else
			{
				this.AddItem();
			}
		}

		private void AddItem()
		{
			if (this.m_lstTweet.Count > 50000)
				if (MessageBox.Show(
					this,
					"검색된 트윗수가 너무 많습니다.\n불러오시겠습니까?",
					Application.ProductName,
					MessageBoxButtons.OKCancel,
					MessageBoxIcon.Asterisk) == DialogResult.Cancel)
					return;

			int i;
			ListViewItem item;
			TweetInfo info;

			this.lsvTweet.BeginUpdate();

			for (i = 0; i < this.m_lstTweet.Count; i++)
			{
				info = this.m_lstTweet[i];

				if (!this.chkFilterMy.Checked && !info.isRt)
					continue;

				if (!this.chkFilterRetweet.Checked && info.isRt)
					continue;

				if (!this.chkFilterLink.Checked && info.urls != null)
					continue;

				if (!this.chkFilterImage.Checked && TweetInfo.IsImageUrl(info.urls))
					continue;

				if (!this.chkFilterMention.Checked && (info.mentionTo != null || info.text[0] == '@'))
					continue;

				item = new ListViewItem();
				item.Text = info.date.ToString("yyyy-MM-dd HH:mm:ss");
				item.Tag = info;
				item.SubItems.Add(info.username);
				item.SubItems.Add(info.text);
				item.SubItems.Add(info.source);
				item.StateImageIndex = info.isRt ? 0 : -1;

				this.lsvTweet.Items.Add(item);
			}

			this.lsvTweet.EndUpdate();

			this.lblCount.Text = String.Format("{0} 트윗", this.lsvTweet.Items.Count);
			this.frmMain_Resize(null, null);
		}

		private void btnAnalytics_Click(object sender, EventArgs e)
		{
			this.bgwAnalytics.RunWorkerAsync(this.txtSearch.Text.Trim());
		}
		private void bgwAnalytics_DoWork(object sender, DoWorkEventArgs e)
		{
			this.Running2();

			int i, j, k, l;

			// 첫 트윗과 마지막 트윗
			DateTime dateStart, dateEnd;
			int totalDay;

			// 일일 최다 트윗
			int dailyMax = 0;
			int dailyMin = int.MaxValue;
			DateTime dailyMaxDate = DateTime.MinValue;
			DateTime dailyMinDate = DateTime.MinValue;

			// 자동 트윗 수
			int countAuto = 0;

			// 트윗한 날
			DateTime countNow;
			int countTweet0 = 0; // 트윗하지 않은 날

			// 가장 많이 사용한 클라이언트
			Dictionary<string, int> dicClient = new Dictionary<string, int>();
			int clientMax = 0;
			string client = null;

			// 가장 많이 대화한 사람
			Dictionary<string, int> dicMention = new Dictionary<string, int>();
			int mentionMax = 0;
			string mention = null;

			// 가장 많이 사용한 키워드
			Dictionary<string, int> dicKeyword = new Dictionary<string, int>();
			int keywordMax = 0;
			string keyword = null;
			int keywordCount = 0;

			// 멘션 수
			int mentionCount = 0;

			// 전체 글자 수 / 4294967296 / 140 = 3067 8337.82
			int totalChar = 0;
			int totalCharBytes = 0;

			// 업로드한 이미지 수
			int imageCount = 0;

			// 트윗한 링크 수
			int urlCount = 0;

			// 알티 수
			int rtCount = 0;

			// 시간대
			int[] timezonecount = { 0, 0, 0, 0, 0, 0, 0, 0 };
			int[,] timezone = new int[8, 24];
			int[,] timezoneRank = new int[8, 24];

			for (i = 0; i < 8 * 24; i++)
			{
				timezone[i / 24, i % 24] = 0;
				timezoneRank[i / 24, i % 24] = i % 24;
			}

			try
			{
				string[] keywords;

				int countTweet = 0;

				this.m_prg_done = false;
				this.m_prg_text = "트윗 분석중 {0} / {1}";

				dateStart = ArchiveReader.Current[0].date;
				countNow = ArchiveReader.Current[0].date.Date;

				DateTime date;

				ArchiveReader.Current.Index = 0;
				TweetInfo info;
				for (i = 0; i < ArchiveReader.Current.Count; i++)
				{
					ArchiveReader.Current.Index = i;
					info = ArchiveReader.Current[i];

					if (info.date.Date == countNow)
					{
						// RT 된 항목이 아니고 자동 트윗이 아닌 경우
						if (info.isRt || !TweetInfo.isAutoSource(info.source))
							countTweet++;
						else
							countAuto++;
					}
					else
					{
						if (countTweet == 0)
						{
							countTweet0 += (int)(info.date.Date - countNow).TotalDays;
						}
						else if (countTweet > dailyMax)
						{
							dailyMax = countTweet;
							dailyMaxDate = countNow;
						}
						else if (countTweet > 0 && countTweet < dailyMin)
						{
							dailyMin = countTweet;
							dailyMinDate = countNow;
						}

						countTweet = 0;

						countNow = info.date.Date;
					}

					if (info.isRt)
						rtCount++;

					if (info.source != null)
					{
						if (dicClient.ContainsKey(info.source))
							dicClient[info.source]++;
						else
							dicClient.Add(info.source, 1);

						if (dicClient[info.source] > clientMax)
						{
							clientMax = dicClient[info.source];
							client = info.source;
						}
					}

					if (info.mentionTo != null)
					{
						mentionCount++;

						if (dicMention.ContainsKey(info.mentionTo))
							dicMention[info.mentionTo]++;
						else
							dicMention.Add(info.mentionTo, 1);

						if (dicMention[info.mentionTo] > mentionMax)
						{
							mentionMax = dicMention[info.mentionTo];
							mention = info.mentionTo;
						}
					}

					if (!info.isRt && info.urls != null) 
					{
						for (j = 0; j < info.urls.Length; j++)
							if (TweetInfo.IsImageUrl(info.urls[j]))
								urlCount++;
					}

					if (!info.isRt)
					{
						keywords = info.text.Split(' ');
						for (j = 0; j < keywords.Length; j++)
						{
							if (keywords[j].Length == 0 || keywords[j][0] == '@') continue;

							keywordCount++;

							if (dicKeyword.ContainsKey(keywords[j]))
								dicKeyword[keywords[j]]++;
							else
								dicKeyword.Add(keywords[j], 1);

							if (dicKeyword[keywords[j]] > keywordMax)
							{
								keywordMax = dicKeyword[keywords[j]];
								keyword = keywords[j];
							}
						}
					}

					totalChar += info.text.Length;
					totalCharBytes += Encoding.ASCII.GetByteCount(info.text);

					date = info.date;

					timezonecount[0]++;
					timezonecount[(int)date.DayOfWeek + 1]++;

					timezone[0, date.Hour]++;
					timezone[(int)date.DayOfWeek + 1, date.Hour]++;
				}

				dateEnd = ArchiveReader.Current[ArchiveReader.Current.Count - 1].date;

				double days = (dateEnd - dateStart).TotalDays;
				totalDay = (int)Math.Ceiling(days);
				
				int min;
				int max;
				
				for (i = 0; i < 8; i++)
				{
					min = timezone[i, 0];
					max = timezone[i, 0];

					for (j = 0; j < 24; j++)
					{
						if (timezone[i, j] < min) min = timezone[i, j];
						if (timezone[i, j] > max) max = timezone[i, j];

						for (k = 0; k < j; k++)
						{
							if (timezone[i, j] > timezone[i, timezoneRank[i, k]])
							{
								l = timezoneRank[i, k];
								timezoneRank[i, k] = timezoneRank[i, j];
								timezoneRank[i, j] = l;
							}
						}
					}
				}

				//////////////////////////////////////////////////////////////////////////

				frmAnalytics frm = new frmAnalytics();

				frm.AddResult("아카이브 기간", String.Format("{0:yyyy-MM-dd} ~ {1:yyyy-MM-dd} ({2})", dateStart, dateEnd, totalDay));

				frm.AddResult("트윗수", ArchiveReader.Current.Count.ToString());
				frm.AddResult("트윗수 (일일 평균)", (ArchiveReader.Current.Count * 1.0 / days).ToString("##0.00"));

				frm.AddResult("리트윗 수", String.Format("{0:#0} ({1:##0.00} %)", rtCount, (rtCount * 100.0d / ArchiveReader.Current.Count)));
				frm.AddResult("리트윗 수 (일일 평균)", (rtCount * 1.0 / days).ToString("##0.0"));

				frm.AddResult("멘션 수", String.Format("{0:#0} ({1:##0.00} %)", mentionCount, (mentionCount * 100.0d / ArchiveReader.Current.Count)));
				frm.AddResult("멘션 수 (일일 평균)", (mentionCount / days).ToString("##0.00"));

				frm.AddResult("자동 트윗 수", String.Format("{0:#0} ({1:##0.00} %)", countAuto, (countAuto * 100.0d / ArchiveReader.Current.Count)));

				frm.AddResult("일일 최다 트윗", String.Format("{0:##0} ({1:yyyy-MM-dd})", dailyMax, dailyMaxDate));
				frm.AddResult("일일 최소 트윗", String.Format("{0:##0} ({1:yyyy-MM-dd})", dailyMin, dailyMinDate));

				frm.AddResult("트윗하지 않은 날", String.Format("{0:#0} ({1:##0.00} %)", countTweet0, (countTweet0 * 100.0d / totalDay)));

				frm.AddResult("작성한 글자 수", String.Format("{0:##0} ({1})", totalChar, Utils.ToSize(totalCharBytes)));
				frm.AddResult("작성한 글자 수", String.Format("원고지 {0:##0.0} 장, 소설책 {1:#,##0.0} 권", totalChar / 200.0d, totalChar / 140000.0d));

				frm.AddResult("트윗당 평균 글자 수", (totalChar * 1.0d / ArchiveReader.Current.Count).ToString("##0"));

				frm.AddResult("이미지 업로드 수", imageCount.ToString("##0"));
				frm.AddResult("링크 업로드 수", urlCount.ToString("##0"));

				frm.AddResult("자주 사용한 앱", String.Format("{0} ({1:##0.00} %)", client, (dicClient[client] * 100.0d / ArchiveReader.Current.Count)));
				frm.AddResult("자주 멘션한 사람", String.Format("{0} ({1:##0.00} %)", mention, (dicMention[mention] * 100.0d / mentionCount)));
				frm.AddResult("자주 사용한 키워드", String.Format("{0:##0} ({1:##0.00} %)", keyword, (keywordMax * 100.0d / keywordCount)));

				frm.AddTimezone(timezonecount, timezone, timezoneRank);

				foreach (KeyValuePair<string, int> item in (from item in dicClient orderby item.Value descending select item))
					frm.AddSource(item.Key, (item.Value * 100.0d / ArchiveReader.Current.Count), item.Value);

				foreach (KeyValuePair<string, int> item in (from item in dicMention orderby item.Value descending select item))
					frm.AddMention(item.Key, (item.Value * 100.0d / mentionCount), item.Value);

				j = 0;
				foreach (KeyValuePair<string, int> item in (from item in dicKeyword orderby item.Value descending select item))
					if (j++ < 500)
						frm.AddKeyword(item.Key, (item.Value * 100.0d / keywordCount), item.Value);
					else
						break;

				e.Result = frm;
			}
			catch (Exception ex)
			{
				e.Result = ex.Message;
			}

			this.m_prg_done = true;
			this.m_prg_text = "트윗 조회 완료";

			this.Wait();
		}
		private void bgwAnalytics_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			this.End();

			if (e.Result is String)
			{
				MessageBox.Show(this, (string)e.Result, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Hand);
				this.Close();
			}
			else
			{
				frmAnalytics frm = (frmAnalytics)e.Result;
				frm.ShowDialog(this);
				frm.Dispose();
			}
		}
	}
}
