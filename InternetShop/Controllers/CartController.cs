using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InternetShop.Controllers
{
    public class CartController : Controller
    {
        private Models.ShopEntities db = new Models.ShopEntities();

        public CartController(Models.ShopEntities repo)
        {
            db = repo;
        }

        public CartController()
        {

        }

        public RedirectToRouteResult AddToCart(int Id, string returnUrl)
        {
            Models.Good good = db.Goods
                .FirstOrDefault(g => g.Id == Id);

            if (good != null)
            {
                GetCart().AddItem(good, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(int Id, string returnUrl)
        {
            Models.Good good = db.Goods
                .FirstOrDefault(g => g.Id == Id);

            if (good != null)
            {
                GetCart().RemoveLine(good);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public Cart GetCart()
        {
            Cart cart = (Cart)Session["Cart"];
            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }

        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = GetCart(),
                ReturnUrl = returnUrl
            });
        }

    }
}