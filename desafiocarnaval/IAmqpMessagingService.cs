using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace desafiocarnaval
{
	public interface IAmqpMessagingService
	{
		IConnection GetRabbitMqConnection();
		void ReceiveOneWayMessages(IModel model);
	}
}
