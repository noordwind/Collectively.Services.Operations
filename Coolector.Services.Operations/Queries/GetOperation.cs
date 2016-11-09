using System;
using Coolector.Common.Queries;

namespace Coolector.Services.Operations.Queries
{
    public class GetOperation : IQuery
    {
        public Guid RequestId { get; set; }
    }
}