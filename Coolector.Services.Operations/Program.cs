using Coolector.Common.Commands.Facebook;
using Coolector.Common.Commands.Remarks;
using Coolector.Common.Commands.Users;
using Coolector.Common.Events.Facebook;
using Coolector.Common.Events.Remarks;
using Coolector.Common.Events.Users;
using Coolector.Common.Host;
using Coolector.Services.Operations.Framework;

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
                .SubscribeToCommand<ChangeAvatar>()
                .SubscribeToCommand<ChangeUserName>()
                .SubscribeToCommand<ChangePassword>()
                .SubscribeToCommand<ResetPassword>()
                .SubscribeToCommand<SetNewPassword>()
                .SubscribeToCommand<EditUser>()
                .SubscribeToCommand<SignIn>()
                .SubscribeToCommand<SignUp>()
                .SubscribeToCommand<SignOut>()
                .SubscribeToCommand<PostMessageOnFacebookWall>()
                .SubscribeToEvent<RemarkCreated>()
                .SubscribeToEvent<CreateRemarkRejected>()
                .SubscribeToEvent<RemarkDeleted>()
                .SubscribeToEvent<DeleteRemarkRejected>()
                .SubscribeToEvent<RemarkResolved>()
                .SubscribeToEvent<ResolveRemarkRejected>()
                .SubscribeToEvent<UserNameChanged>()
                .SubscribeToEvent<AvatarChanged>()
                .SubscribeToEvent<PasswordChanged>()
                .SubscribeToEvent<ResetPasswordInitiated>()
                .SubscribeToEvent<NewPasswordSet>()
                .SubscribeToEvent<ChangePasswordRejected>()
                .SubscribeToEvent<ResetPasswordRejected>()
                .SubscribeToEvent<SetNewPasswordRejected>()
                .SubscribeToEvent<UserSignedIn>()
                .SubscribeToEvent<UserSignedUp>()
                .SubscribeToEvent<UserSignedOut>()
                .SubscribeToEvent<UserSignInRejected>()
                .SubscribeToEvent<UserSignUpRejected>()
                .SubscribeToEvent<MessageOnFacebookWallPosted>()
                .SubscribeToEvent<PostMessageOnFacebookWallRejected>()
                .Build()
                .Run();
        }
    }
}