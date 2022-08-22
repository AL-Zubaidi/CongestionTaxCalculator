
using Api.Infrastracture.Enums;
using Api.Infrastracture.Services.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Api.Infrastracture.Tests
{
    [TestClass()]
    public class CongestionTaxCalculatorTests
    {
        [TestMethod()]
        public void GetCongestionTaxTest_Is_Tax_Free_Month_Is_Not_Tax_Free_Vehicle_Should_Return_0()
        {
            //Arrange
            var dates = new List<DateTime> {
                new DateTime(2013, 7, 4, 7, 5, 7),
                new DateTime(2013, 7, 4, 7, 5, 7),
                new DateTime(2013, 7, 4, 7, 5, 7)};

            var _mockCongestionTaxService = new Mock<ICongestionTaxService>();
            _mockCongestionTaxService.Setup(x => x.IsCongestionTaxFreeDate(It.IsAny<DateTime>())).Returns(true);
            _mockCongestionTaxService.Setup(x=> x.IsCongestionTaxFreeVehicle(It.IsAny<Vehicles>())).Returns(false);
            _mockCongestionTaxService.Setup(x => x.GetCongestionTaxFeePerPassing(It.IsAny<DateTime>())).Returns(0);

             var congestionTaxCalculator = new CongestionTaxCalculator(_mockCongestionTaxService.Object);

            //Act
          var result = congestionTaxCalculator.GetCongestionTax(Vehicles.Car, dates);

            //Assert
            Assert.AreEqual(result, 0);
        }

        [TestMethod()]
        public void GetCongestionTaxTest_Is_Not_Tax_Free_Month_Is_Not_Tax_Free_Vehicle_Should_Return_52()
        {
            //Arrange
            var dates = new List<DateTime> {
                new DateTime(2013, 8, 4, 7, 5, 7),
                new DateTime(2013, 8, 4, 8, 1, 7),
                new DateTime(2013, 8, 4, 8, 59, 7)};

            var _mockCongestionTaxService = new Mock<ICongestionTaxService>();
            _mockCongestionTaxService.Setup(x => x.IsCongestionTaxFreeDate(It.IsAny<DateTime>())).Returns(false);
            _mockCongestionTaxService.Setup(x => x.IsCongestionTaxFreeVehicle(It.IsAny<Vehicles>())).Returns(false);
            _mockCongestionTaxService.Setup(x => x.GetCongestionTaxFeePerPassing(It.IsAny<DateTime>())).Returns(26);

            var congestionTaxCalculator = new CongestionTaxCalculator(_mockCongestionTaxService.Object);

            //Act
            var result = congestionTaxCalculator.GetCongestionTax(Vehicles.Car, dates);

            //Assert
            Assert.AreEqual(result, 52);
        }
    }
} 