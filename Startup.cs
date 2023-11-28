﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace LightSwitcher
{
    public class Startup
    {
        private LightStatus lightStatus = new LightStatus { IsLightOn = false };

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    var templatePath = Path.Combine(env.ContentRootPath, "Templates", "index.html");

                    if (File.Exists(templatePath))
                    {
                        var content = File.ReadAllText(templatePath);
                        await context.Response.WriteAsync(content);
                    }
                    else
                    {
                        context.Response.StatusCode = 500; // Internal Server Error
                        await context.Response.WriteAsync("Template file not found.");
                    }
                });

                endpoints.MapGet("/toggle", async context =>
                {
                    lightStatus.IsLightOn = !lightStatus.IsLightOn;
                    await context.Response.WriteAsync(lightStatus.IsLightOn ? "on" : "off");
                });
            });
        }

    }
}