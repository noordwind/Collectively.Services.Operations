using System;
using System.Threading.Tasks;
using Collectively.Common.Types;
using Collectively.Services.Operations.Domain;

namespace Collectively.Services.Operations.Repositories
{
    public interface IOperationRepository
    {
        Task<Maybe<Operation>> GetAsync(Guid requestId);
        Task AddAsync(Operation operation);
        Task UpdateAsync(Operation operation);
    }
}