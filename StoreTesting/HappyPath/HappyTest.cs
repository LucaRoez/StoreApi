using StoreAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using FluentAssertions;
using StoreAPI.Services.Repository;
using StoreAPI.Models;

namespace StoreTesting.HappyPath
{
    public class HappyTest
    {
        [Fact]
        public async Task Get_201CreatedStatusCode_CreateDb()
        {
            Mock<IDbContext> mockDbContextService = new();
            StoreController sut = new(mockDbContextService.Object);

            var result = (CreatedResult)await sut.CreateDb("Test");

            result.StatusCode.Should().Be(201);
        }

        [Fact]
        public async Task Get_204NoContentStatusCode_DeleteDb()
        {
            Mock<IDbContext> mockDbContextService = new();
            StoreController sut = new(mockDbContextService.Object);

            var result = (NoContentResult)await sut.DeleteDb("Test");

            result.StatusCode.Should().Be(204);
        }

        [Fact]
        public async Task Get_201CreatedStatusCode_CreateEntity()
        {
            Mock<IDbContext> mockDbContextService = new();
            StoreController sut = new(mockDbContextService.Object);

            Entity testDTO = new()
            {
                Database = "Test",
                Name = "Entity",
                Properties = new[] { "Name VARCHAR(50)", "Id INT IDENTITY(1,1) PRIMARY KEY" }
            };

            var result = (CreatedResult)await sut.CreateEntity(testDTO);

            result.StatusCode.Should().Be(201);
        }

        [Fact]
        public async Task Get_204NoContentStatusCode_ModifyAndAddEntity()
        {
            Mock<IDbContext> mockDbContextService = new();
            StoreController sut = new(mockDbContextService.Object);

            Entity testDTO = new()
            {
                Database = "Test",
                Name = "Entity",
                Properties = new[] { "Body VARCHAR(500)", "Footer VARCHAR(50)" }
            };

            var result = (NoContentResult)await sut.AddInEntity(testDTO);

            result.StatusCode.Should().Be(204);
        }

        [Fact]
        public async Task Get_204NoContentStatusCode_ModifyEntity()
        {
            Mock<IDbContext> mockDbContextService = new();
            StoreController sut = new(mockDbContextService.Object);

            Entity testDTO = new()
            {
                Database = "Test",
                Name = "Entity",
                Properties = new[] { "Name VARCHAR(30)", "Id BIGINT IDENTITY(1,1) PRIMARY KEY" }
            };

            var result = (NoContentResult)await sut.ModifyEntity(testDTO);

            result.StatusCode.Should().Be(204);
        }

        [Fact]
        public async Task Get_204NoContentStatusCode_DeleteEntity()
        {
            Mock<IDbContext> mockDbContextService = new();
            mockDbContextService
                .Setup(x => x.DropOldEntityFor(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync("204");
            StoreController sut = new(mockDbContextService.Object);

            var result = (NoContentResult)await sut.DeleteEntity("Test", "Entity");

            result.StatusCode.Should().Be(204);
        }
    }
}