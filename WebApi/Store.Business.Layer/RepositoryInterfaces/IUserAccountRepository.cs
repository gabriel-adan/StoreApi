namespace Store.Business.Layer.RepositoryInterfaces
{
    public interface IUserAccountRepository
    {
        User LogIn(string userName, string password);

        User Find(string userName);
    }
}
