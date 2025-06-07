using Moq;
using Microsoft.AspNetCore.Mvc;
using Ale_Ink.API.Controllers;
using Ale_Ink.API.Services;
using Ale_Ink.Shared.Models;

namespace Ale_Ink.Tests;

[TestClass]
public sealed class PersonControllerTest
{
    private Mock<IPersonService> _mockService;
    private PersonController _controller;

    [TestInitialize]
    public void Setup()
    {
        _mockService = new Mock<IPersonService>();
        _controller = new PersonController(_mockService.Object);
    }

    [TestMethod]
    public async Task GetAllPeople_ReturnsOkResult_WithPeople()
    {
        // Arrange
        var people = new List<Person>
        {
            new Person { PersonId = 1, Name = "John Doe" },
            new Person { PersonId = 2, Name = "Jane Smith" }
        };
        _mockService.Setup(service => service.GetAllPersonsAsync()).ReturnsAsync(people);

        // Act
        var result = await _controller.GetAllPeople();

        // Assert
        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(people, okResult.Value);
    }

    [TestMethod]
    public async Task GetPersonById_ExistingId_ReturnsOkResult_WithPerson()
    {
        // Arrange
        var person = new Person { PersonId = 1, Name = "John Doe" };
        _mockService.Setup(service => service.GetPersonByIdAsync(1)).ReturnsAsync(person);

        // Act
        var result = await _controller.GetPersonById(1);

        // Assert
        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(person, okResult.Value);
    }

    [TestMethod]
    public async Task GetPersonById_NonExistingId_ReturnsNotFound()
    {
        // Arrange
        _mockService.Setup(service => service.GetPersonByIdAsync(999)).ReturnsAsync((Person)null);

        // Act
        var result = await _controller.GetPersonById(999);

        // Assert
        Assert.IsInstanceOfType(result.Result, typeof(NotFoundObjectResult));
    }

    [TestMethod]
    public async Task PostPerson_ValidPerson_ReturnsOk()
    {
        // Arrange
        var person = new Person { PersonId = 1, Name = "John Doe" };
        _mockService.Setup(service => service.AddPersonAsync(person)).ReturnsAsync(person);

        // Act
        var result = await _controller.PostPerson(person);

        // Assert
        var okResult = result.Result as OkObjectResult;
        Assert.IsNotNull(okResult);
        Assert.AreEqual(person, okResult.Value);
    }

    [TestMethod]
    public async Task PostPerson_NullPerson_ReturnsBadRequest()
    {
        // Arrange
        Person person = null;
        // Act
        var result = await _controller.PostPerson(person);
        // Assert
        Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
    }

    [TestMethod]
    public async Task UpdatePerson_Valid_ReturnsOk()
    {
        // Arrange
        var person = new Person { PersonId = 1, Name = "John Doe" };
        _mockService.Setup(service => service.UpdatePersonAsync(1, person)).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.UpdatePerson(1, person);

        // Assert
        Assert.IsInstanceOfType(result, typeof(OkResult));
    }

    [TestMethod]
    public async Task UpdatePerson_NonExistingId_ReturnsNotFound()
    {
        // Arrange
        var person = new Person { PersonId = 2 };

        // Act
        var result = await _controller.UpdatePerson(1, person);

        // Assert
        Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
    }

    [TestMethod]
    public async Task DeletePerson_ExistingId_ReturnsOk()
    {
        // Arrange
        _mockService.Setup(service => service.DeletePersonAsync(1)).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.DeletePerson(1);

        // Assert
        Assert.IsInstanceOfType(result, typeof(OkResult));
    }

    [TestMethod]
    public async Task DeletePerson_NonExistingId_ReturnsNotFound()
    {
        // Arrange
        _mockService.Setup(service => service.DeletePersonAsync(999)).Throws(new KeyNotFoundException());

        // Act
        var result = await _controller.DeletePerson(999);

        // Assert
        Assert.IsInstanceOfType(result, typeof(NotFoundObjectResult));
    }
}
