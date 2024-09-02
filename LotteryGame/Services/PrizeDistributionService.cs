using LotteryGame.Interfaces;
using LotteryGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LotteryGame.Services
{
    // Service responsible for distributing prizes based on ticket sales and game rules
    public class PrizeDistributionService : IPrizeDistributionService
    {
        private readonly LotterySettings _settings;

        public PrizeDistributionService(LotterySettings settings)
        {
            _settings = settings;
        }

        public void DistributePrizes(List<Player> players, int totalRevenue)
        {
            if (players.Count < _settings.MinPlayers || players.Count > _settings.MaxPlayers)
            {
                throw new InvalidOperationException("Lottery cannot proceed with less than 10 players or more than 15 players.");
            }

            int totalTickets = players.Sum(p => p.TicketsPurchased);

            // No tickets purchased scenario
            if (totalTickets == 0)
            {
                throw new InvalidOperationException("No tickets have been purchased.");
            }

            decimal grandPrizeAmount = Math.Round(totalRevenue * _settings.GrandPrizePercentage, 2);
            decimal secondTierAmount = Math.Round(totalRevenue * _settings.SecondTierPercentage, 2);
            decimal thirdTierAmount = Math.Round(totalRevenue * _settings.ThirdTierPercentage, 2);

            List<Player> ticketHolders = new List<Player>();
            foreach (var player in players)
            {
                for (int i = 0; i < player.TicketsPurchased; i++)
                {
                    ticketHolders.Add(player);
                }
            }

            Random random = new Random();

            // Determine Grand Prize winner
            var grandPrizeWinner = ticketHolders[random.Next(ticketHolders.Count)];
            grandPrizeWinner.AddWinnings(grandPrizeAmount);
            ticketHolders.Remove(grandPrizeWinner);

            // Determine Second Tier winners
            int secondTierWinnersCount = Math.Max((int)Math.Round(totalTickets * 0.10), 1); // Ensure at least one winner
            decimal secondTierPrizePerWinner = secondTierWinnersCount > 0 ? Math.Round(secondTierAmount / secondTierWinnersCount, 2) : 0;

            for (int i = 0; i < secondTierWinnersCount; i++)
            {
                if (ticketHolders.Count == 0) break; // No more ticket holders left
                var winner = ticketHolders[random.Next(ticketHolders.Count)];
                winner.AddWinnings(secondTierPrizePerWinner);
                ticketHolders.Remove(winner);
            }

            // Determine Third Tier winners
            int thirdTierWinnersCount = Math.Max((int)Math.Round(totalTickets * 0.20), 1); // Ensure at least one winner
            decimal thirdTierPrizePerWinner = thirdTierWinnersCount > 0 ? Math.Round(thirdTierAmount / thirdTierWinnersCount, 2) : 0;

            for (int i = 0; i < thirdTierWinnersCount; i++)
            {
                if (ticketHolders.Count == 0) break; // No more ticket holders left
                var winner = ticketHolders[random.Next(ticketHolders.Count)];
                winner.AddWinnings(thirdTierPrizePerWinner);
                ticketHolders.Remove(winner);
            }
        }
    }
}
