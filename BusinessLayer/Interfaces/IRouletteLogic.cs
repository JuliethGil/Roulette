using DataAccess.Entities;

namespace BusinessLayer.Interfaces
{
    public interface IRouletteLogic
    {
        int NewRoulette(Roulette roulette);
        string RouletteOpening(Roulette roulette);
    }
}
