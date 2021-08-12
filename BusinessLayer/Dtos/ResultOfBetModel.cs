using System.Collections.Generic;

namespace BusinessLayer.Dto
{
    public class ResultOfBetModel
    {
        public int WinningNumber { get; set; }
        public string Colour { get; set; }
        public List<BetResultModel> Bets { get; set; }
    }
}
