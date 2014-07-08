using System;
using System.IO;
using System.Windows.Forms;

namespace TwitterArchiveViewer
{
	public partial class frmOpen : Form
	{
		public frmOpen()
		{
			InitializeComponent();
		}

		private void txtOpen_Click(object sender, EventArgs e)
		{
			if (!String.IsNullOrEmpty(this.txtPath.Text))
				this.fbd.SelectedPath = this.txtPath.Text;

			if (this.fbd.ShowDialog() == DialogResult.OK)
				if (!this.CheckArchive(this.fbd.SelectedPath))
					MessageBox.Show(this, "올바른 아카이브 디렉토리가 아닙니다!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Hand);
				else
					this.txtPath.Text = this.fbd.SelectedPath;
		}

		private void btnOpen_Click(object sender, EventArgs e)
		{
			// 디렉토리 체크
			if (!this.CheckArchive(this.txtPath.Text))
			{
				MessageBox.Show(this, "올바른 아카이브 디렉토리가 아닙니다!", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Hand);
				this.txtPath.Focus();
				return;
			}

			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private bool CheckArchive(string path)
		{
			if (!Directory.Exists(path))
				return false;

			if (!Directory.Exists(Path.Combine(path, "data/js/tweets")))
				return false;

			if (!File.Exists(Path.Combine(path, "data/js/tweet_index.js")))
				return false;

			if (!File.Exists(Path.Combine(path, "data/js/payload_details.js")))
				return false;
			
			return true;
		}
	}
}
