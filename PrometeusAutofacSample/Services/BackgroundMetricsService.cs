using Prometheus;

namespace PrometeusAutofacSample.Services
{
    public class BackgroundMetricsService : IHostedService, IDisposable
    {
        private Timer _timer;
        private readonly Gauge _backgroundTaskGauge;

        public BackgroundMetricsService()
        {
            _backgroundTaskGauge = Metrics.CreateGauge("background_task_gauge", "Tracks the number of background tasks running");
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(10));
            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            _backgroundTaskGauge.Inc();
            try
            {
                Task.Delay(2000).Wait();
            }
            finally
            {
                _backgroundTaskGauge.Dec();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
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
