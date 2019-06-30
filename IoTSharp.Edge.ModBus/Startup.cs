﻿using IoT.Things.ModBus.Jobs;
using IoT.Things.ModBus.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Quartz;
using QuartzHostedService;
using Quartzmin;
using System;
using System.Linq;
using System.Reflection;

namespace IoT.Things.ModBus
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
            services.AddQuartzmin();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddOptions();
            services.Configure<AppSettings>(Configuration);
            services.AddLogging(builder =>
            {
                builder.AddConsole();
            });
            services.AddQuartzHostedService();
            services.AddTransient<Slaver>();
            services.AddHostedService<ModBusService>();
            services.AddSingleton(options =>
            {
                var mqtt = new IoTSharp.EdgeSdk.MQTT.MQTTClient();
                return mqtt;
            });
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IOptions<AppSettings> options, ISchedulerFactory factory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseQuartzmin(new QuartzminOptions()
            {
                Scheduler = factory.GetScheduler().Result,
                ProductName = typeof(Startup).Assembly.GetName().Name,
            });
            app.UseMvc();
            
         
        }
    }
}