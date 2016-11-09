using System;
using System.Threading.Tasks;
using Coolector.Common.Events;
using Coolector.Common.Events.Operations;
using Coolector.Common.Events.Remarks;
using Coolector.Common.Events.Users;
using Coolector.Services.Operations.Domain;
using Coolector.Services.Operations.Services;
using RawRabbit;

namespace Coolector.Services.Operations.Handlers
{
    public class GenericEventHandler : IEventHandler<RemarkCreated>,
        IEventHandler<RemarkResolved>, IEventHandler<RemarkDeleted>,
        IEventHandler<AvatarChanged>, IEventHandler<UserNameChanged>
    {
        private readonly IBusClient _bus;
        private readonly IOperationService _operationService;

        public GenericEventHandler(IBusClient bus, IOperationService operationService)
        {
            _bus = bus;
            _operationService = operationService;
        }

        public async Task HandleAsync(RemarkCreated @event)
            => await CompleteAsync(@event);

        public async Task HandleAsync(RemarkResolved @event)
            => await CompleteAsync(@event);

        public async Task HandleAsync(RemarkDeleted @event)
            => await CompleteAsync(@event);

        public async Task HandleAsync(AvatarChanged @event)
            => await CompleteAsync(@event);

        public async Task HandleAsync(UserNameChanged @event)
            => await CompleteAsync(@event);

        private async Task CompleteAsync(IAuthenticatedEvent @event)
        {
            await _operationService.CompleteAsync(@event.RequestId);
            await _bus.PublishAsync(new OperationUpdated(@event.RequestId,
                @event.UserId, States.Completed, DateTime.UtcNow, string.Empty));
        }

        private async Task RejectAsync(IRejectedEvent @event)
        {
            await _operationService.RejectAsync(@event.RequestId);
            await _bus.PublishAsync(new OperationUpdated(@event.RequestId,
                @event.UserId, States.Rejected, DateTime.UtcNow, @event.Reason));
        }
    }
}