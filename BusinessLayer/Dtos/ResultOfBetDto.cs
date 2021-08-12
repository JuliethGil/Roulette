using System.Collections.Generic;

namespace BusinessLayer.Dto
{
    public class ResultOfBetDto
    {
        public int WinningNumber { get; set; }
        public string Colour { get; set; }
        public List<BetResultDto> Bets { get; set; }
    }
}
