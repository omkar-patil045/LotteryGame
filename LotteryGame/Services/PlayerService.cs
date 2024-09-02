using LotteryGame.Interfaces;
using LotteryGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotteryGame.Services
{
    // Service responsible for player-related actions, such as purchasing tickets
    public class PlayerService : IPlayerService
    {
        public void PurchaseTickets(Player player, int numberOfTickets)
        {
            player.PurchaseTickets(numberOfTickets);
        }
    }
}
