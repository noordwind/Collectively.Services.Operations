using System;
using System.Threading.Tasks;
using Collectively.Common.Types;
using Collectively.Services.Operations.Domain;
using Collectively.Services.Operations.Repositories.Queries;
using MongoDB.Driver;

namespace Collectively.Services.Operations.Repositories
{
    public class OperationRepository : IOperationRepository
    {
        private readonly IMongoDatabase _database;

        public OperationRepository(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<Maybe<Operation>> GetAsync(Guid requestId)
            => await _database.Operations().GetByRequestIdAsync(requestId);

        public async Task AddAsync(Operation operation) => await _database.Operations().InsertOneAsync(operation);

        public async Task UpdateAsync(Operation operation)
            => await _database.Operations().ReplaceOneAsync(x => x.Id == operation.Id, operation);
    }
}