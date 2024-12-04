using BookTicket.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookTicket.Controllers
{
    public class CategoryController : Controller
    {
        private ApplicationDbContext _dbContext = new ApplicationDbContext();
        public IActionResult Index()
        {
            var categories = _dbContext.Categories.Include(e=>e.Movies).ToList();
            return View(categories);
        }

        public IActionResult SCategory(int CategoryId)
        {
            var movies = _dbContext.Movies.Where(e=>e.CategoryId == CategoryId).Include(e => e.Category).Include(e=>e.Cinema).ToList();
            return View(movies);
        }
    }
}
