using FastEndpoints;

namespace OrderService.Orders
{
    public sealed class OrderGroup : Group
    {
        public OrderGroup()
        {
            Configure("orders", ep => 
            {
                ep.Description(x => x
                    .WithTags("orders"));
            });
        }
    }
}
