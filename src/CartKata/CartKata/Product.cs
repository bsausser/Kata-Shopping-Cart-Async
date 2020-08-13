using System.Collections.Generic;

namespace CartKata
{
    public class Product
    {
        /// <summary>
        /// Product Code
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Product Prices including any discounts
        /// </summary>
        public IEnumerable<Pricing> Prices { get; set; }
    }

    /// <summary>
    /// Link Product Prices, representing a one to many relationship 
    /// </summary>
    /// <remarks>
    /// Using same Product.cs file for brevity
    /// </remarks>
    public class Pricing
    {
        /// <summary>
        /// Foreign Key back to Product
        /// </summary>
        public string ProductId { get; set; }

        public decimal Price { get; set; }

        /// <summary>
        /// Discount Threshold, where zero (0) represents no discount
        /// </summary>
        public decimal Threshold { get; set; }
    }
}
