using System.Linq;

namespace AnimeList.Repository
{
    public interface IRepository<T>
    {
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        IQueryable<T> FindAll();
        IQueryable<T> FindById(int id);
    }
}