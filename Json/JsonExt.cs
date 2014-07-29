using System;
using ComputerBeacon.Json;

namespace TwitterArchiveViewer
{
	internal static class JsonExt
	{
		public static DateTime ToDateTime(string dateTimeString)
		{
			return DateTime.Parse(dateTimeString);
		}
		public static JsonObject GetJson(this JsonObject jsonObject, string key)
		{
			return (JsonObject)jsonObject[key];
		}
		public static string GetString(this JsonObject jsonObject, string key)
		{
			return Convert.ToString(jsonObject[key]);
		}
		public static byte GetByte(this JsonObject jsonObject, string key)
		{
			return Convert.ToByte(jsonObject[key]);
		}
		public static short GetShort(this JsonObject jsonObject, string key)
		{
			return Convert.ToInt16(jsonObject[key]);
		}
		public static int GetInt(this JsonObject jsonObject, string key)
		{
			return Convert.ToInt32(jsonObject[key]);
		}
		public static long GetLong(this JsonObject jsonObject, string key)
		{
			return Convert.ToInt64(jsonObject[key]);
		}
		public static DateTime GetDateTime(this JsonObject jsonObject, string key)
		{
			return JsonExt.ToDateTime((string)jsonObject[key]);
		}
	}
}
