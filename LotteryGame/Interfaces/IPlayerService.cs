using LotteryGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotteryGame.Interfaces
{
    // Interface for handling player-related actions
    public interface IPlayerService
    {
        void PurchaseTickets(Player player, int numberOfTickets);
    }
}
