using BookTicket.Data;
using BookTicket.Models;
using BookTicket.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookTicket.Controllers
{
    public class ActorController : Controller
    {
        //private ApplicationDbContext _dbContext = new ApplicationDbContext();
        private IActorRepository _actorRepository;
        public ActorController(IActorRepository actorRepository)
        {
            this._actorRepository = actorRepository;
        }

        public IActionResult Index()
        {
            var actors = _actorRepository.Get().ToList();

            return View(actors);
        }
        public IActionResult Details(int ActorId)
        {
            //var actor = _dbContext.Actors.Find(ActorId);
            var actor = _actorRepository.GetOne(filter: e=>e.Id == ActorId) as Actor;
            return View(actor);
        }
        public IActionResult Create()
        {
            return View(new Actor());
        }
        [HttpPost]
        public IActionResult Create(Actor actor, IFormFile file)
        {
            ModelState.Remove("ProfilePicture") ;
            ModelState.Remove("Movies");
            ModelState.Remove("ActorMovies");

            if (ModelState.IsValid)
            {
                if (file != null && file.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "./wwwroot\\Images\\cast", fileName);

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        file.CopyTo(stream);
                    }
                    actor.ProfilePicture = fileName;
                }

                _actorRepository.Create(actor);
                return RedirectToAction(nameof(Index));
            }
            return View(actor);
        }
        public IActionResult Edit(int ActorId)
        {
            var actor = _actorRepository.GetOne(filter:e=>e.Id == ActorId) as Actor;
            return View(actor);
        }
        [HttpPost]
        public IActionResult Edit(Actor actor, IFormFile file)
        {
            ModelState.Remove("ProfilePicture");
            ModelState.Remove("Movies");
            ModelState.Remove("ActorMovies");
            if (ModelState.IsValid)
            {
                var oldActor = _actorRepository.GetOne(filter: e => e.Id == actor.Id, trancked: false);
                if (file != null && file.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "./wwwroot\\Images\\cast", fileName);

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        file.CopyTo(stream);
                    }

                    actor.ProfilePicture = fileName;


                    var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "./wwwroot\\Images\\cast", oldActor.ProfilePicture);

                    if (System.IO.File.Exists(oldPath))
                    {
                        System.IO.File.Delete(oldPath);
                    }
                }
                else
                {
                    actor.ProfilePicture = oldActor.ProfilePicture;
                }


                _actorRepository.Alter(actor);
                return RedirectToAction(nameof(Index));
            }
            return View(actor);
        }

        public IActionResult Delete(int ActorId)
        {
            var actor = _actorRepository.GetOne(filter:e=>e.Id == ActorId);

            var oldPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Images\\cast", actor.ProfilePicture);
            if (System.IO.File.Exists(oldPath))
            {
                System.IO.File.Delete(oldPath);
            }


            if (actor != null)
            {
                _actorRepository.Delete(actor);
            }
            return RedirectToAction(nameof(Index));
           
        }
    }
}
