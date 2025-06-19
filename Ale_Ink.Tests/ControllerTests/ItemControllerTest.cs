using Moq;
using Microsoft.AspNetCore.Mvc;
using Ale_Ink.API.Controllers;
using Ale_Ink.API.Services;
using Ale_Ink.Shared.Models;
using Ale_Ink.Shared.DTOs;

namespace Ale_Ink.Tests;

[TestClass]
public sealed class ItemControllerTest
{
    private Mock<IItemService> _mockService;
    private ItemController _controller;

    [TestInitialize]
    public void Setup()
    {
        _mockService = new Mock<IItemService>();
        _controller = new ItemController(_mockService.Object);
    }

    [TestMethod]
    public async Task GetAllItems_ReturnsOkResult_WithNotes()
    {
        // Arrange
        var items = new List<Item>
        {
            new Item { ItemId = 1, Name = "Test Item 1" },
            new Item { ItemId = 2, Name = "Test Item 2" }
        };
        _mockService.Setup(service => service.GetAllItemsAsync()).ReturnsAsync(items);

        // Act
        var result = await _controller.GetAllItems();

        // Assert
        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(items, okResult.Value);
    }

    [TestMethod]
    public async Task GetItemById_ExistingId_ResturnsOkResult_WithItem()
    {
        // Arrange
        var Item = new Item { ItemId = 1, Name = "Test Item" };
        _mockService.Setup(service => service.GetItemByIdAsync(1)).ReturnsAsync(Item);

        // Act
        var result = await _controller.GetItemById(1);

        //Assert
        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(Item, okResult.Value);
    }

    [TestMethod]
    public async Task GetItemById_NonExistingId_ReturnsNotFound()
    {
        // Arrange
        _mockService.Setup(Service => Service.GetItemByIdAsync(1)).ReturnsAsync((Item)null);

        // Act
        var result = await _controller.GetItemById(999);

        // Assert
        Assert.IsInstanceOfType(result.Result, typeof(NotFoundObjectResult));
    }

    //[TestMethod]
    //public async Task PostItem_ValidItem_ReturnsOk()
    //{
    //    // Arrange
    //    var item = new Item { ItemId = 1, Name = "Test Item" };
    //    _mockService.Setup(service => service.AddItemAsync(item)).ReturnsAsync(item);

    //    // Act
    //    var result = await _controller.AddItem(item);

    //    // Assert
    //    var okResult = result.Result as OkObjectResult;
    //    Assert.IsNotNull(okResult);
    //    Assert.AreEqual(item, okResult.Value);
    //}

    [TestMethod]
    public async Task AddItemFromNote_ReturnsOk_WithCreatedItem()
    {
        // Arrange
        var dto = new ItemFromNoteDTO { NoteId = 1, Name = "Sting" };
        var expectedItem = new Item { ItemId = 42, Name = "Sting", Notes = new List<Note> { new Note { NoteId = 1 } } };

        _mockService.Setup(s => s.AddItemFromNoteAsync(dto))
                    .ReturnsAsync(expectedItem);

        // Act
        var result = await _controller.AddItemFromNote(dto);

        // Assert
        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(200, okResult.StatusCode);
        var returnedItem = okResult.Value as Item;
        Assert.IsNotNull(returnedItem);
        Assert.AreEqual("Sting", returnedItem.Name);
    }

    //[TestMethod]
    //public async Task PostItem_NullItem_ReturnsBadRequest()
    //{
    //    // Arrange
    //    Item item = null;

    //    // Act
    //    var result = await _controller.AddItem(item);

    //    // Assert
    //    Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
    //}

    [TestMethod]
    public async Task UpdateItem_IdMismatch_ReturnsBadRequest()
    {
        // Arrange
        var item = new Item {  ItemId = 2 };

        // Act
        var result = await _controller.UpdateItem(1, item);

        // Assert
        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
    }

    [TestMethod]
    public async Task DeleteItem_ExistingId_ReturnsOk()
    {
        // Arrange
        var testItem = new Item { ItemId = 1 };
        _mockService.Setup(service => service.GetItemByIdAsync(1)).ReturnsAsync(testItem);
        _mockService.Setup(service => service.DeleteItemAsync(1)).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.DeleteItem(1);

        // Assert
        Assert.IsInstanceOfType(result, typeof(OkResult));
    }

    [TestMethod]
    public async Task DeleteItem_NonExistingId_ReturnsNotFound()
    {
        // Arrange
        _mockService.Setup(service => service.DeleteItemAsync(999)).Throws(new KeyNotFoundException());

        // Act
        var result = await _controller.DeleteItem(999);

        // Assert
        Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
    }
}
