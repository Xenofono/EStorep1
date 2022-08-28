using ShoppingCartService.BusinessLogic;
using ShoppingCartService.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartService.BusinessLogic.Tests
{
    public class ShippingCalculatorTests
    {
        

        [Fact]
        public void CalculateTotalsTestInternationalShippingStandardCustomer_Success()
        {
            var shippingCalculator = new ShippingCalculator();
            var cart = CreateStandardCustomerCart();
            cart.ShippingAddress = new Models.Address { City = "Söråker", Country = "Sweden", Street = "Cevelidsgatan" };

            var shipping = shippingCalculator.CalculateShippingCost(cart);

            Assert.Equal(120, shipping);
        }

        [Fact]
        public void CalculateTotalsTestSameCountryShippingStandardCustomer_Success()
        {
            var shippingCalculator = new ShippingCalculator();
            var cart = CreateStandardCustomerCart();
            cart.ShippingAddress = new Models.Address { City = "Söråker", Country = "USA", Street = "Cevelidsgatan" };

            var shipping = shippingCalculator.CalculateShippingCost(cart);

            Assert.Equal(16, shipping);
        }

        [Fact]
        public void CalculateTotalsTestSameCityShippingStandardCustomer_Success()
        {
            var shippingCalculator = new ShippingCalculator();
            var cart = CreateStandardCustomerCart();
            cart.ShippingAddress = new Models.Address { City = "Dallas", Country = "USA", Street = "Cevelidsgatan" };

            var shipping = shippingCalculator.CalculateShippingCost(cart);

            Assert.Equal(8, shipping);
        }

        [Fact]
        public void CalculateTotalsTestInternationalShippingPremiumCustomerPriority_Success()
        {
            var shippingCalculator = new ShippingCalculator();
            var cart = CreatePremiumCustomerCart();
            cart.ShippingAddress = new Models.Address { City = "Söråker", Country = "Sweden", Street = "Cevelidsgatan" };

            var shipping = shippingCalculator.CalculateShippingCost(cart);

            Assert.Equal(120, shipping);
        }

        [Fact]
        public void CalculateTotalsTestSameCountryShippingPremiumCustomerPriority_Success()
        {
            var shippingCalculator = new ShippingCalculator();
            var cart = CreatePremiumCustomerCart();
            cart.ShippingAddress = new Models.Address { City = "Söråker", Country = "USA", Street = "Cevelidsgatan" };

            var shipping = shippingCalculator.CalculateShippingCost(cart);

            Assert.Equal(16, shipping);
        }

        [Fact]
        public void CalculateTotalsTestSameCityShippingPremiumCustomerPriority_Success()
        {
            var shippingCalculator = new ShippingCalculator();
            var cart = CreatePremiumCustomerCart();
            cart.ShippingAddress = new Models.Address { City = "Dallas", Country = "USA", Street = "Cevelidsgatan" };

            var shipping = shippingCalculator.CalculateShippingCost(cart);

            Assert.Equal(8, shipping);
        }

        private Cart CreateStandardCustomerCart()
        {
            return new Cart
            {
                Id = "1",
                CustomerId = "1-1",
                CustomerType = Models.CustomerType.Standard,
                ShippingMethod = Models.ShippingMethod.Standard,
                Items = CreateItemList()
            };
        }

        private Cart CreatePremiumCustomerCart()
        {
            return new Cart
            {
                Id = "1",
                CustomerId = "1-1",
                CustomerType = Models.CustomerType.Premium,
                ShippingMethod = Models.ShippingMethod.Priority,
                Items = CreateItemList()
            };
        }

        private List<Item> CreateItemList()
        {
            return new List<Item> {
                new Item {ProductId = "1", ProductName = "Shampoo", Price = 1.5, Quantity = 2},
                new Item {ProductId = "2", ProductName = "Hershey", Price = 0.99, Quantity = 5},
                new Item {ProductId = "3", ProductName = "Cool game", Price = 59.99, Quantity = 1},

            };
        }
    }
}