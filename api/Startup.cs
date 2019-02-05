using api;
using api.Data;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace aspnet_core_docker_workshop
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IRedisDatabase, RedisDatabase>();

            services.AddMvc()
            .AddJsonOptions(options =>
            {
                options.SerializerSettings.DateFormatString = "dd.MM.yyyy";
            });

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                              .AllowAnyMethod()
                                .AllowAnyHeader()
                                .AllowCredentials()
                                .Build();
                    });
            });

            // var connectionStr = "Server=localhost;database=docker-workshop;user id=sa;password=Brt_z!py;MultipleActiveResultSets=True;"; //local
            var connectionStr = "Server=mssql;database=docker-workshop;user id=sa;password=Brt_z!py;"; //container
            services.AddDbContext<TodoListDBContext>(options => options.UseSqlServer(connectionStr));


            services.AddSingleton<RabbitListener>();

            services
            .AddHealthChecksUI()
            .AddHealthChecks()
                .AddSqlServer(connectionString: connectionStr);
            // .AddRedis(redisConnectionString: "redis")
            // .AddRabbitMQ(rabbitMQConnectionString: "rabbitmq");

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors("AllowAll");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<TodoListDBContext>())
                {
                    context.Database.Migrate();
                }
            }

            app.UseRabbitListener();
            app.UseHealthChecks("/healthcheck", new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            })
            .UseHealthChecksUI(setup =>
            {
                setup.UIPath = "/healthcheck-ui"; // this is ui path in your browser
                setup.ApiPath = "/healthcheck-ui-api"; // the UI ( spa app )  use this path to get information from the store ( this is NOT the healthz path, is internal ui api )
            })
            .UseMvc();
        }
    }
}
