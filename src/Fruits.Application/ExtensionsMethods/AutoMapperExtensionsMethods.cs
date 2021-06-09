using AutoMapper;
using Fruits.Application.Mappers.Base;
using System;

namespace Fruits.Application.ExtensionsMethods
{
    public static class AutoMapperExtensionsMethods
    {
        public static void CreateMap<T>(this IMapperConfigurationExpression configuration) where T : AutoMapperBase
        {
            var tipoAutoMapper = typeof(T);
            var magicMapper = Activator.CreateInstance(tipoAutoMapper, configuration) as T;
            magicMapper.CreateMap();
        }
    }
}
