using Ale_Ink.API.Services;
using Ale_Ink.API.Data;
using Ale_Ink.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace Ale_Ink.Tests.ServiceClassTests;

[TestClass]
public sealed class PlaceServiceTest
{

    private AppDbContext GetMockedDbContextWithPlaces() // Helper method to create a mocked DbContext with test data
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Ensures isolation between tests
            .Options;

        var context = new AppDbContext(options);

        // Seed test places
        context.Places.AddRange(
            new Place { PlaceId = 1, Name = "Test Place 1" },
            new Place { PlaceId = 2, Name = "Another Place" }
        );
        context.SaveChanges();
        return context;
    }

    private AppDbContext GetMockedDbContext() // Helper method to create a mocked DbContext without test data
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new AppDbContext(options);
    }
    [TestMethod]
    public async Task GetAllPlacesAsync_ReturnsAllPlaces()
    {
        // Arrange
        var mockContext = GetMockedDbContextWithPlaces();
        var service = new PlaceService(mockContext);

        // Act
        var result = await service.GetAllPlacesAsync();

        // Assert
        Assert.AreEqual(2, result.Count());
    }

    [TestMethod]
    public async Task GetPlaceByIdAsync_ExistingId_ReturnsPlace()
    {
        // Arrange
        var mockContext = GetMockedDbContextWithPlaces();
        var service = new PlaceService(mockContext);

        // Act
        var result = await service.GetPlaceByIdAsync(1);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual("Test Place 1", result.Name);
    }

    [TestMethod]
    public async Task GetPlaceByIdAsync_NonExistingId_ReturnsNull()
    {
        // Arrange
        var mockContext = GetMockedDbContextWithPlaces();
        var service = new PlaceService(mockContext);

        // Act
        var result = await service.GetPlaceByIdAsync(999); // Non-existing ID

        // Assert
        Assert.IsNull(result);
    }

    [TestMethod]
    public async Task AddPlaceAsync_AddsNewPlace()
    {
        // Arrange
        var mockContext = GetMockedDbContext();
        var service = new PlaceService(mockContext);
        var newPlace = new Place { Name = "New Place" };

        // Act
        var result = await service.AddPlaceAsync(newPlace);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual("New Place", result.Name);
        Assert.AreEqual(1, mockContext.Places.Count());
    }
}
