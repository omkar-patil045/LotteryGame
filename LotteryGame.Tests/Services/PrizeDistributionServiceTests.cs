using NUnit.Framework;
using LotteryGame.Models;
using LotteryGame.Services;
using System.Collections.Generic;
using System.Linq;
using LotteryGame.Interfaces;
using Moq;

namespace LotteryGame.Tests.Services
{
    [TestFixture]
    public class PrizeDistributionServiceTests
    {
        private PrizeDistributionService _prizeDistributionService;
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
            _prizeDistributionService = new PrizeDistributionService(_settings);
        }

        [Test]
        public void DistributePrizes_ShouldNotDistribute_WhenPlayerCountIsInvalid()
        {
            // Arrange
            var players = new List<Player>
            {
                new Player("Player 1", 10)
                // Only 1 player, which is less than the required minimum of 10 players
            };
            int totalRevenue = players.Count * (int)_settings.TicketPrice;

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => _prizeDistributionService.DistributePrizes(players, totalRevenue), "Lottery cannot proceed with less than 10 players.");
        }

        [Test]
        public void DistributePrizes_ShouldDistributeCorrectly_WhenPlayerCountIsValidAndTicketsAreWithinRange()
        {
            // Arrange
            var players = new List<Player>();
            for (int i = 1; i <= 12; i++) // Between 10 and 15 players
            {
                players.Add(new Player($"Player {i}", 10)); // Each starts with $10 balance
            }

            // Set up ticket purchases where each player can buy a maximum of 10 tickets
            foreach (var player in players)
            {
                player.PurchaseTickets(7); // Assuming PurchaseTickets is a method on Player
            }

            var userInputService = new Mock<IUserInputService>();
            var playerService = new Mock<IPlayerService>();
            var playerGenerationService = new Mock<IPlayerGenerationService>();
            var cpuTicketService = new Mock<ICPUTicketService>();
            var prizeDistributionService = new Mock<IPrizeDistributionService>();
            var lotteryResultDisplayService = new Mock<ILotteryResultDisplayService>();

            // Setting up the mocked services to return appropriate values
            playerGenerationService.Setup(p => p.GenerateRandomPlayerCount()).Returns(players.Count);
            playerGenerationService.Setup(p => p.GeneratePlayers(It.IsAny<int>())).Returns(players);

            var lotteryManager = new LotteryManager(
                prizeDistributionService.Object,
                userInputService.Object,
                playerService.Object,
                playerGenerationService.Object,
                cpuTicketService.Object,
                lotteryResultDisplayService.Object
            );

            // Act
            lotteryManager.RunLottery();

            // Assert
            var totalTickets = players.Sum(p => p.TicketsPurchased);
            var totalRevenue = totalTickets * (int)_settings.TicketPrice;

            prizeDistributionService.Verify(p => p.DistributePrizes(It.Is<List<Player>>(l => l.Count == players.Count), It.Is<int>(r => r == totalRevenue)), Times.Once);
        }


    }
}
