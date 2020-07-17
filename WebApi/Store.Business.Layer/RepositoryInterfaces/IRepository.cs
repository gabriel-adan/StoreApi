using System.Collections.Generic;

namespace Store.Business.Layer.RepositoryInterfaces
{
    public interface IRepository<T> where T : class
    {
        T Get(int id);

        IList<T> GetAll();

        void Save(T entity);

        void SaveOrUpdate(T entity);

        void Delete(T entity);

        void BeginTransaction();

        void CommitTransaction();

        void RollbackTransaction();
    }
}
