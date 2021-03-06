﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CrossSolar.Controllers;
using CrossSolar.Domain;
using CrossSolar.Models;
using CrossSolar.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace CrossSolar.Tests.Repository
{
    public class PanelRepositoryTests
    {
        private readonly PanelRepository _panelRepository;
        private readonly Mock<CrossSolarDbContext> _crossSolarDbContextMock = new Mock<CrossSolarDbContext>();

        public PanelRepositoryTests()
        {
            _panelRepository = new PanelRepository(_crossSolarDbContextMock.Object);
        }      


        [Fact]
        public async Task testGetById()
        {
            // Arrange
            List<Panel> panels = new List<Panel>();
            _crossSolarDbContextMock.Setup(m => m.FindAsync<Panel>(1)).Returns(Task.FromResult(new Panel { Id = 1 }));

            // Act
            var result = _panelRepository.GetAsync("1");

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
        }

        [Fact]
        public async Task testInsert()
        {
            var panel = new Panel
            {
                Brand = "Areva",
                Latitude = 12.345678,
                Longitude = 98.7655432,
                Serial = "AAAA1111BBBB2222"
            };

            _crossSolarDbContextMock.Setup(m => m.Set<Panel>().Add(panel));

            // Arrange

            // Act
            await _panelRepository.InsertAsync(panel);

            // Assert

            Assert.Equal(typeof(Panel), panel.GetType());
        }
    }
}