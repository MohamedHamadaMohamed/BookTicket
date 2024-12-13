using BookTicket.Data;
using BookTicket.Models;
using BookTicket.Repository.IRepository;

namespace BookTicket.Repository
{
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {
        public MovieRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
