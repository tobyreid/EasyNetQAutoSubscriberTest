using System.Threading;
using EasyNetQ.AutoSubscribe;
using Topshelf.Logging;

namespace EasyNetQAutoSubscriberTest
{
   
    public class TestObjectConsumer : IConsume<TestObject>
    {
        private readonly LogWriter _logger = HostLogger.Get<ConsumerService>();


        public void Consume(TestObject message)
        {
            Thread.Sleep(1000);
            _logger.Debug(string.Format("Message recieved {0}", message));
        }
    }
}
