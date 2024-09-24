// See https://aka.ms/new-console-template for more information
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;
using System.Text;

Console.WriteLine("Hello, World!");
var connectionString = Environment.GetEnvironmentVariable("ServiceBus");
var queueName = "myqueue";

int sessions = 10;
int messagePerSession = 2;

Console.WriteLine("Creating Service Bus sender....");
var taskList = new List<Task>();
var sender = new MessageSender(connectionString, queueName);

for(int s = 0; s < sessions; s++)
{
    var sessionId = $"Patient-{s.ToString()}";
    var messageList = new List<Message>();

    for(int m = 0; m < messagePerSession; m++)
    {
        var message = new Message(Encoding.UTF8.GetBytes($"Message-{m}"))
        {
            SessionId = sessionId
        };
        messageList.Add(message);
    }

    taskList.Add(sender.SendAsync(messageList));
}

Console.WriteLine("Sending all messages...");

await Task.WhenAll(taskList);

Console.WriteLine("All messages sent.");