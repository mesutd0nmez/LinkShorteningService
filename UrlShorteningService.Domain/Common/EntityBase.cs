using System;
namespace UrlShorteningService.Domain.Common
{
	public class EntityBase
	{
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public bool Deleted { get; set; }
        public DateTime? DeleteDate { get; set; }
    }
}

