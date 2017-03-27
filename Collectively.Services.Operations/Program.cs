using Collectively.Common.Host;
using Collectively.Services.Operations.Framework;
using Collectively.Services.Operations.Subscriptions;

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
                .SubscribeRemarks()
                .SubscribeUsers()
                .Build()
                .Run();
        }
    }
}