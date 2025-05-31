using Ale_Ink.API.Services;
using Ale_Ink.API.Data;
using Ale_Ink.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace Ale_Ink.Tests.ServiceClassTests;

[TestClass]
public sealed class ItemServiceTest
{

    private AppDbContext GetMockedDbContextWithItems() // Helper method to create a mocked DbContext with test data
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Ensures isolation between tests
            .Options;
        var context = new AppDbContext(options);
        // Seed test items
        context.Items.AddRange(
            new Item { ItemId = 1, Name = "Test Item 1" },
            new Item { ItemId = 2, Name = "Another Item" }
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
    public async Task GetAllItemsAsync_ReturnsAllItems()
    {
        // Arrange
        var mockContext = GetMockedDbContextWithItems();
        var service = new ItemService(mockContext);
        
        // Act
        var result = await service.GetAllItemsAsync();

        // Assert
        Assert.AreEqual(2, result.Count());
    }

    [TestMethod]
    public async Task GetItemByIdAsync_ExistingId_ReturnsItem()
    {
        // Arrange
        var mockContext = GetMockedDbContextWithItems();
        var service = new ItemService(mockContext);

        // Act
        var result = await service.GetItemByIdAsync(1);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual("Test Item 1", result.Name);
    }

    [TestMethod]
    public async Task GetItemByIdAsync_NonExistingId_ReturnsNull()
    {
        // Arrange
        var mockContext = GetMockedDbContextWithItems();
        var service = new ItemService(mockContext);
        // Act
        var result = await service.GetItemByIdAsync(999); // Non-existing ID
        // Assert
        Assert.IsNull(result);
    }

    [TestMethod]
    public async Task AddItemAsync_AddsNewItem()
    {
        // Arrange
        var mockContext = GetMockedDbContext();
        var service = new ItemService(mockContext);
        var newItem = new Item { Name = "New Test Item" };

        // Act
        var result = await service.AddItemAsync(newItem);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual("New Test Item", result.Name);
        Assert.AreEqual(1, mockContext.Items.Count());
    }
}
