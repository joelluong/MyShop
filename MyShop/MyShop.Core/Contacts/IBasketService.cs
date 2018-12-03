using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MyShop.Core.Contacts
{
    using MyShop.Core.ViewModels;

    public interface IBasketService
    {
        void AddToBasket(HttpContextBase httpContext, string productId);
        void RemoveFromBasket(HttpContextBase httpContext, string itemId);
        List<BasketItemViewModel> GetBasketItems(HttpContextBase httpContext);

        BasketSummaryViewModel GetBasketSummary(HttpContextBase httpContext);
    }
}
