using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Data;
using Microsoft.Data.Sqlite;
using AspNetCoreDapper.Models;


namespace AspNetCoreDapper.Repositories
{
    public class LawyerRepository: AbstractRepository<Lawyer>
    {
        public LawyerRepository(IConfiguration configuration): base(configuration) { }

        public override void Add(Lawyer item)
        {
            using (IDbConnection dbConnection = new SqliteConnection(ConnectionString))
            {
                string sQuery = "INSERT INTO Lawyer (Name, Email)"
                                + " VALUES(@Name, @Email)";
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
                            + " Email = @Email, Price= @Price" + " WHERE Id = @Id";
                dbConnection.Open();
                dbConnection.Query(sQuery, item);
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
        public override IEnumerable<Lawyer> FindAll()
        { 
            using (IDbConnection dbConnection = new SqliteConnection(ConnectionString))
            {
                dbConnection.Open();
                return dbConnection.Query<Lawyer>("SELECT * FROM Lawyer");
            }
        }
    }
}