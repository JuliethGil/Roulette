using DataAccess.Entities;
using System.Collections.Generic;

namespace DataAccess.Interfaces
{
    public interface IBetQuery
    {
        int CreateBet(Bet bet);
        List<BetInformation> BetInformation(int idRoulette);
    }
}
