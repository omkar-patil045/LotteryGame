using LotteryGame.Interfaces;
using LotteryGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotteryGame.Services
{
    // Service responsible for handling user input
    public class UserInputService : IUserInputService
    {

        private readonly LotterySettings _settings;

        public UserInputService(LotterySettings settings)
        {
            _settings = settings;
        }

        // Prompts the user to input the number of tickets they want to purchase
        public int GetPlayerTickets(Player player)
        {
            Console.WriteLine($"Welcome to the Bede Lottery, {player.Name}!");
            Console.WriteLine($"* Your digital balance: ${player.Balance:F2}");
            Console.WriteLine($"* Ticket Price: ${_settings.TicketPrice:F2} each");
            Console.Write("How many tickets do you want to buy? ");
            int ticketsToPurchase;
            while (!int.TryParse(Console.ReadLine(), out ticketsToPurchase) || ticketsToPurchase < 1 || ticketsToPurchase > player.Balance)
            {
                Console.WriteLine("Invalid input. Please enter a valid number of tickets.");
            }
            return ticketsToPurchase;
        }
    }
}
