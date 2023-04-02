using System;
using System.ComponentModel.DataAnnotations;

namespace UrlShorteningService.API.Models
{
	public class ShorteningRequestModel
	{
        [Required]
        [MaxLength(255)]
        public string PerfectUrl { get; set; }
    }
}

