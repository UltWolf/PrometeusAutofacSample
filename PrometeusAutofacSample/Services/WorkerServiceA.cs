using PrometeusAutofacSample.Services.Abstract;
using Prometheus;

namespace PrometeusAutofacSample.Services
{
    public class WorkerServiceA : IWorkerService
    {
        private readonly Counter _taskCounter;
        private readonly Histogram _taskDuration;

        public WorkerServiceA()
        {
            _taskCounter = Metrics.CreateCounter("worker_a_task_counter", "Number of tasks processed by Worker A.");
            _taskDuration = Metrics.CreateHistogram("worker_a_task_duration_seconds", "Duration of tasks processed by Worker A.");
        }

        public void ProcessTask(string taskName)
        {
            _taskCounter.Inc();

            using (_taskDuration.NewTimer())
            {
                // Simulate task processing
                System.Threading.Thread.Sleep(new Random().Next(500, 1500));
            }
        }
    }
}
