using Ecommerce.BAL.Interface;
using Ecommerce.DAL.Interface;

namespace Ecommerce.BAL.Service
{
    public class Service<TEntity> : IService<TEntity> where TEntity : class
    {
        private readonly IRepository<TEntity> repository;

        public Service(IRepository<TEntity> repository)
        {
            this.repository = repository;
        }

        public IList<TEntity> GetAll()
        {
            return this.repository.GetAll();
        }

        public void Insert(TEntity entity)
        {
            this.repository.Insert(entity);
        }

        public TEntity Get(Guid id)
        {
            return this.repository.Get(id);
        }

        public void Update(TEntity entity)
        {
            this.repository.Update(entity);
        }
        public void Delete(Guid id)
        {
            this.repository.Delete(id);
        }
    }
}
