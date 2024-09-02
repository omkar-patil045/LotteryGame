using NUnit.Framework;
using LotteryGame.Models;

namespace LotteryGame.Tests.Models
{
    [TestFixture]
    public class PlayerTests
    {
        [Test]
        public void PurchaseTickets_ShouldUpdateTicketsAndBalance()
        {
            // Arrange
            var player = new Player("Test Player", 10);

            // Act
            player.PurchaseTickets(5);

            // Assert
            Assert.AreEqual(5, player.TicketsPurchased);
            Assert.AreEqual(5, player.Balance);
        }

        [Test]
        public void AddWinnings_ShouldIncreaseWinnings()
        {
            // Arrange
            var player = new Player("Test Player");

            // Act
            player.AddWinnings(10);

            // Assert
            Assert.AreEqual(10, player.Winnings);
        }
    }
}
