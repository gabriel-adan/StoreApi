using NHibernate;
using NHibernate.Criterion;
using Store.Business.Layer;
using Store.Business.Layer.RepositoryInterfaces;

namespace Store.Infraestructure.Layer.Repositories
{
    public class UserAccountRepository : IUserAccountRepository
    {
        private readonly ISession Session;

        public UserAccountRepository(ISession session)
        {
            Session = session;
        }

        public User LogIn(string userName, string password)
        {
            try
            {
                return Session.CreateCriteria<User>()
                    .Add(Restrictions.Where<User>(u => u.UserName == userName && u.Password == password && u.IsEnabled)).UniqueResult<User>();
            }
            catch
            {
                throw;
            }
        }

        public User Find(string userName)
        {
            try
            {
                return Session.CreateCriteria<User>()
                    .Add(Restrictions.Where<User>(u => u.UserName == userName && u.IsEnabled)).UniqueResult<User>();
            }
            catch
            {
                throw;
            }
        }
    }
}
