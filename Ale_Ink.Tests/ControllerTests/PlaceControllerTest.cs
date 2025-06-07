using Moq;
using Microsoft.AspNetCore.Mvc;
using Ale_Ink.API.Controllers;
using Ale_Ink.API.Services;
using Ale_Ink.Shared.Models;

namespace Ale_Ink.Tests;

[TestClass]
public class PlaceControllerTest
{
    private Mock<IPlaceService> _mockService;
    private PlaceController _controller;

    [TestInitialize]
    public void Setup()
    {
        _mockService = new Mock<IPlaceService>();
        _controller = new PlaceController(_mockService.Object);
    }

    [TestMethod]
    public async Task GetAllPlaces_ReturnsOkResult_WithPlaces()
    {
        // Arrange
        var places = new List<Place>
        {
            new Place { PlaceId = 1, Name = "Test Place 1" },
            new Place { PlaceId = 2, Name = "Test Place 2" }
        };
        _mockService.Setup(service => service.GetAllPlacesAsync()).ReturnsAsync(places);

        // Act
        var result = await _controller.GetAllPlaces();

        // Assert
        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(places, okResult.Value);
    }

    [TestMethod]
    public async Task GetPlaceById_ExistingId_ReturnsOkResult_WithPlace()
    {
        // Arrange
        var place = new Place { PlaceId = 1, Name = "Test Place" };
        _mockService.Setup(service => service.GetPlaceByIdAsync(1)).ReturnsAsync(place);

        // Act
        var result = await _controller.GetPlaceById(1);

        // Assert
        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(place, okResult.Value);
    }

    [TestMethod]
    public async Task GetPlaceById_NonExistingId_ReturnsNotFound()
    {
        // Arrange
        _mockService.Setup(service => service.GetPlaceByIdAsync(999)).ReturnsAsync((Place)null);

        // Act
        var result = await _controller.GetPlaceById(999);

        // Assert
        Assert.IsInstanceOfType(result.Result, typeof(NotFoundObjectResult));
    }

    [TestMethod]
    public async Task PostPlace_ValidPlace_ReturnsOk()
    {
        // Arrange
        var place = new Place { PlaceId = 1, Name = "New Place" };
        _mockService.Setup(service => service.AddPlaceAsync(place)).ReturnsAsync(place);

        // Act
        var result = await _controller.PostPlace(place);

        // Assert
        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(place, okResult.Value);
    }

    [TestMethod]
    public async Task PostPlace_NullPlace_ReturnsBadRequest()
    {
        // Arrange
        Note note = null;

        // Act
        var result = await _controller.PostPlace(null);

        // Assert
        Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
    }

    [TestMethod]
    public async Task UpdatePlace_Valid_ReturnsOk()
    {
        // Arrange
        var place = new Place { PlaceId = 1, Name = "Updated Place" };
        _mockService.Setup(service => service.UpdatePlaceAsync(1, place)).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.UpdatePlace(1, place);

        // Assert
        Assert.IsInstanceOfType(result, typeof(OkResult));
    }

    [TestMethod]
    public async Task UpdatePlace_IdMisMatch_ReturnsBadRequest()
    {
        // Arrange
        var place = new Place { PlaceId = 2 };

        // Act
        var result = await _controller.UpdatePlace(1, place);

        // Assert
        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
    }

    [TestMethod]
    public async Task DeletePlace_ExistingId_ReturnsOk()
    {
        // Arrange
        _mockService.Setup(service => service.DeletePlaceAsync(1)).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.DeletePlace(1);

        // Assert
        Assert.IsInstanceOfType(result, typeof(OkResult));
    }

    [TestMethod]
    public async Task DeletePlace_NonExistingId_ReturnsNotFound()
    {
        // Arrange
        _mockService.Setup(service => service.DeletePlaceAsync(999)).Throws(new KeyNotFoundException());

        // Act
        var result = await _controller.DeletePlace(999);

        // Assert
        Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
    }
}
