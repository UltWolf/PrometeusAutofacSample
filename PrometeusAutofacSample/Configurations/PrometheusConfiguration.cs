using Prometheus;

namespace PrometeusAutofacSample.Configurations
{
    public class PrometheusConfiguration
    {
        public static void ConfigurePrometheus(IConfiguration configuration)
        {
            var prometheusConfigSection = configuration.GetSection("Prometheus");


            var bucketWidth = prometheusConfigSection.GetValue<double>("HistogramBucketWidth", 0.1);
            var bucketCount = prometheusConfigSection.GetValue<int>("HistogramBucketCount", 10);

            Metrics.DefaultRegistry.AddBeforeCollectCallback(() =>
            {
                Console.WriteLine(bucketCount);
                Console.WriteLine(bucketWidth);
            });
        }
    }
}
