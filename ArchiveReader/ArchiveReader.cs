using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Threading;
using ComputerBeacon.Json;

namespace TwitterArchiveViewer
{
	[Serializable]
	[DebuggerDisplay("Count = {Count}")]
	internal class ArchiveReader : IList<TweetInfo>
	{
		[Serializable]
		private class ArchiveEnumerator : IEnumerator<TweetInfo>, IDisposable
		{
			readonly ArchiveReader m_reader;
			int m_nowIndex = 0;
			TweetInfo m_current;

			public ArchiveEnumerator(ArchiveReader reader)
			{
				this.m_reader = reader;
				this.m_nowIndex = 0;
			}

			public void Dispose()
			{
			}

			public TweetInfo Current
			{
				get { return this.m_current; }
			}

			object IEnumerator.Current
			{
				get { return this.m_current; }
			}

			bool IEnumerator.MoveNext()
			{
				this.m_nowIndex++;
				this.m_current = this.m_reader[m_nowIndex];

				return (this.m_nowIndex < this.m_reader.Count);
			}

			void IEnumerator.Reset()
			{
				this.m_nowIndex = 0;
			}
		}

		//////////////////////////////////////////////////////////////////////////

		private	static ArchiveReader m_currentArchive;
		public	static ArchiveReader Current
		{
			get { return ArchiveReader.m_currentArchive; }
		}

		public static void SetArchive(string path, bool loadAll, bool useCache)
		{
			ArchiveReader.m_currentArchive = new ArchiveReader(path, loadAll, useCache);
		}

		//////////////////////////////////////////////////////////////////////////

		private ArchiveReader(string path, bool loadAll, bool useCache)
		{
			this.m_path		= path;
			this.m_loadAll	= loadAll;
			this.m_useCache	= useCache;
		}

		private string	m_path;
		private bool	m_loadAll;
		private bool	m_useCache;

		private int m_nowMonthIndex;

		private int m_index = 0; // For LoadAll
		private int m_count = 1;

		private DateTime m_created;

		private int[] m_IndexStart;
		private int[] m_IndexEnd;
		private MonthlyInfo[] m_monthlyInfos;

		private List<TweetInfo>	m_lstTweets = new List<TweetInfo>();

		public int Count
		{
			get { return this.m_count; }
		}
		public int Index
		{
			get { return this.m_index; }
			set { this.m_index = value; }
		}
		public DateTime CreatedAt
		{
			get { return this.m_created; }
		}
		public MonthlyInfo[] MonthlyInfos
		{
			get { return this.m_monthlyInfos; }
		}
		public IList<int> IndexStart
		{
			get { return this.m_IndexStart; }
		}
		public IList<int> IndexEnd
		{
			get { return this.m_IndexEnd; }
		}

		public void Load()
		{
			string	path;
			string	body;

			JsonArray	ja;
			JsonObject	jo;

			int i;

			List<int>			lstIndexStart	= new List<int>();
			List<int>			lstIndexEnd		= new List<int>();
			List<MonthlyInfo>	lstMonth		= new List<MonthlyInfo>();

			#region Load Payload
			path = Path.Combine(this.m_path, "data/js/payload_details.js");
			if (!File.Exists(path))
				throw new Exception("payload_details.js 파일이 존재하지 않습니다");

			try
			{
				body = File.ReadAllText(path, Encoding.UTF8);
				body = body.Substring(body.IndexOf('{'));
				jo = new JsonObject(body);
				//this.m_count = jo.GetInt("tweets");
				this.m_created = jo.GetDateTime("created_at");
			}
			catch
			{
				throw new Exception("payload_details.js 파일이 손상되었습니다!");
			}
			#endregion

			#region Tweet Index
			path = Path.Combine(this.m_path, "data/js/tweet_index.js");
			if (!File.Exists(path))
				throw new Exception("tweet_index.js 파일이 존재하지 않습니다");

			try
			{
				body = File.ReadAllText(path, Encoding.UTF8);
				body = body.Substring(body.IndexOf('['));

				ja = new JsonArray(body);
				for (i = 0; i < ja.Count; i++)
					lstMonth.Add(new MonthlyInfo(ja[i] as JsonObject));
			}
			catch
			{
				throw new Exception("tweet_index.js 파일이 손상되었습니다");
			}

			lstMonth.Sort(
				delegate(MonthlyInfo a, MonthlyInfo b)
				{
					int aa = a.year * 100 + a.month;
					int bb = b.year * 100 + b.month;
					return aa.CompareTo(bb);
				});

			int lastIndex = 0;
			for (i = 0; i < lstMonth.Count; i++)
			{
				lstIndexStart.Add(lastIndex);

				lastIndex += lstMonth[i].count;
				lstIndexEnd.Add(lastIndex);
			}

			this.m_IndexStart	= lstIndexStart.ToArray();
			this.m_IndexEnd		= lstIndexEnd.ToArray();
			this.m_monthlyInfos	= lstMonth.ToArray();

			this.m_count = lastIndex;
			#endregion

			#region Load All Tweets
			if (this.m_loadAll)
			{
				this.m_index = 0;
				for (i = 0; i < this.m_monthlyInfos.Length; i++)
					this.LoadIndex(i, false);

				this.m_lstTweets.Sort(delegate(TweetInfo a, TweetInfo b) { return a.date.CompareTo(b.date); });
			}
			else
			{
				this.m_lstTweets.Clear();
				this.m_nowMonthIndex = -1;
			}
			#endregion

		}
		
