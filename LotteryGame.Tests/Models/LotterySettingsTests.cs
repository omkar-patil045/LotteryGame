using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;
using LotteryGame.Models;

namespace LotteryGame.Tests.Models
{
    [TestFixture]
    public class LotterySettingsTests
    {
        [Test]
        public void LotterySettings_ShouldInitializeCorrectly()
        {
            // Arrange
            var settings = new LotterySettings
            {
                TicketPrice = 1.00m,
                GrandPrizePercentage = 0.50m,
                SecondTierPercentage = 0.30m,
                ThirdTierPercentage = 0.10m,
                MinPlayers = 10,
                MaxPlayers = 15
            };

            // Act & Assert
            Assert.AreEqual(1.00m, settings.TicketPrice);
            Assert.AreEqual(0.50m, settings.GrandPrizePercentage);
            Assert.AreEqual(0.30m, settings.SecondTierPercentage);
            Assert.AreEqual(0.10m, settings.ThirdTierPercentage);
            Assert.AreEqual(10, settings.MinPlayers);
            Assert.AreEqual(15, settings.MaxPlayers);
        }
    }
}

