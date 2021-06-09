using AutoMapper;
using Fruits.Application.Mappers;

namespace Fruits.Application.ExtensionsMethods
{
    public static class MapperModelToDtoExtensionsMethods
    {
        private static readonly IMapper Mapper = AutoMapperConfiguration.GetMapper();
        public static TDto MapModelAndDto<TDto>(this object entidade)
        {
            return Mapper.Map<TDto>(entidade);
        }
    }
}
