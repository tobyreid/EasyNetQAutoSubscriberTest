using Topshelf;

namespace EasyNetQAutoSubscriberTest
{
    class Program
    {
        static void Main(string[] args)
        {
            HostFactory.Run(x =>
            {
                x.Service<ConsumerService>();
                x.RunAsLocalSystem();
                x.SetDescription(typeof(ConsumerService).Namespace);
                x.SetDisplayName(typeof(ConsumerService).Namespace);
                x.SetServiceName(typeof(ConsumerService).Namespace);
                x.StartAutomatically();
                x.EnableServiceRecovery(y => y.RestartService(1).RestartService(1).RestartService(1));
            });
        }
    }
}
