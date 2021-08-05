using DataAccess.Entities;
using DataAccess.Interfaces;
using Npgsql;
using System.Configuration;

namespace DataAccess.Queries
{
    public class RouletteQuery : IRouletteQuery
    {
        public int CreateRoulette(Roulette roulette)
        {
            using NpgsqlConnection cn = new NpgsqlConnection("SERVER=casino.c5c7abyfbkcz.us-east-1.rds.amazonaws.com;port=5432;UID=postgres;PWD=gatomesamata;DATABASE=Casino");
            cn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand();
            cmd.Connection = cn;
            cmd.CommandText = $"INSERT INTO public.\"Roulette\"(\"Name\", \"State\")	VALUES ('{roulette.Name}', {roulette.State});";
            cmd.ExecuteNonQuery();
            cn.Close();
            return 1;
        }
    }
}
