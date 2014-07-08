using System;
using System.Collections.Generic;
using ComputerBeacon.Json;

namespace TwitterArchiveViewer
{
	public class TweetInfo
	{
		public TweetInfo(JsonObject tw)
		{
			if (tw.ContainsKey("retweeted_status"))
			{
				this.isRt = true;

				JsonObject rs	= tw["retweeted_status"] as JsonObject;
				this.date		= tw.GetDateTime("created_at");
				this.id			= rs.GetLong("id");
				this.text		= rs.GetString("text");
				this.username	= rs.GetJson("user").GetString("screen_name");
			}
			else
			{
				this.isRt = false;

				this.date		= tw.GetDateTime("created_at");
				this.id			= tw.GetLong("id");
				this.text		= tw.GetString("text");
				this.username	= tw.GetJson("user").GetString("screen_name");
			}
			this.urls = null;

			// 원본 주소 얻기
			if (tw.ContainsKey("entities"))
			{
				List<string> lstUrls = new List<string>();
				string url;

				JsonObject en = tw.GetJson("entities");

				JsonArray ja;
				JsonObject jo;

				if (en.ContainsKey("media"))
				{
					ja = (JsonArray)en["media"];
					for (int i = 0; i < ja.Count; i++)
					{
						jo = ja[i] as JsonObject;
						if (jo != null)
						{
							url = jo.GetString("media_url_https");
							this.text = this.text.Replace(jo.GetString("url"), url);
							lstUrls.Add(String.Format("{0}:orig", url));
						}


					}
				}

				if (en.ContainsKey("urls"))
				{
					ja = (JsonArray)en["urls"];
					for (int i = 0; i < ja.Count; i++)
					{
						jo = ja[i] as JsonObject;
						if (jo != null)
						{
							url = jo.GetString("expanded_url");
							this.text = this.text.Replace(jo.GetString("url"), url);
							lstUrls.Add(url);
						}
					}
				}
				
				if (lstUrls.Count > 0)
					this.urls = lstUrls.ToArray();
			}

			this.text = this.text.Replace("&gt;", ">").Replace("&lt;", "<").Replace("&amp;", "&");
		}

		public DateTime	date;
		public string	text;
		public string	username;
		public bool		isRt;
		public long		id;
		public string[] urls;
	}
}
