using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using ComputerBeacon.Json;

namespace TwitterArchiveViewer
{
	public partial class frmMain : Form
	{
		string	m_Path;
		bool	m_isLoadAll;
		int		m_totalTweets;

		List<TweetMonth>	m_lstIndex		= new List<TweetMonth>();
		List<TweetInfo>		m_lstTweetAll	= new List<TweetInfo>();
		List<TweetInfo>		m_lstTweet		= new List<TweetInfo>();

		//////////////////////////////////////////////////////////////////////////
		
		public frmMain()
		{
			InitializeComponent();

			this.Text = Application.ProductName;
		}

		private void frmMain_Shown(object sender, EventArgs e)
		{
			using (frmOpen frm = new frmOpen())
			{
				if (frm.ShowDialog() != DialogResult.OK)
				{
					this.Close();
					return;
				}

				this.m_Path			= frm.txtPath.Text;
				this.m_isLoadAll	= frm.chkLoadAll.Checked;

				this.bgwOpen.RunWorkerAsync();
			}
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
				TweetInfo info = this.m_lstTweet[(int)item.Tag];

				System.Diagnostics.Process.Start("explorer", String.Format("\"https://twitter.com/{0}/status/{1}\"", info.username, info.id));
			}
		}

		private void frmMain_Resize(object sender, EventArgs e)
		{
			this.DoResize();
		}
		private void DoResize()
		{
			this.grbSearch.Width	= this.Width	- 40;
			this.txtSearch.Width	= this.Width	- 145;
			this.prg.Width			= this.Width	- 40;
			this.lsvDate.Height		= this.Height	- 132;
			this.lsvTweet.Width	= this.Width	- 214;
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
				TweetInfo info = this.m_lstTweet[(int)this.lsvTweet.SelectedItems[0].Tag];
				System.Diagnostics.Process.Start("explorer", String.Format("\"https://twitter.com/{0}/status/{1}\"", info.username, info.id));
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
					TweetInfo info = this.m_lstTweet[(int)this.lsvTweet.SelectedItems[0].Tag];

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
							item.Tag = url;
							item.Click += OpenUrl;

							this.cmsUrls.Items.Add(item);
							this.m_lstUrls.Add(item);
						}
					}
				}
				catch { }

				e.Cancel = false;
			}
		}

		private void OpenUrl(object sender, EventArgs e)
		{
			System.Diagnostics.Process.Start("explorer", String.Format("\"{0}\"", (string)((ToolStripMenuItem)sender).Tag));
		}

		//////////////////////////////////////////////////////////////////////////

		bool m_isRunning = false;
		private void Running()
		{
			if (this.InvokeRequired)
			{
				this.Invoke(new dv(this.Running));
			}
			else
			{
				this.grbSearch.Enabled = this.lsvDate.Enabled = this.lsvTweet.Enabled = false;
				this.lsvTweet.Items.Clear();
				this.lblCount.Text = "0 트윗";
				this.DoResize();

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
			this.grbSearch.Enabled = this.lsvDate.Enabled = this.lsvTweet.Enabled = true;
		}

		private delegate void dv();

		private void bgwOpen_DoWork(object sender, DoWorkEventArgs e)
		{
			this.Running();

			string		path;
			string		body;
			JsonArray	ja;
			JsonObject	jo;
			DateTime	created;

			// Load Payload
			path = Path.Combine(this.m_Path, "data/js/payload_details.js");
			if (!File.Exists(path))
			{
				e.Result = "payload_details.js 파일이 존재하지 않습니다";
				return;
			}

			try
			{
				body = File.ReadAllText(path, Encoding.UTF8);
				body = body.Substring(body.IndexOf('{'));
				jo = new JsonObject(body);
				this.m_totalTweets = jo.GetInt("tweets");
				created = jo.GetDateTime("created_at");
			}
			catch
			{
				e.Result = "payload_details.js 파일이 손상되었습니다!";
				return;
			}

			//////////////////////////////////////////////////////////////////////////
			// Tweet Index
			path = Path.Combine(this.m_Path, "data/js/tweet_index.js");
			if (!File.Exists(path))
			{
				e.Result = "tweet_index.js 파일이 존재하지 않습니다";
				return;
			}

			try
			{
				body = File.ReadAllText(path, Encoding.UTF8);
				body = body.Substring(body.IndexOf('['));

				ja = new JsonArray(body);
				for (int i = 0; i < ja.Count; i++)
				{
					jo = ja[i] as JsonObject;

					TweetMonth index = new TweetMonth();
					index.year	= jo.GetShort("year");
					index.month = jo.GetByte("month");
					index.count = jo.GetInt("tweet_count");
					index.path	= jo.GetString("file_name");

					this.m_lstIndex.Add(index);
				}
			}
			catch
			{
				e.Result = "tweet_index.js 파일이 손상되었습니다!";
				return;
			}

			this.m_lstIndex.Sort(
				delegate(TweetMonth a, TweetMonth b)
				{
					int aa = a.year * 100 + a.month;
					int bb = b.year * 100 + b.month;
					return aa.CompareTo(bb);
				});

			e.Result = created;
		}
		private void bgwOpen_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			this.End();

			if (e.Result is String)
			{
				MessageBox.Show(this, (string)e.Result, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Hand);
				this.Close();
			}
			else
			{
				DateTime created = (DateTime)e.Result;

				this.lblPayload.Text = String.Format("{0} - {1} 트윗", created.ToString("yyyy.MM.dd HH:mm"), this.m_totalTweets);

				ListViewItem item;
				this.lsvDate.Items.Clear();

				for (int i = 0; i < this.m_lstIndex.Count; i++)
				{
					item = new ListViewItem();
					item.Text = String.Format("{0:0000} {1:00}", this.m_lstIndex[i].year, this.m_lstIndex[i].month);
					item.SubItems.Add(this.m_lstIndex[i].count.ToString());
					this.lsvDate.Items.Add(item);
				}

				if (this.m_isLoadAll)
				{
					this.bgwLoadAll.RunWorkerAsync();
				}
				else
				{
					this.lsvDate.Items[0].Selected = true;
					this.bgwLoad.RunWorkerAsync(0);
				}
			}
		}

		private void bgwLoadAll_DoWork(object sender, DoWorkEventArgs e)
		{
			this.Running();

			string		path;
			string		body;
			int			i;
			int			j;
			int			k = 0;
			JsonArray	ja;
			
			for (i = 0; i < this.m_lstIndex.Count; i++)
			{
				if (this.Disposing || this.IsDisposed) return;

				path = Path.Combine(this.m_Path, this.m_lstIndex[i].path);
				if (!File.Exists(path))
				{
					e.Result = String.Format("{0} 파일이 존재하지 않습니다!", Path.GetFileName(path));
					return;
				}

				body = File.ReadAllText(path, Encoding.UTF8);
				body = body.Substring(body.IndexOf('['));
				body = body.Replace(@"\b", "");


				try
				{
					ja = new JsonArray(body);
					for (j = ja.Count - 1; j >= 0; j--)
					{
						if (this.Disposing || this.IsDisposed) return;

						k++;

						TweetInfo info = new TweetInfo(ja[j] as JsonObject);
						this.m_lstTweetAll.Add(info);

						this.m_prg_max = this.m_totalTweets;
						this.m_prg_val = k;
						this.m_prg_text = String.Format("트윗 불러오는중 : {0} / {1}", k, this.m_totalTweets);
					}
				}
				catch
				{
					e.Result = String.Format("{0} 파일이 손상되었습니다!", Path.GetFileName(path));
					return;
				}
			}

			this.m_lstTweet.Sort(delegate(TweetInfo a, TweetInfo b) { return a.date.CompareTo(b.date) * -1; });

			this.m_prg_max = 1;
			this.m_prg_val = 1;
			this.m_prg_text = "트윗 불러오기 완료";

			this.Wait();
		}
		private void bgwLoadAll_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			this.End();

			if (e.Result is String)
			{
				MessageBox.Show(this, (string)e.Result, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Hand);
				this.Close();
			}
			else
			{
				this.lsvDate.Items[0].Selected = true;
				this.bgwLoad.RunWorkerAsync(0);
			}
		}

		private void bgwLoad_DoWork(object sender, DoWorkEventArgs e)
		{
			GC.Collect();

			this.Running();

			TweetMonth	index = this.m_lstIndex[(int)e.Argument];
			TweetInfo	info;
			int			i;

			this.m_lstTweet.Clear();

			if (this.m_isLoadAll)
			{
				for (i = 0; i < this.m_lstTweetAll.Count; i++)
				{
					if (this.Disposing || this.IsDisposed) return;

					this.m_prg_max = this.m_lstTweetAll.Count;
					this.m_prg_val = i;
					this.m_prg_text = String.Format("{0} {1} 트윗 조회중 : {2} / {3}", index.year, index.month, i, this.m_lstTweetAll.Count);

					info = this.m_lstTweetAll[i];

					if (info.date.Year == index.year && info.date.Month == index.month)
						this.m_lstTweet.Add(info);
				}
			}
			else
			{
				// 해당하는 날자만 불러온다
				string path = Path.Combine(this.m_Path, index.path);

				if (!File.Exists(path))
				{
					e.Result = String.Format("{0} 파일이 존재하지 않습니다!", Path.GetFileName(path));
					return;
				}

				try
				{
					this.m_lstTweet.Clear();

					string body = File.ReadAllText(path, Encoding.UTF8);
					body = body.Substring(body.IndexOf('['));

					JsonArray ja = new JsonArray(body);
					for (i = 0; i < ja.Count; i++)
					{
						if (this.Disposing || this.IsDisposed) return;

						info = new TweetInfo(ja[i] as JsonObject);

						this.m_prg_max = index.count;
						this.m_prg_val = i;
						this.m_prg_text = String.Format("{0} {1} 트윗 조회중 : {2} / {3}", index.year, index.month, i, index.count);

						this.m_lstTweet.Add(info);
					}
				}
				catch
				{
					e.Result = String.Format("{0} 파일이 손상되었습니다!", Path.GetFileName(path));
					return;
				}
			}

			this.m_lstTweet.Sort(delegate(TweetInfo a, TweetInfo b) { return a.date.CompareTo(b.date) * -1; });

			this.m_prg_max = 1;
			this.m_prg_val = 1;
			this.m_prg_text = String.Format("{0} {1} 트윗 조회 완료", index.year, index.month);

			this.Wait();
		}
		private void bgwLoad_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
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

		private void bgwSearch_DoWork(object sender, DoWorkEventArgs e)
		{
			GC.Collect();

			this.Running();

			TweetSearch sear = new TweetSearch((string)e.Argument);

			int i, j;

			this.m_lstTweet.Clear();

			if (this.m_isLoadAll)
			{
				for (i = 0; i < this.m_lstTweetAll.Count; i++)
				{
					if (this.Disposing || this.IsDisposed) return;

					this.m_prg_max = this.m_totalTweets;
					this.m_prg_val = i;
					this.m_prg_text = String.Format("검색중 : {0} / {1}", i, this.m_totalTweets);

					if (sear.Check(this.m_lstTweetAll[i]))
						this.m_lstTweet.Add(this.m_lstTweetAll[i]);
				}
			}
			else
			{
				string path, body;
				JsonArray ja;

				int k = 0;

				for (i = 0; i < this.m_lstIndex.Count; i++)
				{
					if (this.Disposing || this.IsDisposed) return;

					path = Path.Combine(this.m_Path, this.m_lstIndex[i].path);
					if (!File.Exists(path))
					{
						e.Result = String.Format("{0} 파일이 존재하지 않습니다!", Path.GetFileName(path));
						return;
					}

					body = File.ReadAllText(path, Encoding.UTF8);
					body = body.Substring(body.IndexOf('['));
					body = body.Replace(@"\b", "");

					try
					{
						ja = new JsonArray(body);
						for (j = ja.Count - 1; j >= 0; j--)
						{
							if (this.Disposing || this.IsDisposed) return;

							k++;

							this.m_prg_max = this.m_totalTweets;
							this.m_prg_val = k;
							this.m_prg_text = String.Format("검색중 : {0} / {1}", k, this.m_totalTweets);

							TweetInfo info = new TweetInfo(ja[j] as JsonObject);

							if (sear.Check(info))
								this.m_lstTweet.Add(info);
						}
					}
					catch
					{
						e.Result = String.Format("{0} 파일이 손상되었습니다!", Path.GetFileName(path));
						return;
					}
				}
			}

			this.m_lstTweet.Sort(delegate(TweetInfo a, TweetInfo b) { return a.date.CompareTo(b.date) * -1; });

			this.m_prg_max = 1;
			this.m_prg_val = 1;
			this.m_prg_text = "검색 완료";

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

			this.lsvTweet.BeginUpdate();

			ListViewItem item;
			for (int i = 0; i < this.m_lstTweet.Count; i++)
			{
				item = new ListViewItem();
				item.Text = this.m_lstTweet[i].date.ToString("yyyy-MM-dd HH:mm:ss");
				item.Tag = i;
				item.SubItems.Add(this.m_lstTweet[i].username);
				item.SubItems.Add(this.m_lstTweet[i].text);
				item.StateImageIndex = this.m_lstTweet[i].isRt ? 0 : -1;

				this.lsvTweet.Items.Add(item);
			}

			this.lsvTweet.EndUpdate();

			this.lblCount.Text = String.Format("{0} 트윗", this.lsvTweet.Items.Count);
			this.DoResize();
		}

		int		m_prg_max	= 1;
		int		m_prg_val	= 1;
		string	m_prg_text	= null;
		private void bgwRefresh_DoWork(object sender, DoWorkEventArgs e)
		{
			while (this.m_isRunning && !this.Disposing && !this.IsDisposed)
			{
				try
				{
					this.Invoke(new dv(
						delegate()
						{
							this.prg.Value = (int)(this.m_prg_val * 150f / this.m_prg_max);
							this.lbl.Text = this.m_prg_text;
						}));
				}
				catch { }

				Thread.Sleep(50);
			}

			this.Invoke(new dv(
				delegate()
				{
					this.prg.Value = (int)(this.m_prg_val * 150f / this.m_prg_max);
					this.lbl.Text = this.m_prg_text;
				}));
		}
	}
}
