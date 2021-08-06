using DataAccess.Entities;
using DataAccess.Interfaces;
using Npgsql;
using System;

namespace DataAccess.Queries
{
    public class RouletteQuery : IRouletteQuery
    {
        public string conectionstring = "SERVER=casino.c5c7abyfbkcz.us-east-1.rds.amazonaws.com;port=5432;UID=postgres;PWD=gatomesamata;DATABASE=Casino";

        public int CreateRoulette(Roulette roulette)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(conectionstring);
            connection.Open();
            string sql = $"INSERT INTO public.\"Roulette\" (\"Name\", \"State\") VALUES ('{roulette.Name}', {roulette.State}) RETURNING \"Id\"";
            NpgsqlCommand query = new NpgsqlCommand(sql);
            query.Connection = connection;
            int idRoulette = Convert.ToInt32(query.ExecuteScalar());
            connection.Close();

            return idRoulette;
        }

        public bool UpdateOpeningRoulette(Roulette roulette)
        {
            try
            {
                using NpgsqlConnection connection = new NpgsqlConnection(conectionstring);
                connection.Open();
                string sql = $"UPDATE public.\"Roulette\" SET \"State\" ={roulette.State}, \"Opening\" ='{roulette.Opening}' WHERE \"Id\" ={roulette.Id};";
                NpgsqlCommand query = new NpgsqlCommand(sql);
                query.Connection = connection;
                NpgsqlDataReader dataReader = query.ExecuteReader();
                connection.Close();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
