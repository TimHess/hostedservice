using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Steeltoe.Management.Endpoint.Health;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace hostedservice
{
    public class DlqProcessingHostedService : IHostedService
    {
        private readonly IApplicationLifetime _appLifetime;

        private readonly ILogger<DlqProcessingHostedService> _logger;

        private readonly IServiceScopeFactory _serviceScopeFactory;

        public DlqProcessingHostedService(IApplicationLifetime appLifetime,
            IServiceScopeFactory serviceScopeFactory,
            ILogger<DlqProcessingHostedService> logger)
        {
            _appLifetime = appLifetime;
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var he = scope.ServiceProvider.GetRequiredService<HealthEndpoint>();
                //var worker = scope.ServiceProvider.GetRequiredService<IWorker>();
                //CheckStartupHealth(he);
                //await worker.ProcessDlxMessages();
                Console.WriteLine("Waiting for a thing...");
                await Task.Delay(5000);
                Console.WriteLine("Done waiting for a thing.");
            }
            _appLifetime.StopApplication();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Finished executing task!");
        }
    }
}
