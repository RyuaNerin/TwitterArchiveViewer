using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TwitterArchiveViewer
{
	internal class Utils
	{
		public static string ToSize(int size)
		{
			if (size < 1000)
				return String.Format("{0:##0.0} B", size);

			if (size < 1024000)
				return String.Format("{0:##0.0} KiB", size / 1024.0d);

			if (size < 1048576000)
				return String.Format("{0:##0.0} MiB", size / 1048576.0d);

			return String.Format("{0:##0.0} GiB", size / 1073741824.0d);
		}
	}
}
