using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.ViewModels
{
    using MyShop.Core.Models;

    public class ProductListViewModel
    {

        public IEnumerable<Product> Products { get; set; }

        public IEnumerable<ProductCategory> ProductCategories { get; set; }
    }
}
