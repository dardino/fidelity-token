using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace cg_webapi
{
    public class Program
    {

#if !DEBUG
        public const string HttpAddress = "http://0.0.0.0:80";
        public const string HttpsAddress = "https://0.0.0.0:443";
#else
        public const string HttpAddress = "http://localhost:5000";
        public const string HttpsAddress = "https://localhost:5001";
#endif
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseUrls(HttpAddress, HttpsAddress)
                .UseStartup<Startup>();
    }
}
