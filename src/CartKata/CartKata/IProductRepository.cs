using System;
using System.Collections.Generic;
using System.Text;

namespace CartKata
{
    public interface IProductRepository
    {
        IList<Product> Products { get; }

        Product Get(string id);

        void Add(Product product);

        Product Update(Product product);
        
        Boolean Save();

    }
}
