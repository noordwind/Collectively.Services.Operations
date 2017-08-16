using Collectively.Messages.Commands.Groups;
using Collectively.Messages.Events.Groups;
using static Collectively.Common.Host.WebServiceHost;

namespace Collectively.Services.Operations.Subscriptions
{
    public static class GroupsSubscriptions
    {
        public static BusBuilder SubscribeGroups(this BusBuilder busBuilder)
            => busBuilder.SubscribeCommands()
                         .SubscribeEvents();

        private static BusBuilder SubscribeCommands(this BusBuilder busBuilder)
            => busBuilder.SubscribeToCommand<CreateGroup>()
                .SubscribeToCommand<CreateOrganization>()
                .SubscribeToCommand<AddMemberToGroup>()
                .SubscribeToCommand<AddMemberToOrganization>();

        private static BusBuilder SubscribeEvents(this BusBuilder busBuilder)
            => busBuilder.SubscribeToEvent<GroupCreated>()
                .SubscribeToEvent<CreateGroupRejected>()
                .SubscribeToEvent<OrganizationCreated>()
                .SubscribeToEvent<CreateOrganizationRejected>()
                .SubscribeToEvent<MemberAddedToGroup>()
                .SubscribeToEvent<AddMemberToGroupRejected>()
                .SubscribeToEvent<MemberAddedToOrganization>()
                .SubscribeToEvent<AddMemberToOrganizationRejected>();
    }
}