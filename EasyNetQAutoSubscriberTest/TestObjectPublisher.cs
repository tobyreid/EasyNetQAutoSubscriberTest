using System;
using System.Threading;
using Castle.Core;
using EasyNetQ;

namespace EasyNetQAutoSubscriberTest
{
    public class TestObjectPublisher : IStartable
    {
        private readonly IBus _bus;

        public TestObjectPublisher(IBus bus)
        {
            _bus = bus;
        }


        public void Start()
        {
            while (true)
            {
                try
                {
                    if (_bus.IsConnected)
                    {
                        _bus.Publish(new TestObject());
                    }
                    else
                    {
                        Thread.Sleep(1000);
                    }
                }
                catch (Exception)
                {
                    //Rabbit has been restarted and connection is closed mid publish
                    Thread.Sleep(1000);
                    
                }

            }
        }

        public void Stop()
        {
            
        }
    }
}
