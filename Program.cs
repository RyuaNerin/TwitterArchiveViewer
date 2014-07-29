using System;
using System.Windows.Forms;

namespace TwitterArchiveViewer
{
	internal static class Program
	{
		public const string ConsumerKey = "";
		public const string ConsumerSec = "";

		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new frmMain());
		}
	}
}
