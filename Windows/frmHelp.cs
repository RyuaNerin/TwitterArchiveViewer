using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace TwitterArchiveViewer
{
	internal partial class frmHelp : Form
	{
		/*
			A B C
				> A B C 를 포함하는 트윗
			*A *B *C
				> A B C 중에 하나라도 포함하는 트윗
			"AB C"
				> "AB C" 가 정확하게 일치하는 트윗
			-A
				> A 를 반드시 제외한 트윗
			~RT
				> 리트윗한 트윗 제외
			~MY
				> 내 트윗 제외
			~2014.06.29
				> 2014.06.29 이후의 트윗
			~~2014.06.30
				> 2014.06.30 이전의 트윗
			~2014.06.29~2014.06.30
				> 2014.06.29 ~ 30 의 트윗
			*/

		public frmHelp()
		{
			InitializeComponent();

			Dictionary<string, string> dic = new Dictionary<string, string>();

			dic.Add("A B C", "모든 단어 포함");
			dic.Add("\"A B C\"", "문구 정확히 포함");
			dic.Add("*A *B *C", "적어도 하나의 단어 포함");
			dic.Add("-A -B -C", "단어 제외");
			dic.Add("~RT", "리트윗 제외");
			dic.Add("~MY", "내 트윗 제외");
			dic.Add("~2012.01.01", "해당 날자 이후 트윗");
			dic.Add("~~2012.01.01", "해당 날자 이전 트윗");
			dic.Add("~2012.01.01~2012.01.01", "A ~ B 에 작성된 트윗");
			dic.Add("/정규식/", "정규식 (regular Expression)");
			dic.Add("\\~ \\\" \\* \\-", "~ \" * -");

			this.tlp.RowCount = dic.Count;

			int r = 0;
			foreach (KeyValuePair<string, string> st in dic)
			{
				this.tlp.RowStyles.Add(new RowStyle(SizeType.Absolute, 16));
				this.AddLable(st.Key, r, 0);
				this.AddLable(st.Value, r, 1);
				r++;
			}

			this.tlp.Height = this.tlp.RowCount * 16;
			this.btnClose.Top = 12 + this.tlp.Height + 6;
			this.Height = 12 + this.tlp.Height + 6 + this.btnClose.Height + 12;
		}

		private void AddLable(string text, int row, int column)
		{
			Label l = new Label();
			l.Dock = DockStyle.Fill;
			l.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			l.Text = text;
			l.Visible = true;

			this.tlp.Controls.Add(l);
			this.tlp.SetCellPosition(l, new TableLayoutPanelCellPosition(column, row));
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void frmHelp_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.Dispose();
		}
	}
}
