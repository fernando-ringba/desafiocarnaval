using System;
using RabbitMQ.Client;

namespace OneWayMessageSender
{
    class Program
    {
        static void Main(string[] args)
        {

	        AmqpMessagingService messagingService = new AmqpMessagingService();
	        IConnection connection = messagingService.GetRabbitMqConnection();
	        IModel model = connection.CreateModel();
	        //messagingService.SetUpQueueForOneWayMessageDemo(model);
			
	        RunOneWayMessageDemo(model, messagingService);
 
	        Console.ReadKey();

        }

	    private static void RunOneWayMessageDemo(IModel model, AmqpMessagingService messagingService)
	    {
		    Console.WriteLine("Enter your message and press Enter. Quit with 'q'.");
		    while (true)
		    {
			    string message = Console.ReadLine();
			    if (message.ToLower() == "q") break;
 
			    messagingService.SendOneWayMessage(message, model);
		    }
	    }
    }
}