		private void LoadIndex(int index)
		{
			this.LoadIndex(index, true);
		}
		private void LoadIndex(int index, bool sort)
		{
			MonthlyInfo minfo = this.m_monthlyInfos[index];

			if (this.CacheLoad(minfo.year, minfo.month))
			{
				this.m_nowMonthIndex = index;
			}
			else
			{
				string path, body;

				path = Path.Combine(this.m_path, minfo.path);
				if (!File.Exists(path))
					throw new Exception(String.Format("{0} 파일이 존재하지 않습니다!", Path.GetFileName(path)));

				body = File.ReadAllText(path, Encoding.UTF8);
				body = body.Substring(body.IndexOf('['));
				body = body.Replace(@"\b", "");

				int listStart = this.m_lstTweets.Count;
				int listCount = 0;
				
				try
				{
					JsonArray ja = new JsonArray(body);
					for (int i = ja.Count - 1; i >= 0; i--)
					{
						this.m_index++;
						listCount++;

						this.m_lstTweets.Add(new TweetInfo(ja[i] as JsonObject));
					}

					if (sort)
						this.m_lstTweets.Sort(delegate(TweetInfo a, TweetInfo b) { return a.date.CompareTo(b.date); });

					this.CacheSave(minfo.year, minfo.month, listStart, listCount);

					this.m_nowMonthIndex = index;
				}
				catch
				{
					throw new Exception(String.Format("{0} 파일이 손상되었습니다!", Path.GetFileName(path)));
				}
			}
		}

