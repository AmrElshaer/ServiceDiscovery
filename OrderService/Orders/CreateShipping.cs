

using MediatR;

namespace OrderService.Orders
{
    internal static class CreateShipping
    {
        internal sealed class Client(HttpClient http)
        {

            public async Task CreateShipping(OrderCreatedEvent shippingRequest)
            {
                using var response = await http.PostAsJsonAsync("api/shipping/create-shipping", shippingRequest);
                response.EnsureSuccessStatusCode();
            }
        }
        internal sealed class Handler(Client client) : INotificationHandler<OrderCreatedEvent>
        {
            public async Task Handle(OrderCreatedEvent request, CancellationToken cancellationToken)
            {
                await client.CreateShipping(request);
            }
        }

    }
}
