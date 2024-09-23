using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace ExampleAzureFunctionWithServiceBus
{
    public class ServiceBusTrigger
    {
        private readonly ILogger<ServiceBusTrigger> _logger;

        public ServiceBusTrigger(ILogger<ServiceBusTrigger> logger)
        {
            _logger = logger;
        }

        [Function(nameof(ServiceBusTrigger))]
        public async Task Run(
            [ServiceBusTrigger("myqueue", Connection = "ServiceBus")]
            ServiceBusReceivedMessage message,
            ServiceBusMessageActions messageActions)
        {
            _logger.LogInformation("ServiceBus Trigger function processed message");

            _logger.LogInformation("Message ID: {id}", message.MessageId);
            _logger.LogInformation("Message Body: {body}", message.Body);
            _logger.LogInformation("Message Content-Type: {contentType}", message.ContentType);

            // Complete the message
            await messageActions.CompleteMessageAsync(message);
        }
    }
}
