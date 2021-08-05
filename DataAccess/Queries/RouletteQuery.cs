using DataAccess.Entities;
using DataAccess.Interfaces;
using Npgsql;
using System;

namespace DataAccess.Queries
{
    public class RouletteQuery : IRouletteQuery
    {
        public int CreateRoulette(Roulette roulette)
        {
            using NpgsqlConnection cn = new NpgsqlConnection("SERVER=casino.c5c7abyfbkcz.us-east-1.rds.amazonaws.com;port=5432;UID=postgres;PWD=gatomesamata;DATABASE=Casino");
            cn.Open();
            string sql = $"INSERT INTO public.\"Roulette\" (\"Name\", \"State\") VALUES ('{roulette.Name}', {roulette.State}) RETURNING \"Id\"";
            NpgsqlCommand query = new NpgsqlCommand(sql);
            query.Connection = cn;
            int idRoulette = Convert.ToInt32(query.ExecuteScalar());
            cn.Close();

            return idRoulette;
        }
    }
}
