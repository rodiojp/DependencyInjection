using System;
using Microsoft.Extensions.DependencyInjection;

namespace DependencyLibrary
{
    public class ContainerBuilder
    {
        public IServiceProvider Build()
        {
            ServiceCollection container = new ServiceCollection();
            container.AddSingleton<IProductStockRepo, ProductStockRepo>();
            container.AddSingleton<IShippingProcessor, ShippingProcessor>();
            container.AddSingleton<IPaymentProcessor, PaymentProcessor>();
            container.AddSingleton<IOrderManager, OrderManager>();
            return container.BuildServiceProvider();
        }
    }
}