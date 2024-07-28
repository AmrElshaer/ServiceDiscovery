using FastEndpoints;
using MediatR;
using OrderService.Persistence;
using OrderService.Shipping;

namespace OrderService.Orders
{
    internal static class CreateOrder
    {
        
        internal readonly record struct Request(decimal TotalPrice,string Address):IRequest;
        internal class  Handler(ISender sender ,OrderContext orderContext):IRequestHandler<Request>
        {
            public async Task Handle(Request request, CancellationToken cancellationToken)
            {
                int orderCount = orderContext.Orders.Count;
                Order order = new(orderCount+1,request.TotalPrice, request.Address);
                orderContext.Orders.Add(order);
                CreateShipping.Request createShippingReq = new(order.Id,order.TotalPrice);
                // use domain event or integration don't call handler in handler it's bad this for demo purpose
                await sender.Send(createShippingReq,cancellationToken);
            }
        }
        internal class Endpoint(ISender sender) : Endpoint<Request>
        {
            public override void Configure()
            {

                Post("create-order");
                Group<OrderGroup>();
                AllowAnonymous();
            }

            public override async Task HandleAsync(Request req, CancellationToken ct)
            {
                await  sender.Send(req, ct);
            }
        }
    }
}
