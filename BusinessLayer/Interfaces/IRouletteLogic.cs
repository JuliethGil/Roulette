using BusinessLayer.Dtos;
using DataAccess.Entities;
using System.Collections.Generic;

namespace BusinessLayer.Interfaces
{
    public interface IRouletteLogic
    {
        int NewRoulette(Roulette roulette);
        string RouletteOpening(Roulette roulette);
        string Bet(Bet bet);
        ResultOfBetDto RouletteClose(Roulette roulette);
        List<RoulettesDto> GetAllRoulettes();
    }
}
