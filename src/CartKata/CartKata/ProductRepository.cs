using System.Collections.Generic;
using System.Linq;

namespace CartKata
{
    /// <summary>
    /// Repository for Products
    /// </summary>
    public class ProductRepository
    {
        private IList<Product> _products;

        public ProductRepository()
        {
            _products = new List<Product>();

            #region - Static Product List -
            _products.Add(new Product()
            {
                Id = "A",
                Prices = new List<Pricing>() {
                    { new Pricing() { ProductId = "A", Price = 2m, Threshold = 0 } },
                    { new Pricing() { ProductId = "A", Price = 1.75m, Threshold = 4 } }
                }
            });

            _products.Add(new Product()
            {
                Id = "B",
                Prices = new List<Pricing>() {
                    { new Pricing() { ProductId = "B", Price = 12m, Threshold = 0 } }
                }
            });

            // Curious if the 7th threw 11th are $1.25 each until count = 12 (or a multiple of 6, e.g. % 6)
            _products.Add(new Product()
            {
                Id = "C",
                Prices = new List<Pricing>() {
                    { new Pricing() { ProductId = "C", Price = 1.25m, Threshold = 0 } },
                    { new Pricing() { ProductId = "C", Price = 1m, Threshold = 6 } } // $6 / 6 items = $1.00
                }
            });

            _products.Add(new Product()
            {
                Id = "D",
                Prices = new List<Pricing>() {
                    { new Pricing() { ProductId = "D", Price = 0.15m, Threshold = 0 } }
                }
            });

            #endregion
        }

        public Product Get(string id)
        {
            return _products.FirstOrDefault(p => p.Id.Equals(id));
        }
    }
}
