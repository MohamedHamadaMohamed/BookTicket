using BookTicket.Data;
using BookTicket.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using BookTicket.Data.Enums;
using BookTicket.Repository.IRepository;

namespace BookTicket.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //private ApplicationDbContext _dbContext = new ApplicationDbContext();
        private IMovieRepository _movieRepository;

        public HomeController(IMovieRepository movieRepository, ILogger<HomeController> logger)
        {
            this._movieRepository= movieRepository;
            _logger = logger;

        }
        public IActionResult Index()
        {
            //var movies = _dbContext.Movies.Include(e=>e.Cinema).Include(e=>e.Category).ToList();
            var movies = _movieRepository.Get(includeProps: [e => e.Cinema, e => e.Category]).ToList();
            return View(movies);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
