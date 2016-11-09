using Coolector.Services.Operations.Domain;
using Coolector.Services.Operations.Queries;
using Coolector.Services.Operations.Services;

namespace Coolector.Services.Operations.Modules
{
    public class OperationModule : ModuleBase
    {
        public OperationModule(IOperationService operationService) : base("operations")
        {
            Get("{requestId}", args => Fetch<GetOperation, Operation>
                (async x => await operationService.GetAsync(x.RequestId)).HandleAsync());
        }
    }
}