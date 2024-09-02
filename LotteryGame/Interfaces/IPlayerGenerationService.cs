using LotteryGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotteryGame.Interfaces
{
    // Interface for Player Generation Service
    public interface IPlayerGenerationService
    {
        int GenerateRandomPlayerCount();
        List<Player> GeneratePlayers(int totalPlayers);
    }

}
