using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CartKata
{
    public interface IProductRepository
    {
        IList<Product> Products { get; }

        Task<Product> GetAsync(string id);

        Task AddAsync(Product product);

        Task<Product> UpdateAsync(Product product);
        
        Task<Boolean> SaveAsync();

    }
}
