using Microsoft.EntityFrameworkCore;
using AnimeList.Models;
using System.Linq;

namespace AnimeList.Repository
{
    public class UsersRepository<TContext> : BaseRepository<Users, TContext> where TContext : DbContext
    {
        public UsersRepository(TContext context):base(context){
        }

        public IQueryable<Users> FindUserLoggin(Users user)
        {
            return this.Db.Set<Users>()
                .Where(
                    x => (
                        x.User.ToLower().Equals(user.User.ToLower()) 
                        && x.Password.Equals(user.Password)
                    )
                ).AsNoTracking();
        }
    }
}