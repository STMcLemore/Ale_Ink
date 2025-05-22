using Ale_Ink.API.Services;
using Ale_Ink.API.Data;
using Ale_Ink.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace Ale_Ink.Tests
{
    [TestClass]
    public sealed class NoteServiceTest
    {

        private AppDbContext GetMockedDbContextWithNotes() // Helper method to create a mocked DbContext with test data
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Ensures isolation between tests
                .Options;

            var context = new AppDbContext(options);

            // Seed test notes
            context.Notes.AddRange(
                new Note { NoteId = 1, Content = "Test Note 1"},
                new Note { NoteId = 2, Content = "Another Note"}
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
        public async Task GetAllNotesAsync_ReturnsAllNotes()
        {
            // Arrange
            var mockContext = GetMockedDbContextWithNotes();
            var service = new NoteService(mockContext);

            // Act
            var result = await service.GetAllNotesAsync();

            // Assert
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public async Task GetNoteByIdAsync_ExistingId_ReturnsNote()
        {
            // Arrange
            var mockContext = GetMockedDbContextWithNotes();
            var service = new NoteService(mockContext);

            // Act
            var result = await service.GetNoteByIdAsync(1);

            // Assert
            Assert.AreEqual(1, result.NoteId);
        }

        [TestMethod]
        public async Task GetNoteByIdAsync_NonExistingId_ReturnsNull()
        {
            //Arrange
            var mockContext = GetMockedDbContextWithNotes();
            var service = new NoteService(mockContext);
            // Act
            var result = await service.GetNoteByIdAsync(999);
            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task AddNoteAsync_AddsNote()
        {
            // Arrange
            var mockContext = GetMockedDbContext();
            var service = new NoteService(mockContext);
            var newNote = new Note { Content = "Test Note" };

            // Act
            var result = await service.AddNoteAsync(newNote);

            // Assert
            Assert.AreEqual(newNote.Content, result.Content);
        }
    }
}
