using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShop.Core.Models;

namespace MyShop.DataAccess.InMemory
{
    public class ProductCategoryRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<ProductCategory> productCategories;

        public ProductCategoryRepository()
        {
            this.productCategories = this.cache["productCategories"] as List<ProductCategory>;

            if (this.productCategories == null)
            {
                this.productCategories = new List<ProductCategory>();
            }
        }

        public void Commit()
        {
            this.cache["productCategories"] = this.productCategories;
        }

        public void Insert(ProductCategory p)
        {
            this.productCategories.Add(p);
        }

        public void Update(ProductCategory productCategory)
        {
            ProductCategory productCategoryToUpdate = this.productCategories.Find(p => p.Id == productCategory.Id);

            if (productCategoryToUpdate != null)
            {
                productCategoryToUpdate = productCategory;
            }
            else
            {
                throw new Exception("Product Category no found");
            }
        }

        public ProductCategory Find(string id)
        {
            ProductCategory productCategory = this.productCategories.Find(p => p.Id == id);

            if (productCategory != null)
            {
                return productCategory;
            }
            else
            {
                throw new Exception("Product Category no found");
            }
        }

        public IQueryable<ProductCategory> Collection()
        {
            return this.productCategories.AsQueryable();
        }

        public void Delete(string id)
        {
            ProductCategory productCategoryToDelete = this.productCategories.Find(p => p.Id == id);

            if (productCategoryToDelete != null)
            {
                this.productCategories.Remove(productCategoryToDelete);
            }
            else
            {
                throw new Exception("Product no found");
            }
        }
    }
}
