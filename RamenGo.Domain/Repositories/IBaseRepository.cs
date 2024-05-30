namespace RamenGo.Domain.Repositories
{
    // Interface base de repositorio de onde as interfaces dos repositorios das entidades estendem.

    public interface IBaseRepository<TEntity, TKey>
        where TEntity : class
        where TKey : struct, IEquatable<TKey>
    {
        public TEntity Get(TKey key);
        public void Insert(TEntity entity);
        public void Update(TEntity entity);
        public void Delete(TEntity entity);
    }
}
