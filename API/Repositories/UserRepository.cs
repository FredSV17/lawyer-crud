using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Data;
using Microsoft.Data.Sqlite;
using API.Models;
using System.Web.Http;
using System.Net;
using MySql.Data.MySqlClient;

namespace API.Repositories
{
    public class UserRepository: AbstractRepository<User>
    {
        public UserRepository(IConfiguration configuration): base(configuration) { }

        public override void Add(User item){
            using (IDbConnection dbConnection = new MySqlConnection(ConnectionString))
            {
                string sQuery = "INSERT INTO Users (Name, Email, Password, Role)"
                                + " VALUES(@Name, @Email,@Password, @Role);";
                dbConnection.Open();
                dbConnection.Execute(sQuery, item);
            }
        }
        public override void Remove(int id){
            using (IDbConnection dbConnection = new MySqlConnection(ConnectionString))
            {
                string sQuery = "DELETE FROM Users" 
                            + " WHERE Id = @Id;";
                dbConnection.Open();
                dbConnection.Execute(sQuery, new { Id = id });
            }
        }
        public override void Update(User item){
            using (IDbConnection dbConnection = new MySqlConnection(ConnectionString))
            {
                string sQuery = "UPDATE Users SET Name = @Name," + " Email = @Email" + "Password = @Password" + 
                                " WHERE Id = @Id;";
                dbConnection.Open();
                dbConnection.Execute(sQuery, item);
            }
        }
        public override User FindByID(int id){
            using (IDbConnection dbConnection = new MySqlConnection(ConnectionString))
            {
                string sQuery = "SELECT * FROM Users" 
                            + " WHERE Id = @Id;";
                dbConnection.Open();
                return dbConnection.Query<User>(sQuery, new { Id = id }).FirstOrDefault();
            }
        }
        
        public Lawyer FindByEmail(string email)
        { 
            using (IDbConnection dbConnection = new MySqlConnection(ConnectionString))
            {
                string sQuery = "SELECT * FROM Users" 
                            + " WHERE Email = @Email;";
                dbConnection.Open();
                return dbConnection.Query<Lawyer>(sQuery, new { Email = email }).FirstOrDefault();
            }
        }

        public User Get(string email, string password)
        {
            using (IDbConnection dbConnection = new MySqlConnection(ConnectionString))
            {
                string sQuery = "SELECT * FROM Users" 
                            + " WHERE Email = @Email AND Password = @Password;";
                dbConnection.Open();
                return dbConnection.Query<User>(sQuery, new { Email = email, Password = password }).FirstOrDefault();
            }
        }

    }
}