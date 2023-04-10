using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using TaskManager.Services.Services;

namespace TaskManager.Api
{
    public class ApiStartup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        
        public ApiStartup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Services.ServicesStartup.ConfigureServices(services, Configuration);
            
            services.AddCors(options => {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                policy => 
                                {
                                    policy.WithOrigins("https://localhost:44398", "https://localhost:5001");
                                }
                
                );
            });

            services.AddControllers();
            services.AddRazorPages();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Task Manager API", Version = "v1" });
            });

            services.AddScoped<IToDoService, ToDoService>();

            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IServiceProvider serviceProvider, IWebHostEnvironment env)
        {
            Services.ServicesStartup.Configure(serviceProvider);
            

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
               
                endpoints.MapControllers()
                    .RequireCors(MyAllowSpecificOrigins);

               

                endpoints.MapRazorPages();
            });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Task Manager API V1");
            });
        }
    }
}
