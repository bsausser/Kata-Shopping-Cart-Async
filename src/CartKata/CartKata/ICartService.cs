namespace CartKata
{
    /// <summary>
    /// Extension of ITerminal 
    /// </summary>
    public interface ICartService : ITerminal
    {
        /// <summary>
        /// Resets the internal cart
        /// </summary>
        public void NewCart();

        /// <summary>
        /// Un-Scans for deleting items in the cart 
        /// </summary>
        /// <param name="item">Product Code (Product.Id)</param>
        public void Remove(string item);
    }
}
