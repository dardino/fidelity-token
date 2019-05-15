using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace my_webapi
{
    public class Startup
    {
        private const string httpAddressStaging = "http://0.0.0.0:80";
        private const string httpsAddressStaging = "https://0.0.0.0:443";
        private const string httpAddressDev = "http://localhost:5000";
        private const string httpsAddressDev = "https://localhost:5001";

        public static string HttpAddress { get; set; } = httpAddressStaging;
        public static string HttpsAddress { get; set; } = httpsAddressStaging;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.Configure<Model.Nodes>(Configuration.GetSection("Nodes"));
        }

        public static string ContentRootPath;

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            Startup.ContentRootPath = env.ContentRootPath;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                HttpAddress = httpAddressDev;
                HttpsAddress = httpsAddressDev;
            }
            else if (env.IsStaging())
            {
                app.UseDeveloperExceptionPage();
                HttpAddress = httpAddressStaging;
                HttpsAddress = httpsAddressStaging;
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
