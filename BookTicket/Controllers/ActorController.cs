using BookTicket.Data;
using BookTicket.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookTicket.Controllers
{
    public class ActorController : Controller
    {
        private ApplicationDbContext _dbContext = new ApplicationDbContext();
        public IActionResult Index()
        {
            var actors = _dbContext.Actors.ToList();

            return View(actors);
        }
        public IActionResult Details(int ActorId)
        {
            var actor = _dbContext.Actors.Find(ActorId);

            return View(actor);
        }
    }
}
