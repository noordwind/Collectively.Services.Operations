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
        IEventHandler<AvatarChanged>, IEventHandler<UserNameChanged>,
        IEventHandler<UserSignedIn>, IEventHandler<UserSignedUp>,
        IEventHandler<UserSignedOut>, IEventHandler<UserSignInRejected>
    {
        private readonly IBusClient _bus;
        private readonly IOperationService _operationService;

        public GenericEventHandler(IBusClient bus, IOperationService operationService)
        {
            _bus = bus;
            _operationService = operationService;
        }

        public async Task HandleAsync(RemarkCreated @event)
            => await CompleteForAuthenticatedUserAsync(@event);

        public async Task HandleAsync(RemarkResolved @event)
            => await CompleteForAuthenticatedUserAsync(@event);

        public async Task HandleAsync(RemarkDeleted @event)
            => await CompleteForAuthenticatedUserAsync(@event);

        public async Task HandleAsync(AvatarChanged @event)
            => await CompleteForAuthenticatedUserAsync(@event);

        public async Task HandleAsync(UserNameChanged @event)
            => await CompleteForAuthenticatedUserAsync(@event);

        public async Task HandleAsync(UserSignedIn @event)
            => await CompleteAsync(@event);

        public async Task HandleAsync(UserSignedUp @event)
            => await CompleteAsync(@event);

        public async Task HandleAsync(UserSignedOut @event)
            => await CompleteAsync(@event);

        public async Task HandleAsync(UserSignInRejected @event)
            => await RejectAsync(@event);

        private async Task CompleteForAuthenticatedUserAsync(IAuthenticatedEvent @event)
            => await CompleteAsync(@event, @event.UserId);

        private async Task CompleteAsync(IEvent @event, string userId = null)
        {
            await _operationService.CompleteAsync(@event.RequestId);
            await _bus.PublishAsync(new OperationUpdated(@event.RequestId,
                userId, States.Completed, DateTime.UtcNow, string.Empty));
        }

        private async Task RejectAsync(IRejectedEvent @event)
        {
            await _operationService.RejectAsync(@event.RequestId);
            await _bus.PublishAsync(new OperationUpdated(@event.RequestId,
                @event.UserId, States.Rejected, DateTime.UtcNow, @event.Reason));
        }
    }
}