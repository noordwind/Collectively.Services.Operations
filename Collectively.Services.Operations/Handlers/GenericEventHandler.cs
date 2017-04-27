using System;
using System.Threading.Tasks;
using Collectively.Messages.Events;
using Collectively.Messages.Events.Notifications;
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
        IEventHandler<RemarkDeleted>, IEventHandler<RemarkResolved>, 
        IEventHandler<RemarkProcessed>, IEventHandler<RemarkRenewed>, 
        IEventHandler<RemarkCanceled>, 
        IEventHandler<PhotosToRemarkAdded>, IEventHandler<AddPhotosToRemarkRejected>, 
        IEventHandler<PhotosFromRemarkRemoved>, IEventHandler<RemovePhotosFromRemarkRejected>, 
        IEventHandler<RemarkVoteSubmitted>, IEventHandler<SubmitRemarkVoteRejected>, 
        IEventHandler<RemarkVoteDeleted>, IEventHandler<DeleteRemarkVoteRejected>, 
        IEventHandler<CommentAddedToRemark>, IEventHandler<AddCommentToRemarkRejected>, 
        IEventHandler<CommentEditedInRemark>, IEventHandler<EditRemarkCommentRejected>, 
        IEventHandler<CommentDeletedFromRemark>, IEventHandler<DeleteRemarkCommentRejected>, 
        IEventHandler<RemarkCommentVoteSubmitted>, IEventHandler<SubmitRemarkCommentVoteRejected>, 
        IEventHandler<RemarkCommentVoteDeleted>, IEventHandler<DeleteRemarkCommentVoteRejected>, 
        IEventHandler<FavoriteRemarkAdded>, IEventHandler<AddFavoriteRemarkRejected>, 
        IEventHandler<FavoriteRemarkDeleted>, IEventHandler<DeleteFavoriteRemarkRejected>, 
        IEventHandler<RemarkActionTaken>, IEventHandler<TakeRemarkActionRejected>, 
        IEventHandler<RemarkActionCanceled>, IEventHandler<CancelRemarkActionRejected>, 
        IEventHandler<AvatarUploaded>, IEventHandler<UploadAvatarRejected>,
        IEventHandler<AvatarRemoved>, IEventHandler<RemoveAvatarRejected>,  
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
        IEventHandler<ProcessRemarkRejected>, IEventHandler<RenewRemarkRejected>,
        IEventHandler<CancelRemarkRejected>,
        IEventHandler<DeleteRemarkRejected>,
        IEventHandler<UserNotificationSettingsUpdated>, IEventHandler<UpdateUserNotificationSettingsRejected>,
        IEventHandler<RemarkStateDeleted>, IEventHandler<DeleteRemarkStateRejected>
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

        public async Task HandleAsync(RemarkDeleted @event)
            => await CompleteForAuthenticatedUserAsync(@event);

        public async Task HandleAsync(RemarkResolved @event)
            => await CompleteForAuthenticatedUserAsync(@event);

        public async Task HandleAsync(RemarkProcessed @event)
            => await CompleteForAuthenticatedUserAsync(@event);

        public async Task HandleAsync(RemarkRenewed @event)
            => await CompleteForAuthenticatedUserAsync(@event);

        public async Task HandleAsync(RemarkCanceled @event)
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

        public async Task HandleAsync(CommentAddedToRemark @event)
            => await CompleteForAuthenticatedUserAsync(@event);

        public async Task HandleAsync(AddCommentToRemarkRejected @event)
            => await RejectAsync(@event);

        public async Task HandleAsync(CommentEditedInRemark @event)
            => await CompleteForAuthenticatedUserAsync(@event);

        public async Task HandleAsync(EditRemarkCommentRejected @event)
            => await RejectAsync(@event);

        public async Task HandleAsync(CommentDeletedFromRemark @event)
            => await CompleteForAuthenticatedUserAsync(@event);  

        public async Task HandleAsync(DeleteRemarkCommentRejected @event)
            => await RejectAsync(@event);

        public async Task HandleAsync(RemarkCommentVoteSubmitted @event)
            => await CompleteForAuthenticatedUserAsync(@event);

        public async Task HandleAsync(SubmitRemarkCommentVoteRejected @event)
            => await RejectAsync(@event);

        public async Task HandleAsync(RemarkCommentVoteDeleted @event)
            => await CompleteForAuthenticatedUserAsync(@event);

        public async Task HandleAsync(DeleteRemarkCommentVoteRejected @event)
            => await RejectAsync(@event);

        public async Task HandleAsync(RemarkActionTaken @event)
            => await CompleteForAuthenticatedUserAsync(@event);

        public async Task HandleAsync(TakeRemarkActionRejected @event)
            => await RejectAsync(@event); 

        public async Task HandleAsync(RemarkActionCanceled @event)
            => await CompleteForAuthenticatedUserAsync(@event);

        public async Task HandleAsync(CancelRemarkActionRejected @event)
            => await RejectAsync(@event);

        public async Task HandleAsync(FavoriteRemarkAdded @event)
            => await CompleteForAuthenticatedUserAsync(@event);

        public async Task HandleAsync(AddFavoriteRemarkRejected @event)
            => await RejectAsync(@event);

        public async Task HandleAsync(FavoriteRemarkDeleted @event)
            => await CompleteForAuthenticatedUserAsync(@event);

        public async Task HandleAsync(DeleteFavoriteRemarkRejected @event)
            => await RejectAsync(@event);

        public async Task HandleAsync(AvatarUploaded @event)
            => await CompleteForAuthenticatedUserAsync(@event);

        public async Task HandleAsync(UploadAvatarRejected @event)
            => await RejectAsync(@event);

        public async Task HandleAsync(AvatarRemoved @event)
            => await CompleteForAuthenticatedUserAsync(@event);

        public async Task HandleAsync(RemoveAvatarRejected @event)
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

        public async Task HandleAsync(ProcessRemarkRejected @event)
            => await RejectAsync(@event);

        public async Task HandleAsync(RenewRemarkRejected @event)
            => await RejectAsync(@event);

        public async Task HandleAsync(CancelRemarkRejected @event)
            => await RejectAsync(@event);       
                 
        public async Task HandleAsync(DeleteRemarkRejected @event)
            => await RejectAsync(@event);

        public async Task HandleAsync(UserNotificationSettingsUpdated @event)
            => await CompleteForAuthenticatedUserAsync(@event);

        public async Task HandleAsync(UpdateUserNotificationSettingsRejected @event)
            => await RejectAsync(@event);

        public async Task HandleAsync(RemarkStateDeleted @event)
            => await CompleteForAuthenticatedUserAsync(@event);

        public async Task HandleAsync(DeleteRemarkStateRejected @event)
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