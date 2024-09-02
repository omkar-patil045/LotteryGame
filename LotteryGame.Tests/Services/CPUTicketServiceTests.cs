using Moq;
using NUnit.Framework;
using LotteryGame.Interfaces;
using LotteryGame.Models;
using LotteryGame.Services;
using System.Collections.Generic;

namespace LotteryGame.Tests.Services
{
    [TestFixture]
    public class CPUTicketServiceTests
    {
        private CPUTicketService _cpuTicketService;
        private Mock<IPlayerService> _mockPlayerService;

        [SetUp]
        public void Setup()
        {
            _mockPlayerService = new Mock<IPlayerService>();
            _cpuTicketService = new CPUTicketService(_mockPlayerService.Object);
        }

        [Test]
        public void HandleCPUTicketPurchases_ShouldCallPurchaseTicketsOnPlayers()
        {
            // Arrange
            var cpuPlayers = new List<Player>
            {
                new Player("CPU Player 1"),
                new Player("CPU Player 2")
            };
            _mockPlayerService.Setup(ps => ps.PurchaseTickets(It.IsAny<Player>(), It.IsAny<int>()));

            // Act
            _cpuTicketService.HandleCPUTicketPurchases(cpuPlayers);

            // Assert
            _mockPlayerService.Verify(ps => ps.PurchaseTickets(It.IsAny<Player>(), It.IsAny<int>()), Times.Exactly(cpuPlayers.Count));
        }
    }
}
