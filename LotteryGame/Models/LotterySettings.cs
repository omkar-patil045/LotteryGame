using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotteryGame.Models
{
    public class LotterySettings
    {
        public decimal TicketPrice { get; set; }
        public decimal GrandPrizePercentage { get; set; }
        public decimal SecondTierPercentage { get; set; }
        public decimal ThirdTierPercentage { get; set; }
        public int MinPlayers { get; set; }
        public int MaxPlayers { get; set; }
       
    }
}
