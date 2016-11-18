using System;
using System.Threading.Tasks;
using Coolector.Common.Commands;
using Coolector.Common.Commands.Facebook;
using Coolector.Common.Commands.Remarks;
using Coolector.Common.Commands.Users;
using Coolector.Common.Events.Operations;
using Coolector.Services.Operations.Domain;
using Coolector.Services.Operations.Services;
using RawRabbit;

namespace Coolector.Services.Operations.Handlers
{
    public class GenericCommandHandler : ICommandHandler<CreateRemark>,
        ICommandHandler<ResolveRemark>, ICommandHandler<DeleteRemark>,
        ICommandHandler<ChangeAvatar>, ICommandHandler<ChangeUserName>,
        ICommandHandler<EditUser>, ICommandHandler<SignIn>,
        ICommandHandler<SignUp>, ICommandHandler<SignOut>,
        ICommandHandler<PostMessageOnFacebookWall>
    {
        private readonly IBusClient _bus;
        private readonly IOperationService _operationService;

        public GenericCommandHandler(IBusClient bus, IOperationService operationService)
        {
            _bus = bus;
            _operationService = operationService;
        }

        public async Task HandleAsync(CreateRemark command)
            => await CreateForAuthenticatedUserAsync(command);

        public async Task HandleAsync(ResolveRemark command)
            => await CreateForAuthenticatedUserAsync(command);

        public async Task HandleAsync(DeleteRemark command)
            => await CreateForAuthenticatedUserAsync(command);

        public async Task HandleAsync(ChangeAvatar command)
            => await CreateForAuthenticatedUserAsync(command);

        public async Task HandleAsync(ChangeUserName command)
            => await CreateForAuthenticatedUserAsync(command);

        public async Task HandleAsync(EditUser command)
            => await CreateForAuthenticatedUserAsync(command);

        public async Task HandleAsync(SignIn command)
            => await CreateAsync(command);

        public async Task HandleAsync(SignUp command)
            => await CreateAsync(command);

        public async Task HandleAsync(SignOut command)
            => await CreateForAuthenticatedUserAsync(command);

        public async Task HandleAsync(PostMessageOnFacebookWall command)
            => await CreateForAuthenticatedUserAsync(command);

        private async Task CreateForAuthenticatedUserAsync(IAuthenticatedCommand command)
            => await CreateAsync(command, command.UserId);

        private async Task CreateAsync(ICommand command, string userId = null)
        {
            await _operationService.CreateAsync(command.Request.Id, command.Request.Name, userId,
                command.Request.Origin, command.Request.Resource, command.Request.CreatedAt);
            await _bus.PublishAsync(new OperationCreated(command.Request.Id, command.Request.Name,
                userId, command.Request.Origin, command.Request.Resource, States.Created,
                command.Request.CreatedAt, DateTime.UtcNow, string.Empty));
        }
    }
}