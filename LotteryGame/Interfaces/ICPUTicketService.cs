using LotteryGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotteryGame.Interfaces
{
    // Interface for CPU Ticket Service
    public interface ICPUTicketService
    {
        void HandleCPUTicketPurchases(IEnumerable<Player> cpuPlayers);
    }
}
