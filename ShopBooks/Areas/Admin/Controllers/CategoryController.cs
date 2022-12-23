using Microsoft.AspNetCore.Mvc;
using ShopBooks.DataAccess;
using ShopBooks.DataAccess.Repository.IRepository;
using ShopBooks.Models;

namespace ShopBooks.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var objList = _unitOfWork.CategoryRepository.GetAll();
            return View(objList);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(Category obj)
        {
            //Same name Validation
            if(obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The Display Order cannot exactly match the name");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.CategoryRepository.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }

            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            var objCategoriesFromDb = _unitOfWork.CategoryRepository.GetFirstOrDefault(x => x.Id == id);
            if(objCategoriesFromDb == null)
            {
                return NotFound();
            }
            return View(objCategoriesFromDb);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(Category obj)
        {
            //Same name Validation
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The Display Order cannot exactly match the name");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.CategoryRepository.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }

            return View(obj);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var objCategoriesFromDb = _unitOfWork.CategoryRepository.GetFirstOrDefault(x => x.Id == id);
            if (objCategoriesFromDb == null)
            {
                return NotFound();
            }
            return View(objCategoriesFromDb);
        }
        [HttpPost,ActionName("Delete")]
        [AutoValidateAntiforgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _unitOfWork.CategoryRepository.GetFirstOrDefault(x => x.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.CategoryRepository.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Category deleted successfully";

            return RedirectToAction("Index");
        }
    }
}
