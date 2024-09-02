using LotteryGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotteryGame.Interfaces
{
    // Interface for Lottery Result Display Service
    public interface ILotteryResultDisplayService
    {
        void DisplayResults(List<Player> players, int totalRevenue);
    }
}
