using AutoMapper;
using Fruits.Application.Mappers.Base;
using Fruits.Application.ViewModels;
using Fruits.Domain.Models;

namespace Fruits.Application.Mappers
{
    public class StoreMapper : AutoMapperBase
    {
        public StoreMapper(IMapperConfigurationExpression configuration) : base(configuration)
        { }

        public override void CreateMap()
        {
            MapperConfiguration.CreateMap<StoreViewModel, Store>()
                .ForPath(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForPath(dest => dest.FruitId, opt => opt.MapFrom(src => src.FruitId))
                .ForPath(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForPath(dest => dest.TotalValue, opt => opt.MapFrom(src => src.TotalValue));
        }
    }
}
