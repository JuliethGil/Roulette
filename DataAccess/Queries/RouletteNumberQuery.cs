using DataAccess.Entities;
using DataAccess.Interfaces;
using Npgsql;
using System;

namespace DataAccess.Queries
{
    public class RouletteNumberQuery : IRouletteNumberQuery
    {
        public string conectionstring = "SERVER=casino.c5c7abyfbkcz.us-east-1.rds.amazonaws.com;port=5432;UID=postgres;PWD=gatomesamata;DATABASE=Casino";

        public int SelectIdRouletteNumber(int number)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(conectionstring);
            connection.Open();
            string sql = $"SELECT \"Id\" FROM public.\"RouletteNumber\" WHERE \"Number\" = {number};";
            NpgsqlCommand query = new NpgsqlCommand(sql);
            query.Connection = connection;
            int idRouletteNumber = Convert.ToInt32(query.ExecuteScalar());
            connection.Close();

            return idRouletteNumber;
        }
    }
}
