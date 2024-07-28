using FastEndpoints;
using MediatR;
using ShippingService.Persistence;

namespace ShippingService.Shipping
{
    internal static class CreateShipping
    {
        internal  readonly record struct Request(int OrderId,decimal TotalPrice):IRequest;
        internal class Handler(ShippingContext context):IRequestHandler<Request>
        {
            public Task Handle(Request request, CancellationToken cancellationToken)
            {
               
                OrderShipping entity = new(request.OrderId,request.TotalPrice*0.1m);
                context.OrdersShipping.Add(entity);
                return Task.CompletedTask;
            }
        }

        internal class Endpoint(ISender sender) : Endpoint<Request>
        {
            public override void Configure()
            {
                Post("create-shipping");
                Group<ShippingGroup>();
                AllowAnonymous();
            }

            public override async Task HandleAsync(Request req, CancellationToken ct)
            {
                 await sender.Send(req,ct);

                 await SendOkAsync(ct);

            }
        }
    }
}
