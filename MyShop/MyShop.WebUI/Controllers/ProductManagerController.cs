using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;
using MyShop.DataAccess.InMemory;

namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        ProductRepository context;

        public ProductManagerController()
        {
            this.context = new ProductRepository();
        }


        // GET: ProductManager
        public ActionResult Index()
        {
            List<Product> products = this.context.Collection().ToList();
            
            return View(products);
        }

        public ActionResult Create()
        {
            Product product = new Product();

            return View(product);
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {
                this.context.Insert(product);
                this.context.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(string Id)
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

        [HttpPost]
        public ActionResult Edit(Product product, string Id)
        {
            Product productToEdit = this.context.Find(Id);

            if (productToEdit == null)
            {
                return this.HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return this.View(product);
                }

                productToEdit.Category = product.Category;
                productToEdit.Description = product.Description;
                productToEdit.Image = product.Image;
                productToEdit.Name = product.Name;
                productToEdit.Price = product.Price;

                this.context.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(string Id)
        {
            Product productToDelete = this.context.Find(Id);

            if (productToDelete == null)
            {
                return this.HttpNotFound();
            }
            else
            {
                return this.View(productToDelete);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            Product productToDelete = this.context.Find(Id);

            if (productToDelete == null)
            {
                return this.HttpNotFound();
            }
            else
            {
                this.context.Delete(Id);
                this.context.Commit();
                return RedirectToAction("Index");
            }
        }
    }
}