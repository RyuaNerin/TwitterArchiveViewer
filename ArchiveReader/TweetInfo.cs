using System;
using System.Collections.Generic;
using System.Diagnostics;
using ComputerBeacon.Json;

namespace TwitterArchiveViewer
{
	[Serializable]
	internal class TweetInfo
	{
		private static readonly string[] autoSource =
		{
			"twittbot.net",
			"SaucerInfo",
			"TweetMag1c for Android",
			"REFLEC BEAT colette AC",
			"このまま眠りつづけて死ぬ",
			"ツイート数カウントくん",
			"占ぃったー",
			"うんこはにがくてうまい",
			"ツイ廃あらーと",
			"なるほどコカインマンじゃねーの",
			"リプライ数チェッカ", 
			"ReflecInfo",
			"Linkis.com"
		};
		public static bool isAutoSource(string source)
		{
			int i;

			for (i = 0; i < TweetInfo.autoSource.Length; i++)
				if (source.IndexOf(TweetInfo.autoSource[i]) >= 0)
					return true;
			return false;
		}


		private static readonly string[] imageUrls =
		{
			"pbs.twimg.com/",	"p.twipple.jp/",	"lockerz.com/s/",	"twitrpix.com/",	"img.ly/",
			"pikchur.com/",		"pk.gd/",			"grab.by/",			"via.me/",			"puu.sh/",
			"pckles.com/",		"twitpic.com/",
		};
		public static bool IsImageUrl(string url)
		{
			int i;

			for (i = 0; i < TweetInfo.imageUrls.Length; i++)
				if (url.IndexOf(TweetInfo.imageUrls[i]) >= 0)
					return true;
			return false;
		}
		public static bool IsImageUrl(string[] urls)
		{
			if (urls == null)
				return false;

			int i, j;

			for (i = 0; i < urls.Length; i++)
				for (j = 0; j < TweetInfo.imageUrls.Length; j++)
					if (urls[i].IndexOf(TweetInfo.imageUrls[j]) >= 0)
						return true;

			return false;
		}

		public DateTime	date;
		public string	source;
		public string	username;
		public string	mentionTo;

		public string	text;
		public bool		isRt;
		public long		id;
		public string[] urls;

		public override string ToString()
		{
			return String.Format("{0} {1} : {2}", this.date.ToString("yyyy-MM-dd HH:mm:ss"), this.username, this.text);
		}

		internal TweetInfo()
		{
		}
		internal TweetInfo(JsonObject tw)
		{
			int i;

			if (tw.ContainsKey("retweeted_status"))
			{
				this.isRt = true;

				JsonObject rs	= tw["retweeted_status"] as JsonObject;
				this.date		= tw.GetDateTime("created_at");
				this.id			= rs.GetLong("id");
				this.text		= rs.GetString("text");
				this.mentionTo	= null;
				this.username	= rs.GetJson("user").GetString("screen_name");
			}
			else
			{
				this.isRt = false;

				this.date		= tw.GetDateTime("created_at");
				this.id			= tw.GetLong("id");
				this.text		= tw.GetString("text");
				this.username	= tw.GetJson("user").GetString("screen_name");

				if (tw.ContainsKey("in_reply_to_screen_name"))
					this.mentionTo	= tw.GetString("in_reply_to_screen_name");
				else
					this.mentionTo	= null;
			}
			this.urls = null;

			string client;
			client = tw.GetString("source");
			if (client.IndexOf('>') >= 0)
			{
				client = client.Substring(client.IndexOf('>') + 1);
				client = client.Substring(0, client.IndexOf('<'));
			}

			this.source = client;

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
					for (i = 0; i < ja.Count; i++)
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
					for (i = 0; i < ja.Count; i++)
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
	}
}
