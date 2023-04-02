using System;
using AutoMapper;
using UrlShorteningService.Domain.Entities;
using UrlShorteningService.Domain.DTOs;

namespace UrlShorteningService.Infrastructure.Mapper
{
	public class Mapping : Profile
    {
		public Mapping()
		{
            CreateMap<LinkDto, Link>().ReverseMap();
        }
	}
}

