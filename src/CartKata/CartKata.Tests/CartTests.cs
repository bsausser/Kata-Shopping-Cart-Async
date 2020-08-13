using System;
using Xunit;

namespace CartKata.Tests
{
    public class CartTests
    {
        private readonly CartService _cartService;

        public CartTests()
        {
            _cartService = new CartService();
        }

        #region - Cart Initializers -
        private void initalize_Requirement_01()
        {
            _cartService.NewCart();

            _cartService.Scan("A");
            _cartService.Scan("B");
            _cartService.Scan("C");
            _cartService.Scan("D");
            _cartService.Scan("A");
            _cartService.Scan("B");
            _cartService.Scan("A");
            _cartService.Scan("A");
        }

        private void initalize_Requirement_02()
        {
            _cartService.NewCart();

            _cartService.Scan("C");
            _cartService.Scan("C");
            _cartService.Scan("C");
            _cartService.Scan("C");
            _cartService.Scan("C");
            _cartService.Scan("C");
            _cartService.Scan("C");
        }

        private void initalize_Requirement_03()
        {
            _cartService.NewCart();

            _cartService.Scan("A");
            _cartService.Scan("B");
            _cartService.Scan("C");
            _cartService.Scan("D");

        }
        #endregion

        #region - Parameter Tests -

        [Fact]
        public void Scan_IsNullOrEmpty_Throws_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _cartService.Scan(string.Empty));
        }

        [Fact]
        public void Scan_InvalidProductCode_Throws_ArgumentException()
        {
            Assert.Throws<ArgumentException>(() => _cartService.Scan("#3*8"));
        }


        [Fact]
        public void Remove_IsNullOrEmpty_Throws_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _cartService.Scan(string.Empty));
        }

        [Fact]
        public void Remove_InvalidProductCode_Throws_ArgumentException()
        {
            Assert.Throws<ArgumentException>(() => _cartService.Scan("#3*8"));
        }
        #endregion

        #region - Requirements Acceptance Tests -
        [Fact]
        public void Scan_Requirement_01()
        {
            initalize_Requirement_01();

            Assert.Equal(32.40m, _cartService.Total());
        }

        [Fact]
        public void Scan_Requirement_02()
        {
            initalize_Requirement_02();

            Assert.Equal(7.25m, _cartService.Total());
        }

        [Fact]
        public void Scan_Requirement_03()
        {
            initalize_Requirement_03();

            Assert.Equal(15.4m, _cartService.Total());
        }
        #endregion

        #region - Gold Plating Tests :) -
        [Fact]
        public void Empty_Cart_Total_Returns_Zero()
        {
            _cartService.NewCart();
            Assert.Equal(0m, _cartService.Total());
        }

        [Fact]
        public void Remove_Cart_Intializer_01_Item_Removes_Discount()
        {
            initalize_Requirement_01();

            _cartService.Remove("A");

            Assert.Equal(31.40m, _cartService.Total());
        }

        [Fact]
        public void Remove_Last_Item_Total_Returns_Zero()
        {
            _cartService.NewCart();

            _cartService.Scan("A");

            Assert.Equal(2m, _cartService.Total());

            _cartService.Remove("A");

            Assert.Equal(0m, _cartService.Total());
        }
        #endregion
    }
}