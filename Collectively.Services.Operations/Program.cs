using Collectively.Common.Host;
using Collectively.Services.Operations.Framework;
using Collectively.Messages.Commands.Remarks;
using Collectively.Messages.Events.Remarks;
using Collectively.Messages.Commands.Users;
using Collectively.Messages.Events.Users;

namespace Collectively.Services.Operations
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebServiceHost
                .Create<Startup>(port: 10010)
                .UseAutofac(Bootstrapper.LifeTimeScope)
                .UseRabbitMq(queueName: typeof(Program).Namespace)
                .SubscribeToCommand<CreateRemark>()
                .SubscribeToCommand<DeleteRemark>()
                .SubscribeToCommand<ResolveRemark>()
                .SubscribeToCommand<ProcessRemark>()
                .SubscribeToCommand<RenewRemark>()
                .SubscribeToCommand<CancelRemark>()
                .SubscribeToCommand<AddPhotosToRemark>()
                .SubscribeToCommand<RemovePhotosFromRemark>()
                .SubscribeToCommand<SubmitRemarkVote>()
                .SubscribeToCommand<DeleteRemarkVote>()
                .SubscribeToCommand<ChangeAvatar>()
                .SubscribeToCommand<ChangeUsername>()
                .SubscribeToCommand<ChangePassword>()
                .SubscribeToCommand<ResetPassword>()
                .SubscribeToCommand<SetNewPassword>()
                .SubscribeToCommand<EditUser>()
                .SubscribeToCommand<SignIn>()
                .SubscribeToCommand<SignUp>()
                .SubscribeToCommand<SignOut>()
                .SubscribeToCommand<PostOnFacebookWall>()
                .SubscribeToEvent<RemarkCreated>()
                .SubscribeToEvent<CreateRemarkRejected>()
                .SubscribeToEvent<RemarkDeleted>()
                .SubscribeToEvent<DeleteRemarkRejected>()
                .SubscribeToEvent<RemarkResolved>()
                .SubscribeToEvent<ResolveRemarkRejected>()
                .SubscribeToEvent<RemarkProcessed>()
                .SubscribeToEvent<ProcessRemarkRejected>()
                .SubscribeToEvent<RemarkRenewed>()
                .SubscribeToEvent<RenewRemarkRejected>()
                .SubscribeToEvent<RemarkCanceled>()
                .SubscribeToEvent<CancelRemarkRejected>()                                                
                .SubscribeToEvent<PhotosToRemarkAdded>()
                .SubscribeToEvent<AddPhotosToRemarkRejected>()
                .SubscribeToEvent<PhotosFromRemarkRemoved>()
                .SubscribeToEvent<RemovePhotosFromRemarkRejected>()  
                .SubscribeToEvent<RemarkVoteSubmitted>()
                .SubscribeToEvent<SubmitRemarkVoteRejected>()
                .SubscribeToEvent<RemarkVoteDeleted>()
                .SubscribeToEvent<DeleteRemarkVoteRejected>()                   
                .SubscribeToEvent<UsernameChanged>()
                .SubscribeToEvent<ChangeUsernameRejected>()
                .SubscribeToEvent<AvatarChanged>()
                .SubscribeToEvent<ChangeAvatarRejected>()
                .SubscribeToEvent<PasswordChanged>()
                .SubscribeToEvent<ResetPasswordInitiated>()
                .SubscribeToEvent<NewPasswordSet>()
                .SubscribeToEvent<ChangePasswordRejected>()
                .SubscribeToEvent<ResetPasswordRejected>()
                .SubscribeToEvent<SetNewPasswordRejected>()
                .SubscribeToEvent<SignedIn>()
                .SubscribeToEvent<SignedUp>()
                .SubscribeToEvent<SignedOut>()
                .SubscribeToEvent<SignInRejected>()
                .SubscribeToEvent<SignUpRejected>()
                .SubscribeToEvent<SignOutRejected>()
                .SubscribeToEvent<MessageOnFacebookWallPosted>()
                .SubscribeToEvent<PostOnFacebookWallRejected>()
                .Build()
                .Run();
        }
    }
}