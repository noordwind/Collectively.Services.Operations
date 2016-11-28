using AutoMapper;
using Coolector.Common.Dto.Operations;
using Coolector.Services.Operations.Domain;
using Coolector.Services.Operations.Queries;
using Coolector.Services.Operations.Services;

namespace Coolector.Services.Operations.Modules
{
    public class OperationModule : ModuleBase
    {
        public OperationModule(IOperationService operationService, IMapper mapper) 
            : base(mapper, "operations")
        {
            Get("{requestId}", args => Fetch<GetOperation, Operation>
                (async x => await operationService.GetAsync(x.RequestId))
                .MapTo<OperationDto>()
                .HandleAsync());
        }
    }
}