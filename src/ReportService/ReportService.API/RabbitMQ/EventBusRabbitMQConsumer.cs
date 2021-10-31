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

namespace ReportService.API.RabbitMQ
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
            channel.QueueDeclare(queue: EventBusConstants.SendRecordsQueue, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(channel);

            //Create event when something receive
            consumer.Received += ReceivedEvent;

            channel.BasicConsume(queue: EventBusConstants.SendRecordsQueue, autoAck: true, consumer: consumer);
        }

        private async void ReceivedEvent(object sender, BasicDeliverEventArgs e)
        {
            if (e.RoutingKey == EventBusConstants.SendRecordsQueue)
            {
                string message = Encoding.UTF8.GetString(e.Body.Span);
                SendRecordsEvent sendRecordsEvent = JsonConvert.DeserializeObject<SendRecordsEvent>(message);

                // EXECUTION
                // DEAL WITH INCOMING RECORDS (filter groupby etc.)
            }
        }

        public void Disconnect()
        {
            _connection.Dispose();
        }
    }
}
