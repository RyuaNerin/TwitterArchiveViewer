using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace TwitterArchiveViewer
{
	internal class TweetSearch
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

		private List<string>	kAnd	= new List<string>();
		private List<string>	kOr		= new List<string>();
		private List<string>	kExc	= new List<string>();
		private List<Regex>		kReg	= new List<Regex>();

		private bool excludeRT = false;
		private bool excludeMy = false;

		private DateTime dateFrom	= DateTime.MinValue;
		private DateTime dateTo		= DateTime.MaxValue;

		static Regex regFrom	= new Regex("~(20[0-9]{2}\\.[0-9]{2}\\.[0-9]{2})", RegexOptions.Compiled);
		static Regex regTo		= new Regex("~~(20[0-9]{2}\\.[0-9]{2}\\.[0-9]{2})", RegexOptions.Compiled);
		static Regex regFromTo	= new Regex("~(20[0-9]{2}\\.[0-9]{2}\\.[0-9]{2})~([0-9]{4}\\.[0-9]{2}\\.[0-9]{2})", RegexOptions.Compiled);

		public TweetSearch(string keyword)
		{
			keyword = String.Format("{0} ", keyword);

			StringBuilder sb = new StringBuilder();
			byte status = 0;
			string s;
			bool escape = false;
			bool regex = false;

			foreach (char c in keyword)
			{
				if (escape)
				{
					escape = false;
					sb.Append(c);
				}
				else if (regex)
				{
					switch (c)
					{
						case '/':
							regex = false;
							kReg.Add(new Regex(sb.ToString(), RegexOptions.Compiled | RegexOptions.Multiline));
							sb.Remove(0, sb.Length);
							break;

						case '\\':
							escape = true;
							break;

						default:
							sb.Append(c);
							break;
					}
				}
				else
				{
					switch (c)
					{
						case '\\':
							escape = true;
							break;

						case '*':
							status = 1;
							break;

						case '\"':
							if (status != 2)
							{
								status = 2;
							}
							else
							{
								this.kAnd.Add(sb.ToString());
								sb.Remove(0, sb.Length);
							}
							break;

						case '-':
							status = 3;
							break;

						case '~':
							if (status == 4)
								sb.Append(c);
							else
								status = 4;
							break;

						case '/':
							regex = true;
							break;

						case ' ':
							switch (status)
							{
								case 0:
									if (sb.Length > 0)
									{
										this.kAnd.Add(sb.ToString());

										sb.Remove(0, sb.Length);
									}
									break;

								case 1:
									if (sb.Length > 0)
									{
										this.kOr.Add(sb.ToString());

										sb.Remove(0, sb.Length);
									}
									break;

								case 2:
									sb.Append(c);
									break;

								case 3:
									if (sb.Length > 0)
									{
										this.kExc.Add(sb.ToString());

										sb.Remove(0, sb.Length);
									}
									break;

								case 5:
									sb.Append(c);
									break;

								case 4:
									if (sb.Length > 0)
									{
										s = sb.ToString();
										sb.Remove(0, sb.Length);

										if (s == "RT")
										{
											this.excludeRT = true;
										}
										else if (s == "MY")
										{
											this.excludeRT = true;
										}
										else
										{
											Match m = regFromTo.Match(s);
											if (m.Success)
											{
												this.dateFrom	= DateTime.Parse(m.Groups[1].Value);
												this.dateTo		= DateTime.Parse(m.Groups[2].Value);
											}
											else
											{
												m = regFrom.Match(s);
												if (m.Success)
												{
													this.dateFrom = DateTime.Parse(m.Groups[1].Value);
												}
												else
												{
													m = regTo.Match(s);
													if (m.Success)
													{
														this.dateTo = DateTime.Parse(m.Groups[1].Value);
													}
												}
											}
										}
									}

									break;
							}

							status = 0;
							break;

						default:
							sb.Append(c);
							break;
					}
				}
			}
		}

		public bool Check(TweetInfo info)
		{
			int i;

			if (this.kAnd.Count > 0)
				for (i = 0; i < this.kAnd.Count; i++)
						if (info.text.IndexOf(this.kAnd[i]) == -1)
							return false;

			if (this.kOr.Count > 0)
			{
				bool ok = false;
				for (i = 0; i < this.kOr.Count; i++)
				{
					if (info.text.IndexOf(this.kOr[i]) >= 0)
					{
						ok = true;
						break;
					}
				}
				if (!ok) return false;
			}

			if (this.kExc.Count > 0)
				for (i = 0; i < this.kExc.Count; i++)
					if (info.text.IndexOf(this.kExc[i]) >= 0)
						return false;

			if (this.kReg.Count > 0)
				for (i = 0; i < this.kReg.Count; i++)
					if (!this.kReg[i].IsMatch(info.text))
						return false;

			if (info.isRt && this.excludeRT)
				return false;

			if (!info.isRt && this.excludeMy)
				return false;

			DateTime d = info.date.Date;

			return (this.dateFrom <= d && d <= this.dateTo);

		}
	}
}
