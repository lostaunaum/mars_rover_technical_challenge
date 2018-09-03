using MarsRoverTechnicalChallenge.Configuration;
using MarsRoverTechnicalChallenge.service.Interface;
using Microsoft.Extensions.DependencyInjection;
using MarsRoverTechnicalChallenge.service;
using Microsoft.Extensions.Configuration;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace MarsRoverTechnicalChallenge
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
            services.AddSingleton<IRoverRepository, RoverRepository>();
            services.Configure<ServiceConfiguration>(Configuration.GetSection("ServiceConfiguration"));
            services.AddOptions();
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "MarsRover.TechnicalChallenge", Version = "v1" });
            });
            
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //This can be added to a Swagger extension class
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mars Rover Tech Challenge V1"); });
            app.UseMvc();
        }
    }
}
