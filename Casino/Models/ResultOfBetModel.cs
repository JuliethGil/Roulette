using System.Collections.Generic;

namespace Casino.Models
{
    public class ResultOfBetModel
    {
        public int WinningNumber { get; set; }
        public string Colour { get; set; }
        public List<BetResult> Bets { get; set; }
    }
}
