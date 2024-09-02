using LotteryGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotteryGame.Interfaces
{
    // Interface for handling user input
    public interface IUserInputService
    {
        int GetPlayerTickets(Player player);
    }
}
