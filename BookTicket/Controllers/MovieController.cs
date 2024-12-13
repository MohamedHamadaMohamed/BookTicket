using Microsoft.AspNetCore.Mvc;
using BookTicket.Models;
using BookTicket.Data;
using Microsoft.EntityFrameworkCore;
using BookTicket.Data.Enums;
using System.Linq;
using BookTicket.Repository.IRepository;

namespace BookTicket.Controllers
{
    public class MovieController : Controller
    {
        //private ApplicationDbContext _dbContext = new ApplicationDbContext();
        private IMovieRepository _movieRepository;
        private ICategoryRepository _categoryRepository;
        private ICinemaRepository _cinemaRepository;
        private IActorRepository _actorRepository;
        private IActorMovieRepository _actorMovieRepository;

        public MovieController(IMovieRepository movieRepository, ICategoryRepository categoryRepository, ICinemaRepository cinemaRepository, IActorRepository actorRepository, IActorMovieRepository actorMovieRepository)
        {
            this._movieRepository = movieRepository;
            this._categoryRepository = categoryRepository;
            this._cinemaRepository = cinemaRepository;
            this._actorRepository = actorRepository;
            this._actorMovieRepository = actorMovieRepository;
        }
        public IActionResult Index()
        {
            //var movies = _dbContext.Movies.Include(e => e.Cinema).Include(e => e.Category).ToList();
            var movies = _movieRepository.Get(includeProps: [e => e.Cinema, e => e.Category]).ToList();
            return View(movies);
        }
        public IActionResult Details(int movieId)
        {
            //Movie movie = _dbContext.Movies.Where(e=>e.Id == movieId).Include(e=>e.Category).Include(e=>e.Cinema).Include(m => m.ActorMovies).ThenInclude(ma => ma.Actors).FirstOrDefault();
            Movie? movie = _movieRepository.Get(filter: e => e.Id == movieId, includeProps: [e => e.Category, e => e.Cinema]).Include(m => m.ActorMovies).ThenInclude(ma => ma.Actors).FirstOrDefault() as Movie;

            var actors = movie.ActorMovies.Select(ma => ma.Actors).ToList();

            ViewBag.actors = actors;
            if (movie != null)
            {
                (movie.views)++;
                _movieRepository.Alter(movie);
                return View(movie);
            }
            return NotFound();
        
        }
        public IActionResult Search(string MovieName)
        {
            //var result = _dbContext.Movies.Where(e=>e.Name.Contains(MovieName)).Include(e => e.Cinema).Include(e => e.Category).ToList();
            var result = _movieRepository.Get(filter: e => e.Name.Contains(MovieName), includeProps: [e => e.Cinema, e => e.Category]).ToList();
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
        public IActionResult Create()
        {
            var categories = _categoryRepository.Get().ToList();
            var cinemas =_cinemaRepository.Get().ToList();
            var actors = _actorRepository.Get().ToList();
            ViewBag.categories = categories;
            ViewBag.cinemas = cinemas;
            ViewBag.actors = actors;
            return View(new Movie());
        }
        [HttpPost]
        public IActionResult Create(Movie movie,List<int>ActorsId,IFormFile file)
        {
            ModelState.Remove("ImgUrl");
            ModelState.Remove("Cinema");
            ModelState.Remove("Category");

            if (ModelState.IsValid)
            {

                if (file != null && file.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "./wwwroot\\Images\\movies", fileName);

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        file.CopyTo(stream);
                    }
                    movie.ImgUrl = fileName;
                }

                _movieRepository.Create(movie);
                foreach (var item in ActorsId)
                {
                    _actorMovieRepository.Create(new ActorMovie { ActorsId = item, MoviesId = movie.Id });

                }
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }
        public IActionResult Edit(int MovieId)
        {
            var categories = _categoryRepository.Get().ToList();
            var cinemas = _cinemaRepository.Get().ToList();
            var actors = _actorRepository.Get().ToList();
            ViewBag.categories = categories;
            ViewBag.cinemas = cinemas;
            ViewBag.actors = actors;
            var movie = _movieRepository.GetOne(filter:e=>e.Id == MovieId);
            return View(movie);
        }
        [HttpPost]
        public IActionResult Edit(Movie movie, IFormFile file)
        {
            ModelState.Remove("ImgUrl");
            ModelState.Remove("Cinema");
            ModelState.Remove("Category");
            if (ModelState.IsValid)
            {
                var oldMovie = _movieRepository.GetOne(filter: e => e.Id == movie.Id, trancked: false);
                if (file != null && file.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "./wwwroot\\Images\\movies", fileName);

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        file.CopyTo(stream);
                    }

                    movie.ImgUrl = fileName;


                    var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "./wwwroot\\Images\\movies", oldMovie.ImgUrl);

                    if (System.IO.File.Exists(oldPath))
                    {
                        System.IO.File.Delete(oldPath);
                    }
                }
                else
                {
                    movie.ImgUrl = oldMovie.ImgUrl;
                }


                _movieRepository.Alter(movie);
                return RedirectToAction(nameof(Index));
            }
            return View(movie); 
        }
        public ActionResult Delete(int MovieId)
        {
            var movie = _movieRepository.GetOne(filter:e=>e.Id == MovieId);

            var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images\\movies", movie.ImgUrl);
            if (System.IO.File.Exists(oldPath))
            {
                System.IO.File.Delete(oldPath);
            }


            if (movie != null)
            {
                _movieRepository.Delete(movie);
            }
            return RedirectToAction(nameof(Index));
        }






    }
}
