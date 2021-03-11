using System;

namespace DependencyLibrary
{
    public interface IOrderManager
    {
        void Submit(Product product, string creditCardNumber, string expiryDate);
    }
    public class OrderManager : IOrderManager
    {
        private readonly IProductStockRepo _productStockRepo;
        private readonly IPaymentProcessor _paymentProcessor;
        private readonly IShippingProcessor _shippingProcessor;

        public OrderManager(IProductStockRepo productStockRepo, IPaymentProcessor paymentProcessor, IShippingProcessor shippingProcessor)
        {
            _productStockRepo = productStockRepo;
            _paymentProcessor = paymentProcessor;
            _shippingProcessor = shippingProcessor;
        }
        public void Submit(Product product, string creditCardNumber, string expiryDate)
        {
            // Check product stock
            if (!_productStockRepo.IsInStock(product))
            {
                throw new Exception($"{product} is currently not in stock.");
            }
            // Payment
            _paymentProcessor.ChargeCreditCard(creditCardNumber, expiryDate);
            // Ship the product
            _shippingProcessor.MailProduct(product);
            Console.WriteLine($"Order Manager: {product} has been shipped");
            _productStockRepo.PrintStock();
        }
    }
}
