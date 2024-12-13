using BookTicket.Data;
using BookTicket.Models;
using BookTicket.Repository.IRepository;

namespace BookTicket.Repository
{
    public class CinemaRepository : Repository<Cinema>, ICinemaRepository
    {
        public CinemaRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
