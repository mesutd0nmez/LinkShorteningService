using AutoMapper;
using AutoMapper.Internal;

namespace UrlShorteningService.Infrastructure.Mapper
{
	public static class ObjectMapper
    {
        private static readonly Lazy<IMapper> _mapper = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cgf => {
                cgf.Internal().MethodMappingEnabled = false;
                cgf.AddProfile<Mapping>();
            });
            return config.CreateMapper();
        });

        public static IMapper GetMapper => _mapper.Value;
    }
}

