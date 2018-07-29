using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using CrossSolar.Controllers;
using CrossSolar.Models;
using CrossSolar.Repository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace CrossSolar.Tests.Model
{
    public class PanelModelDataTest
    {
        [Fact]
        public void PanelModelValidateData()
        {
            var panelModelNotValid = new PanelModel
            {
                Brand = "Areva",
                Latitude = 91,
                Longitude = 181,
                Serial = "XXXX1111YYYY2222"
            };

            var context1 = new ValidationContext(panelModelNotValid, null, null);
            var results1 = new List<ValidationResult>();
            var isModelStateNotValid = Validator.TryValidateObject(panelModelNotValid, context1, results1, true);

            var panelModelValid = new PanelModel
            {
                Brand = "Areva",
                Latitude = 62.946264,
                Longitude = 180,
                Serial = "XXXX1111YYYY2222"
            };

            var context = new ValidationContext(panelModelValid, null, null);
            var results = new List<ValidationResult>();
            var isModelStateValid = Validator.TryValidateObject(panelModelValid, context, results, true);


            // Assert 
            Assert.False(isModelStateNotValid);
            Assert.True(isModelStateValid);
        }
    }
}
