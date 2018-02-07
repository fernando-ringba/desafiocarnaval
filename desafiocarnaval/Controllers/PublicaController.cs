using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;

namespace desafiocarnaval.Controllers
{
    [Produces("application/json")]
    [Route("api/Publica")]
    public class PublicaController : Controller
    {
	    private IModel _model;
	    private IBasicProperties _basicProperties;

		public PublicaController(IModel model)
		{
			_model = model;
			_basicProperties = Program._basicProperties;
		}

		// GET: api/Publica
		[HttpGet, Route("/api/publicados")]
        public async Task<IActionResult> Get()
		{
			try
			{
				string[] lines = System.IO.File.ReadAllLines(@"C:\Users\fmota\Desktop\teste\testando.txt");
				return Accepted(lines);
			}
			catch (Exception e)
			{
				return new StatusCodeResult(500);
			}
			
        }
		
        
        // POST: api/Publica
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]string value)
        {
	        try
	        {
		        byte[] mensagem = Encoding.UTF8.GetBytes(value);
		        _model.BasicPublish(Request.Headers["Exchange"],Request.Headers["RoutingKey"],_basicProperties, mensagem);
		        return Accepted();
	        }
	        catch (Exception e)
	        {
		        return new StatusCodeResult(500);
	        }
	        
        }
       
    }
}
