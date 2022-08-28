using AutoMapper;
using ShoppingCartService.BusinessLogic;
using ShoppingCartService.Controllers.Models;
using ShoppingCartService.DataAccess.Entities;
using ShoppingCartService.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartService.BusinessLogic.Tests
{
    
    public class CheckOutEngineTests
    {
        [Fact]
        public void CalculateTotalsTestInternationalShippingStandardCustomer_Success()
        {
            var checkoutEngine = CreateCheckoutEngine();
            var cart = CreateStandardCustomerCart();
            cart.ShippingAddress = new Models.Address { City = "Söråker", Country = "Sweden", Street = "Cevelidsgatan" };

            var total = checkoutEngine.CalculateTotals(cart).Total;

            Assert.Equal(187.94, total);
        }

        [Fact]
        public void CalculateTotalsTestSameCountryShippingStandardCustomer_Success()
        {
            var checkoutEngine = CreateCheckoutEngine();
            var cart = CreateStandardCustomerCart();
            cart.ShippingAddress = new Models.Address { City = "Söråker", Country = "USA", Street = "Cevelidsgatan" };

            var total = checkoutEngine.CalculateTotals(cart).Total;

            Assert.Equal(83.94, total);
        }

        [Fact]
        public void CalculateTotalsTestSameCityShippingStandardCustomer_Success()
        {
            var checkoutEngine = CreateCheckoutEngine();
            var cart = CreateStandardCustomerCart();
            cart.ShippingAddress = new Models.Address { City = "Dallas", Country = "USA", Street = "Cevelidsgatan" };

            var total = checkoutEngine.CalculateTotals(cart).Total;

            Assert.Equal(75.94, total);
        }

        [Fact]
        public void CalculateTotalsTestInternationalShippingPremiumCustomer_Success()
        {
            var checkoutEngine = CreateCheckoutEngine();
            var cart = CreatePremiumCustomerCart();
            cart.ShippingAddress = new Models.Address { City = "Söråker", Country = "Sweden", Street = "Cevelidsgatan" };

            var total = checkoutEngine.CalculateTotals(cart).Total;

            Assert.Equal(169.15, Math.Round(total, 2));
        }

        [Fact]
        public void CalculateTotalsTestSameCountryShippingPremiumCustomer_Success()
        {
            var checkoutEngine = CreateCheckoutEngine();
            var cart = CreatePremiumCustomerCart();
            cart.ShippingAddress = new Models.Address { City = "Söråker", Country = "USA", Street = "Cevelidsgatan" };

            var total = checkoutEngine.CalculateTotals(cart).Total;

            Assert.Equal(75.55, Math.Round(total, 2));
        }

        [Fact]
        public void CalculateTotalsTestSameCityShippingPremiumCustomer_Success()
        {
            var checkoutEngine = CreateCheckoutEngine();
            var cart = CreatePremiumCustomerCart();
            cart.ShippingAddress = new Models.Address { City = "Dallas", Country = "USA", Street = "Cevelidsgatan" };

            var total = checkoutEngine.CalculateTotals(cart).Total;

            Assert.Equal(68.35, Math.Round(total, 2));
        }

        private CheckOutEngine CreateCheckoutEngine()
        {
            return new CheckOutEngine(new ShippingCalculator(), CreateMapper());
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
                ShippingMethod = Models.ShippingMethod.Standard,
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

        private IMapper CreateMapper()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            return new Mapper(config);
        }

    }
}