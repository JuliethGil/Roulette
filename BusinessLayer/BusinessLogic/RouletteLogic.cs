using AutoMapper;
using BusinessLayer.Dtos;
using BusinessLayer.Interfaces;
using DataAccess.Entities;
using DataAccess.Interfaces;
using System;
using System.Collections.Generic;

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
            roulette.OpeningDate = DateTime.Now;
            bool operationStatus = _rouletteQuery.UpdateRoulette(roulette);

            return operationStatus ? "exitosa" : "denegada";
        }

        public string Bet(Bet bet)
        {
            if (bet.BetNumber != null && !IsWithinBetRange(bet.BetNumber.Value))
                return "El numero apostado no es valido.";
            if (bet.BetNumber != null)
                bet.IdRouletteNumber = _rouletteNumberQuery.SelectIdRouletteNumber(bet.BetNumber.Value);
            if (!BetTypeIsValid(bet.IdTypeBet))
                return "El tipo de apuesta no es valido";
            if (!_rouletteQuery.RouletteStatus(bet.IdRoulette))
                return "La ruleta no se encuentra activa";
            bet.BetTime = DateTime.Now;
            _betQuery.CreateBet(bet);

            return "Creada exitosamente";
        }

        public ResultOfBetDto RouletteClose(Roulette roulette)
        {
            ResultOfBetDto resultOfBetModel = null;
            if (!_rouletteQuery.RouletteStatus(roulette.Id))
                return resultOfBetModel;

            List<BetInformation> betsInformation = _betQuery.BetInformation(roulette.Id);
            int winningNumber = SelectWinningNumber();
            List<BetResultDto> BetsResultModel = GenerateProfit(betsInformation, winningNumber);

            bool operationStatus = ActualizarRuleta(roulette, winningNumber);
            if (operationStatus)
            {
                resultOfBetModel = new ResultOfBetDto
                {
                    WinningNumber = winningNumber,
                    Colour = winningNumber % 2 == 0 ? "Rojo" : "Negro",
                    Bets = BetsResultModel
                };
            }

            return resultOfBetModel;
        }

        public List<RoulettesDto> GetAllRoulettes()
        {
            List<Roulette> roulettes = _rouletteQuery.GetAllRoulettes();

            return Mapper.Map<List<RoulettesDto>>(roulettes);
        }

        private bool IsWithinBetRange(int number)
        {
            return (number >= 0) && (number <= 36);
        }

        private bool BetTypeIsValid(int idTypeBet)
        {
            return _typeBetQuery.SelectTypeBetId(idTypeBet);
        }        

        private bool ActualizarRuleta(Roulette roulette, int winningNumber)
        {
            roulette.Status = false;
            roulette.WinningNumber = winningNumber;
            roulette.EndingDate = DateTime.Now;
            return _rouletteQuery.UpdateRoulette(roulette);
        }

        private List<BetResultDto> GenerateProfit(List<BetInformation> betsInformation, int winningNumber)
        {
            List<BetResultDto> betsResult = new List<BetResultDto>();

            foreach (BetInformation betInformation in betsInformation)
            {
                BetResultDto betResultModel = new BetResultDto();
                betResultModel.BetNumber = betInformation.BetNumber;
                betResultModel.BetColour = GetColour(betInformation.IBet);
                betResultModel.MoneyStaked = betInformation.MoneyStaked;
                betResultModel.EarnedMoney = CalculateProfit(betInformation.IBet, betInformation.BetNumber, betInformation.MoneyStaked, winningNumber);
                betResultModel.BetStatus = betResultModel.EarnedMoney != null ? "Gano" : "Perdio";
                betsResult.Add(betResultModel);
            }

            return betsResult;
        }

        private string GetColour(int bet)
        {
            string colour = string.Empty;
            if (bet == 2)
                colour = "Negro";
            else if (bet == 3)
                colour = "Rojo";

            return colour;
        }

        private int SelectWinningNumber()
        {
            int rouletteRotalNumbers = _rouletteNumberQuery.SelectRouletteNumbers();
            int winningNumber = new Random().Next(0, rouletteRotalNumbers--);

            return winningNumber;
        }

        private decimal? CalculateProfit(int bet, int? betNumber, decimal moneyStaked, int winningNumber)
        {
            decimal? gain = null;
            switch (bet)
            {
                case 1:
                    int number = (int)betNumber;
                    if (number == winningNumber)
                        gain = moneyStaked * 5;
                    break;
                case 2:
                case 3:
                    if (winningNumber % 2 == 0 && bet == 3 || winningNumber % 2 == 1 && bet == 2)
                        gain = moneyStaked * Convert.ToDecimal(1.8);

                    break;
            }

            return gain;
        }
    }
}
