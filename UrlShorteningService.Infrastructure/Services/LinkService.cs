using UrlShorteningService.Domain.Entities;
using UrlShorteningService.Application.Interfaces;
using UrlShorteningService.Persistence.Repositories;
using UrlShorteningService.Domain.DTOs;
using UrlShorteningService.Infrastructure.Mapper;
using Microsoft.EntityFrameworkCore;


namespace UrlShorteningService.Infrastructure.Services
{
    public class LinkService : ILinkService
    {
        private readonly IGenericRepository<Link> _repository;

        public LinkService(IGenericRepository<Link> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<IEnumerable<LinkDto>> GetAllLinks()
        {
            var records = await _repository.Where(x => 1 == 1).AsNoTracking().ToListAsync();
            return ObjectMapper.GetMapper.Map<IEnumerable<LinkDto>>(records);
        }

        public async Task<LinkDto> GetShortenedUrl(string perfectUrl)
        {
            var record = await _repository.Where(x => x.PerfectUrl == perfectUrl).AsNoTracking().FirstOrDefaultAsync();
            return ObjectMapper.GetMapper.Map<LinkDto>(record);
        }

        public async Task<LinkDto> GetPerfectUrl(string shortenedUrl)
        {
            var record = await _repository.Where(x => x.ShortenedUrl == shortenedUrl).AsNoTracking().FirstOrDefaultAsync();
            return ObjectMapper.GetMapper.Map<LinkDto>(record);
        }

        public async Task<LinkDto> AddLink(LinkDto recordDto)
        {
            var record = ObjectMapper.GetMapper.Map<Link>(recordDto);
            record = await _repository.AddAsync(record);
            return ObjectMapper.GetMapper.Map<LinkDto>(record);
        }

        public LinkDto UpdateLink(LinkDto recordDto)
        {
            var record = ObjectMapper.GetMapper.Map<Link>(recordDto);
            record = _repository.Update(record);
            return ObjectMapper.GetMapper.Map<LinkDto>(record);
        }

        public void DeleteLink(LinkDto recordDto)
        {
            var record = ObjectMapper.GetMapper.Map<Link>(recordDto);
            _repository.Delete(record);
        }
    }
}

