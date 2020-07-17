using NHibernate;
using System;
using System.Collections.Generic;
using Store.Business.Layer.RepositoryInterfaces;

namespace Store.Infraestructure.Layer.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected ISession Session { get; }
        protected ITransaction transaction;

        public Repository(ISession session)
        {
            Session = session;
        }

        public void BeginTransaction()
        {
            if (transaction == null)
            {
                transaction = Session.BeginTransaction();
            }
            else
            {
                if (transaction.IsActive)
                    throw new Exception("Transacción en curso.");
            }
        }

        public void CommitTransaction()
        {
            try
            {
                if (transaction != null && transaction.IsActive)
                {
                    transaction.Commit();
                    transaction.Dispose();
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                transaction = null;
            }
        }

        public void Delete(T entity)
        {
            try
            {
                Session.Delete(entity);
            }
            catch
            {
                throw;
            }
        }

        public T Get(int id)
        {
            try
            {
                return Session.Get<T>(id);
            }
            catch
            {
                throw;
            }
        }

        public IList<T> GetAll()
        {
            try
            {
                return Session.CreateCriteria<T>().List<T>();
            }
            catch
            {
                throw;
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                    transaction.Dispose();
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                transaction = null;
            }
        }

        public void SaveOrUpdate(T entity)
        {
            try
            {
                Session.SaveOrUpdate(entity);
            }
            catch
            {
                throw;
            }
        }

        public void Save(T entity)
        {
            try
            {
                Session.Save(entity);
            }
            catch
            {
                throw;
            }
        }
    }
}
