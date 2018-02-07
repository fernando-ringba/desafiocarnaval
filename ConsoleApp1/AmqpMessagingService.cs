using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace desafiocarnaval
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

		public void SendOneWayMessage(string message, IModel model)
		{
			IBasicProperties basicProperties = model.CreateBasicProperties();
			basicProperties.SetPersistent(_durable);
			byte[] messageBytes = Encoding.UTF8.GetBytes(message);
			model.BasicPublish(_exchangeName, _oneWayMessageQueueName, basicProperties, messageBytes);
		}
	}
}
