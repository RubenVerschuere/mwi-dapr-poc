using System.Text.Json.Serialization;
using Dapr.Client;
using Dapr;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

const string storeName = "statestore";

// Service invocation
//app.MapPost("/orders", async (Order order) =>
//{
//    Console.WriteLine("Order received : " + order);
//    var daprClient = new DaprClientBuilder().Build();
//    await daprClient.SaveStateAsync(storeName, order.OrderId.ToString(), order);

//    return order.ToString();
//});

//
//var daprClient = new DaprClientBuilder().Build();
//await daprClient.SaveStateAsync(storeName, order.OrderId.ToString(), order);

//.Pub / sub
app.MapSubscribeHandler();
app.UseCloudEvents();
app.MapPost("/orders", [Topic("order_pub_sub", "orders")] async (Order order) =>
{
    Console.WriteLine("Subscriber received : " + order);
    var daprClient = new DaprClientBuilder().Build();
    await daprClient.SaveStateAsync(storeName, order.OrderId.ToString(), order);
    return Results.Ok(order);
});

await app.RunAsync();

public record Order([property: JsonPropertyName("orderId")] int OrderId);
