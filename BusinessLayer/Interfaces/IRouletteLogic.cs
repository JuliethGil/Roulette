using DataAccess.Entities;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IRouletteLogic
    {
        int NewRoulette(Roulette roulette);
    }
}
