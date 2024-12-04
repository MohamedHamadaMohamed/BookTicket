using Microsoft.AspNetCore.Mvc;
using BookTicket.Models;
using BookTicket.Data;
using Microsoft.EntityFrameworkCore;
using BookTicket.Data.Enums;
using System.Linq;

namespace BookTicket.Controllers
{
    public class MovieController : Controller
    {
        private ApplicationDbContext _dbContext = new ApplicationDbContext();
        public IActionResult Index()
        {
            var movies = _dbContext.Movies.Include(e => e.Cinema).Include(e => e.Category).ToList();
            return View(movies);
        }
        public IActionResult Details(int movieId)
        {
            Movie movie = _dbContext.Movies.Where(e=>e.Id == movieId).Include(e=>e.Category).Include(e=>e.Cinema).Include(m => m.ActorMovies).ThenInclude(ma => ma.Actors).FirstOrDefault();
            // var actors = movie.Actors.ToList();// _dbContext.Actors.Include(e => e.ActorMovies).ThenInclude(e=>e.Movies).FirstOrDefault(e=>e.m);

            //var moviee = _dbContext.Movies.Include(m => m.ActorMovies).ThenInclude(ma => ma.Actors);

            var actors = movie.ActorMovies.Select(ma => ma.Actors).ToList();

            ViewBag.actors = actors;
            if (movie != null)
            {
                return View(movie);
            }
            return NotFound();
        
        }
        public IActionResult Search(string MovieName)
        {
           var result = _dbContext.Movies.Where(e=>e.Name.Contains(MovieName)).Include(e => e.Cinema).Include(e => e.Category).ToList();
            if (result.Count != 0)
            {
                return View(result);
            }
            return RedirectToAction(nameof(NotFoundAnyThing));
        }
        public IActionResult NotFoundAnyThing()
        {
            return View();
        }



    }
}
