using DataFactory.Database.Entities;
using System.Collections.Generic;

namespace DataFactory.Database.Repository.Contracts
{
    public interface IRepositoryTableBase<T> where T : BaseTableEntity
    {
        IEnumerable<T> FindAll();

        void DeleteAll();
    }
}