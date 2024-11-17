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
            var createdDate = DateTime.Now;

            var createdJournal = new Journal { Id = 1, Title = title, Description = content, CreatedDate =  createdDate};

            mockJournalService
                .Setup(service => service.AddEntryAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>()))
                .ReturnsAsync(createdJournal);

            var sut = new JournalController(mockJournalService.Object);

            // Act
            var result = (CreatedAtActionResult)await sut.AddEntry(title, content, createdDate);

            // Assert
            result.StatusCode.Should().Be(201);
            result.Value.Should().BeEquivalentTo(createdJournal);
            mockJournalService.Verify(service => service.AddEntryAsync(title, content,createdDate), Times.Once);
        }

        [Fact]
        public async Task AddEntry_WithInvalidData_ReturnsStatusCode400()
        {
            // Arrange
            var mockJournalService = new Mock<IJournalService>();
            var title = ""; 
            var content = "";
            DateTime date = DateTime.Now;

            var sut = new JournalController(mockJournalService.Object);

            // Act
            var result = (BadRequestObjectResult)await sut.AddEntry(title, content,date);

            // Assert
            result.StatusCode.Should().Be(400);
            result.Value.Should().Be("Title and content cannot be empty.");
            mockJournalService.Verify(service => service.AddEntryAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<DateTime>()), Times.Never);
        }

        [Fact]
        public async Task EditEntry_ServiceFailure_ReturnsStatusCode500()
        {
            // Arrange
            var mockJournalService = new Mock<IJournalService>();
            var id = 1;
            var title = "Updated Title";
            var content = "Updated Content";
            DateTime date = DateTime.Now;

            mockJournalService
                .Setup(service => service.UpdateEntryAsync(id, title, content,date))
                .ReturnsAsync(false); 

            var sut = new JournalController(mockJournalService.Object);

            // Act
            var result = (ObjectResult)await sut.UpdateEntry(id, title, content,date);

            // Assert
            result.StatusCode.Should().Be(500);
            result.Value.Should().Be("Failed to update the journal entry.");
            mockJournalService.Verify(service => service.UpdateEntryAsync(id, title, content, date), Times.Once);
        }


        [Fact]
        public async Task DeleteEntry_ServiceFailure_ReturnsStatusCode500()
        {
            // Arrange
            var mockJournalService = new Mock<IJournalService>();
            var id = 1;

            mockJournalService
                .Setup(service => service.DeleteEntryAsync(id))
                .ReturnsAsync(false); 

            var sut = new JournalController(mockJournalService.Object);

            // Act
            var result = (ObjectResult)await sut.DeleteEntry(id);

            // Assert
            result.StatusCode.Should().Be(500);
            result.Value.Should().Be("Failed to delete the journal entry.");
            mockJournalService.Verify(service => service.DeleteEntryAsync(id), Times.Once);
        }

        [Fact]
        public async Task FilterByDate_NoEntriesFound_ReturnsStatusCode404()
        {
            // Arrange
            var mockJournalService = new Mock<IJournalService>();
            var startDate = new DateTime(2024, 01, 01);
         

            mockJournalService
                .Setup(service => service.FilterByDateAsync(startDate))
                 .ReturnsAsync((Journal)null);

            var sut = new JournalController(mockJournalService.Object);

            // Act
            var result = (NotFoundObjectResult)await sut.FilterByDate(startDate);

            // Assert
            result.StatusCode.Should().Be(404);
            result.Value.Should().Be("No journal entries found for the specified date range.");
            mockJournalService.Verify(service => service.FilterByDateAsync(startDate), Times.Once);
        }

        [Fact]
        public async Task Register_OnSuccess_ReturnsStatusCode200()
        {
            // Arrange
            var mockJournalService = new Mock<IJournalService>();

            var username = "testuser";
            var password = "password123";

            mockJournalService
                .Setup(service => service.RegisterUserAsync(username, password))
                .Returns(Task.CompletedTask); 

            var sut = new JournalController(mockJournalService.Object);

            // Act
            var result = (OkObjectResult)await sut.Register(username, password);

            // Assert
            result.StatusCode.Should().Be(200);
            result.Value.Should().Be("User registered successfully.");
            mockJournalService.Verify(service => service.RegisterUserAsync(username, password), Times.Once);
        }

        [Fact]
        public async Task Login_ValidCredentials_ReturnsStatusCode200()
        {
            // Arrange
            var mockJournalService = new Mock<IJournalService>();

            var username = "testuser";
            var password = "password123";

            
            mockJournalService
                .Setup(service => service.LoginAsync(username, password))
                .ReturnsAsync(true);

            var sut = new JournalController(mockJournalService.Object);

            // Act
            var result = (OkObjectResult)await sut.Login(username, password);

            // Assert
            result.StatusCode.Should().Be(200); 
            result.Value.Should().Be("Login successful.");
            mockJournalService.Verify(service => service.LoginAsync(username, password), Times.Once);
        }



    }


}
