using System;
using System.Threading.Tasks;
using Coolector.Common.Commands;
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
        ICommandHandler<EditUser>
    {
        private readonly IBusClient _bus;
        private readonly IOperationService _operationService;

        public GenericCommandHandler(IBusClient bus, IOperationService operationService)
        {
            _bus = bus;
            _operationService = operationService;
        }

        public async Task HandleAsync(CreateRemark command)
            => await CreateAsync(command);

        public async Task HandleAsync(ResolveRemark command)
            => await CreateAsync(command);

        public async Task HandleAsync(DeleteRemark command)
            => await CreateAsync(command);

        public async Task HandleAsync(ChangeAvatar command)
            => await CreateAsync(command);

        public async Task HandleAsync(ChangeUserName command)
            => await CreateAsync(command);

        public async Task HandleAsync(EditUser command)
            => await CreateAsync(command);

        private async Task CreateAsync(IAuthenticatedCommand command)
        {
            await _operationService.CreateAsync(command.Request.Id, command.Request.Name, command.UserId,
                command.Request.Origin, command.Request.Resource, command.Request.CreatedAt);
            await _bus.PublishAsync(new OperationCreated(command.Request.Id, command.Request.Name,
                command.UserId, command.Request.Origin, command.Request.Resource, States.Created,
                command.Request.CreatedAt, DateTime.UtcNow, string.Empty));
        }
    }
}