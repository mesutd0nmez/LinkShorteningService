using UrlShorteningService.Domain.DTOs;

namespace UrlShorteningService.Application.Interfaces
{
	public interface ILinkService
	{
        Task<IEnumerable<LinkDto>> GetAllLinks();
        Task<LinkDto> GetShortenedUrl(string perfectUrl);
        Task<LinkDto> GetPerfectUrl(string shortenedUrl);

        Task<LinkDto> AddLink(LinkDto recordDto);
        LinkDto UpdateLink(LinkDto recordDto);
        void DeleteLink(LinkDto recordDto);
    }
}

