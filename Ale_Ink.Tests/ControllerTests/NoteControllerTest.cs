using Moq;
using Microsoft.AspNetCore.Mvc;
using Ale_Ink.API.Controllers;
using Ale_Ink.API.Services;
using Ale_Ink.Shared.Models;

namespace Ale_Ink.Tests;

[TestClass]
public sealed class NoteControllerTest
{
    private Mock<INoteService> _mockService;
    private NoteController _controller;

    [TestInitialize]
    public void Setup()
    {
        _mockService = new Mock<INoteService>();
        _controller = new NoteController(_mockService.Object);
    }

    [TestMethod]
    public async Task GetAllNotes_ReturnsOkResult_WithNotes()
    {
        // Arrange
        var notes = new List<Note>
        {
            new Note { NoteId = 1, Content = "Test Note 1" },
            new Note { NoteId = 2, Content = "Test Note 2" }
        };
        _mockService.Setup(service => service.GetAllNotesAsync()).ReturnsAsync(notes);

        // Act
        var result = await _controller.GetAllNotes();

        // Assert
        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(notes, okResult.Value);
    }

    [TestMethod]
    public async Task GetNoteById_ExistingId_ReturnsOkResult_WithNote()
    {
        // Arrange
        var note = new Note { NoteId = 1, Content = "Test Note" };
        _mockService.Setup(service => service.GetNoteByIdAsync(1)).ReturnsAsync(note);

        // Act
        var result = await _controller.GetNoteById(1);

        // Assert
        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(note, okResult.Value);
    }

    [TestMethod]
    public async Task GetNoteById_NonExistingId_ReturnsNotFound()
    {
        // Arrange
        _mockService.Setup(service => service.GetNoteByIdAsync(999)).ReturnsAsync((Note)null);

        // Act
        var result = await _controller.GetNoteById(999);

        // Assert
        Assert.IsInstanceOfType(result.Result, typeof(NotFoundObjectResult));
    }

    [TestMethod]
    public async Task  PostNote_ValidNote_ReturnsOk()
    {
        // Arrange
        var note = new Note { NoteId = 1, Content = "New Note" };
        _mockService.Setup(service => service.AddNoteAsync(note)).ReturnsAsync(note);

        // Act
        var result = await _controller.PostNote(note);

        // Assert
        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(note, okResult.Value);
    }

    [TestMethod]
    public async Task PostNote_NullNote_ReturnsBadRequest()
    {
        // Arrange
        Note note = null;

        // Act
        var result = await _controller.PostNote(note);

        // Assert
        Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
    }

    [TestMethod]
    public async Task UpdateNote_Valid_ReturnOk()
    {
        // Arrange
        var note = new Note { NoteId = 1, Content = "Updated Note" };
        _mockService.Setup(service => service.UpdateNoteAsync(1, note)).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.UpdateNote(1, note);

        // Assert
        Assert.IsInstanceOfType(result, typeof(OkResult));
    }

    [TestMethod]
    public async Task UpdateNote_IdMismatch_ReturnsBadRequest()
    {
        // Arrange
        var note = new Note { NoteId = 2 };

        // Act
        var result = await _controller.UpdateNote(1, note);

        // Assert
        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
    }

    [TestMethod]
    public async Task DeleteNote_ExistingId_ReturnsOk()
    {
        // Arrange
        _mockService.Setup(service => service.DeleteNoteAsync(1)).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.DeleteNote(1);

        // Assert
        Assert.IsInstanceOfType(result, typeof(OkResult));
    }

    [TestMethod]
    public async Task DeleteNote_NonExistingId_ReturnsNotFound()
    {
        // Arrange
        _mockService.Setup(service => service.DeleteNoteAsync(999)).Throws(new KeyNotFoundException());

        // Act
        var result = await _controller.DeleteNote(999);

        // Assert
        Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
    }

    [TestMethod]
    public async Task SearchNotes_ValidKeyword_ReturnsNotes()
    {
        // Arrange
        var notes = new List<Note>         {
            new Note { NoteId = 1, Content = "Test Note 1" },
            new Note { NoteId = 2, Content = "Test Note 2" }
        };
        _mockService.Setup(service => service.GetNotesByKeywordAsync("Test")).ReturnsAsync(notes);

        // Act
        var result = await _controller.SearchNotes("Test");

        // Assert
        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);
        var returnedNotes = okResult.Value as IEnumerable<Note>;
        Assert.AreEqual(2, returnedNotes.Count());
    }
}
