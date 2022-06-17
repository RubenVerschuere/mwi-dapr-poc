using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Dapr.Client;


for (int i = 1; i <= 900000; i++) {
    var order = new Order(i);

    // Invoking a service
    //var orderJson = JsonSerializer.Serialize<Order>(order);
    //var content = new StringContent(orderJson, Encoding.UTF8, "application/json");
    //var daprClient = DaprClient.CreateInvokeHttpClient("order-processor-demo");
    //var response = await daprClient.PostAsync("/orders", content);
    //Console.WriteLine("Order passed: " + order);

    // Pub/sub
    using var client = new DaprClientBuilder().Build();
    await client.PublishEventAsync("order_pub_sub", "orders", order);
    Console.WriteLine("Published data: " + order);

    await Task.Delay(TimeSpan.FromSeconds(1));
}

public record Order([property: JsonPropertyName("orderId")] int OrderId);
