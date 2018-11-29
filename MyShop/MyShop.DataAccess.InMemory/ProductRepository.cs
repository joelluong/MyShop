using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShop.Core.Models;

namespace MyShop.DataAccess.InMemory
{
    using MyShop.Core.Models;

    class ProductRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<Product> products;

        public ProductRepository()
        {
            this.products = this.cache["products"] as List<Product>;
            if (this.products == null)
            {
                this.products = new List<Product>();
            }
        }

        public void Commit()
        {
            this.cache["products"] = this.products;
        }

        public void Insert(Product p)
        {
            this.products.Add(p);
        }

        public void Update(Product product)
        {
            Product productToUpdate = this.products.Find(p => p.Id == product.Id);

            if (productToUpdate != null)
            {
                productToUpdate = product;
            }
            else
            {
                throw new Exception("Product no found");
            }
        }

        public Product Find(string id)
        {
            Product product = this.products.Find(p => p.Id == id);

            if (product != null)
            {
                return product;
            }
            else
            {
                throw new Exception("Product no found");
            }
        }

        public IQueryable<Product> Collection()
        {
            return this.products.AsQueryable();
        }

        public void Delete(string id)
        {
            Product productToDelete = this.products.Find(p => p.Id == id);

            if (productToDelete != null)
            {
                this.products.Remove(productToDelete);
            }
            else
            {
                throw new Exception("Product no found");
            }
        }
    }
}
