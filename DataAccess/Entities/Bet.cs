using System;

namespace DataAccess.Entities
{
    public class Bet
    {
        public int Id { get; set; }
        public int IdRoulette { get; set; }
        public int? IdRouletteNumber { get; set; }
        public int IdTypeBet { get; set; }
        public int? BetNumber { get; set; }
        public decimal MoneyStaked { get; set; }
        public DateTime BetTime { get; set; }
        public DateTime Closing { get; set; }
        public int? Result { get; set; }

        public Bet() {
            IdRouletteNumber = null;
        }
    }
}
