using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.DurableTask;
using Microsoft.DurableTask.Client;
using Microsoft.Extensions.Logging;

namespace Spurious2.Function2;

public class Function2
{
    private readonly ILogger<Function2> logger;
    public Function2(ILoggerFactory loggerFactory)
    {
        this.logger = loggerFactory.CreateLogger<Function2>();
    }

    [Function("Function_HttpStart")]
    public async Task<HttpResponseData> HttpStart(
       [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req,
       [DurableClient] DurableTaskClient client,
       FunctionContext executionContext)
    {
        //var logger = loggerFactory.CreateLogger<Function2>();
        var logger = executionContext.GetLogger<Function2>();

        // Function input comes from the request content.
        string instanceId = await client.ScheduleNewOrchestrationInstanceAsync(
            nameof(Function2));

        logger.LogInformation("Started orchestration with ID = '{instanceId}'.", instanceId);

        // Returns an HTTP 202 response with an instance management payload.
        // See https://learn.microsoft.com/azure/azure-functions/durable/durable-functions-http-api#start-orchestration
        return await client.CreateCheckStatusResponseAsync(req, instanceId);
    }

    [Function(nameof(Function2))]
    public async Task<string> RunOrchestrator(
        [OrchestrationTrigger] TaskOrchestrationContext context)
    {
        var logger = context.CreateReplaySafeLogger<Function2>();
        logger.LogInformation("Saying hello.");
        string result = "";
        result += await context.CallActivityAsync<string>(nameof(SayHello), "Tokyo") + " ";
        result += await context.CallActivityAsync<string>(nameof(SayHello), "London") + " ";
        result += await context.CallActivityAsync<string>(nameof(SayHello), "Seattle");
        return result;
    }

    [Function(nameof(SayHello))]
    public string SayHello([ActivityTrigger] string name, FunctionContext executionContext)
    {
        var logger = executionContext.GetLogger<Function2>();
        logger.LogInformation("Saying hello to {name}.", name);
        return $"Hello {name}!";
    }
}