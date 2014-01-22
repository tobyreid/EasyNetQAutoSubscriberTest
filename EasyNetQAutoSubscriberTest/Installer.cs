using System.Configuration;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using EasyNetQ;

namespace EasyNetQAutoSubscriberTest
{
    public class Installer : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IBus>()
                         .UsingFactoryMethod(kernel => BusBuilderFactory.CreateMessageBus(ConfigurationManager.AppSettings["RabbitConnectString"]))
                         .LifestyleSingleton());
        }
    }
}
