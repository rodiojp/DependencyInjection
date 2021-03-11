using Microsoft.VisualStudio.TestTools.UnitTesting;
using DependencyLibrary;
using System;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace DependencyLibraryTest
{
    [TestClass]
    public class OrderManagerTest
    {
        public static readonly IServiceProvider Container = new ContainerBuilder().Build();

        [TestMethod]
        [DataRow(Product.Keyboard, "1010101010101010", "0622")]
        [ExpectedException(typeof(Exception), "Keyboard is currently not in stock.")]
        public void OrderManagerSubmitTest(Product product, string creditCardNumber, string expiryDate)
        {
            // Tests that we expect to get an Exception.
            var productStockRepoMock = new Mock<IProductStockRepo>();
            productStockRepoMock.Setup(m => m.IsInStock(It.IsAny<Product>()))
                                .Returns(false);
            var paymentProcessorMock = new Mock<IPaymentProcessor>();
            var shippingProcessorMock = new Mock<IShippingProcessor>();
            OrderManager orderManager = new OrderManager(productStockRepoMock.Object, paymentProcessorMock.Object, shippingProcessorMock.Object); 
            orderManager.Submit(product, creditCardNumber, expiryDate);

            // Sipping the same product to generate availability Exception
            orderManager.Submit(product, creditCardNumber, expiryDate);
        }

    }
}
