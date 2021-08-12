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
            string sql = $"INSERT INTO public.\"Roulette\" (\"Name\", \"Status\") VALUES ('{roulette.Name}', {roulette.Status}) RETURNING \"Id\"";
            NpgsqlCommand query = new NpgsqlCommand(sql);
            query.Connection = connection;
            int idRoulette = Convert.ToInt32(query.ExecuteScalar());
            connection.Close();

            return idRoulette;
        }

        public bool UpdateRoulette(Roulette roulette)
        {
            try
            {
                string isOpening = string.Empty;
                if (roulette.Status)
                    isOpening = $", \"OpeningDate\" = '{roulette.OpeningDate}'";
                else
                    isOpening = $", \"EndingDate\" = '{roulette.EndingDate}', \"WinningNumber\" = {roulette.WinningNumber} ";

                using NpgsqlConnection connection = new NpgsqlConnection(conectionstring);
                connection.Open();
                string sql = $"UPDATE public.\"Roulette\" SET \"Status\" = {roulette.Status} {isOpening} WHERE \"Id\" ={roulette.Id};";
                NpgsqlCommand query = new NpgsqlCommand(sql);
                query.Connection = connection;
                NpgsqlDataReader dataReader = query.ExecuteReader();
                connection.Close();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool RouletteStatus(int idRoulette)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(conectionstring);
            connection.Open();
            string sql = $"SELECT \"Status\" FROM public.\"Roulette\" WHERE \"Id\" = {idRoulette};";
            NpgsqlCommand query = new NpgsqlCommand(sql);
            query.Connection = connection;
            bool statusRoulette = Convert.ToBoolean(query.ExecuteScalar());
            connection.Close();

            return statusRoulette;
        }
    }
}
