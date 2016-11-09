using System;
using System.Threading.Tasks;
using Coolector.Common.Types;
using Coolector.Services.Operations.Domain;

namespace Coolector.Services.Operations.Repositories
{
    public interface IOperationRepository
    {
        Task<Maybe<Operation>> GetAsync(Guid requestId);
        Task AddAsync(Operation operation);
        Task UpdateAsync(Operation operation);
    }
}