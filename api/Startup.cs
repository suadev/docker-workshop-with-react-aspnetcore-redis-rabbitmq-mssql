using System;
using System.Text;
using api;
using api.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace aspnet_core_docker_workshop
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

            services.AddMvc();
            services.AddSingleton<RabbitListener>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors("AllowAll");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.UseRabbitListener();

            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<TodoListDBContext>())
                {
                    context.Database.Migrate();
                }
            }
        }
    }
}
