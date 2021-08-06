namespace DataAccess.Entities
{
    public class Bet
    {
        public int Id { get; set; }
        public int IdRoulette { get; set; }
        public int IdRouletteNumber { get; set; }
        public int IdBetGuy { get; set; }
        public int Result { get; set; }
    }
}
