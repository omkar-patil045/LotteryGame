using LotteryGame.Interfaces;
using LotteryGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotteryGame.Services
{
    // Service that handles the entire lottery process, including player generation, ticket purchasing, and prize distribution
    public class LotteryManager : ILotteryManager
    {
        private readonly List<Player> _players;
        private readonly IPrizeDistributionService _prizeDistributionService;
        private readonly IUserInputService _userInputService;
        private readonly IPlayerService _playerService;
        private readonly IPlayerGenerationService _playerGenerationService;
        private readonly ICPUTicketService _cpuTicketService;
        private readonly ILotteryResultDisplayService _lotteryResultDisplayService;

        public LotteryManager(
            IPrizeDistributionService prizeDistributionService,
            IUserInputService userInputService,
            IPlayerService playerService,
            IPlayerGenerationService playerGenerationService,
            ICPUTicketService cpuTicketService,
            ILotteryResultDisplayService lotteryResultDisplayService)
        {
            _players = new List<Player>();
            _prizeDistributionService = prizeDistributionService;
            _userInputService = userInputService;
            _playerService = playerService;
            _playerGenerationService = playerGenerationService;
            _cpuTicketService = cpuTicketService;
            _lotteryResultDisplayService = lotteryResultDisplayService;
        }

        // Manages the overall flow of the lottery game
        public void RunLottery()
        {
            try
            {
                // Generate random number of players
                int totalPlayers = _playerGenerationService.GenerateRandomPlayerCount();
                _players.AddRange(_playerGenerationService.GeneratePlayers(totalPlayers));

                var humanPlayer = _players.First();
                int ticketsToPurchase = _userInputService.GetPlayerTickets(humanPlayer);
                _playerService.PurchaseTickets(humanPlayer, ticketsToPurchase);

                // Display the remaining balance of Player 1
                Console.WriteLine($"\nYour remaining balance: ${humanPlayer.Balance:F2}");

                // Handle ticket purchases for CPU players
                Console.WriteLine("\n\nCPU players are purchasing tickets...");
                _cpuTicketService.HandleCPUTicketPurchases(_players.Skip(1));

                Console.WriteLine($"\n{_players.Count - 1} other CPU players also have purchased tickets.");

                var totalRevenue = _players.Sum(p => p.TicketsPurchased);

                // Draw the tickets and distribute prizes
                Console.WriteLine("\nDrawing the tickets...");
                _prizeDistributionService.DistributePrizes(_players, totalRevenue);

                _lotteryResultDisplayService.DisplayResults(_players, totalRevenue);

                // Display menu for next actions
                DisplayMenu();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public void DisplayMenu()
        {
            while (true)
            {
                Console.WriteLine("\nMenu:");
                Console.WriteLine("1. Restart the lottery");
                Console.WriteLine("2. Quit");
                Console.Write("Please select an option (1 Or 2): ");

                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        Console.Clear();
                        // Reset player list and start a new lottery
                        _players.Clear();
                        RunLottery(); // Restart lottery
                        break;

                    case "2":
                        Console.WriteLine("Thank you for playing!");
                        Environment.Exit(0); // Exit the application
                        break;

                    default:
                        Console.WriteLine("Invalid input. Please enter 1 Or 2");
                        break;
                }
            }
        }

    }
}
