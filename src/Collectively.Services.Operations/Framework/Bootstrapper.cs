using System.Reflection;
using Autofac;
using Collectively.Messages.Commands;
using Collectively.Messages.Events;
using Collectively.Common.Exceptionless;
using Collectively.Common.Mongo;
using Collectively.Common.NancyFx;
using Collectively.Common.Security;
using Collectively.Common.Extensions;
using Collectively.Common.RabbitMq;
using Collectively.Common.Services;
using Collectively.Services.Operations.Repositories;
using Collectively.Services.Operations.Services;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Bootstrappers.Autofac;
using Serilog;
using RawRabbit.Configuration;

namespace Collectively.Services.Operations.Framework
{
    public class Bootstrapper : AutofacNancyBootstrapper
    {
        private static readonly ILogger Logger = Log.Logger;
        private static IExceptionHandler _exceptionHandler;
        private readonly IConfiguration _configuration;

        public static ILifetimeScope LifeTimeScope { get; private set; }

        public Bootstrapper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void ConfigureApplicationContainer(ILifetimeScope container)
        {
            Logger.Information("Collectively.Services.Storage Configuring application container");
            base.ConfigureApplicationContainer(container);

            container.Update(builder =>
            {
                builder.RegisterType<CustomJsonSerializer>().As<JsonSerializer>().SingleInstance();
                builder.RegisterInstance(_configuration.GetSettings<MongoDbSettings>()).SingleInstance();
                builder.RegisterInstance(AutoMapperConfig.InitializeMapper());
                builder.RegisterModule<MongoDbModule>();
                builder.RegisterType<MongoDbInitializer>().As<IDatabaseInitializer>();
                builder.RegisterInstance(_configuration.GetSettings<ExceptionlessSettings>()).SingleInstance();
                builder.RegisterType<ExceptionlessExceptionHandler>().As<IExceptionHandler>().SingleInstance();
                builder.RegisterType<OperationRepository>().As<IOperationRepository>();
                builder.RegisterType<OperationService>().As<IOperationService>();

                var assembly = typeof(Startup).GetTypeInfo().Assembly;
                builder.RegisterAssemblyTypes(assembly).AsClosedTypesOf(typeof(IEventHandler<>)).InstancePerLifetimeScope();
                builder.RegisterAssemblyTypes(assembly).AsClosedTypesOf(typeof(ICommandHandler<>)).InstancePerLifetimeScope();

                SecurityContainer.Register(builder, _configuration);
                RabbitMqContainer.Register(builder, _configuration.GetSettings<RawRabbitConfiguration>());
            });
            LifeTimeScope = container;
        }

        protected override void RequestStartup(ILifetimeScope container, IPipelines pipelines, NancyContext context)
        {
            pipelines.SetupTokenAuthentication(container.Resolve<IJwtTokenHandler>());
            pipelines.OnError.AddItemToEndOfPipeline((ctx, ex) =>
            {
                _exceptionHandler.Handle(ex, ctx.ToExceptionData(),
                    "Request details", "Collectively", "Service", "Operations");

                return ctx.Response;
            });
        }

        protected override void ApplicationStartup(ILifetimeScope container, IPipelines pipelines)
        {
            var databaseSettings = container.Resolve<MongoDbSettings>();
            var databaseInitializer = container.Resolve<IDatabaseInitializer>();
            databaseInitializer.InitializeAsync();

            pipelines.AfterRequest += (ctx) =>
            {
                ctx.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                ctx.Response.Headers.Add("Access-Control-Allow-Methods", "POST,PUT,GET,OPTIONS,DELETE");
                ctx.Response.Headers.Add("Access-Control-Allow-Headers",
                    "Authorization, Origin, X-Requested-With, Content-Type, Accept");
            };
            _exceptionHandler = container.Resolve<IExceptionHandler>();
            Logger.Information("Collectively.Services.Operations API has started.");
        }
    }
}