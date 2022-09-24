using Autofac;
using CarBuddy.WebApi.Config;
using CarBuddy.WebApi.Extensions;
using CarBuddy.WebApi.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace CarBuddy.WebApi
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration) => _configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDefaultDbContext(_configuration.GetConnectionString("DefaultConnection"));
            services.AddDefaultCors("CorsPolicy", _configuration["ClientUrl"]);
            services.AddDefaultAuthentication(_configuration);
            services.AddDefaultAuthorization();
            services.AddSignalR();
            services.AddSingleton(_ => new SmtpConfig(_configuration.GetSection("Smtp")));
            services.AddMvc();

            services.AddSwaggerGen(options =>
                  options.SwaggerDoc("v1", new OpenApiInfo { Title = "CarBuddy API", Version = "v1" }));

            services.AddControllers(options => options.EnableEndpointRouting = false)
                .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterDependencies();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CarBuddy API V1"));
            }

            app.UseRouting();
            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStaticFiles();
            app.UseMiddleware<ExceptionMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHubs();
                endpoints.MapControllers();
            });
        }
    }
}
