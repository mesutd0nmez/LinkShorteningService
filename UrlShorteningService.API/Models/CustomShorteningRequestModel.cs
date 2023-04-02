using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace UrlShorteningService.API.Models
{
	public class CustomShorteningRequestModel
	{
		[Required]
		[MaxLength(255)]
		public string PerfectUrl { get; set; }

        [Required]
        [MaxLength(6)]
        public string ShortenedHashPortion { get; set; }
	}
}

