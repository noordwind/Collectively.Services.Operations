using System.Threading.Tasks;
using Collectively.Messages.Commands;
using Collectively.Messages.Commands.Notifications;
using Collectively.Services.Operations.Domain;
using Collectively.Services.Operations.Services;
using Collectively.Messages.Events.Operations;
using Collectively.Messages.Commands.Groups;
using Collectively.Messages.Commands.Remarks;
using Collectively.Messages.Commands.Users;
using Serilog;
using RawRabbit;

namespace Collectively.Services.Operations.Handlers
{
    public class GenericCommandHandler : ICommandHandler<CreateRemark>,
        ICommandHandler<DeleteRemark>,
        ICommandHandler<ResolveRemark>, ICommandHandler<ProcessRemark>, 
        ICommandHandler<RenewRemark>, ICommandHandler<CancelRemark>,
        ICommandHandler<AddPhotosToRemark>, ICommandHandler<RemovePhotosFromRemark>, 
        ICommandHandler<SubmitRemarkVote>, ICommandHandler<DeleteRemarkVote>,
        ICommandHandler<AddCommentToRemark>, ICommandHandler<EditRemarkComment>,
        ICommandHandler<DeleteRemarkComment>, ICommandHandler<SubmitRemarkCommentVote>,
        ICommandHandler<DeleteRemarkCommentVote>, 
        ICommandHandler<AddFavoriteRemark>, ICommandHandler<DeleteFavoriteRemark>,
        ICommandHandler<TakeRemarkAction>, ICommandHandler<CancelRemarkAction>, 
        ICommandHandler<UploadAvatar>, ICommandHandler<RemoveAvatar>, 
        ICommandHandler<ChangeUsername>,
        ICommandHandler<ResetPassword>, ICommandHandler<SetNewPassword>,
        ICommandHandler<ChangePassword>, ICommandHandler<EditUser>,
        ICommandHandler<SignIn>, ICommandHandler<SignUp>,
        ICommandHandler<SignOut>, ICommandHandler<PostOnFacebookWall>,
        ICommandHandler<DeleteAccount>,
        ICommandHandler<UpdateUserNotificationSettings>,
        ICommandHandler<DeleteRemarkState>,
        ICommandHandler<ActivateAccount>,
        ICommandHandler<LockAccount>, ICommandHandler<UnlockAccount>,
        ICommandHandler<CreateGroup>, ICommandHandler<CreateOrganization>,
        ICommandHandler<AddMemberToGroup>, ICommandHandler<AddMemberToOrganization>,
        ICommandHandler<ReportRemark>, ICommandHandler<EditRemark>
    {
        private static readonly ILogger Logger = Log.Logger;
        private readonly IBusClient _bus;
        private readonly IOperationService _operationService;

        public GenericCommandHandler(IBusClient bus, IOperationService operationService)
        {
            _bus = bus;
            _operationService = operationService;
        }

        public async Task HandleAsync(CreateRemark command)
            => await CreateForAuthenticatedUserAsync(command);
        public async Task HandleAsync(DeleteRemark command)
            => await CreateForAuthenticatedUserAsync(command);
        public async Task HandleAsync(ResolveRemark command)
            => await CreateForAuthenticatedUserAsync(command);
        public async Task HandleAsync(ProcessRemark command)
            => await CreateForAuthenticatedUserAsync(command);
        public async Task HandleAsync(RenewRemark command)
            => await CreateForAuthenticatedUserAsync(command);
        public async Task HandleAsync(CancelRemark command)
            => await CreateForAuthenticatedUserAsync(command);
        public async Task HandleAsync(AddPhotosToRemark command)
            => await CreateForAuthenticatedUserAsync(command);
        public async Task HandleAsync(RemovePhotosFromRemark command)
            => await CreateForAuthenticatedUserAsync(command);
        public async Task HandleAsync(SubmitRemarkVote command)
            => await CreateForAuthenticatedUserAsync(command);
        public async Task HandleAsync(DeleteRemarkVote command)
            => await CreateForAuthenticatedUserAsync(command);
        public async Task HandleAsync(AddCommentToRemark command)
            => await CreateForAuthenticatedUserAsync(command);
        public async Task HandleAsync(EditRemarkComment command)
            => await CreateForAuthenticatedUserAsync(command);
        public async Task HandleAsync(DeleteRemarkComment command)
            => await CreateForAuthenticatedUserAsync(command);
        public async Task HandleAsync(SubmitRemarkCommentVote command)
            => await CreateForAuthenticatedUserAsync(command);
        public async Task HandleAsync(DeleteRemarkCommentVote command)
            => await CreateForAuthenticatedUserAsync(command);
        public async Task HandleAsync(AddFavoriteRemark command)
            => await CreateForAuthenticatedUserAsync(command);
        public async Task HandleAsync(DeleteFavoriteRemark command)
            => await CreateForAuthenticatedUserAsync(command);
        public async Task HandleAsync(TakeRemarkAction command)
            => await CreateForAuthenticatedUserAsync(command);
        public async Task HandleAsync(CancelRemarkAction command)
            => await CreateForAuthenticatedUserAsync(command);
        public async Task HandleAsync(UploadAvatar command)
            => await CreateForAuthenticatedUserAsync(command);
        public async Task HandleAsync(RemoveAvatar command)
            => await CreateForAuthenticatedUserAsync(command);
        public async Task HandleAsync(ChangeUsername command)
            => await CreateForAuthenticatedUserAsync(command);
        public async Task HandleAsync(ResetPassword command)
            => await CreateAsync(command);
        public async Task HandleAsync(SetNewPassword command)
            => await CreateAsync(command);
        public async Task HandleAsync(ChangePassword command)
            => await CreateForAuthenticatedUserAsync(command);
        public async Task HandleAsync(EditUser command)
            => await CreateForAuthenticatedUserAsync(command);
        public async Task HandleAsync(SignIn command)
            => await CreateAsync(command);
        public async Task HandleAsync(SignUp command)
            => await CreateAsync(command);
        public async Task HandleAsync(SignOut command)
            => await CreateForAuthenticatedUserAsync(command);
        public async Task HandleAsync(PostOnFacebookWall command)
            => await CreateForAuthenticatedUserAsync(command);

        public async Task HandleAsync(DeleteAccount command)
            => await CreateForAuthenticatedUserAsync(command);
        public async Task HandleAsync(UpdateUserNotificationSettings command)
            => await CreateForAuthenticatedUserAsync(command);
        public async Task HandleAsync(DeleteRemarkState command)
            => await CreateForAuthenticatedUserAsync(command);

        public async Task HandleAsync(ActivateAccount command)
            => await CreateAsync(command);

        public async Task HandleAsync(LockAccount command)
            => await CreateForAuthenticatedUserAsync(command);

        public async Task HandleAsync(UnlockAccount command)
            => await CreateForAuthenticatedUserAsync(command);

        public async Task HandleAsync(CreateGroup command)
            => await CreateForAuthenticatedUserAsync(command);

        public async Task HandleAsync(CreateOrganization command)
            => await CreateForAuthenticatedUserAsync(command);

        public async Task HandleAsync(AddMemberToGroup command)
            => await CreateForAuthenticatedUserAsync(command);

        public async Task HandleAsync(AddMemberToOrganization command)
            => await CreateForAuthenticatedUserAsync(command);

        public async Task HandleAsync(ReportRemark command)
            => await CreateForAuthenticatedUserAsync(command);

        public async Task HandleAsync(EditRemark command)
            => await CreateForAuthenticatedUserAsync(command);
        private async Task CreateForAuthenticatedUserAsync(IAuthenticatedCommand command)
            => await CreateAsync(command, command.UserId);

        private async Task CreateAsync(ICommand command, string userId = null)
        {
            Logger.Debug($"Create operation for {command.GetType().Name} command");
            await _operationService.CreateAsync(command.Request.Id, command.Request.Name, userId,
                command.Request.Origin, command.Request.Resource, command.Request.CreatedAt);
            await _bus.PublishAsync(new OperationCreated(command.Request.Id, command.Request.Name,
                userId, command.Request.Origin, command.Request.Resource, States.Created,
                command.Request.CreatedAt));
        }
    }
}