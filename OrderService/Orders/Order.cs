using Shared.Events;

namespace OrderService.Orders
{
    public class Order
    {
        public int Id { get; private set; }
        public decimal TotalPrice { get; private set; }
        public string Address { get; private set; }

        public Order(int id, decimal totalPrice,string address)
        {
            Id = id;
            TotalPrice = totalPrice;
            Address = address;
            var orderCreatedEvent = new OrderCreatedEvent(id,totalPrice);
            DomainEvents.Raise(orderCreatedEvent).Wait();
        }
    }
}
