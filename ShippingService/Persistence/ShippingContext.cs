using ShippingService.Shipping;

namespace ShippingService.Persistence
{
    public class ShippingContext
    {
        public List<OrderShipping> OrdersShipping { get; set; } = [];

    }
}
