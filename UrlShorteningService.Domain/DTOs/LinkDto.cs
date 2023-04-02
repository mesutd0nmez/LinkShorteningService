using System;
using UrlShorteningService.Domain.Common;

namespace UrlShorteningService.Domain.DTOs
{
	public class LinkDto : EntityBase
    {
        public string PerfectUrl { get; set; }
        public string ShortenedUrl { get; set; }
        public string Ip { get; set; }
    }
}

