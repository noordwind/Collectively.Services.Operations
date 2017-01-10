using Coolector.Common.Host;
using Coolector.Services.Operations.Framework;
using Coolector.Services.Remarks.Shared.Commands;
using Coolector.Services.Remarks.Shared.Events;
using Coolector.Services.Users.Shared.Commands;
using Coolector.Services.Users.Shared.Events;

namespace Coolector.Services.Operations
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