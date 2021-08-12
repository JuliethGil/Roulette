using DataAccess.Entities;
using System.Collections.Generic;

namespace DataAccess.Interfaces
{
    public interface IRouletteQuery
    {
        int CreateRoulette(Roulette roulette);
        bool UpdateRoulette(Roulette roulette);
        bool RouletteStatus(int idRoulette);
        List<Roulette> GetAllRoulettes();
    }
}
