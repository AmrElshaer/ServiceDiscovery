

using MediatR;

namespace OrderService.Shipping
{
    internal static class CreateShipping
    {
        internal readonly record struct Request(int OrderId, decimal TotalPrice):IRequest;
        internal sealed class Client(HttpClient http)
        {
           
            public async Task CreateShipping(Request shippingRequest)
            {
                using var response = await http.PostAsJsonAsync("api/shipping/create-shipping", shippingRequest);
                response.EnsureSuccessStatusCode();
            }
        }
        internal sealed class Handler(Client client):IRequestHandler<Request>
        {
            public async Task Handle(Request request, CancellationToken cancellationToken)
            {
                await client.CreateShipping(request);
            }
        }

    }
}
