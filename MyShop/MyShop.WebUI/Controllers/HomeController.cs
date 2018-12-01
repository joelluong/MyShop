using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    using MyShop.Core.Contacts;
    using MyShop.Core.Models;

    public class HomeController : Controller
    {
        IRepository<Product> context;

        IRepository<ProductCategory> productCategories;

        public HomeController(IRepository<Product> productContext, IRepository<ProductCategory> productCategoryContext)
        {
            this.context = productContext;
            productCategories = productCategoryContext;
        }

        public ActionResult Index()
        {
            List<Product> products = this.context.Collection().ToList();

            return View(products);
        }

        public ActionResult Details(string Id)
        {
            Product product = this.context.Find(Id);
            if (product == null)
            {
                return this.HttpNotFound();
            }
            else
            {
                return View(product);

            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}