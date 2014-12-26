using System.Data.Entity;
using System.Linq;
using Moq;
using NUnit.Framework;
using TaxCalculator.Dal;
using TaxCalculator.Domain;
using TaxCalculator.Service.Dto;
using TaxCalculator.Service.Iterfaces;
using TaxCalculator.Web.Controllers;
using TaxCalculator.Web.Models;

namespace TaxCalculator.Web.Tests
{
    [TestFixture]
    public class the_calculator_controller
    {
        private readonly Mock<TaxCalculatorContext> _mockContext;
        private readonly Mock<DbSet<TaxRate>> _mockSet;

        public the_calculator_controller()
        {
            var data = Enumerable.Empty<TaxRate>().AsQueryable();

            _mockSet = new Mock<DbSet<TaxRate>>();
            _mockSet.As<IQueryable<TaxRate>>().Setup(m => m.Provider).Returns(data.Provider);
            _mockSet.As<IQueryable<TaxRate>>().Setup(m => m.Expression).Returns(data.Expression);
            _mockSet.As<IQueryable<TaxRate>>().Setup(m => m.ElementType).Returns(data.ElementType);
            _mockSet.As<IQueryable<TaxRate>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            _mockContext = new Mock<TaxCalculatorContext>();
            _mockContext.Setup(c => c.TaxRates).Returns(_mockSet.Object); 
        }

        [Test]
        public void index_action_should_query_taxrates()
        {
            //arrange
            var mockService = new Mock<ICalculateService>();
            var controller = new CalculatorController(mockService.Object) { TaxCalculatorContext = _mockContext.Object};

            //act
            controller.Index();

            //assert
            _mockContext.VerifyGet(m => m.TaxRates);
        }

        [Test]
        public void calculate_should_call_calculator_service()
        {
            //arrange
            var result = new Result();

            _mockSet.Setup(m => m.Find(It.IsAny<object>())).Returns(new TaxRate());

            var mockService = new Mock<ICalculateService>();
            mockService.Setup(s => s.Calculate(It.IsAny<TaxRate>(), It.IsAny<decimal>())).Returns(result);

            var controller = new CalculatorController(mockService.Object) { TaxCalculatorContext = _mockContext.Object };

            var model = new TaxCalculateViewModel
            {
                SelectedYear = 1,
                TaxableIncome = 1,
            };

            //act
            controller.Index(model);

            //assert
            mockService.Verify(s => s.Calculate(It.IsAny<TaxRate>(), It.IsAny<decimal>()), Times.Once());
            Assert.AreEqual(result, model.Result);
        }
    }
}
