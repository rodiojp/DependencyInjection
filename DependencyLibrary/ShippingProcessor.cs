using System;

namespace DependencyLibrary
{
    public interface IShippingProcessor
    {
        void MailProduct(Product product);
    }

    public class ShippingProcessor : IShippingProcessor
    {
        private readonly IProductStockRepo _productStockRepo;
        public ShippingProcessor(IProductStockRepo productStockRepo)
        {
            _productStockRepo = productStockRepo;
        }
        public void MailProduct(Product product)
        {
            _productStockRepo.ReduceStock(product);
            Console.WriteLine("Call to Shipping Processor API");
        }
    }
}