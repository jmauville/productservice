using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.ModelBinding;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using productservice.Infrastructure;

namespace productservice
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile("config.json")
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();

            // one a few ways to remove XML formatters or force JSON
            // see http://blogs.msdn.com/b/webdev/archive/2014/11/24/content-negotiation-in-mvc-5-or-how-can-i-just-write-json.aspx
            //services.Configure<MvcOptions>(options =>
            //                                    options
            //                                        .OutputFormatters
            //                                        .RemoveAll(formatter => formatter.Instance is XmlDataContractSerializerOutputFormatter)
            //                               );

            //configure the options <classname> and bind from the configuration, uses the IOptions interface Microsoft.Framework.OptionsModel
            services.Configure<Settings>(Configuration);

            //add the speaker repository to the service collection
            services.AddSingleton<IProductRepository, ProductRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseIISPlatformHandler();

            app.UseStaticFiles();

            app.UseMvc();
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
