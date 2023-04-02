using System;
using System.Text;
using System.Web;

namespace UrlShorteningService.API.Configuations
{
	public static class UrlExtensions
	{
		public static bool IsWellFormedUri(this string url)
		{
			return Uri.IsWellFormedUriString(url, UriKind.Absolute);
        }

        public static string ToUrlEncode(this string value)
        {
            return HttpUtility.UrlEncode(value.ToLower(), Encoding.UTF8);
        }
        public static string ToUrlDecode(this string value)
        {
            return HttpUtility.UrlDecode(value.ToLower(), Encoding.UTF8);
        }

        public static string GenerateShortCode(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }
    }
}

