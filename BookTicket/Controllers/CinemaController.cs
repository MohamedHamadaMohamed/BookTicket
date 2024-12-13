using BookTicket.Data;
using BookTicket.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookTicket.Repository.IRepository;
using BookTicket.Repository;

namespace BookTicket.Controllers
{
    public class CinemaController : Controller
    {
        // private ApplicationDbContext _dbContext = new ApplicationDbContext();
        private ICinemaRepository _cinemaRepository;
        private IMovieRepository _movieRepository;
        public CinemaController(ICinemaRepository cinemaRepository, IMovieRepository movieRepository)
        {
            this._cinemaRepository = cinemaRepository;
            this._movieRepository = movieRepository;
        }
        public IActionResult Index()
        {
            //var cinemas = _dbContext.Cinemas.Include(e => e.Movies).ToList();
            var cinemas = _cinemaRepository.Get(includeProps: [e => e.Movies]).ToList();
            return View(cinemas);
        }
        public IActionResult SCinema(int CinemaId)
        {
              
            //var movies = _dbContext.Movies.Where(e => e.CinemaId == CinemaId).Include(e => e.Category).Include(e => e.Cinema).ToList();
            var movies = _movieRepository.Get(filter: e => e.CinemaId == CinemaId,includeProps: [e => e.Category, e => e.Cinema]).ToList();
            return View(movies);
        }
        public IActionResult Create()
        {
            
            return View(new Cinema());
        }
        [HttpPost]
        public IActionResult Create(Cinema cinema)
        {
            ModelState.Remove("Movies");
            if (ModelState.IsValid)
            {
                _cinemaRepository.Create(cinema);
                return RedirectToAction(nameof(Index));
            }
            return View(cinema);
        }
        public IActionResult Edit(int CinemaId) {
          
            var cinema = _cinemaRepository.GetOne(filter: e => e.Id == CinemaId) as Cinema;
            return View(cinema);
        }
        [HttpPost]
        public IActionResult Edit(Cinema cinema)
        {
            if (ModelState.IsValid)
            {
                _cinemaRepository.Alter(cinema);
                return RedirectToAction(nameof(Index));
            }
            return View(cinema);
        }
        public IActionResult Delete(int CinemaId)
        {
            var cinema = _cinemaRepository.GetOne(filter: e => e.Id == CinemaId) as Cinema;
            if (cinema != null)
            {
                _cinemaRepository.Delete(cinema);
            }
            return RedirectToAction(nameof(Index));
        }


    }
}
