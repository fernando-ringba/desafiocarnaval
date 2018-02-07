using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;


namespace desafiocarnaval
{
	public class AmqpMessagingService : IAmqpMessagingService
	{
		private string _hostName = "localhost";
		private string _userName = "guest";
		private string _password = "guest";
		private string _exchangeName = "";
		private string _oneWayMessageQueueName = "OneWayMessageQueue";
		private string _queueDesafio = "queueDesafio";
		private bool _durable = true;

		public IConnection GetRabbitMqConnection()
		{
			ConnectionFactory connectionFactory = new ConnectionFactory();
			connectionFactory.HostName = _hostName;
			connectionFactory.UserName = _userName;
			connectionFactory.Password = _password;

			return connectionFactory.CreateConnection();
		}
		public void ReceiveOneWayMessages(IModel model)
		{
			model.BasicQos(0, 1, false); //basic quality of service
			QueueingBasicConsumer consumer = new QueueingBasicConsumer(model);
			model.BasicConsume(_queueDesafio, false, consumer);
			while (true)
			{
				BasicDeliverEventArgs deliveryArguments = consumer.Queue.Dequeue();
				String message = Encoding.UTF8.GetString(deliveryArguments.Body);

				using (System.IO.StreamWriter file =
					new System.IO.StreamWriter(@"C:\Users\fmota\Desktop\teste\testando.txt", true))
				{
					file.WriteLine(message);
				}


				model.BasicAck(deliveryArguments.DeliveryTag, false);
			}
		}
	}
}
