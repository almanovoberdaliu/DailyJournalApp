using DailyJournal.Controllers;
using DailyJournal.Interfaces;
using DailyJournal.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyJournalTests.Systems.Controllers
{
    public class TestJournalController
    {
        [Fact]
        public async Task Get_OnSuccess_ReturnsStatusCode200() 
        {
            var mockJournalsService = new Mock<IJournalService>();

            var sut = new JournalController(mockJournalsService.Object);

            var result = (OkObjectResult) await sut.GetAllEntries();

            result.StatusCode.Should().Be(200);
            
        }


        [Fact]
        public async Task Get_OnSuccess_InvokesJournalServiceExactlyOnce() 
        {
            var mockJournalsService = new Mock<IJournalService>();

            mockJournalsService.Setup(service => service.GetAllEntriesAsync()).ReturnsAsync(new List<Journal>());
            var sut = new JournalController(mockJournalsService.Object);

            var result = await sut.GetAllEntries();

            mockJournalsService.Verify(
                service => service.GetAllEntriesAsync(),
                Times.Once());
        
        }

        [Fact]
        public async Task AddEntry_OnSuccess_ReturnsStatusCode201()
        {
            // Arrange
            var mockJournalService = new Mock<IJournalService>();
            var title = "Test Title";
            var content = "Test Content";

            var createdJournal = new Journal { Id = 1, Title = title, Description = content };

            mockJournalService
                .Setup(service => service.AddEntryAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(createdJournal);

            var sut = new JournalController(mockJournalService.Object);

            // Act
            var result = (CreatedAtActionResult)await sut.AddEntry(title, content);

            // Assert
            result.StatusCode.Should().Be(201);
            result.Value.Should().BeEquivalentTo(createdJournal);
            mockJournalService.Verify(service => service.AddEntryAsync(title, content), Times.Once);
        }

        [Fact]
        public async Task AddEntry_WithInvalidData_ReturnsStatusCode400()
        {
            // Arrange
            var mockJournalService = new Mock<IJournalService>();
            var title = ""; 
            var content = "";

            var sut = new JournalController(mockJournalService.Object);

            // Act
            var result = (BadRequestObjectResult)await sut.AddEntry(title, content);

            // Assert
            result.StatusCode.Should().Be(400);
            result.Value.Should().Be("Title and content cannot be empty.");
            mockJournalService.Verify(service => service.AddEntryAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async Task EditEntry_ServiceFailure_ReturnsStatusCode500()
        {
            // Arrange
            var mockJournalService = new Mock<IJournalService>();
            var id = 1;
            var title = "Updated Title";
            var content = "Updated Content";

            mockJournalService
                .Setup(service => service.UpdateEntryAsync(id, title, content))
                .ReturnsAsync(false); // Supozojmë që metoda kthen 'false' në rast dështimi.

            var sut = new JournalController(mockJournalService.Object);

            // Act
            var result = (ObjectResult)await sut.UpdateEntry(id, title, content);

            // Assert
            result.StatusCode.Should().Be(500);
            result.Value.Should().Be("Failed to update the journal entry.");
            mockJournalService.Verify(service => service.UpdateEntryAsync(id, title, content), Times.Once);
        }




    }


}
