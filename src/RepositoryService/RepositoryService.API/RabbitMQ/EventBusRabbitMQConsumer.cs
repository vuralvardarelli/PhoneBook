using EventBusRabbitMQ;
using EventBusRabbitMQ.Events;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryService.API.RabbitMQ
{
    public class EventBusRabbitMQConsumer
    {
        private readonly IRabbitMQConnection _connection;

        public EventBusRabbitMQConsumer(IRabbitMQConnection connection)
        {
            _connection = connection;
        }

        public void Consume()
        {
            var channel = _connection.CreateModel();
            channel.QueueDeclare(queue: EventBusConstants.GetRecordsQueue, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(channel);

            //Create event when something receive
            consumer.Received += ReceivedEvent;

            channel.BasicConsume(queue: EventBusConstants.GetRecordsQueue, autoAck: true, consumer: consumer);
        }

        private async void ReceivedEvent(object sender, BasicDeliverEventArgs e)
        {
            if (e.RoutingKey == EventBusConstants.GetRecordsQueue)
            {
                string message = Encoding.UTF8.GetString(e.Body.Span);
                GetRecordsEvent recordsEvent = JsonConvert.DeserializeObject<GetRecordsEvent>(message);

                // EXECUTION
                // GIVE ALL RECORDS
            }
        }

        public void Disconnect()
        {
            _connection.Dispose();
        }
    }
}
