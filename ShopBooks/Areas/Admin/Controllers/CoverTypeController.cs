using Microsoft.AspNetCore.Mvc;
using ShopBooks.DataAccess.Repository.IRepository;
using ShopBooks.Models;

namespace ShopBooks.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var objList = _unitOfWork.CoverTypeRepository.GetAll();
            return View(objList);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(CoverType obj)
        {
            
            if (ModelState.IsValid)
            {
                _unitOfWork.CoverTypeRepository.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Cover Type created successfully";
                return RedirectToAction("Index");
            }

            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var objCoverTypeFromDb = _unitOfWork.CoverTypeRepository.GetFirstOrDefault(x => x.Id == id);
            if (objCoverTypeFromDb == null)
            {
                return NotFound();
            }
            return View(objCoverTypeFromDb);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(CoverType obj)
        {
            
            if (ModelState.IsValid)
            {
                _unitOfWork.CoverTypeRepository.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Cover Type updated successfully";
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
            var objCoverTypeFromDb = _unitOfWork.CoverTypeRepository.GetFirstOrDefault(x => x.Id == id);
            if (objCoverTypeFromDb == null)
            {
                return NotFound();
            }
            return View(objCoverTypeFromDb);
        }
        [HttpPost, ActionName("Delete")]
        [AutoValidateAntiforgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _unitOfWork.CoverTypeRepository.GetFirstOrDefault(x => x.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.CoverTypeRepository.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Cover Type deleted successfully";

            return RedirectToAction("Index");
        }
    }
}
