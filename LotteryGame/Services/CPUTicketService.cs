using LotteryGame.Interfaces;
using LotteryGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotteryGame.Services
{
    public class CPUTicketService : ICPUTicketService
    {

        private readonly IPlayerService _playerService;

        public CPUTicketService(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        public void HandleCPUTicketPurchases(IEnumerable<Player> cpuPlayers)
        {
            Random random = new Random();
            foreach (var player in cpuPlayers)
            {
                int cpuTickets = new Random().Next(1, 11);
                _playerService.PurchaseTickets(player, cpuTickets);
            }
        }

    }
}
