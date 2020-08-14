using System;
using System.Collections.Generic;
using System.Linq;

namespace CartKata
{
    public class CartService : ICartService
    {
        private readonly IProductRepository _productRepository;

        private IList<Tuple<string, decimal>> _cart { get; set; } //IDictionary requires unique keys

        public CartService(IProductRepository productRepo)
        {
            _productRepository = productRepo; // for eventual Dependency Injection

            NewCart(); //instantiate the cart
        }

        /// <summary>
        /// Resets the internal cart
        /// </summary>
        public void NewCart()
        {
            _cart = new List<Tuple<string, decimal>>();
        }

        /// <summary>
        /// Add Product Item to Cart
        /// </summary>
        /// <param name="item">Product Code (Product.Id)</param>
        public void Scan(string item)
        {
            if (String.IsNullOrWhiteSpace(item))
            {
                throw new ArgumentNullException(nameof(item));
            }

            Product product = _productRepository.Get(item);
            if(product == null)
            {
                throw new ArgumentException($"{nameof(item)} not found using id: {item}");
            }

            // add product to cart
            var cartItem = new Tuple<string, decimal>(product.Id, product.Prices.OrderBy(t => t.Threshold).First().Price);
            _cart.Add(cartItem); //first price is no discount or threshold = 0

            // check for and apply qualifying discount to cart items
            updatePricing(product.Id);

        }

        /// <summary>
        /// Un-Scans for deleting items in the cart 
        /// </summary>
        /// <param name="item">Product Code (Product.Id)</param>
        public void Remove(string item)
        {
            if (String.IsNullOrWhiteSpace(item))
            {
                throw new ArgumentNullException(nameof(item));
            }

            var lastItem = _cart.LastOrDefault(l => l.Item1.Equals(item));

            if (lastItem != null)
            {
                _cart.Remove(lastItem);

                // check for and remove disqualifying discount to cart items
                updatePricing(lastItem.Item1);
            }
            else
            {
                throw new ArgumentException($"{nameof(item)} not found in the cart using id: {item}");
            }
        }

        /// <summary>
        /// Discount Business Logic for Scan and Remove methods
        /// </summary>
        /// <param name="productId"></param>
        private void updatePricing(string productId)
        {
            Product product = _productRepository.Get(productId); //optimistic without a try/catch, however only 2 methods call it and pass a repository object id.

            //Get product count to be compared to pricing thresholds
            int productCount = _cart.Count(p => p.Item1.Equals(product.Id)); //on remove, this could be zero
            if (_cart.Count.Equals(0) || productCount.Equals(0))
            {
                return; //stop processing because there are no items in the cart or no products matching the productId
            }

            // order thresholds (defensively of out of order threshold values), then use the Last price object with a qualifying threshold value.
            Pricing price = product.Prices.OrderBy(t => t.Threshold).Last(p => productCount >= p.Threshold);

            // Check if there is a need to update pricing in cart (including if an item has been removed, thus remove the discount)
            decimal currentPriceInCart = _cart
                .FirstOrDefault(c => c.Item1.Equals(product.Id))
                .Item2; //No possible exception because productCount condition (line 71).
            
            if (!currentPriceInCart.Equals(price.Price))
            {
                // Update all selected products in the cart with qualifying price
                for (int i = 0; i < _cart.Count; i++) // need index of list item to replace Tuple with new price
                {
                    if (_cart[i].Item1.Equals(product.Id))
                    {
                        _cart[i] = new Tuple<string, decimal>(product.Id, price.Price);
                    }
                }
            }

        }

        /// <summary>
        /// Discounts should be applied in Scan, so this becomes a simple Sum operation
        /// </summary>
        /// <returns></returns>
        public decimal Total()
        {
            return _cart.Sum(c => c.Item2);
        }
    }
}
