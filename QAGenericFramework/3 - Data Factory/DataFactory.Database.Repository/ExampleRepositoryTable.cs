using Dapper;
using DataFactory.Database.Entities;
using DataFactory.Database.Repository.Contracts;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DataFactory.Database.Repository
{
    public class ExampleRepositoryTable : IExampleRepositoryTable
    {
        private readonly IConfigurationRoot configurationRoot;
        private string connectionString => configurationRoot.GetSection("ConnectionStrings")["KYCDataAccessPostgreSqlProvider"];

        internal IDbConnection Connection { get => new NpgsqlConnection(this.connectionString); }

        public ExampleRepositoryTable(IConfigurationRoot configurationRoot)
        {
            this.configurationRoot = configurationRoot ?? throw new ArgumentNullException(nameof(configurationRoot));
        }

        public void DeleteAll()
        {
            using (var dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Query("DELETE FROM example_db.example_table");
            }
        }

        public IEnumerable<ExampleTableEntity> FindAll()
        {
            using (var dbConnection = Connection)
            {
                dbConnection.Open();

                return dbConnection.Query<ExampleTableEntity>("SELECT * FROM example_db.example_table");
            }
        }

        public ExampleTableEntity GetInsertExample(Guid id)
        {
            using (var dbConnection = Connection)
            {
                dbConnection.Open();
                var exampleEntities = dbConnection.Query<ExampleTableEntity>("SELECT example_id, example_one, example_two FROM example_db.example_table");

                return exampleEntities.FirstOrDefault(x => x.Id == id);
            }
        }

        public void InsertExample(ExampleTableEntity exampleTableEntity)
        {
            using (var dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("INSERT INTO example_db.example_table (example_one, example_two) Values (@ExampleOne, @ExampleTwo)", exampleTableEntity);
            }
        }
    }
}