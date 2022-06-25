using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Data;
using MySql.Data.MySqlClient;
using API.Models;


namespace API.DB
{
    public class Seed
    {
        private static IDbConnection _dbConnection;

        public static void CreateTables(IConfiguration configuration)
        {
            var connectionString = configuration.GetValue<string>("DBInfo:ConnectionString");

            _dbConnection = new MySqlConnection(connectionString);
            _dbConnection.Open();
            // Create a Lawyer table
            _dbConnection.Execute(@"
                CREATE TABLE IF NOT EXISTS Lawyer (
                    Id INT AUTO_INCREMENT PRIMARY KEY,
                    Name VARCHAR(255) NOT NULL,
                    Email VARCHAR(255) NOT NULL,
                    CreatedAt DATETIME NOT NULL
                );");
            _dbConnection.Close();
            
        }
    }
}