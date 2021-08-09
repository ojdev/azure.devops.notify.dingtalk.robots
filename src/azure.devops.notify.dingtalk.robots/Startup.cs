using azure.devops.notify.dingtalk.robots.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NSwag;

namespace azure.devops.notify.dingtalk.robots
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
            services.AddControllers()
                .AddNewtonsoftJson();
            services.AddScoped<IDingTalkService, DingTalkService>();
            services.AddOpenApiDocument(c =>
            {
                c.PostProcess = document =>
                {
                    document.Info.Title = "AzureDevOps To Dingtalk Notify";
                    document.Info.Version = "v1";
                    document.Info.Description = "AzureDevOps To Dingtalk Notify";
                    document.Info.Contact = new OpenApiContact
                    {
                        Name = "lujing.tech",
                        Email = "admin@lujing.tech",
                        Url = "https://www.lujing.tech/"
                    };
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseOpenApi().UseSwaggerUi3();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
