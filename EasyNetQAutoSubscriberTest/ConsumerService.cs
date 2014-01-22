using Castle.Core;
using Castle.Facilities.Startable;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Topshelf;
using Topshelf.Logging;

namespace EasyNetQAutoSubscriberTest
{
    class ConsumerService : ServiceControl
    {
        private IWindsorContainer _container;
        private readonly LogWriter _logger = HostLogger.Get<ConsumerService>();

        public bool Start(HostControl hostControl)
        {
            _container = new WindsorContainer();
            _container.Install(FromAssembly.This());
            _container.Register(Component.For<IWindsorContainer>().Instance(_container));
            _container.AddFacility<StartableFacility>(f => f.DeferredTryStart());
            _container.Register(Classes.FromThisAssembly().BasedOn<IStartable>());
            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            _container.Dispose();
            _logger.Info("Stopped!");
            return true;
        }
    }
}
