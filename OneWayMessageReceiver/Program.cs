using System;
using System.Text;
using OneWayMessageSender;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace OneWayMessageReceiver
{
	class Program
	{
		static void Main(string[] args)
		{
			AmqpMessagingService messagingService = new AmqpMessagingService();
			IConnection connection = messagingService.GetRabbitMqConnection();
			IModel model = connection.CreateModel();
			messagingService.ReceiveOneWayMessages(model);
		}
	}
}
