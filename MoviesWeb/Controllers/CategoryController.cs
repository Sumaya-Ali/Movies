using Microsoft.AspNetCore.Mvc;
using MoviesWeb.Data;
using MoviesWeb.Models;

namespace MoviesWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDBContext _dbContext;

        public CategoryController(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _dbContext.Categories;
            return View(objCategoryList);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString()) {
                ModelState.AddModelError("name", "Display order can't exactly match the Name");
            }
            if (ModelState.IsValid)
            {
                _dbContext.Categories.Add(obj);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(obj);
            }
        }


        public IActionResult Edit(int? id)
        {
            if (id == null || id ==0)
            {
                return NotFound();
            }

            var CategoryFromDB = _dbContext.Categories.Find(id);

            //       var CategoryFromDB = _dbContext.Categories.SingleOrDefault(u=> u.Id==id);
            //       var CategoryFromDB = _dbContext.Categories.FirstOrDefault(u=> u.Id==id);


            if (CategoryFromDB == null) {
                return NotFound();
            }

            return View(CategoryFromDB);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "Display order can't exactly match the Name");
            }
            if (ModelState.IsValid)
            {
                _dbContext.Categories.Update(obj);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(obj);
            }
        }

        public IActionResult Delete(int? id) {
            if (id == 0 || id == null) {
                return NotFound();
            }
            var cat = _dbContext.Categories.Find(id);
            if(cat == null)
            {
                return NotFound();
            }
            return View(cat);
        }

        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            if(id==0 || id== null)
            {
                return NotFound();
            }
            var cat = _dbContext.Categories.Find(id);
            if(cat == null)
            {
                return NotFound();
            }
            _dbContext.Categories.Remove(cat);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }




    }
}
