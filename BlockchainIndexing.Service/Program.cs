using BlockchainIndexing.Application.Services;
using BlockchainIndexing.Application.Services.Interfaces;
using BlockchainIndexing.Data.Context;
using BlockchainIndexing.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Serilog;
using System.Net;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureLogging(loggingBuilder =>
            {
                var configuration = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build();

                var logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(configuration)
                    .CreateLogger();
                loggingBuilder.AddSerilog(logger, true);
            })
            .ConfigureServices((hostContext, services) =>
            {
                var configuration = hostContext.Configuration;

                services.AddDbContext<BlockchainContext>(options =>
                {
                    options.UseMySQL(configuration.GetConnectionString("DefaultConnection"));
                });

                services.AddScoped<BlockchainContext>();
                services.AddScoped<IIndexingService, IndexingService>();
                services.AddScoped<IEtherscanService, EtherscanService>();

                services.AddHttpClient<IEtherscanService, EtherscanService>(clientConfig =>
                {
                    var baseUri = $"{configuration["BaseUri"]}&apiKey={configuration["ApiKey"]}";
                    clientConfig.BaseAddress = new Uri(configuration.GetValue<string>("BaseUri"));
                })
                .SetHandlerLifetime(TimeSpan.FromMinutes(1))
                .AddPolicyHandler(policy =>
                {
                    var maxRetryAttempts = configuration.GetValue<int>("MaxRetryAttemps");
                    return Policy<HttpResponseMessage>
                        .Handle<HttpRequestException>()
                        .OrResult(x => x.StatusCode is >= HttpStatusCode.InternalServerError or HttpStatusCode.RequestTimeout)
                        .WaitAndRetryAsync(maxRetryAttempts, retryAttemps => TimeSpan.FromSeconds(10));
                });

                services.AddHostedService<Worker>();
            });
}