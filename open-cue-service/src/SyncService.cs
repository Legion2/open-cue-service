using System.Threading.Tasks;
using System;
using System.Threading;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OpenCue.Sdk;

namespace OpenCue.Service
{
    public class SyncService : IHostedService, IDisposable
    {
        private readonly ILogger _logger;

        private readonly int AutoSyncInterval;
        public SyncService(ILogger<SyncService> logger, IOptions<Config> config, ProfileManager profileManager)
        {
            _logger = logger;
            AutoSyncInterval = config.Value.AutoSyncInterval;
            ProfileManager = profileManager;
        }

        public ProfileManager ProfileManager { get; }

        private Timer? _timer;

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(AutoSyncInterval));
            return Task.CompletedTask;
        }

        private void DoWork(object? state)
        {
            try
            {
                ProfileManager.TriggerAutoSync();
            }
            catch (SdkError e)
            {
                _logger.LogWarning(e, "Ignoring exception thrown while syncing state");
            }
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
