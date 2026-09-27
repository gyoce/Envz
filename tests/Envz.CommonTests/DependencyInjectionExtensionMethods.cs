using Microsoft.Extensions.DependencyInjection;

namespace Envz.CommonTests;

public static class DependencyInjectionExtensionMethods
{
    extension(IServiceCollection services)
    {
        public IServiceCollection Replace<TOld, TNew>()
            where TOld : class
            where TNew : class
        {
            ServiceDescriptor? serviceDescriptor = services.FirstOrDefault(sd => sd.ServiceType == typeof(TOld));
            if (serviceDescriptor is null)
                throw new Exception($"No service of type {typeof(TOld)} was found.");

            services.Remove(serviceDescriptor);
            services.Add(new ServiceDescriptor(typeof(TOld), typeof(TNew), serviceDescriptor.Lifetime));
            return services;
        }

        public IServiceCollection ReplaceByMock<TService>(Mock<TService> mock)
            where TService : class
        {
            ServiceDescriptor? serviceDescriptor = services.FirstOrDefault(sd => sd.ServiceType == typeof(TService));
            if (serviceDescriptor is null)
                throw new Exception($"No service of type {typeof(TService)} was found.");

            services.Remove(serviceDescriptor);
            services.Add(new ServiceDescriptor(typeof(TService), _ => mock.Object, serviceDescriptor.Lifetime));
            return services;
        }
    }
}