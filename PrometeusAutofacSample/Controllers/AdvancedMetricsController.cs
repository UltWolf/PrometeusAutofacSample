using Microsoft.AspNetCore.Mvc;
using PrometeusAutofacSample.Services.Abstract;

namespace PrometeusAutofacSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AdvancedMetricsController : ControllerBase
    {
        private readonly IWorkerService _workerService;

        public AdvancedMetricsController(IWorkerService workerService)
        {
            _workerService = workerService;
        }

        [HttpGet("run")]
        public async Task<IActionResult> RunTask([FromQuery] string taskName)
        {
            await Task.Run(() => _workerService.ProcessTask(taskName));
            return Ok($"Task '{taskName}' completed.");
        }

        [HttpGet("health")]
        public IActionResult CheckHealth()
        {
            return Ok("Healthy");
        }
    }
}
