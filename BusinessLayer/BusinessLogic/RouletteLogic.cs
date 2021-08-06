using BusinessLayer.Interfaces;
using DataAccess.Entities;
using DataAccess.Interfaces;
using System;
using System.Globalization;

namespace BusinessLayer.BusinessLogic
{
    public class RouletteLogic : IRouletteLogic
    {
        private readonly IRouletteQuery _dnaSequenceQuery;
        public RouletteLogic(IRouletteQuery rouletteQuery) => _dnaSequenceQuery = rouletteQuery;

        public int NewRoulette(Roulette roulette)
        {
            roulette.State = false;

            return _dnaSequenceQuery.CreateRoulette(roulette);
        }

        public string RouletteOpening(Roulette roulette)
        {
            roulette.State = true;
            roulette.Opening = DateTime.Now;
            bool operationStatus = _dnaSequenceQuery.UpdateOpeningRoulette(roulette);

            return operationStatus ? "exitosa" : "denegada";
        }
    }
}
