using BookTicket.Data;
using BookTicket.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookTicket.Controllers
{
    public class CinemaController : Controller
    {
        private ApplicationDbContext _dbContext = new ApplicationDbContext();
        public IActionResult Index()
        {
            var cinemas = _dbContext.Cinemas.Include(e => e.Movies).ToList();

            return View(cinemas);
        }
        public IActionResult SCinema(int CinemaId)
        {
            var movies = _dbContext.Movies.Where(e => e.CinemaId == CinemaId).Include(e => e.Category).Include(e => e.Cinema).ToList();

            return View(movies);
        }
    }
}
