using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Couchbase;
using Couchbase.Authentication;
using Couchbase.Configuration.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace emonthly_web
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            ClusterHelper.Initialize(new ClientConfiguration
            {
                Servers = new List<Uri> { new Uri("couchbase://ec2-52-47-100-5.eu-west-3.compute.amazonaws.com") }
            }, new PasswordAuthenticator("emonthly", "password"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime applicationLifeTime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();

            applicationLifeTime.ApplicationStopped.Register(() =>
            {
                ClusterHelper.Close();
            });
        }
    }
}
