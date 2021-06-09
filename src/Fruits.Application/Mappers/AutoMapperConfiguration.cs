using AutoMapper;
using Fruits.Application.ExtensionsMethods;

namespace Fruits.Application.Mappers
{
    public static class AutoMapperConfiguration
    {
        private static IMapper _mapper;

        public static IMapper GetMapper()
        {
            if (_mapper != null)
            {
                return _mapper;
            }

            var configuration = new MapperConfiguration(mapperConfiguration =>
            {
                mapperConfiguration.CreateMap<FruitMapper>();
                mapperConfiguration.CreateMap<StoreMapper>();
            });

            _mapper = configuration.CreateMapper();

            return _mapper;
        }
    }
}
