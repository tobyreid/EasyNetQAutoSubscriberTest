using System;
using System.Threading.Tasks;
using Castle.MicroKernel.Lifestyle;
using Castle.Windsor;
using EasyNetQ.AutoSubscribe;
using Topshelf.Logging;

namespace EasyNetQAutoSubscriberTest
{
    public class WindsorMessageDispatcher : IAutoSubscriberMessageDispatcher
    {
        private readonly IWindsorContainer _container;

        private readonly LogWriter _logger = HostLogger.Get<ConsumerService>();



        public WindsorMessageDispatcher(IWindsorContainer container)
        {
            _container = container;
        }

        public void Dispatch<TMessage, TConsumer>(TMessage message)
            where TMessage : class
            where TConsumer : IConsume<TMessage>
        {
            using (_container.BeginScope()) //this ensures data is persisted once the consume is complete
            {
                var consumer = (IConsume<TMessage>)_container.Resolve<TConsumer>();
                try
                {
                    consumer.Consume(message);
                }
                catch (Exception e)
                {
                    _logger.Fatal(e);
                }

                finally
                {
                    _container.Release(consumer);
                }
            }
        }

        public Task DispatchAsync<TMessage, TConsumer>(TMessage message) where TMessage : class where TConsumer : IConsumeAsync<TMessage>
        {
            throw new NotImplementedException();
        }
    }

}
