using AnimeList.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AnimeList.Repository
{
    public abstract class BaseRepository<T, TContext> : IRepository<T> where T : BaseModel where TContext : DbContext
    {
        protected TContext Db{get;set;}

        public BaseRepository(TContext context){
            Db=context;
        }
        public void Update(T entity)
        {
            this.Db.Set<T>().Update(entity);
            this.Db.SaveChanges();
        }

        public void Create(T entity)
        {
            this.Db.Set<T>().Add(entity);
            this.Db.SaveChanges();
        }

        public void Delete(T entity)
        {
           this.Db.Set<T>().Remove(entity);
           this.Db.SaveChanges();
        }

        public IQueryable<T> FindAll()
        {
            return this.Db.Set<T>().AsNoTracking();
        }

        public IQueryable<T> FindById(int id)
        {
            return this.Db.Set<T>().Where(x=>x.Id==id).AsNoTracking();
        }
    }
}