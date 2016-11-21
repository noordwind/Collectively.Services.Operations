using System;
using System.Threading.Tasks;
using Coolector.Common.Types;
using Coolector.Services.Operations.Domain;

namespace Coolector.Services.Operations.Services
{
    public interface IOperationService
    {
        Task<Maybe<Operation>> GetAsync(Guid requestId);
        Task CreateAsync(Guid requestId, string name, string userId, string origin, string resource, DateTime createdAt);
        Task RejectAsync(Guid requestId, string code, string message);
        Task CompleteAsync(Guid requestId, string message = null);
    }
}