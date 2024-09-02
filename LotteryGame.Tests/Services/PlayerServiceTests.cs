using Moq;
using NUnit.Framework;
using LotteryGame.Interfaces;
using LotteryGame.Models;
using LotteryGame.Services;

namespace LotteryGame.Tests.Services
{
    [TestFixture]
    public class PlayerServiceTests
    {
        private PlayerService _playerService;
        private LotterySettings _settings;

        [SetUp]
        public void Setup()
        {
            _settings = new LotterySettings
            {
                TicketPrice = 1.00m
            };
            _playerService = new PlayerService();
        }

        [Test]
        public void PurchaseTickets_ShouldUpdatePlayerBalanceAndTickets()
        {
            // Arrange
            var player = new Player("Test Player", 10);
            int ticketsToPurchase = 5;

            // Act
            _playerService.PurchaseTickets(player, ticketsToPurchase);

            // Assert
            Assert.AreEqual(5, player.TicketsPurchased);
            Assert.AreEqual(5, player.Balance);  // Balance should decrease by tickets purchased * ticket price
        }
    }
}
