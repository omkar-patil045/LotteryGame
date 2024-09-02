using LotteryGame.Interfaces;
using LotteryGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotteryGame.Services
{
    // Service responsible for displaying lottery results
    public class LotteryResultDisplayService : ILotteryResultDisplayService
    {
        public void DisplayResults(List<Player> players, int totalRevenue)
        {
            Console.WriteLine("\nTicket Draw Results:");

            // Display Grand Prize winner
            var grandPrizeWinner = players.FirstOrDefault(p => p.Winnings >= totalRevenue * 0.50m);
            if (grandPrizeWinner != null)
            {
                Console.WriteLine($"* Grand Prize: {grandPrizeWinner.Name} wins ${grandPrizeWinner.Winnings:F2}!");
            }
            else
            {
                Console.WriteLine("* Grand Prize: No winner!");
            }

            // Display Second Tier winners
            var secondTierWinners = players.Where(p => p.Winnings < totalRevenue * 0.50m && p.Winnings >= totalRevenue * 0.30m / players.Count).ToList();
            if (secondTierWinners.Any())
            {
                Console.Write("* Second Tier: ");
                Console.WriteLine($"{string.Join(", ", secondTierWinners.Select(w => $"{w.Name}"))} win ${totalRevenue * 0.30m / secondTierWinners.Count():F2} each!");
            }
            else
            {
                Console.WriteLine("* Second Tier: No winners!");
            }

            // Display Third Tier winners
            var thirdTierWinners = players.Where(p => p.Winnings < totalRevenue * 0.30m / players.Count && p.Winnings > 0).ToList();
            if (thirdTierWinners.Any())
            {
                Console.Write("* Third Tier: ");
                Console.WriteLine($"{string.Join(", ", thirdTierWinners.Select(w => $"{w.Name}"))} win ${totalRevenue * 0.10m / thirdTierWinners.Count():F2} each!");
            }
            else
            {
                Console.WriteLine("* Third Tier: No winners!");
            }

            // Display house profit
            decimal houseProfit = totalRevenue - players.Sum(p => p.Winnings);
            Console.WriteLine($"\nCongratulations to the winners!");
            Console.WriteLine($"House Revenue: ${houseProfit:F2}");
        }
    }
}
