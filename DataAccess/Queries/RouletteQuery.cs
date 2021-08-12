using DataAccess.Entities;
using DataAccess.Interfaces;
using Npgsql;
using System;
using System.Collections.Generic;

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
            catch (Exception)
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

        public List<Roulette> GetAllRoulettes()
        {
            using NpgsqlConnection connection = new NpgsqlConnection(conectionstring);
            connection.Open();
            string sql = $"SELECT \"Id\", \"Name\", \"Status\", \"WinningNumber\", \"OpeningDate\", \"EndingDate\" FROM public.\"Roulette\";";
            NpgsqlCommand query = new NpgsqlCommand(sql);
            query.Connection = connection;
            NpgsqlDataReader data = query.ExecuteReader();
            List<Roulette> roulettes = new List<Roulette>();
            while (data.Read())
            {
                Roulette roulette = new Roulette();
                roulette.Id = Convert.ToInt32(data[0]);
                if (!data[1].Equals(DBNull.Value)) roulette.Name = data[1].ToString();
                roulette.Status = Convert.ToBoolean(data[2]);
                if (!data[3].Equals(DBNull.Value)) roulette.WinningNumber = Convert.ToInt32(data[3]);
                if (!data[4].Equals(DBNull.Value)) roulette.OpeningDate = Convert.ToDateTime(data[4]);
                if (!data[5].Equals(DBNull.Value)) roulette.EndingDate = Convert.ToDateTime(data[5]);
                roulettes.Add(roulette);
            }
            connection.Close();

            return roulettes;
        }
    }
}
