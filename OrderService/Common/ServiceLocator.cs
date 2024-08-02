namespace OrderService.Common
{
    public class ServiceLocator(ServiceProvider currentServiceProvider)
    {
        private static ServiceProvider _serviceProvider=default!;

        public static ServiceLocator Current => new(_serviceProvider);

        public static void SetLocatorProvider(ServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public object GetInstance(Type serviceType)
        {
            return currentServiceProvider.GetService(serviceType)!;
        }

        public TService GetInstance<TService>()
        {
            return currentServiceProvider.GetService<TService>()!;
        }
    }
}
