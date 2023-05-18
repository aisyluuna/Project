using Microsoft.EntityFrameworkCore;
using QueueForChildren.Data.Interfaces;
using QueueForChildren.Web.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueForChildren.Web.Interfaces
{
    public interface IRepository<TEntity>
        where TEntity: class, IEntity
    {
        IQueryable<TEntity> GetAll();

        TEntity GetById(long id);

        void Create(TEntity[] entity);

        void Update(TEntity[] entity);

        void Delete (TEntity[] entity);
    }

    internal sealed class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity
    {
        private readonly QueueDbContext _dbContext;

        public Repository(QueueDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Create(TEntity[] entities)
        {
            foreach(var entity in entities)
            {
                entity.CreateDate = DateTime.Now;
            }

            _dbContext.Set<TEntity>().AddRange(entities);
            _dbContext.SaveChanges();
        }

        public void Delete(TEntity[] entities)
        {
            foreach(var entity in entities)
            {
                entity.DeletedDate = DateTime.Now;
                entity.Deleted = true;
            }

            Update(entities);
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbContext.Set<TEntity>()
                .AsNoTracking()
                .Where(entity => !entity.Deleted);             
        }

        public TEntity GetById(long id)
        {
            return _dbContext.Set<TEntity>()
                .AsNoTracking()
                .SingleOrDefault(entity => entity.Id == id);
        }

        public void Update(TEntity[] entities)
        {
            _dbContext.Set<TEntity>().UpdateRange(entities);
            _dbContext.SaveChanges();
        }
    }
}
