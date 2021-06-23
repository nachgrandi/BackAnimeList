using Microsoft.EntityFrameworkCore;
using AnimeList.Models;
using System.Linq;

namespace AnimeList.Repository
{
    public class AnimeRepository<TContext> : BaseRepository<Anime, TContext> where TContext : DbContext
    {
        public AnimeRepository(TContext context):base(context){
        }

        public IQueryable<Anime> FindByTitle(string title)
        {
            return this.Db.Set<Anime>().Where(x=>x.Title.ToLower().Contains(title.ToLower())).AsNoTracking();
        }
            
        public IQueryable<Anime> OrderByTitle()
        {
            return this.Db.Set<Anime>().OrderBy(x => x.Title).AsNoTracking();
        }
        public IQueryable<Anime> OrderByDate()
        {
            return this.Db.Set<Anime>().OrderBy(x => x.StartYear).AsNoTracking();
        }
    }
}