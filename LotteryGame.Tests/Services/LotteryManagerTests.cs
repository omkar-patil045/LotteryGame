using Moq;
using NUnit.Framework;
using LotteryGame.Interfaces;
using LotteryGame.Models;
using LotteryGame.Services;
using System.Collections.Generic;
using System.Linq;

namespace LotteryGame.Tests.Services
{
    [TestFixture]
    public class LotteryManagerTests
    {
        private Mock<IPrizeDistributionService> _mockPrizeDistributionService;
        private Mock<IUserInputService> _mockUserInputService;
        private Mock<IPlayerService> _mockPlayerService;
        private Mock<IPlayerGenerationService> _mockPlayerGenerationService;
        private Mock<ICPUTicketService> _mockCPUTicketService;
        private Mock<ILotteryResultDisplayService> _mockLotteryResultDisplayService;
        private LotteryManager _lotteryManager;

        [SetUp]
        public void Setup()
        {
            _mockPrizeDistributionService = new Mock<IPrizeDistributionService>();
            _mockUserInputService = new Mock<IUserInputService>();
            _mockPlayerService = new Mock<IPlayerService>();
            _mockPlayerGenerationService = new Mock<IPlayerGenerationService>();
            _mockCPUTicketService = new Mock<ICPUTicketService>();
            _mockLotteryResultDisplayService = new Mock<ILotteryResultDisplayService>();

            _lotteryManager = new LotteryManager(
                _mockPrizeDistributionService.Object,
                _mockUserInputService.Object,
                _mockPlayerService.Object,
                _mockPlayerGenerationService.Object,
                _mockCPUTicketService.Object,
                _mockLotteryResultDisplayService.Object
            );
        }

        [Test]
        public void RunLottery_ShouldGeneratePlayersAndHandlePurchases()
        {
            // Arrange
            var players = new List<Player>
            {
                new Player("Player 1", 10),
                new Player("Player 2", 10)
            };

            _mockPlayerGenerationService
                .Setup(p => p.GenerateRandomPlayerCount())
                .Returns(players.Count);

            _mockPlayerGenerationService
                .Setup(p => p.GeneratePlayers(It.IsAny<int>()))
                .Returns(players);

            _mockUserInputService
                .Setup(u => u.GetPlayerTickets(It.IsAny<Player>()))
                .Returns(5);

            // Act
            _lotteryManager.RunLottery();

            // Assert
            _mockPlayerGenerationService.Verify(p => p.GeneratePlayers(It.IsAny<int>()), Times.Once);
            _mockPlayerService.Verify(p => p.PurchaseTickets(It.IsAny<Player>(), It.IsAny<int>()), Times.Once);
            _mockCPUTicketService.Verify(c => c.HandleCPUTicketPurchases(It.IsAny<IEnumerable<Player>>()), Times.Once);
            _mockPrizeDistributionService.Verify(p => p.DistributePrizes(It.IsAny<List<Player>>(), It.IsAny<int>()), Times.Once);
            _mockLotteryResultDisplayService.Verify(l => l.DisplayResults(It.IsAny<List<Player>>(), It.IsAny<int>()), Times.Once);
        }


    }
}
