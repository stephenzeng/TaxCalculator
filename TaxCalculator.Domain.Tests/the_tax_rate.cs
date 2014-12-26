using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using NUnit.Framework;

namespace TaxCalculator.Domain.Tests
{
    [TestFixture]
    public class the_tax_rate
    {
        [Test]
        public void tax_rate_description_must_not_be_empty()
        {
            //arrange
            var taxRate = new TaxRate();
            var validatonContext = new ValidationContext(taxRate, null, null);
            var validationResults = new Collection<ValidationResult>();

            //act
            var isValid = Validator.TryValidateObject(taxRate, validatonContext, validationResults);

            //assert
            Assert.IsFalse(isValid);
            CollectionAssert.Contains(validationResults.Select(r => r.ErrorMessage), "The Description field is required.");
        }

        [Test]
        public void tax_rate_items_must_not_be_empty()
        {
            //arrange
            var taxRate = new TaxRate { Description = "Test" };
            var validatonContext = new ValidationContext(taxRate, null, null);
            var validationResults = new Collection<ValidationResult>();

            //act
            var isValid = Validator.TryValidateObject(taxRate, validatonContext, validationResults);

            //assert
            Assert.IsFalse(isValid);
            CollectionAssert.Contains(validationResults.Select(r => r.ErrorMessage), "The Items field is required.");
        }

        [Test]
        public void tax_rate_item_description_must_not_be_empty()
        {
            //arrange
            var taxRate = new TaxRate
            {
                Description = "Test",
                Items = new List<TaxRateItem>
                {
                    new TaxRateItem(),
                }
            };
            var validatonContext = new ValidationContext(taxRate, null, null);
            var validationResults = new Collection<ValidationResult>();

            //act
            var isValid = Validator.TryValidateObject(taxRate, validatonContext, validationResults);

            //assert
            Assert.IsFalse(isValid);
            CollectionAssert.Contains(validationResults.Select(r => r.ErrorMessage), "The Description field is required.");
        }

        [Test]
        public void tax_rate_threshold_must_start_from_0()
        {
            //arrange
            var taxRate = new TaxRate
            {
                Description = "Test",
                Items = new List<TaxRateItem>
                {
                    new TaxRateItem {Description = "Test", Thresholds =  new List<TaxThreshold>() {new TaxThreshold {Start = 1}}}
                }
            };
            var validatonContext = new ValidationContext(taxRate, null, null);
            var validationResults = new Collection<ValidationResult>();

            //act
            var isValid = Validator.TryValidateObject(taxRate, validatonContext, validationResults);

            //assert
            Assert.IsFalse(isValid);
            CollectionAssert.Contains(validationResults.Select(r => r.ErrorMessage), "Tax rate threshold must start from 0.");
        }

        [Test]
        public void the_last_tax_rate_threshold_must_end_with_empty()
        {
            //arrange
            var taxRate = new TaxRate
            {
                Description = "Test",
                Items = new List<TaxRateItem>
                {
                    new TaxRateItem {Description = "Test", Thresholds = new List<TaxThreshold>()
                    {
                        new TaxThreshold {Start = 1, End = 2},
                        new TaxThreshold {Start = 2, End = 4},
                    }}
                }
            };
            var validatonContext = new ValidationContext(taxRate, null, null);
            var validationResults = new Collection<ValidationResult>();

            //act
            var isValid = Validator.TryValidateObject(taxRate, validatonContext, validationResults);

            //assert
            Assert.IsFalse(isValid);
            CollectionAssert.Contains(validationResults.Select(r => r.ErrorMessage), "The last tax rate threshold must end with empty.");
        }
    }
}
