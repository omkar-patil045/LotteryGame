using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotteryGame.Models
{
    // Represents a player in the lottery game
    public class Player
    {
        public string Name { get; private set; }
        public int Balance { get; private set; } = 10;
        public int TicketsPurchased { get; private set; }
        public decimal Winnings { get; private set; }

        public Player(string name, int initialBalance = 10)
        {
            Name = name;
            Balance = initialBalance;
        }

        // Handles ticket purchase logic, ensuring the player does not exceed their balance
        public void PurchaseTickets(int numberOfTickets)
        {
            int affordableTickets = Math.Min(numberOfTickets, Balance);
            TicketsPurchased += affordableTickets;
            Balance -= affordableTickets;
        }

        // Adds winnings to the player's balance
        public void AddWinnings(decimal amount)
        {
            Winnings += amount;
        }

        public override string ToString()
        {
            return $"{Name} - Tickets Purchased: {TicketsPurchased}, Winnings: ${Winnings:F2}";
        }
    }
}
