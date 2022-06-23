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

namespace API.Repositories
{
    public class LawyerRepository: AbstractRepository<Lawyer>
    {
        public LawyerRepository(IConfiguration configuration): base(configuration) { }

        public override void Add(Lawyer item)
        {
            using (IDbConnection dbConnection = new SqliteConnection(ConnectionString))
            {
                string sQuery = "INSERT INTO Lawyer (Name, Email, CreatedAt)"
                                + " VALUES(@Name, @Email,@CreatedAt)";
                dbConnection.Open();
                dbConnection.Execute(sQuery, item);
            }
        }
        public override void Remove(int id)
        {
            using (IDbConnection dbConnection = new SqliteConnection(ConnectionString))
            {
                string sQuery = "DELETE FROM Lawyer" 
                            + " WHERE Id = @Id";
                dbConnection.Open();
                dbConnection.Execute(sQuery, new { Id = id });
            }
        }
        public override void Update(Lawyer item)
        {
            using (IDbConnection dbConnection = new SqliteConnection(ConnectionString))
            {
                string sQuery = "UPDATE Lawyer SET Name = @Name,"
                            + " Email = @Email" + " WHERE Id = @Id";
                dbConnection.Open();
                dbConnection.Execute(sQuery, item);
            }
        }
        public override Lawyer FindByID(int id)
        { 
            using (IDbConnection dbConnection = new SqliteConnection(ConnectionString))
            {
                string sQuery = "SELECT * FROM Lawyer" 
                            + " WHERE Id = @Id";
                dbConnection.Open();
                return dbConnection.Query<Lawyer>(sQuery, new { Id = id }).FirstOrDefault();
            }
        }

        public Lawyer FindByEmail(string email)
        { 
            using (IDbConnection dbConnection = new SqliteConnection(ConnectionString))
            {
                string sQuery = "SELECT * FROM Lawyer" 
                            + " WHERE Email = @Email";
                dbConnection.Open();
                return dbConnection.Query<Lawyer>(sQuery, new { Email = email }).FirstOrDefault();
            }
        }

        public override IEnumerable<Lawyer> FindAll(string orderBy="Name",string order="ASC")
        { 
            using (IDbConnection dbConnection = new SqliteConnection(ConnectionString))
            {
                dbConnection.Open();
                string queryString = "SELECT * FROM Lawyer ORDER BY " + orderBy + " " + order;

                return dbConnection.Query<Lawyer>(queryString);
            }
        }

        public IEnumerable<Lawyer> FindRecentLawyersCreated(int n=5)
        {
            using (IDbConnection dbConnection = new SqliteConnection(ConnectionString))
            {
                dbConnection.Open();
                string queryString = "SELECT * FROM Lawyer ORDER BY CreatedAt DESC LIMIT " + n;

                return dbConnection.Query<Lawyer>(queryString);
            }
        }
    }
}