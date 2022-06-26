using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace API.Repositories
{
    public abstract class AbstractRepository<T>
    {
        private string _connectionString;
        protected string ConnectionString => _connectionString;
        public AbstractRepository(IConfiguration configuration){
            _connectionString = configuration.GetValue<string>("DBInfo:ConnectionString");
            API.DB.Seed.CreateTables(configuration);

        }
        
        public abstract void Add(T item);
        public abstract void Remove(int id);
        public abstract void Update(T item);
        public abstract T FindByID(int id);
    }
} 
