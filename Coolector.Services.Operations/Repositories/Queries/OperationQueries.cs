using System;
using System.Threading.Tasks;
using Coolector.Services.Operations.Domain;
using Coolector.Common.Extensions;
using Coolector.Common.Mongo;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Coolector.Services.Operations.Repositories.Queries
{
    public static class OperationQueries
    {
        public static IMongoCollection<Operation> Operations(this IMongoDatabase database)
            => database.GetCollection<Operation>();

        public static async Task<Operation> GetByRequestIdAsync(this IMongoCollection<Operation> operations, Guid id)
        {
            if (id.IsEmpty())
                return null;

            return await operations.AsQueryable().FirstOrDefaultAsync(x => x.RequestId == id);
        }
    }
}