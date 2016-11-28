using AutoMapper;
using Coolector.Common.Dto.Operations;
using Coolector.Services.Operations.Domain;

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