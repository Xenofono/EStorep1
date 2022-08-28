using ShoppingCartService.BusinessLogic.Validation;
using ShoppingCartService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartService.BusinessLogic.Validation.Tests
{
    public class AddressValidatorTests
    {
        [Fact]
        public void IsValidSuccess()
        {
            var addressValidator = new AddressValidator();
            var address = new Address { City = "Söråker", Country = "Country", Street = "Cevelidsgatan" };

            var isValid = addressValidator.IsValid(address);

            Assert.True(isValid);
        }

        [Theory]
        [InlineData(null, "Sweden", "Cevelidsgatan")]
        [InlineData("Sundsvall", null, "Cevelidsgatan")]
        [InlineData("Sundsvall", "Sweden", null)]
        public void IsValidFailure(string city, string country, string street)
        {
            var addressValidator = new AddressValidator();
            var address= new Address {City = city, Country = country, Street = street };

            var isValid = addressValidator.IsValid(address);

            Assert.False(isValid);
        }
    }
}