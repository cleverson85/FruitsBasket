using AutoMapper;

namespace Fruits.Application.Mappers.Base
{
    public abstract class AutoMapperBase
    {
        public IMapperConfigurationExpression MapperConfiguration { get; set; }
        protected AutoMapperBase(IMapperConfigurationExpression configuration)
        {
            MapperConfiguration = configuration;
        }
        public abstract void CreateMap();

    }
}
