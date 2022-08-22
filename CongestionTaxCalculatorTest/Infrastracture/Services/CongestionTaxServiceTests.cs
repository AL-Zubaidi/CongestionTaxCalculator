using Api.Infrastracture.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Api.Infrastracture.Services.Tests
{
    [TestClass()]
    public class CongestionTaxServiceTests
    {
        #region IsCongestionTaxFreeDate tests
        public void IsCongestionTaxFreeDate_Year_Is_Not_2013_Should_Return_False()
        {
            //Arrange
            var date = new DateTime(2013, 7, 4, 7, 5, 7);
            var congestionTaxService = new CongestionTaxService();

            //Act
            var result = congestionTaxService.IsCongestionTaxFreeDate(date);

            //Assert
            Assert.IsTrue(result);
        }
        [TestMethod()]
        public void IsCongestionTaxFreeDate_July_Is_Tax_Free_Month_Should_Return_True()
        {
            //Arrange
            var date = new DateTime(2013, 7, 4, 7, 5, 7);
            var congestionTaxService = new CongestionTaxService();

            //Act
            var result = congestionTaxService.IsCongestionTaxFreeDate(date);

            //Assert
            Assert.IsTrue(result);
        }
        [TestMethod()]
        public void IsCongestionTaxFreeDate_August_Is_Not_Tax_Free_Month_And_Not_Tax_Free_Day_Should_Return_False()
        {
            //Arrange
            var date = new DateTime(2013, 8, 5, 7, 5, 7);
            var congestionTaxService = new CongestionTaxService();

            //Act
            var result = congestionTaxService.IsCongestionTaxFreeDate(date);

            //Assert
            Assert.IsFalse(result);
        }

        [TestMethod()]
        public void IsCongestionTaxFreeDate_May_Is_Not_Tax_Free_Month_But_Is_Weekend_Should_Return_True()
        {
            //Arrange
            var date = new DateTime(2013, 5, 12, 7, 5, 7);
            var congestionTaxService = new CongestionTaxService();

            //Act
            var result = congestionTaxService.IsCongestionTaxFreeDate(date);

            //Assert
            Assert.IsTrue(result);
        }
        #endregion

        #region IsCongestionTaxFreeVehicle tests
        [TestMethod()]
        public void IsCongestionTaxFreeVehicle_Vehicle_Is_Buss_Should_Return_True()
        {
            //Arrange
            var congestionTaxService = new CongestionTaxService();

            //Act
            var result = congestionTaxService.IsCongestionTaxFreeVehicle(Vehicles.Busses);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod()]
        public void IsCongestionTaxFreeVehicle_Vehicle_Is_Car_Should_Return_False()
        {
            //Arrange
            var congestionTaxService = new CongestionTaxService();

            //Act
            var result = congestionTaxService.IsCongestionTaxFreeVehicle(Vehicles.Car);

            //Assert
            Assert.IsFalse(result);
        }
        #endregion

        #region GetCongestionTaxFeePerPassing tests
        [TestMethod()]
        public void GetCongestionTaxFeePerPassing_Hour_Is_6_Minute_Is_5_Should_Return_8()
        {
            //Arrange
            var date = new DateTime(2013, 5, 4, 6, 5, 7);
            var congestionTaxService = new CongestionTaxService();

            //Act
            var result = congestionTaxService.GetCongestionTaxFeePerPassing(date);

            //Assert
            Assert.AreEqual(result,8);
        }

        [TestMethod()]
        public void GetCongestionTaxFeePerPassing_Hour_Is_18_Minute_Is_55_Should_Return_0()
        {
            //Arrange
            var date = new DateTime(2013, 5, 4, 18, 55, 7);
            var congestionTaxService = new CongestionTaxService();

            //Act
            var result = congestionTaxService.GetCongestionTaxFeePerPassing(date);

            //Assert
            Assert.AreEqual(result, 0);
        }

        [TestMethod()]
        public void GetCongestionTaxFeePerPassing_Hour_Is_9_Minute_Is_30_Should_Return_8()
        {
            //Arrange
            var date = new DateTime(2013, 5, 4, 9, 30, 7);
            var congestionTaxService = new CongestionTaxService();

            //Act
            var result = congestionTaxService.GetCongestionTaxFeePerPassing(date);

            //Assert
            Assert.AreEqual(result, 8);
        }
        #endregion
    }
}