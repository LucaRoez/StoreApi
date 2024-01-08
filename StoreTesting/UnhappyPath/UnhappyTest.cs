using StoreAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using FluentAssertions;
using StoreAPI.Services.Repository;
using StoreAPI.Models;

namespace StoreTesting.UnhappyPath
{
    public class UnhappyTest
    {
        [Fact]
        public async Task Get_UndocumentedStatusCode_CreateDb()
        {
            Mock<IDbContext> mockDbContextService = new();
            StoreController sut = new(mockDbContextService.Object);

            var result = (CreatedResult)await sut.CreateDb("");

            result.StatusCode.Should().Be(201);
        }

        [Fact]
        public async Task Get_UndocumentedStatusCode_DeleteDb()
        {
            Mock<IDbContext> mockDbContextService = new();
            StoreController sut = new(mockDbContextService.Object);

            var result = (NoContentResult)await sut.DeleteDb("");

            result.StatusCode.Should().Be(204);
        }

        [Fact]
        public async Task Get_UndocumentedStatusCode_CreateEntity()
        {
            Mock<IDbContext> mockDbContextService = new();
            StoreController sut = new(mockDbContextService.Object);

            Entity testDTO = new()
            {
                Database = "",
                Name = "",
                Properties = new[] { "" }
            };

            var result = (CreatedResult)await sut.CreateEntity(testDTO);

            result.StatusCode.Should().Be(201);
        }

        [Fact]
        public async Task Get_UndocumentedStatusCode_ModifyAndAddEntity()
        {
            Mock<IDbContext> mockDbContextService = new();
            StoreController sut = new(mockDbContextService.Object);

            Entity testDTO = new()
            {
                Database = "",
                Name = "",
                Properties = new[] { "" }
            };

            var result = (NoContentResult)await sut.AddInEntity(testDTO);

            result.StatusCode.Should().Be(204);
        }

        [Fact]
        public async Task Get_UndocumentedStatusCode_ModifyEntity()
        {
            Mock<IDbContext> mockDbContextService = new();
            StoreController sut = new(mockDbContextService.Object);

            Entity testDTO = new()
            {
                Database = "",
                Name = "",
                Properties = new[] { "" }
            };

            var result = (NoContentResult)await sut.ModifyEntity(testDTO);

            result.StatusCode.Should().Be(204);
        }

        [Fact]
        public async Task Get_UndocumentedStatusCode_DeleteEntity()
        {
            Mock<IDbContext> mockDbContextService = new();
            StoreController sut = new(mockDbContextService.Object);
            mockDbContextService
                .Setup(x => x.DropOldEntityFor(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync("500 Internal Server Error");

            var result = (NoContentResult)await sut.DeleteEntity("", "");

            result.StatusCode.Should().Be(500);
        }
    }
}