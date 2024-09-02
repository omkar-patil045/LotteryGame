using Moq;
using NUnit.Framework;
using LotteryGame.Models;
using LotteryGame.Services;
using System;

namespace LotteryGame.Tests.Services
{
    [TestFixture]
    public class UserInputServiceTests
    {
        private LotterySettings _settings;
        private UserInputService _userInputService;

        [SetUp]
        public void Setup()
        {
            
            _settings = new LotterySettings
            {
                TicketPrice = 1.00m
            };

            _userInputService = new UserInputService(_settings);
        }

        [Test]
        public void GetPlayerTickets_ShouldReturnValidNumberOfTickets()
        {
            // Arrange
            var player = new Player("Test Player", 10);
            Console.SetIn(new System.IO.StringReader("5"));

            // Act
            int tickets = _userInputService.GetPlayerTickets(player);

            // Assert
            Assert.AreEqual(5, tickets);
        }
    }
}
