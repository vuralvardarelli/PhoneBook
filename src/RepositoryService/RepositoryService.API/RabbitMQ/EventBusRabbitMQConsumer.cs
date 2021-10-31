using EventBusRabbitMQ;
using EventBusRabbitMQ.Events;
using EventBusRabbitMQ.Producer;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RepositoryService.Infrastructure.Services.Interfaces;
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
        private readonly EventBusRabbitMQProducer _eventBus;
        private readonly IServiceProvider _serviceProvider;

        public EventBusRabbitMQConsumer(IRabbitMQConnection connection, EventBusRabbitMQProducer eventBus, IServiceProvider serviceProvider)
        {
            _connection = connection;
            _eventBus = eventBus;
            _serviceProvider = serviceProvider;
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
                GetRecordsEvent evt = JsonConvert.DeserializeObject<GetRecordsEvent>(message);

                // EXECUTION
                using (var scope = _serviceProvider.CreateScope()) // this will use `IServiceScopeFactory` internally
                {
                    var phoneBookService = scope.ServiceProvider.GetService<IPhoneBookService>();
                    var records = await phoneBookService.GetRecords();
                    List<EventBusRabbitMQ.Models.Record> rabbitRecords = new List<EventBusRabbitMQ.Models.Record>();

                    foreach (var rec in records)
                    {
                        EventBusRabbitMQ.Models.Record rabbitRecord = new EventBusRabbitMQ.Models.Record()
                        {
                            RecordId = rec.RecordId,
                            Company = rec.Company,
                            Name = rec.Name,
                            Surname = rec.Surname
                        };

                        rabbitRecord.ContactInfos = new List<EventBusRabbitMQ.Models.ContactInfo>();

                        foreach (var contactInfo in rec.ContactInfos)
                        {
                            rabbitRecord.ContactInfos.Add(new EventBusRabbitMQ.Models.ContactInfo()
                            {
                                ContactInfoId = contactInfo.ContactInfoId,
                                Type = contactInfo.Type,
                                Value = contactInfo.Value
                            });
                        }

                        rabbitRecords.Add(rabbitRecord);
                    }

                    _eventBus.PublishSendRecords(EventBusConstants.SendRecordsQueue, new SendRecordsEvent()
                    {
                        RequestId = evt.RequestId,
                        Records = rabbitRecords
                    });
                }
            }
        }

        public void Disconnect()
        {
            _connection.Dispose();
        }
    }
}
