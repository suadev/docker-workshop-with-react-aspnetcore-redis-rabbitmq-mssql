using api.Data;
using Microsoft.AspNetCore.Builder;
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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IRedisDatabase, RedisDatabase>();

            services.AddMvc();
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

            var connectionStr = "Server=localhost;database=docker-workshop;user id=sa;password=Brt_z!py;MultipleActiveResultSets=True;";
            services.AddDbContext<TodoListDBContext>(options => options.UseSqlServer(connectionStr));

            //           services.AddEntityFrameworkSqlServer();
            // services.AddDbContext<TodoListDBContext>((serviceProvider, options) =>
            // {
            //     options.UseSqlServer(connectionStr)
            //     .UseInternalServiceProvider(serviceProvider);
            // });


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
