using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrossSolar.Controllers;
using CrossSolar.Domain;
using CrossSolar.Models;
using CrossSolar.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace CrossSolar.Tests.Controller
{
    public class AnaliticsControllerTests
    {
        public AnaliticsControllerTests()
        {
            _analyticsController = new AnalyticsController(_analyticsRepositoryMock.Object, _panelRepositoryMock.Object);
        }

        private readonly AnalyticsController _analyticsController;

        private readonly Mock<IAnalyticsRepository> _analyticsRepositoryMock = new Mock<IAnalyticsRepository>();
        private readonly Mock<IPanelRepository> _panelRepositoryMock = new Mock<IPanelRepository>();

        [Fact]
        public async Task InsertOneHourElectricityTest()
        {

            // Arrange
            var panelID = "XXXX1111YYYY2222";
            var oneHourElectricityModel = new OneHourElectricityModel
            {
                KiloWatt = 17
            };

            // Act
            var result = await _analyticsController.Post(panelID, oneHourElectricityModel);

            // Assert
            Assert.NotNull(result);

            var createdResult = result as CreatedResult;
            Assert.NotNull(createdResult);
            Assert.Equal(201, createdResult.StatusCode);
        }


        #region mock data for GetPowerPanelOutPutTest
        private List<Panel> GetListPanel()
        {
            var odem = new List<Panel>();
            odem.Add(new Panel()
            {
                Id = 1,
                Latitude = -8.709006,
                Longitude = -8.709006,
                Serial = "XXXX1111YYYY2222",
                Brand = "Areva"

            });
            odem.Add(new Panel()
            {
                Id = 2,
                Latitude = -8.709006,
                Longitude = -8.709006,
                Serial = "XXXX1111YYYY3333",
                Brand = "Areva2"
            });
            return odem;
        }


        private List<OneHourElectricity> GetListOneHourElectricity()
        {
            var ohe = new List<OneHourElectricity>();
            ohe.Add(new OneHourElectricity()
            {
                Id = 1,
                KiloWatt = 14,
                PanelId = "XXXX1111YYYY2222",
                DateTime = new DateTime(2010, 12, 31)
            });
            ohe.Add(new OneHourElectricity()
            {
                Id = 1,
                KiloWatt = 17,
                PanelId = "XXXX1111YYYY2222",
                DateTime = new DateTime(2010, 12, 31)
            });
            return ohe;
        }

        #endregion

        [Fact]
        public async Task GetPowerPanelOutPutTest()
        {
            //Arrange
            var panelID = "XXXX1111YYYY2222";
            _panelRepositoryMock.Setup(m => m.GetPanelBySerial(panelID)).Returns(Task.FromResult(GetListPanel().Where(p => p.Serial == panelID).FirstOrDefault()));

            _analyticsRepositoryMock.Setup(m => m.ReturnOneHourElectricity(panelID)).Returns(Task.FromResult(GetListOneHourElectricity()));

            //Act
            var result = await _analyticsController.Get(panelID);

            // Assert
            Assert.NotNull(result);

            var objectResult = result as OkObjectResult;
            Assert.NotNull(objectResult);

            var content = objectResult.Value as OneHourElectricityListModel;
            Assert.NotNull(content);
        }

        #region mock data for DayResultsTests


        private List<OneDayElectricityModel> GetListOneHourElectricityModel()
        {
            var ohem = new List<OneDayElectricityModel>();
            ohem.Add(new OneDayElectricityModel()
            {
                Sum = 2000,
                Average = 17,
                Maximum = 30,
                Minimum = 0,
                DateTime = new DateTime(2010, 12, 31)
            });
            return ohem;
        }

        #endregion

        [Fact]
        public async Task DayResultsTests()
        {
            var panelID = "XXXX1111YYYY2222";
            _analyticsRepositoryMock.Setup(m => m.DayResults(panelID)).Returns(Task.FromResult(GetListOneHourElectricityModel().ToList()));

            //Act
            var result = await _analyticsController.DayResults(panelID);

            // Assert
            Assert.NotNull(result);

            var objectResult = result as OkObjectResult;
            Assert.NotNull(objectResult);

            var content = objectResult.Value as List<OneDayElectricityModel>;
            Assert.NotNull(content);
        }



        [Fact]
        public async Task GetPowerPanelOutPutNullTest()
        {
            //Arrange
            var panelID = "XXXX1111YYYY2222";
            _panelRepositoryMock.Setup(m => m.GetPanelBySerial(panelID)).Returns(Task.FromResult<Panel>(null));            

            //Act
            var result = await _analyticsController.Get(panelID);

            // Assert
            Assert.NotNull(result);

            var objectResult = result as NotFoundResult;
            Assert.NotNull(objectResult);
        }
    }
}