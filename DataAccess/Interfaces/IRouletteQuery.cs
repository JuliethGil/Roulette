using DataAccess.Entities;

namespace DataAccess.Interfaces
{
    public interface IRouletteQuery
    {
        int CreateRoulette(Roulette roulette);
        bool UpdateRoulette(Roulette roulette);
        bool RouletteStatus(int idRoulette);
    }
}
