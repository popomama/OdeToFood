using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Routing;
using OdeToFood.Services;
using OdeToFood.Entities;
using Microsoft.EntityFrameworkCore;

namespace OdeToFood
{
    public class Startup
    {

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(env.ContentRootPath).AddJsonFile("appsettings.json");
                       //Configuration = builder.Build();

            builder.Build();
            //Greeters = new Greeter(Configuration);
        }

        public IConfiguration  Configuration { get; set; }

        public IGreeter Greeters { get; set; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddMvc();
            services.AddSingleton<IGreeter, Greeter>();
            //           services.AddSingleton(Configuration); //looks like we don't need to call this line at all, as other wise this should be added before IGreeter service is added
            //services.AddScoped<IRestaurantData, InMemoryRestaurantData>();
            services.AddScoped<IRestaurantData, SqlRestaurantData>();
            services.AddDbContext<OdeToFoodDbContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("OdeToFood")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IGreeter greeter)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(new ExceptionHandlerOptions
                {

                    ExceptionHandler = context => context.Response.WriteAsync("Oops")
                });
            }
            //app.UseDefaultFiles();
            //app.UseStaticFiles();
            app.UseFileServer();

            //app.UseWelcomePage("/welcome");

            //app.Run(async (context) =>
            //{

            //    //throw new Exception("wrong here");
            //    var message = greeter.GetGreeting();//Configuration["Greeting"];
            //await context.Response.WriteAsync(message);
            //});

            //app.UseMvcWithDefaultRoute();
            app.UseMvc(configureRoutes);
            app.Run(ctx => ctx.Response.WriteAsync("Not found"));
        }

        private void configureRoutes(IRouteBuilder routeBuilder)
        {
            // /Home/Index/
            routeBuilder.MapRoute("Default", "{controller=Home}/{action=Index}/{id?}");
        }
    }
}
