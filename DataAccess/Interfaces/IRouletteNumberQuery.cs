using DataAccess.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IRouletteNumberQuery
    {
        int SelectIdRouletteNumber(int number);
        int SelectRouletteNumbers();
    }
}
