using DataAccess.Entities;
using DataAccess.Interfaces;
using Npgsql;
using System;
using System.Collections.Generic;

namespace DataAccess.Queries
{
    public class TypeBetQuery : ITypeBetQuery
    {
        public string conectionstring = "SERVER=casino.c5c7abyfbkcz.us-east-1.rds.amazonaws.com;port=5432;UID=postgres;PWD=gatomesamata;DATABASE=Casino";

        public bool SelectTypeBetId(int idTypeBet)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(conectionstring);
            connection.Open();
            string sql = $"SELECT \"Id\" FROM public.\"TypeBet\" WHERE \"Id\" = {idTypeBet};";
            NpgsqlCommand query = new NpgsqlCommand(sql);
            query.Connection = connection;
            int haveData = Convert.ToInt32(query.ExecuteScalar());
            connection.Close();

            return haveData >= 1;
        }
    }
}
