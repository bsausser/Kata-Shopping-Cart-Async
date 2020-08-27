using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CartKata.Tests
{
    public class CartTests
    {
        private readonly CartService _cartService;

        public CartTests()
        {
            IProductRepository mockProducts = new ProductRepository();
            seedMockProducts(ref mockProducts);

            _cartService = new CartService(mockProducts);
        }

        #region - Product & Cart Initializers -
        
        private void seedMockProducts(ref IProductRepository mockProducts)
        {
            // A
            mockProducts.AddAsync(new Product()
            {
                Id = "A",
                Prices = new List<Pricing>() {
                    new Pricing() { ProductId = "A", Price = 2m, Threshold = 0 },
                    new Pricing() { ProductId = "A", Price = 1.75m, Threshold = 4 }
                }
            });

            // B
            mockProducts.AddAsync(new Product()
            {
                Id = "B",
                Prices = new List<Pricing>() {
                    new Pricing() { ProductId = "B", Price = 12m, Threshold = 0 }
                }
            });

            // C
            // Curious if the 7th threw 11th are $1.25 each until count = 12 (or a multiple of 6, e.g. % 6)
            mockProducts.AddAsync(new Product()
            {
                Id = "C",
                Prices = new List<Pricing>() {
                    new Pricing() { ProductId = "C", Price = 1.25m, Threshold = 0 },
                    new Pricing() { ProductId = "C", Price = 1m, Threshold = 6 } // $6 / 6 items = $1.00
                }
            });

            // D
            mockProducts.AddAsync(new Product()
            {
                Id = "D",
                Prices = new List<Pricing>() {
                    new Pricing() { ProductId = "D", Price = 0.15m, Threshold = 0 }
                }
            });
        }

        private async Task initialize_Requirement_01()
        {
            _cartService.NewCart();

            await _cartService.ScanAsync("A");
            await _cartService.ScanAsync("B");
            await _cartService.ScanAsync("C");
            await _cartService.ScanAsync("D");
            await _cartService.ScanAsync("A");
            await _cartService.ScanAsync("B");
            await _cartService.ScanAsync("A");
            await _cartService.ScanAsync("A");
        }

        private async Task initialize_Requirement_02()
        {
            _cartService.NewCart();

            await _cartService.ScanAsync("C");
            await _cartService.ScanAsync("C");
            await _cartService.ScanAsync("C");
            await _cartService.ScanAsync("C");
            await _cartService.ScanAsync("C");
            await _cartService.ScanAsync("C");
            await _cartService.ScanAsync("C");
        }

        private async Task initialize_Requirement_03()
        {
            _cartService.NewCart();

            await _cartService.ScanAsync("A");
            await _cartService.ScanAsync("B");
            await _cartService.ScanAsync("C");
            await _cartService.ScanAsync("D");

        }
        #endregion

        #region - Parameter Tests -

        [Fact]
        public async System.Threading.Tasks.Task Scan_IsNullOrEmpty_Throws_ArgumentNullException()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _cartService.ScanAsync(string.Empty));
        }

        [Fact]
        public async System.Threading.Tasks.Task Scan_InvalidProductCode_Throws_ArgumentException()
        {
            await Assert.ThrowsAsync<ArgumentException>(() => _cartService.ScanAsync("#3*8"));
        }


        [Fact]
        public async System.Threading.Tasks.Task Remove_IsNullOrEmpty_Throws_ArgumentNullException()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _cartService.ScanAsync(string.Empty));
        }

        [Fact]
        public async Task Remove_InvalidProductCode_Throws_ArgumentException()
        {
            await Assert.ThrowsAsync<ArgumentException>(() => _cartService.ScanAsync("#3*8"));
        }
        #endregion

        #region - Requirements Acceptance Tests -
        [Fact]
        public async Task Scan_Requirement_01()
        {
            await initialize_Requirement_01();

            Assert.Equal(32.40m, await _cartService.TotalAsync());
        }

        [Fact]
        public async Task Scan_Requirement_02()
        {
            await initialize_Requirement_02();

            Assert.Equal(7.25m, await _cartService.TotalAsync());
        }

        [Fact]
        public async Task Scan_Requirement_03()
        {
            await initialize_Requirement_03();

            Assert.Equal(15.4m, await _cartService.TotalAsync());
        }
        #endregion

        #region - Gold Plating Tests :) -
        [Fact]
        public async Task Empty_Cart_Total_Returns_Zero()
        {
            _cartService.NewCart();
            Assert.Equal(0m, await _cartService.TotalAsync());
        }

        [Fact]
        public async Task Remove_Cart_Initializer_01_Item_Removes_Discount()
        {
            await initialize_Requirement_01();

            await _cartService.RemoveAsync("A");

            Assert.Equal(31.40m, await _cartService.TotalAsync());
        }

        [Fact]
        public async Task Remove_Last_Item_Total_Returns_Zero()
        {
            _cartService.NewCart();

            await _cartService.ScanAsync("A");

            Assert.Equal(2m, await _cartService.TotalAsync());

            await _cartService.RemoveAsync("A");

            Assert.Equal(0m, await _cartService.TotalAsync());
        }
        #endregion
    }
}
