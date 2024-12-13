using BookTicket.Data;
using BookTicket.Models;
using BookTicket.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookTicket.Controllers
{
    public class CategoryController : Controller
    {
        // private ApplicationDbContext _dbContext = new ApplicationDbContext();
        private ICategoryRepository _categoryRepository;
        private IMovieRepository _movieRepository;
        public CategoryController(ICategoryRepository categoryRepository, IMovieRepository movieRepository)
        {
            this._categoryRepository = categoryRepository;
            this._movieRepository = movieRepository;
        }
        public IActionResult Index()
        {
            //var categories = _dbContext.Categories.Include(e=>e.Movies).ToList();
            var categories = _categoryRepository.Get(includeProps: [e => e.Movies]).ToList();
            return View(categories);
        }

        public IActionResult SCategory(int CategoryId)
        {
            //var movies = _dbContext.Movies.Where(e=>e.CategoryId == CategoryId).Include(e => e.Category).Include(e=>e.Cinema).ToList();
            var movies = _movieRepository.Get(filter: e => e.CategoryId == CategoryId, includeProps: [e => e.Category, e => e.Cinema]).ToList();
            return View(movies);
        }
        public IActionResult Create()
        {
            return View(new Category());
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {
            ModelState.Remove("Movies");
            if (ModelState.IsValid)
            {
                _categoryRepository.Create(category);

                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }
        public IActionResult Edit(int CategoryId)
        {
            var category = _categoryRepository.GetOne(filter:e => e.Id == CategoryId); //dbCotext.Catrgories.Find(CategoryId);
            return View(category);
        }
        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryRepository.Alter(category);
            return RedirectToAction(nameof(Index));
            }
            return View(category);
            
        }
        public IActionResult Delete(int CategoryId)
        {
            var category = _categoryRepository.GetOne(filter: e => e.Id == CategoryId) as Category;
            if (category != null)
            {
                _categoryRepository.Delete(category);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
