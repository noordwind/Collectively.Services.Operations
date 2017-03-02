using System;
using System.Threading.Tasks;
using Collectively.Messages.Events;
using Collectively.Services.Operations.Domain;
using Collectively.Services.Operations.Services;
using Collectively.Messages.Events.Operations;
using Collectively.Messages.Events.Remarks;
using Collectively.Messages.Events.Users;
using Humanizer;
using NLog;
using RawRabbit;

namespace Collectively.Services.Operations.Handlers
{
    public class GenericEventHandler : IEventHandler<RemarkCreated>,
        IEventHandler<RemarkResolved>, IEventHandler<RemarkDeleted>,
        IEventHandler<PhotosToRemarkAdded>, IEventHandler<AddPhotosToRemarkRejected>, 
        IEventHandler<PhotosFromRemarkRemoved>, IEventHandler<RemovePhotosFromRemarkRejected>, 
        IEventHandler<RemarkVoteSubmitted>, IEventHandler<SubmitRemarkVoteRejected>, 
        IEventHandler<RemarkVoteDeleted>, IEventHandler<DeleteRemarkVoteRejected>, 
        IEventHandler<AvatarChanged>, IEventHandler<ChangeAvatarRejected>, 
        IEventHandler<UsernameChanged>, IEventHandler<ChangeUsernameRejected>,
        IEventHandler<ResetPasswordInitiated>, IEventHandler<NewPasswordSet>,
        IEventHandler<ResetPasswordRejected>, IEventHandler<SetNewPasswordRejected>,
        IEventHandler<PasswordChanged>, IEventHandler<ChangePasswordRejected>,
        IEventHandler<SignedIn>, IEventHandler<SignedUp>,
        IEventHandler<SignedOut>, IEventHandler<SignOutRejected>, 
        IEventHandler<SignInRejected>, IEventHandler<SignUpRejected>, 
        IEventHandler<MessageOnFacebookWallPosted>,
        IEventHandler<PostOnFacebookWallRejected>,
        IEventHandler<CreateRemarkRejected>, IEventHandler<ResolveRemarkRejected>,
        IEventHandler<DeleteRemarkRejected>
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
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

        public async Task HandleAsync(PhotosToRemarkAdded @event)
            => await CompleteForAuthenticatedUserAsync(@event);

        public async Task HandleAsync(AddPhotosToRemarkRejected @event)
            => await RejectAsync(@event);

        public async Task HandleAsync(PhotosFromRemarkRemoved @event)
            => await CompleteForAuthenticatedUserAsync(@event);

        public async Task HandleAsync(RemovePhotosFromRemarkRejected @event)
            => await RejectAsync(@event);

        public async Task HandleAsync(RemarkVoteSubmitted @event)
            => await CompleteForAuthenticatedUserAsync(@event);

        public async Task HandleAsync(SubmitRemarkVoteRejected @event)
            => await RejectAsync(@event);

        public async Task HandleAsync(RemarkVoteDeleted @event)
            => await CompleteForAuthenticatedUserAsync(@event);

        public async Task HandleAsync(DeleteRemarkVoteRejected @event)
            => await RejectAsync(@event);

        public async Task HandleAsync(AvatarChanged @event)
            => await CompleteForAuthenticatedUserAsync(@event);

        public async Task HandleAsync(ChangeAvatarRejected @event)
            => await RejectAsync(@event);

        public async Task HandleAsync(UsernameChanged @event)
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

        public async Task HandleAsync(SignedIn @event)
            => await CompleteAsync(@event);

        public async Task HandleAsync(SignedUp @event)
            => await CompleteAsync(@event);

        public async Task HandleAsync(SignedOut @event)
            => await CompleteAsync(@event);

        public async Task HandleAsync(SignOutRejected @event)
            => await RejectAsync(@event);

        public async Task HandleAsync(MessageOnFacebookWallPosted @event)
            => await CompleteForAuthenticatedUserAsync(@event);

        public async Task HandleAsync(ChangePasswordRejected @event)
            => await RejectAsync(@event);

        public async Task HandleAsync(SignInRejected @event)
            => await RejectAsync(@event);

        public async Task HandleAsync(SignUpRejected @event)
            => await RejectAsync(@event);

        public async Task HandleAsync(PostOnFacebookWallRejected @event)
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
            Logger.Debug($"Complete operation after receiving {@event.GetType().Name} event");
            await _operationService.CompleteAsync(@event.RequestId);
            await _bus.PublishAsync(new OperationUpdated(@event.RequestId, userId, 
                @event.GetType().Name.Humanize(LetterCasing.LowerCase).Underscore(),
                States.Completed, OperationCodes.Success, string.Empty, DateTime.UtcNow));
        }

        private async Task RejectAsync(IRejectedEvent @event)
        {
            Logger.Debug($"Reject operation after receiving {@event.GetType().Name} event");
            await _operationService.RejectAsync(@event.RequestId, @event.Code, @event.Reason);
            await _bus.PublishAsync(new OperationUpdated(@event.RequestId, @event.UserId,
                @event.GetType().Name.Humanize(LetterCasing.LowerCase).Underscore(),
                States.Rejected, @event.Code, @event.Reason, DateTime.UtcNow));
        }
    }
}