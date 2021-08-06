﻿using BusinessLayer.Interfaces;
using DataAccess.Entities;
using DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace BusinessLayer.BusinessLogic
{
    public class RouletteLogic : IRouletteLogic
    {
        private readonly IRouletteQuery _rouletteQuery;
        private readonly ITypeBetQuery _typeBetQuery;
        private readonly IRouletteNumberQuery _rouletteNumberQuery;
        private readonly IBetQuery _betQuery;
        public RouletteLogic(IRouletteQuery rouletteQuery, ITypeBetQuery typeBetQuery, IRouletteNumberQuery rouletteNumberQuery, IBetQuery betQuery)
        {
            _rouletteQuery = rouletteQuery;
            _typeBetQuery = typeBetQuery;
            _rouletteNumberQuery = rouletteNumberQuery;
            _betQuery = betQuery;
        }

        public int NewRoulette(Roulette roulette)
        {
            roulette.Status = false;

            return _rouletteQuery.CreateRoulette(roulette);
        }

        public string RouletteOpening(Roulette roulette)
        {
            roulette.Status = true;
            bool operationStatus = _rouletteQuery.UpdateOpeningRoulette(roulette);

            return operationStatus ? "exitosa" : "denegada";
        }

        public string Bet(Bet bet)
        {
            if (bet.BetNumber != null && !IsWithinBetRange(bet.BetNumber.Value))
                return "El numero apostado no es valido.";
            if (bet.BetNumber != null)
                bet.IdRouletteNumber = _rouletteNumberQuery.SelectIdRouletteNumber(bet.IdRouletteNumber.Value);
            if (!BetTypeIsValid(bet.IdTypeBet))
                return "El tipo de apuesta no es valido";
            if (!_rouletteQuery.RouletteActive(bet.IdRoulette))
                return "La ruleta no se encuentra activa";
            bet.BetTime = DateTime.Now;
            _betQuery.CreateBet(bet);

            return "";
        }

        private bool IsWithinBetRange(int number)
        {
            return (number >= 0) && (number <= 36);
        }

        private bool BetTypeIsValid(int idTypeBet)
        {
            return _typeBetQuery.SelectTypeBetId(idTypeBet);
        }
    }
}
