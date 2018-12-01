using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using MyShop.DataAccess.InMemory;
using MyShop.Core.Contacts;

namespace MyShop.WebUI.Controllers
{
    using System.IO;

    public class ProductManagerController : Controller
    {
        IRepository<Product> context;

        IRepository<ProductCategory> productCategories;

        public ProductManagerController(IRepository<Product> productContext, IRepository<ProductCategory> productCategoryContext)
        {
            this.context = productContext;
            productCategories = productCategoryContext;
        }


        // GET: ProductManager
        public ActionResult Index()
        {
            List<Product> products = this.context.Collection().ToList();
            
            return View(products);
        }

        public ActionResult Create()
        {
            ProductManagerViewModel viewModel = new ProductManagerViewModel();
            viewModel.Product = new Product();
            viewModel.ProductCategories = this.productCategories.Collection();

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(Product product, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            else
            {

                if (file != null)
                {
                    product.Image = product.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//Content//ProductImages//")+product.Image);
                }
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
                ProductManagerViewModel viewModel = new ProductManagerViewModel();
                viewModel.Product = product;
                viewModel.ProductCategories = this.productCategories.Collection();
                return View(viewModel);
            }
        }

        [HttpPost]
        public ActionResult Edit(Product product, string Id, HttpPostedFileBase file)
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

                if (file != null)
                {
                    productToEdit.Image = product.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//Content//ProductImages//") + productToEdit.Image);
                }

                productToEdit.Category = product.Category;
                productToEdit.Description = product.Description;
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