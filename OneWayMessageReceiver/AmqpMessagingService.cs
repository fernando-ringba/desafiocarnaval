using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace OneWayMessageSender
{
	public class AmqpMessagingService
	{
		private string _hostName = "localhost";
		private string _userName = "guest";
		private string _password = "guest";
		private string _exchangeName = "";
		private string _oneWayMessageQueueName = "OneWayMessageQueue";
		private bool _durable = true;

		public IConnection GetRabbitMqConnection()
		{
			ConnectionFactory connectionFactory = new ConnectionFactory();
			connectionFactory.HostName = _hostName;
			connectionFactory.UserName = _userName;
			connectionFactory.Password = _password;

			return connectionFactory.CreateConnection();
		}

		public void SetUpQueueForOneWayMessageDemo(IModel model)
		{
			model.QueueDeclare(_oneWayMessageQueueName, _durable, false, false, null);
		}

		public void ReceiveOneWayMessages(IModel model)
		{
			model.BasicQos(0, 1, false); //basic quality of service
			QueueingBasicConsumer consumer = new QueueingBasicConsumer(model);
			model.BasicConsume(_oneWayMessageQueueName, false, consumer);
			while (true)
			{
				BasicDeliverEventArgs deliveryArguments = consumer.Queue.Dequeue() as BasicDeliverEventArgs;
				String message = Encoding.UTF8.GetString(deliveryArguments.Body);
				Console.WriteLine("Message received: {0}", message);
				model.BasicAck(deliveryArguments.DeliveryTag, false);
			}
		}
	}
}
