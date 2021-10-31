using EventBusRabbitMQ;
using EventBusRabbitMQ.Events;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ReportService.Core.Models;
using ReportService.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportService.API.RabbitMQ
{
    public class EventBusRabbitMQConsumer
    {
        private readonly IRabbitMQConnection _connection;
        private readonly IServiceProvider _serviceProvider;

        public EventBusRabbitMQConsumer(IRabbitMQConnection connection, IServiceProvider serviceProvider)
        {
            _connection = connection;
            _serviceProvider = serviceProvider;
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
                using (var scope = _serviceProvider.CreateScope()) // this will use `IServiceScopeFactory` internally
                {
                    var reportService = scope.ServiceProvider.GetService<IReportService>();

                    List<Record> records = new List<Record>();

                    foreach (var rec in sendRecordsEvent.Records)
                    {
                        Record reco = new Record();
                        reco.Company = rec.Company;
                        reco.Name = rec.Name;
                        reco.RecordId = rec.RecordId;
                        reco.Surname = rec.Surname;

                        reco.ContactInfos = new List<ContactInfo>();

                        foreach (var contactInfo in rec.ContactInfos)
                        {
                            ContactInfo ci = new ContactInfo()
                            {
                                ContactInfoId = contactInfo.ContactInfoId,
                                Type = contactInfo.Type,
                                Value = contactInfo.Value
                            };

                            reco.ContactInfos.Add(ci);
                        }

                        records.Add(reco);
                    }

                    reportService.UpdateReport(sendRecordsEvent.RequestId, records);
                }
            }
        }

        public void Disconnect()
        {
            _connection.Dispose();
        }
    }
}
