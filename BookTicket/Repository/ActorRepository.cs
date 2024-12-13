using BookTicket.Data;
using BookTicket.Models;
using BookTicket.Repository.IRepository;

namespace BookTicket.Repository
{
    public class ActorRepository : Repository<Actor>, IActorRepository
    {
        public ActorRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
