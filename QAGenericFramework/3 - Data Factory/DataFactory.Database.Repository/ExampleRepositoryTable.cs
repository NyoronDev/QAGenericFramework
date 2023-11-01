using CrossLayer.Configuration;
using Dapper;
using DataFactory.Database.Entities;
using DataFactory.Database.Repository.Contracts;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Xunit.Abstractions;

namespace DataFactory.Database.Repository
{
    public class ExampleRepositoryTable : IExampleRepositoryTable
    {
        private readonly ITestOutputHelper testOutputHelper;

        private readonly string connectionString;

        internal IDbConnection Connection { get => new NpgsqlConnection(this.connectionString); }

        public ExampleRepositoryTable(ITestOutputHelper testOutputHelper, AppSettings appSettings)
        {
            this.testOutputHelper = testOutputHelper ?? throw new ArgumentNullException(nameof(testOutputHelper));

            connectionString = appSettings.ConnectionStrings.DataAccessPostgreSqlProvider;
        }

        public void DeleteAll()
        {
            testOutputHelper.WriteLine("Delete data from Example repository table");

            using (var dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Query("DELETE FROM example_db.example_table");
            }
        }

        public IEnumerable<ExampleTableEntity> FindAll()
        {
            testOutputHelper.WriteLine("Find all from Example repository table");

            using (var dbConnection = Connection)
            {
                dbConnection.Open();

                return dbConnection.Query<ExampleTableEntity>("SELECT * FROM example_db.example_table");
            }
        }

        public ExampleTableEntity GetExample(Guid id)
        {
            testOutputHelper.WriteLine($"Get example by Id {id} from Example repository table");

            using (var dbConnection = Connection)
            {
                dbConnection.Open();
                var exampleEntities = dbConnection.Query<ExampleTableEntity>("SELECT example_id, example_one, example_two FROM example_db.example_table");

                return exampleEntities.FirstOrDefault(x => x.Id == id);
            }
        }

        public void InsertExample(ExampleTableEntity exampleTableEntity)
        {
            testOutputHelper.WriteLine("Insert example from Example repository table");
            using (var dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("INSERT INTO example_db.example_table (example_one, example_two) Values (@ExampleOne, @ExampleTwo)", exampleTableEntity);
            }
        }
    }
}