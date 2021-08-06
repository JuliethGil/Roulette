namespace Casino.Models
{
    public class BetModel
    {
        public int IdRoulette { get; set; }
        public int IdTypeBet { get; set; }
        public decimal MoneyStaked { get; set; }
        public int? BetNumber { get; set; }
    }
}
