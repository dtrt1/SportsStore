using System.Linq;
using System.Web.Mvc;
using System.Web.Services.Description;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;

namespace SportsStore.WebUI.Controllers
{
    public class AdminController : Controller
    {
        private IProductRepository repository;

        public AdminController(IProductRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index()
        {
            return View(repository.Products);
        }

        public ViewResult Edit(int productID)
        {
            Product product = repository.Products
                .FirstOrDefault(p => p.ProductID == productID);
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                repository.SaveProduct(product);
                TempData["Message"] = string.Format("{0} has been saved", product.Name);
                return RedirectToAction("Index");
            }
            else
            { 
                // Что-то не так со значениями данных 
                return View(product);
            }
        }
    }
}