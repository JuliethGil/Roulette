using BusinessLayer.Dto;
using DataAccess.Entities;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IRouletteLogic
    {
        int NewRoulette(Roulette roulette);
        string RouletteOpening(Roulette roulette);
        string Bet(Bet bet);
        ResultOfBetDto RouletteClose(Roulette roulette);
    }
}
