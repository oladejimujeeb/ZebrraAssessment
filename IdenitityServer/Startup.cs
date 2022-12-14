using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using IdenitityServer.Configure;

namespace IdenitityServer
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentityServer().AddInMemoryClients(AuthenticationService.Clients)
                .AddInMemoryIdentityResources(AuthenticationService.IdentityResources)
                .AddInMemoryApiResources(AuthenticationService.ApiResources())
                .AddInMemoryApiScopes(AuthenticationService.ApiScopes)
                .AddTestUsers(AuthenticationService.TestUsers)
                .AddDeveloperSigningCredential(); ;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseIdentityServer();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Server Running");
                });
            });
        }
    }
}
