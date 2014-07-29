using System;
using System.Diagnostics;
using ComputerBeacon.Json;

namespace TwitterArchiveViewer
{
	[Serializable]
	internal class MonthlyInfo
	{
		internal MonthlyInfo(JsonObject jo)
		{
			this.year	= jo.GetShort("year");
			this.month = jo.GetByte("month");
			this.count = jo.GetInt("tweet_count");
			this.path	= jo.GetString("file_name");
		}

		public short	year;
		public byte		month;
		public int		count;
		public string	path;

		public override string ToString()
		{
			return String.Format("{0:0000}-{1:00} : {2}", year, month, count);
		}
	}
}
