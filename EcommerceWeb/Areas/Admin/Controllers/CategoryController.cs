using EShop.DataAccess.Data;
using EShop.DataAccess.Repository.IRepository;
using EShop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.AspNetCore.Authorization;
using EShop.Utility;

namespace EShopWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unityOfWork;
        public CategoryController(IUnitOfWork unityOfWork)
        {
            _unityOfWork = unityOfWork; //BY-whatever get inside parameter, will stored inside local variable _db
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _unityOfWork.Category.GetAll().ToList(); //Retrieve data
            return View(objCategoryList);
        }

        public IActionResult Create() //By Default, this is a GET action method
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            // This Validation is checked at server side
            //if (obj.Name == obj.DisplayOrder.ToString())
            //{
            //    ModelState.AddModelError("Name", "The Display Order cannot exactly match with Name");
            //}
            if (ModelState.IsValid)
            {
                _unityOfWork.Category.Add(obj);
                _unityOfWork.Save();
                TempData["success"] = "Category created successfully";

                return RedirectToAction("Index", "Category");
            }
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Category? categoryFromDb = _unityOfWork.Category.Get(u => u.Id == id);
            //Category? categoryFromDb1 = _db.Category.FirstOrDefault(x => x.Id == id);
            //Category? categoryFromDb2 = _db.Category.Where(x => x.Id == id).FirstOrDefault();

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _unityOfWork.Category.Update(obj);
                _unityOfWork.Save();
                TempData["success"] = "Category updated successfully";

                return RedirectToAction("Index", "Category");
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Category? categoryFromDb = _unityOfWork.Category.Get(u => u.Id == id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int id)
        {
            Category? obj = _unityOfWork.Category.Get(u => u.Id == id);

            if (obj == null)
            {
                return NotFound();
            }

            _unityOfWork.Category.Remove(obj);
            _unityOfWork.Save();
            TempData["success"] = "Category deleted successfully";

            return RedirectToAction("Index");
        }

    }
}
