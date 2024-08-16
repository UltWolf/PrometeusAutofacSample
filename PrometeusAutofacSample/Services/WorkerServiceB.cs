using PrometeusAutofacSample.Services.Abstract;
using Prometheus;

namespace PrometeusAutofacSample.Services
{
    public class WorkerServiceB : IWorkerService
    {
        private readonly Summary _taskSummary;

        public WorkerServiceB()
        {
            _taskSummary = Metrics.CreateSummary("worker_b_task_summary", "Summary of tasks processed by Worker B.",
                new SummaryConfiguration
                {
                    Objectives = new[]
                    {
                    new QuantileEpsilonPair(0.5, 0.05),
                    new QuantileEpsilonPair(0.9, 0.01),
                    new QuantileEpsilonPair(0.99, 0.001)
                    }
                });
        }

        public void ProcessTask(string taskName)
        {
            _taskSummary.Observe(taskName.Length);
            // Simulate task processing
            System.Threading.Thread.Sleep(new Random().Next(200, 1000));
        }
    }
}
