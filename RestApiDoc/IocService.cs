using Microsoft.Extensions.DependencyInjection;

namespace RestApiDoc
{
    public static class IocService
    {
        private static ServiceProvider service;

        public static T? Get<T>()
        {
            return service.GetService<T>();
        }

        public static void Initialize(ServiceCollection modules)
        {
            if (service is null)
            {
                service = modules.BuildServiceProvider();
            }
        }
    }
}