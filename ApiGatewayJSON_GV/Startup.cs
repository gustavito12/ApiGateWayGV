using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AdevintaApiGateway.Aggregators;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using AdevintaApiGateway.Handlers;

namespace AdevintaApiGateway
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // Este método es llamado por el tiempo de ejecución. Utilice este método para agregar servicios al contenedor.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOcelot()
                .AddDelegatingHandler<RemoveEncodingDelegatingHandler>(true)
                .AddSingletonDefinedAggregator<UsersPostsAggregator>();
        }

        // Este método es llamado por el tiempo de ejecución. Utilice este método para configurar la canalización de solicitudes HTTP.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseOcelot().Wait();
        }
    }
}
