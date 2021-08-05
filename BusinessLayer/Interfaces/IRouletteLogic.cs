using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IRouletteLogic
    {
        Task<int> NewRoulette();
        Task<string> RouletteOpening(int idRoulette);
    }
}
