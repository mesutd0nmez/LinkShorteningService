using System;
using UrlShorteningService.Domain.Common;

namespace UrlShorteningService.Domain.Entities
{
	public class Link : EntityBase
    {
		public string PerfectUrl { get; set; }
		public string ShortenedUrl { get; set; }
		public string Ip { get; set; }
	}
}

