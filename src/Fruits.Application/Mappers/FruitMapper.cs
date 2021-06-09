using AutoMapper;
using Fruits.Application.Mappers.Base;
using Fruits.Application.ViewModels;
using Fruits.Domain.Models;

namespace Fruits.Application.Mappers
{
    public class FruitMapper : AutoMapperBase
    {
        public FruitMapper(IMapperConfigurationExpression configuration) : base(configuration)
        { }

        public override void CreateMap()
        {
            MapperConfiguration.CreateMap<FruitViewModel, Fruit>();
        }
    }
}
