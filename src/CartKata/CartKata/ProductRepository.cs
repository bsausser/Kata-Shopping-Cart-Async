using System.Collections.Generic;
using System.Linq;

namespace CartKata
{
    /// <summary>
    /// Repository for Products
    /// </summary>
    public class ProductRepository : IProductRepository
    {
        public IList<Product> Products { get; }

        public ProductRepository()
        {
            Products = new List<Product>();
        }

        public Product Get(string id)
        {
            return Products.FirstOrDefault(p => p.Id.Equals(id));
        }

        public void Add(Product product)
        {
            Products.Add(product);
        }

        public Product Update(Product product)
        {
            throw new System.NotImplementedException();
        }

        public bool Save()
        {
            throw new System.NotImplementedException();
        }
    }
}
