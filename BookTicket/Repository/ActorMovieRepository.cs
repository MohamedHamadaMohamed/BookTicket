using BookTicket.Data;
using BookTicket.Models;
using BookTicket.Repository.IRepository;

namespace BookTicket.Repository
{
    public class ActorMovieRepository : Repository<ActorMovie>, IActorMovieRepository
    {
        public ActorMovieRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
