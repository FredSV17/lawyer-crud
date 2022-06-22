using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Data;
using Microsoft.Data.Sqlite;
using AspNetCoreDapper.Models;


namespace AspNetCoreDapper.DB
{
    public class Seed
    {
        private static IDbConnection _dbConnection;

        public static void CreateDb(IConfiguration configuration)
        {
            var connectionString = configuration.GetValue<string>("DBInfo:ConnectionString");
            var dbFilePath = configuration.GetValue<string>("DBInfo:ConnectionString");
            if (!File.Exists(dbFilePath))
            {
                _dbConnection = new SqliteConnection(connectionString);
                _dbConnection.Open();

                // Create a Lawyer table
                _dbConnection.Execute(@"
                    CREATE TABLE IF NOT EXISTS [Lawyer] (
                        [Id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                        [Name] NVARCHAR(255) NOT NULL,
                        [Email] NVARCHAR(255) NOT NULL
                    )");
                _dbConnection.Close();
            }
            
        }
    }
}