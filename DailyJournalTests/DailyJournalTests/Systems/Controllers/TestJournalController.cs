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
    }

     
}
