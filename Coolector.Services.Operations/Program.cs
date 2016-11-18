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
                .SubscribeToCommand<EditUser>()
                .SubscribeToCommand<SignIn>()
                .SubscribeToCommand<SignUp>()
                .SubscribeToCommand<SignOut>()
                .SubscribeToCommand<PostMessageOnFacebookWall>()
                .SubscribeToEvent<RemarkCreated>()
                .SubscribeToEvent<RemarkDeleted>()
                .SubscribeToEvent<RemarkResolved>()
                .SubscribeToEvent<UserNameChanged>()
                .SubscribeToEvent<AvatarChanged>()
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