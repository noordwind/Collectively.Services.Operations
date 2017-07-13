using AutoMapper;
using Collectively.Services.Operations.Domain;
using Collectively.Services.Operations.Dto;

namespace Collectively.Services.Operations.Framework
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