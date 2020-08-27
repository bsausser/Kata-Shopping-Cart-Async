using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<Product> GetAsync(string id)
        {
            return await Task.Run(() => Products.FirstOrDefault(p => p.Id.Equals(id)));
        }

        public async Task AddAsync(Product product)
        {
            await Task.Run(() => Products.Add(product));
        }

        Task<Product> IProductRepository.UpdateAsync(Product product)
        {
            throw new System.NotImplementedException();
        }

        Task<bool> IProductRepository.SaveAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}
