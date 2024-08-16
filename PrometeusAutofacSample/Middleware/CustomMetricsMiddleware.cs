using Prometheus;
using System.Diagnostics;

namespace PrometeusAutofacSample.Middleware
{
    public class CustomMetricsMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly Histogram _requestDurationHistogram;

        public CustomMetricsMiddleware(RequestDelegate next)
        {
            _next = next;
            _requestDurationHistogram = Metrics.CreateHistogram("custom_request_duration_seconds",
                "Duration of HTTP requests handled by the application",
                new HistogramConfiguration
                {
                    Buckets = Histogram.LinearBuckets(start: 0.1, width: 0.1, count: 10)
                });
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();

            _requestDurationHistogram.Observe(stopwatch.Elapsed.TotalSeconds);

            await _next(context);

            stopwatch.Stop();
        }
    }
}
