using FastEndpoints;
using MediatR;
using OrderService.Persistence;
using static OrderService.Orders.GetOrders.OrderDto;
namespace OrderService.Orders
{
    internal static class GetOrders
    {

        internal readonly record struct Request:IRequest<Response>;

        internal readonly record struct Response(IReadOnlyList<OrderDto> Orders);

        internal readonly record struct OrderDto(int Id, decimal TotalPrice, string Address)
        {
            public static Func<Order,OrderDto>  MapTo()
            {
                return order=> new OrderDto(order.Id, order.TotalPrice, order.Address);
            }
        }
        internal class Handler(OrderContext context):IRequestHandler<Request,Response>
        {
            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                List<OrderDto> orders = (await Task.FromResult(context.Orders))
                    .Select(MapTo()).ToList();

                return new Response(orders);
            }
        }
        internal class Endpoint(ISender sender) : EndpointWithoutRequest<Response>
        {
            public override void Configure()
            {
                
                Get("get-orders");
                Group<OrderGroup>();
                AllowAnonymous();
            }

            public  override async Task HandleAsync(CancellationToken ct)
            {
                Response response = await sender.Send(new Request(), ct);
                await SendAsync(response,cancellation:ct);
            }
        }
    }
}