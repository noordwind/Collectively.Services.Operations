using Collectively.Messages.Commands.Notifications;
using Collectively.Messages.Commands.Users;
using Collectively.Messages.Events.Notifications;
using Collectively.Messages.Events.Users;
using static Collectively.Common.Host.WebServiceHost;

namespace Collectively.Services.Operations.Subscriptions
{
    public static class UsersSubscriptions
    {
        public static BusBuilder SubscribeUsers(this BusBuilder busBuilder)
            => busBuilder.SubscribeCommands()
                         .SubscribeEvents();

        private static BusBuilder SubscribeCommands(this BusBuilder busBuilder)
            => busBuilder.SubscribeToCommand<UploadAvatar>()
                .SubscribeToCommand<RemoveAvatar>()
                .SubscribeToCommand<ChangeUsername>()
                .SubscribeToCommand<ChangePassword>()
                .SubscribeToCommand<ResetPassword>()
                .SubscribeToCommand<SetNewPassword>()
                .SubscribeToCommand<EditUser>()
                .SubscribeToCommand<SignIn>()
                .SubscribeToCommand<SignUp>()
                .SubscribeToCommand<SignOut>()
                .SubscribeToCommand<PostOnFacebookWall>()
                .SubscribeToCommand<UpdateUserNotificationSettings>();

        private static BusBuilder SubscribeEvents(this BusBuilder busBuilder)
            => busBuilder.SubscribeToEvent<UsernameChanged>()
                .SubscribeToEvent<ChangeUsernameRejected>()
                .SubscribeToEvent<AvatarUploaded>()
                .SubscribeToEvent<UploadAvatarRejected>()
                .SubscribeToEvent<AvatarRemoved>()
                .SubscribeToEvent<RemoveAvatarRejected>()
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
                .SubscribeToEvent<UserNotificationSettingsUpdated>()
                .SubscribeToEvent<UpdateUserNotificationSettingsRejected>();
    }
}