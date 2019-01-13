using aspnet_core_docker_workshop;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace api
{
    public static class ApplicationBuilderExtentions
    {
        private static RabbitListener _listener { get; set; }
        public static IApplicationBuilder UseRabbitListener(this IApplicationBuilder app)
        {
            _listener = (RabbitListener)app.ApplicationServices.GetService(typeof(RabbitListener));

            var lifetime = (IApplicationLifetime)app.ApplicationServices.GetService(typeof(IApplicationLifetime));

            lifetime.ApplicationStarted.Register(OnStarted);

            lifetime.ApplicationStopping.Register(OnStopping);

            return app;
        }

        private static void OnStarted()
        {
            _listener.Register();
        }

        private static void OnStopping()
        {
            _listener.Deregister();
        }
    }

}