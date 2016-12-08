using System;
using System.Threading.Tasks;
using Coolector.Common.Events;
using Coolector.Common.Events.Facebook;
using Coolector.Common.Events.Remarks;
using Coolector.Services.Operations.Domain;
using Coolector.Services.Operations.Services;
using Coolector.Services.Operations.Shared;
using Coolector.Services.Operations.Shared.Events;
using Coolector.Services.Users.Shared.Events;
using RawRabbit;

namespace Coolector.Services.Operations.Handlers
{
    public class GenericEventHandler : IEventHandler<RemarkCreated>,
        IEventHandler<RemarkResolved>, IEventHandler<RemarkDeleted>,
        IEventHandler<AvatarChanged>, IEventHandler<ChangeAvatarRejected>, 
        IEventHandler<UserNameChanged>, IEventHandler<ChangeUsernameRejected>,
        IEventHandler<ResetPasswordInitiated>, IEventHandler<NewPasswordSet>,
        IEventHandler<ResetPasswordRejected>, IEventHandler<SetNewPasswordRejected>,
        IEventHandler<PasswordChanged>, IEventHandler<ChangePasswordRejected>,
        IEventHandler<UserSignedIn>, IEventHandler<UserSignedUp>,
        IEventHandler<UserSignedOut>, IEventHandler<SignOutRejected>, 
        IEventHandler<UserSignInRejected>, IEventHandler<UserSignUpRejected>, 
        IEventHandler<MessageOnFacebookWallPosted>,
        IEventHandler<PostMessageOnFacebookWallRejected>,
        IEventHandler<CreateRemarkRejected>, IEventHandler<ResolveRemarkRejected>,
        IEventHandler<DeleteRemarkRejected>
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

        public async Task HandleAsync(ChangeAvatarRejected @event)
            => await RejectAsync(@event);

        public async Task HandleAsync(UserNameChanged @event)
            => await CompleteForAuthenticatedUserAsync(@event);

        public async Task HandleAsync(ChangeUsernameRejected @event)
            => await RejectAsync(@event);

        public async Task HandleAsync(ResetPasswordInitiated @event)
            => await CompleteAsync(@event);

        public async Task HandleAsync(NewPasswordSet @event)
            => await CompleteAsync(@event);

        public async Task HandleAsync(ResetPasswordRejected @event)
            => await RejectAsync(@event);

        public async Task HandleAsync(SetNewPasswordRejected @event)
            => await RejectAsync(@event);

        public async Task HandleAsync(PasswordChanged @event)
            => await CompleteForAuthenticatedUserAsync(@event);

        public async Task HandleAsync(UserSignedIn @event)
            => await CompleteAsync(@event);

        public async Task HandleAsync(UserSignedUp @event)
            => await CompleteAsync(@event);

        public async Task HandleAsync(UserSignedOut @event)
            => await CompleteAsync(@event);

        public async Task HandleAsync(SignOutRejected @event)
            => await RejectAsync(@event);

        public async Task HandleAsync(MessageOnFacebookWallPosted @event)
            => await CompleteForAuthenticatedUserAsync(@event);

        public async Task HandleAsync(ChangePasswordRejected @event)
            => await RejectAsync(@event);

        public async Task HandleAsync(UserSignInRejected @event)
            => await RejectAsync(@event);

        public async Task HandleAsync(UserSignUpRejected @event)
            => await RejectAsync(@event);

        public async Task HandleAsync(PostMessageOnFacebookWallRejected @event)
            => await RejectAsync(@event);

        public async Task HandleAsync(CreateRemarkRejected @event)
            => await RejectAsync(@event);

        public async Task HandleAsync(ResolveRemarkRejected @event)
            => await RejectAsync(@event);

        public async Task HandleAsync(DeleteRemarkRejected @event)
            => await RejectAsync(@event);

        private async Task CompleteForAuthenticatedUserAsync(IAuthenticatedEvent @event)
            => await CompleteAsync(@event, @event.UserId);

        private async Task CompleteAsync(IEvent @event, string userId = null)
        {
            await _operationService.CompleteAsync(@event.RequestId);
            await _bus.PublishAsync(new OperationUpdated(@event.RequestId, userId,
                States.Completed, OperationCodes.Success, string.Empty, DateTime.UtcNow));
        }

        private async Task RejectAsync(IRejectedEvent @event)
        {
            await _operationService.RejectAsync(@event.RequestId, @event.Code, @event.Reason);
            await _bus.PublishAsync(new OperationUpdated(@event.RequestId, @event.UserId,
                States.Rejected, @event.Code, @event.Reason, DateTime.UtcNow));
        }
    }
}