using System;
using Collectively.Common.Queries;

namespace Collectively.Services.Operations.Queries
{
    public class GetOperation : IQuery
    {
        public Guid RequestId { get; set; }
    }
}