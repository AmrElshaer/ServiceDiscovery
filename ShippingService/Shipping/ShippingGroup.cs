using FastEndpoints;

namespace ShippingService.Shipping
{
    public sealed class ShippingGroup : Group
    {
        public ShippingGroup()
        {
            Configure("shipping", ep =>
            {
                ep.Description(x => x
                    .WithTags("shipping"));
            });
        }
    }
}