		private object m_sync = new object();
		public TweetInfo this[int index]
		{
			get
			{
				lock (m_sync)
				{
					if (this.m_loadAll)
					{
						return this.m_lstTweets[index];
					}
					else
					{
						// 어느 인덱스인지 확인한다.
						int monthIndex;
						for (monthIndex = 0; monthIndex < this.m_IndexEnd.Length; monthIndex++)
							if (index < this.m_IndexEnd[monthIndex])
								break;

						if (this.m_nowMonthIndex != monthIndex)
						{
							this.m_lstTweets.Clear();
							this.LoadIndex(monthIndex);
						}

						return this.m_lstTweets[index - this.m_IndexStart[monthIndex]];
					}
				}
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		public IEnumerator<TweetInfo> GetEnumerator()
		{
			return new ArchiveEnumerator(this);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return new ArchiveEnumerator(this);
		}

		public void ForEach(Action<TweetInfo> action)
		{
			if (action != null)
				for (int i = 0 ; i < this.m_count; i++)
					action.Invoke(this[i]);
		}

		public bool IsReadOnly
		{
			get { return true; }
		}

		public void Add(TweetInfo item)
		{
			throw new NotSupportedException();
		}

		public void Clear()
		{
			throw new NotSupportedException();
		}

		public bool Contains(TweetInfo item)
		{
			throw new NotSupportedException();
		}

		public void CopyTo(TweetInfo[] array, int arrayIndex)
		{
			throw new NotSupportedException();
		}

		public void Insert(int index, TweetInfo item)
		{
			throw new NotSupportedException();
		}

		public bool Remove(TweetInfo item)
		{
			throw new NotSupportedException();
		}

		public void RemoveAt(int index)
		{
			throw new NotSupportedException();
		}

		public int IndexOf(TweetInfo item)
		{
			throw new NotSupportedException();
		}

		private byte CacheVerKey = 0;
		private void CacheSave(short year, byte month, int offset, int length)
		{
			if (!this.m_useCache) return;

			TweetInfo info;

			string path = Path.Combine(this.m_path, ".cache");
			Directory.CreateDirectory(path);

			path = Path.Combine(path, String.Format("{0:0000}{1:00}", year, month));

			int i;

			length = offset + length;

			Stream stm = null;
			BinaryWriter bw = null;

			try
			{
				stm = new FileStream(path, FileMode.Create, FileAccess.Write);
				stm = new BufferedStream(stm, 4096);
				bw = new BinaryWriter(stm, Encoding.UTF8);
				bw.Write(this.CacheVerKey);

				for (int index = offset; index < length; index++)
				{
					info = this.m_lstTweets[index];

					bw.Write((byte)1);
					bw.Write(info.id);

					bw.Write((byte)2);
					bw.Write(info.date.ToBinary());

					if (info.source != null)
					{
						bw.Write((byte)3);
						bw.Write(info.source);
					}

					if (info.username != null)
					{
						bw.Write((byte)4);
						bw.Write(info.username);
					}

					if (info.mentionTo != null)
					{
						bw.Write((byte)5);
						bw.Write(info.mentionTo);
					}

					if (info.text != null)
					{
						bw.Write((byte)6);
						bw.Write(info.text);
					}

					if (info.urls != null)
					{
						for (i = 0; i < info.urls.Length; i++)
						{
							bw.Write((byte)7);
							bw.Write(info.urls[i]);
						}
					}

					bw.Write((byte)8);
					bw.Write(info.isRt);

					bw.Write((byte)0);
				}

				bw.Flush();

				stm.Dispose();
			}
			catch
			{
				if (bw != null)
					stm.Dispose();

				File.Delete(path);
			}
		}

		private bool CacheLoad(short year, byte month)
		{
			if (!this.m_useCache) return false;

			TweetInfo info;

			string path = Path.Combine(this.m_path, String.Format(".cache/{0:0000}{1:00}", year, month));

			if (!File.Exists(path))
				return false;

			int key;

			List<string> urls = new List<string>();

			int startcount = this.m_lstTweets.Count;
			int startindex = this.m_index;

			bool b;

			Stream stm = null;
			BinaryReader br = null;

			try
			{
				stm = new FileStream(path, FileMode.Open, FileAccess.Read);
				stm = new BufferedStream(stm, 4096);
				br = new BinaryReader(stm, Encoding.UTF8);

				key = br.ReadByte();

				if (key != this.CacheVerKey)
				{
					br.BaseStream.Close();
					br.BaseStream.Dispose();
					throw new Exception();
				}

				while (br.BaseStream.Position < br.BaseStream.Length)
				{
					info = new TweetInfo();
					urls.Clear();

					b = false;
					while ((key = br.ReadByte()) > 0)
					{
						switch (key)
						{
							case 1:
								info.id = br.ReadInt64();
								b = true;
								break;
							case 2:
								info.date = DateTime.FromBinary(br.ReadInt64());
								b = true;
								break;
							case 3:
								info.source = br.ReadString();
								b = true;
								break;
							case 4:
								info.username = br.ReadString();
								b = true;
								break;
							case 5:
								info.mentionTo = br.ReadString();
								b = true;
								break;
							case 6:
								info.text = br.ReadString();
								b = true;
								break;
							case 7:
								urls.Add(br.ReadString());
								b = true;
								break;
							case 8:
								info.isRt = br.ReadBoolean();
								b = true;
								break;
						}
					}

					if (b)
					{
						if (urls.Count > 0)
							info.urls = urls.ToArray();

						this.m_lstTweets.Add(info);
						this.m_index++;
					}
				}

				stm.Dispose();
			}
			catch
			{
				if (stm != null)
					stm.Dispose();

				File.Delete(path);

				this.m_lstTweets.RemoveRange(startindex, this.m_lstTweets.Count - startcount);

				this.m_index = startindex;
				return false;
			}

			return true;
		}
	}
}
