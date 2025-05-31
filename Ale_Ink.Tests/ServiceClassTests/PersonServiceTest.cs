using Ale_Ink.API.Services;
using Ale_Ink.API.Data;
using Ale_Ink.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace Ale_Ink.Tests.ServiceClassTests;

[TestClass]
public sealed class PersonServiceTest
{

    private AppDbContext GetMockedDbContextWithPersons() // Helper method to create a mocked DbContext with test data
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Ensures isolation between tests
            .Options;

        var context = new AppDbContext(options);

        // Seed test persons
        context.People.AddRange(
            new Person { PersonId = 1, Name = "Test Person 1" },
            new Person { PersonId = 2, Name = "Another Person" }
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
    public async Task GetAllPersonsAsync_ReturnsAllPersons()
    {
        // Arrange
        var mockContext = GetMockedDbContextWithPersons();
        var service = new PersonService(mockContext);

        // Act
        var result = await service.GetAllPersonsAsync();

        // Assert
        Assert.AreEqual(2, result.Count());
    }

    [TestMethod]
    public async Task GetPersonByIdAsync_ExistingId_ReturnsPerson()
    {
        // Arrange
        var mockContext = GetMockedDbContextWithPersons();
        var service = new PersonService(mockContext);

        // Act
        var result = await service.GetPersonByIdAsync(1);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual("Test Person 1", result.Name);
    }

    [TestMethod]
    public async Task GetPersonByIdAsync_NonExistingId_ReturnsNull()
    {
        // Arrange
        var mockContext = GetMockedDbContextWithPersons();
        var service = new PersonService(mockContext);

        // Act
        var result = await service.GetPersonByIdAsync(999); // Non-existing ID

        // Assert
        Assert.IsNull(result);
    }

    [TestMethod]
    public async Task AddPersonAsync_AddsNewPerson()
    {
        // Arrange
        var mockContext = GetMockedDbContext();
        var service = new PersonService(mockContext);
        var newPerson = new Person { Name = "New Person" };

        // Act
        await service.AddPersonAsync(newPerson);
        await mockContext.SaveChangesAsync();

        // Assert
        var addedPerson = await mockContext.People.FindAsync(newPerson.PersonId);
        Assert.IsNotNull(addedPerson);
        Assert.AreEqual("New Person", addedPerson.Name);
    }
}
