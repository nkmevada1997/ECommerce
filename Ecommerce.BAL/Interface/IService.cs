namespace Ecommerce.BAL.Interface
{
    public interface IService<TEntity> where TEntity : class
    {
        IList<TEntity> GetAll();

        public void Insert(TEntity entity);

        TEntity Get(Guid id);

        public void Update(TEntity entity);

        public void Delete(Guid id);
    }
}
