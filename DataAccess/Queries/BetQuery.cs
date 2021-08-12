using DataAccess.Entities;
using DataAccess.Interfaces;
using Npgsql;
using System;
using System.Collections.Generic;

namespace DataAccess.Queries
{
    public class BetQuery : IBetQuery
    {
        public string conectionstring = "SERVER=casino.c5c7abyfbkcz.us-east-1.rds.amazonaws.com;port=5432;UID=postgres;PWD=gatomesamata;DATABASE=Casino";


        public int CreateBet(Bet bet)
        {
            string IdRouletteNumber = bet.IdRouletteNumber == null ? "null" : bet.IdRouletteNumber.ToString(); 
            using NpgsqlConnection connection = new NpgsqlConnection(conectionstring);
            connection.Open();
            string sql = $"INSERT INTO public.\"Bet\"(\"IdRoulette\", \"IdRouletteNumber\", \"IdTypeBet\", \"BetTime\", \"MoneyStaked\") VALUES ({bet.IdRoulette}, {IdRouletteNumber}, {bet.IdTypeBet}, '{bet.BetTime}',  {bet.MoneyStaked});";
            NpgsqlCommand query = new NpgsqlCommand(sql);
            query.Connection = connection;
            int idBet = Convert.ToInt32(query.ExecuteScalar());
            connection.Close();

            return idBet;
        }

        public List<BetInformation> BetInformation(int idRoulette)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(conectionstring);
            connection.Open();
            string sql = $"SELECT \"IdTypeBet\", \"IdRouletteNumber\", \"MoneyStaked\" FROM public.\"Bet\" " +
                $"JOIN public.\"Roulette\" ON public.\"Roulette\".\"Id\" = public.\"Bet\".\"IdRoulette\" " +
                $"  WHERE public.\"Bet\".\"IdRoulette\" = {idRoulette} " +
                $"  AND public.\"Roulette\".\"Status\" = true;";
            NpgsqlCommand query = new NpgsqlCommand(sql);
            query.Connection = connection;
            NpgsqlDataReader data = query.ExecuteReader();
            List<BetInformation> betsInformation = new List<BetInformation>();
            while (data.Read())
            {
                BetInformation betInformation = new BetInformation();
                betInformation.IBet = Convert.ToInt32(data[0].ToString());
                if (!data[1].Equals(DBNull.Value))
                    betInformation.BetNumber =  Convert.ToInt32(data[1]);
                betInformation.MoneyStaked = Convert.ToDecimal(data[2]);
                betsInformation.Add(betInformation);
            }
            connection.Close();

            return betsInformation;
        }
    }
}
