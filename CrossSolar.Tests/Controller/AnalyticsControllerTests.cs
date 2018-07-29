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
        public async Task ShouldInsertOneHourElectricity()
        {
            var panelID = "XXXX1111YYYY2222";
            var oneHourElectricityModel = new OneHourElectricityModel
            {
                KiloWatt = 17
            };

            // Arrange

            // Act
            var result = await _analyticsController.Post(panelID, oneHourElectricityModel);

            // Assert
            Assert.NotNull(result);

            var createdResult = result as CreatedResult;
            Assert.NotNull(createdResult);
            Assert.Equal(201, createdResult.StatusCode);
        }




        //[Fact]
        //public async Task GetPowerPanelOutPut()
        //{
        //    //Arrange
        //    var panelID = "XXXX1111YYYY2222";
        //    _panelRepositoryMock.Setup(m => m.Query()
        //        .FirstOrDefault(It.IsAny<Func<Panel, bool>>())).Returns(new Panel { Id = 1, Serial = "XXXX1111YYYY2222" });

        //    _analyticsRepositoryMock.Setup(m => m.Query()
        //        .Where(x => x.PanelId.Equals(panelID)).ToList()).Returns(new List<OneHourElectricity>());

        //    //Act
        //    var result = await _analyticsController.Get(panelID);

        //    // Assert
        //    Assert.NotNull(result);

        //    var objectResult = result as OkObjectResult;
        //    Assert.NotNull(objectResult);

        //    var content = objectResult.Value as OneHourElectricityListModel;
        //    Assert.NotNull(content);
        //}
    }
}