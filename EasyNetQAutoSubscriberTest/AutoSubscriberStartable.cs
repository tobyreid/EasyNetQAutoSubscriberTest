using System;
using System.Reflection;
using Castle.Core;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using EasyNetQ;
using EasyNetQ.AutoSubscribe;

namespace EasyNetQAutoSubscriberTest
{
    public class AutoSubscriberStartable : IStartable
    {
        private readonly IWindsorContainer _container;
        private readonly IBus _bus;

        public AutoSubscriberStartable(IWindsorContainer container, IBus bus)
        {

            _container = container;
            _bus = bus;
        }

        public void Start()
        {
            _container.Register(Classes.FromThisAssembly().BasedOn(typeof(IConsume<>)).WithServiceSelf());


            var autoSubscriber = new AutoSubscriber(_bus, AppDomain.CurrentDomain.FriendlyName)
            {
                GenerateSubscriptionId = c => c.MessageType.Name,
                AutoSubscriberMessageDispatcher = new WindsorMessageDispatcher(_container)
            };

            autoSubscriber.Subscribe(Assembly.GetExecutingAssembly());
        }

        public void Stop()
        {

        }
    }
}
