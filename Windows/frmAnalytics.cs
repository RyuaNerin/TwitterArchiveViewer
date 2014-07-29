using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TwitterArchiveViewer
{
	internal partial class frmAnalytics : Form
	{
		int[] counts;
		int[,] tweets;
		int[,] ranks;

		public frmAnalytics()
		{
			InitializeComponent();

			string days = "일월화수목금토";

			this.tbcDay.TabPages.Add("종합");

			for (int i = 1; i < 8; i++)
				this.tbcDay.TabPages.Add(days.Substring(i - 1, 1));
		}

		public void AddResult()
		{
			this.lsvResult.Items.Add("");
		}
		public void AddResult(string key, string value)
		{
			ListViewItem item = new ListViewItem(key);
			item.SubItems.Add(value);

			this.lsvResult.Items.Add(item);
		}

		public void AddTimezone(int[] counts, int[,] tweets, int[,] ranks)
		{
			this.counts = counts;
			this.tweets = tweets;
			this.ranks = ranks;

			this.tbcDay_SelectedIndexChanged(null, null);
		}

		int nowIndex = -1;
		private void tbcDay_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (nowIndex == this.tbcDay.SelectedIndex) return;

			int day = this.nowIndex = this.tbcDay.SelectedIndex;

			ListViewItem item;

			this.lsvTimeZone.BeginUpdate();

			this.lsvTimeZone.Items.Clear();

			item = new ListViewItem("전체");
			item.SubItems.Add("");
			item.SubItems.Add(String.Format("{0:##0.00} %", (this.counts[day] * 100.0d / this.counts[0])));
			item.SubItems.Add(this.counts[day].ToString());
			this.lsvTimeZone.Items.Add(item);

			for (int i = 0; i < 24; i++)
			{
				item = new ListViewItem(String.Format("{0:00}:00 ~ {1:00}:00", i, i + 1));
				item.SubItems.Add(String.Format("{0:##0.00} %", (this.tweets[day, i] * 100.0d / this.counts[day])));
				item.SubItems.Add(String.Format("{0:##0.00} %", (this.tweets[day, i] * 100.0d / this.counts[0])));
				item.SubItems.Add(this.tweets[day, i].ToString());

				if (ranks[day, 0] == i)
				{
					item.ForeColor = Color.DarkRed;
					item.Font = new Font(this.Font, FontStyle.Bold);
				}
				else if (ranks[day, 1] == i)
				{
					item.ForeColor = Color.Red;
					item.Font = new Font(this.Font, FontStyle.Bold);
				}
				else if (ranks[day, 2] == i)
				{
					item.ForeColor = Color.OrangeRed;
					item.Font = new Font(this.Font, FontStyle.Bold);
				}

				this.lsvTimeZone.Items.Add(item);
			}

			this.lsvTimeZone.EndUpdate();
		}

		public void AddSource(string client, double persent, int count)
		{
			ListViewItem item = new ListViewItem(client);
			item.SubItems.Add(String.Format("{0:##0.000} %", persent));
			item.SubItems.Add(count.ToString());

			this.lsvSource.Items.Add(item);
		}

		public void AddMention(string client, double persent, int count)
		{
			ListViewItem item = new ListViewItem(client);
			item.SubItems.Add(String.Format("{0:##0.000} %", persent));
			item.SubItems.Add(count.ToString());

			this.lsvMention.Items.Add(item);
		}

		public void AddKeyword(string client, double persent, int count)
		{
			ListViewItem item = new ListViewItem(client);
			item.SubItems.Add(String.Format("{0:##0.000} %", persent));
			item.SubItems.Add(count.ToString());

			this.lsvKeyword.Items.Add(item);
		}
	}
}
