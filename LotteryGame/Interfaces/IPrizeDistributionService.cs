using LotteryGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotteryGame.Interfaces
{
    // Interface for distributing prizes among players
    public interface IPrizeDistributionService
    {
        void DistributePrizes(List<Player> players, int totalRevenue);
    }
}
