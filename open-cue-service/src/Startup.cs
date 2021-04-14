using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace OpenCue.Service
{
    using Controllers;
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
            services.Configure<Config>(Configuration);

            services.AddSingleton<SdkHandler>();
            services.AddSingleton<ProfileManager>();

            services.AddHostedService<SyncService>();

            services.AddControllers(options =>
            {
                options.Filters.Add(new SdkExceptionFilter());
                options.Filters.Add(new ApiErrorFilter());
            });

            services.AddOpenApiDocument(document =>
            {
                document.DocumentName = "openapi";
                document.Title = "Open CUE Service";
                document.Description = "HTTP REST API service for Open CUE CLI";
                document.Version = "0.5.0";
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseOpenApi(options =>
            {
                options.DocumentName = "openapi";
                options.Path = "/openapi/v1/openapi.json";
            });
            app.UseSwaggerUi3(options =>
            {
                options.Path = "/openapi";
                options.DocumentPath = "/openapi/v1/openapi.json";
                options.DocExpansion = "list";
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
