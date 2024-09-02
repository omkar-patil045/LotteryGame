using Moq;
using NUnit.Framework;
using LotteryGame.Models;
using LotteryGame.Services;

namespace LotteryGame.Tests.Services
{
    [TestFixture]
    public class PlayerGenerationServiceTests
    {
        private PlayerGenerationService _playerGenerationService;
        private LotterySettings _settings;

        [SetUp]
        public void Setup()
        {
            _settings = new LotterySettings
            {
                TicketPrice = 1.00m,
                GrandPrizePercentage = 0.50m,
                SecondTierPercentage = 0.30m,
                ThirdTierPercentage = 0.10m,
                MinPlayers = 10,
                MaxPlayers = 15
            };

            _playerGenerationService = new PlayerGenerationService(_settings);
        }

        [Test]
        public void GenerateRandomPlayerCount_ShouldReturnValueWithinRange()
        {
            // Act
            int count = _playerGenerationService.GenerateRandomPlayerCount();

            // Assert
            Assert.That(count, Is.InRange(10, 15));
        }

        [Test]
        public void GeneratePlayers_ShouldCreateCorrectNumberOfPlayers()
        {
            // Act
            var players = _playerGenerationService.GeneratePlayers(5);

            // Assert
            Assert.AreEqual(5, players.Count);
            Assert.AreEqual("Player 1", players.First().Name);
        }
    }
}
