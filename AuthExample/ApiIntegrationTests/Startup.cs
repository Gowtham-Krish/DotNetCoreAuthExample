using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Text;
using WeatherApp;
using Xunit;
using Xunit.Abstractions;
using Xunit.DependencyInjection;

[assembly: TestFramework("ApiIntegrationTests.Startup", "ApiIntegrationTests")]
namespace ApiIntegrationTests
{
    public class Startup : DependencyInjectionTestFramework
    {
        public Startup(IMessageSink messageSink) : base(messageSink) { }

        protected void ConfigureServices(IServiceCollection services)
        {
            var configuration = new ConfigurationBuilder()
                   .AddJsonFile("appSettings.json")
                   .Build();
            services.AddHttpClient();
            services.AddSingleton<IConfiguration>(configuration);
            services.AddTransient<ITokenManager, TokenManager>();
        }

        protected override IHostBuilder CreateHostBuilder(AssemblyName assemblyName) =>
            base.CreateHostBuilder(assemblyName)
                .ConfigureServices(ConfigureServices);
    }
}
