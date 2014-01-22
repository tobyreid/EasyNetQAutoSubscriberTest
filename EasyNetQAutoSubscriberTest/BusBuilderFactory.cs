using EasyNetQ;
using System;

namespace EasyNetQAutoSubscriberTest
{
    public class BusBuilderFactory
    {
        public static IBus CreateMessageBus(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new Exception("easynetq connection string is missing or empty");
            }
            return RabbitHutch.CreateBus(connectionString);
        }
    }
}
