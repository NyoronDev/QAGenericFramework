using DataFactory.Database.Entities;
using System;

namespace DataFactory.Database.Repository.Contracts
{
    public interface IExampleRepositoryTable : IRepositoryTableBase<ExampleTableEntity>
    {
        void InsertExample(ExampleTableEntity exampleTableEntity);

        ExampleTableEntity GetInsertExample(Guid id);
    }
}