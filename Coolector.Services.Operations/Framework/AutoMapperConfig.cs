using AutoMapper;
using Coolector.Services.Operations.Domain;
using Coolector.Services.Operations.Shared.Dto;

namespace Coolector.Services.Operations.Framework
{
    public class AutoMapperConfig
    {
        public static IMapper InitializeMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Operation, OperationDto>();
            });

            return config.CreateMapper();
        }
    }
}