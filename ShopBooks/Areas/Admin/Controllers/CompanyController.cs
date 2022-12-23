using Microsoft.AspNetCore.Mvc;
using ShopBooks.DataAccess.Repository.IRepository;
using ShopBooks.Models;

namespace ShopBooks.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        public IActionResult Index()
        {
            //var objList = _unitOfWork.ProductRepository.GetAll();
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            Company company = new()
            {

            };

            if (id == null || id == 0)
            {
                //Create Product
                return View(company);
            }
            else
            {
                //Update Product
                company = _unitOfWork.CompanyRepository.GetFirstOrDefault(x => x.Id == id);
                return View(company);
            }

        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Upsert(Company obj, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                if (obj.Id == 0)
                {
                    _unitOfWork.CompanyRepository.Add(obj);
                    TempData["success"] = "Company created successfully";
                }
                else
                {
                    _unitOfWork.CompanyRepository.Update(obj);
                    TempData["success"] = "Company updated successfully";
                }

                _unitOfWork.Save();

                return RedirectToAction("Index");

            }

            return View(obj);

        }

        //Api Call

        //To Retrieve all Products with Json, data is passed to wwwroot/js/product and it is used there
        public IActionResult GetAll()
        {
            var companyList = _unitOfWork.CompanyRepository.GetAll();
            return Json(new { data = companyList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var obj = _unitOfWork.CompanyRepository.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

          

            _unitOfWork.CompanyRepository.Remove(obj);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });

        }

    }



}
       

