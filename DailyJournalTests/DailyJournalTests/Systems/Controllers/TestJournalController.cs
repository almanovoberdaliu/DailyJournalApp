using DailyJournal.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
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
            var sut = new JournalController();

            var result = (OkObjectResult) await sut.GetAllEntries();

            result.StatusCode.Should().Be(200);
            
        }
    }
}
