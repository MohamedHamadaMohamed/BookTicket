using BookTicket.Data;
using BookTicket.Models;
using BookTicket.Repository.IRepository;

namespace BookTicket.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
