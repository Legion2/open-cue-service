using System.Threading.Tasks;
using System;
using System.Threading;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace OpenCueService
{
    using CgSdk;

    public class SyncService : IHostedService, IDisposable
    {
        private readonly ILogger _logger;
        public SyncService(ILogger<SyncService> logger, ProfileManager profileManager)
        {
            _logger = logger;
            ProfileManager = profileManager;
        }

        public ProfileManager ProfileManager { get; }

        private Timer? _timer;

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(10));
            return Task.CompletedTask;
        }

        private void DoWork(object? state)
        {
            try
            {
                ProfileManager.Sync();
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
