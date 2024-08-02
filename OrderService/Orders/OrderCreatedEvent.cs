using MediatR;

namespace OrderService.Orders
{
    public record OrderCreatedEvent(int OrderId, decimal TotalPrice):INotification;
}
