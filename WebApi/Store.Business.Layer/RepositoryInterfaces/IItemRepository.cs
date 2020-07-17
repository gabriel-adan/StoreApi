namespace Store.Business.Layer.RepositoryInterfaces
{
    public interface IItemRepository<T> : IRepository<T> where T : class
    {
        T FindByName(string name);
    }
}
