namespace BusinessLayer.Dto
{
    public class BetResultDto
    {
        public int? BetNumber { get; set; }
        public string BetColour { get; set; }
        public decimal MoneyStaked { get; set; }
        public decimal? EarnedMoney { get; set; }
        public string BetStatus { get; set; }
    }
}
