using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace lab01
{
    public class Startup
    {
        
        public Startup(IConfiguration conf)
        {
            app = conf;
        }
        public IConfiguration app;
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<person>(app);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error");
                
            }

            app.UseStaticFiles();
            app.UseStatusCodePages();
            
            //app.UseStatusCodePagesWithReExecute("/hello", "?code={0}");
            
            app.Map("/error", ap => ap.Run(async context =>
            {
                await context.Response.WriteAsync("Error!");
            }));
            
            app.Map("/hello", ap => ap.Run(async context =>
            {
                await context.Response.WriteAsync($"Hel: {context.Request.Query["code"]}");
            }));
            
            
            app.Map("/p", P);
            
        }
        
        
        private static void P(IApplicationBuilder app)
        {
            app.UseStaticFiles();
            app.UseMiddleware<MiddlePerson>();
            app.Run(async context =>
            {
                await context.Response.WriteAsync(" ");
            });
        }
        
       
        
    }
}
