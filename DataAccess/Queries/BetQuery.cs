using DataAccess.Entities;
using DataAccess.Interfaces;
using Npgsql;
using System;

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
    }
}
