using OrderService.Orders;

namespace OrderService.Persistence
{
    public class OrderContext
    {
        public List<Order> Orders { get; set; } =
        [
            new Order(1, 789, "cairo"),
            new Order(2, 6988, "Giza"),
        ];
    }
}
