using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.WebEncoders.Testing;
using RabbitMQ.Client;
using RabbitMQ.Client.Framing;

namespace desafiocarnaval
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

			services.AddSingleton(ConfiguraRabbit());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

	    public static IModel ConfiguraRabbit()
	    {
		    IAmqpMessagingService messagingService = new AmqpMessagingService();
		    var _connection = messagingService.GetRabbitMqConnection();
		    var _model = _connection.CreateModel();
			
		    var msgservicetask = new Task(() =>messagingService.ReceiveOneWayMessages(_model));

		    msgservicetask.Start();

		    return _model;
	    }


    }
}
