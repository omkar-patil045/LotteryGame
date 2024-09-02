using LotteryGame.Interfaces;
using LotteryGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotteryGame.Services
{
    public class PlayerGenerationService : IPlayerGenerationService
    {
        private readonly LotterySettings _settings;

        public PlayerGenerationService(LotterySettings settings)
        {
            _settings = settings;
        }

        // Generates a random number of players between 10 and 15
        public int GenerateRandomPlayerCount()
        {
            Random random = new Random();
            return random.Next(_settings.MinPlayers, _settings.MaxPlayers + 1); 
        }

        // Generates player instances for the game
        public List<Player> GeneratePlayers(int totalPlayers)
        {
            var players = new List<Player>();

            // Generate (Human Player)
            players.Add(new Player("Player 1"));

            for (int i = 2; i <= totalPlayers; i++)
            {
                players.Add(new Player($"Player {i}"));
            }

            return players;
        }
    }
}
