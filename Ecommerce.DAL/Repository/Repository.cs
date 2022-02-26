using Ecommerce.DAL.Data;
using Ecommerce.DAL.Interface;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.DAL.Repository
{
    public class Repository<TEntitry> : IRepository<TEntitry> where TEntitry : class
    {
        private readonly ApplicationDbContext dbContext;
        private readonly DbSet<TEntitry> dbSet;

        public Repository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.dbSet = dbContext.Set<TEntitry>();
        }

        public IList<TEntitry> GetAll()
        {
            return this.dbSet.ToList();
        }

        public void Insert(TEntitry entity)
        {
            this.dbSet.Add(entity);
            this.dbContext.SaveChanges();
        }

        public TEntitry Get(Guid id)
        {
            return this.dbSet.Find(id);
        }

        public void Update(TEntitry entity)
        {
            this.dbContext.Entry(entity).State = EntityState.Modified;
            this.dbContext.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var data = this.Get(id);
            if (data != null)
            {
                this.dbSet.Remove(data);
                this.dbContext.SaveChanges();
            }
        }
    }
}
