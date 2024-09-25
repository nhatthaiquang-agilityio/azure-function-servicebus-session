using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace ExampleAzureFunctionWithServiceBus
{
    public class WelcomeTrigger
    {
        private readonly ILogger<WelcomeTrigger> _logger;

        public WelcomeTrigger(ILogger<WelcomeTrigger> logger)
        {
            _logger = logger;
        }

        [Function("Welcome")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            return new OkObjectResult("Welcome to Azure Functions!");
        }
    }
}