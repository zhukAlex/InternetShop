
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

        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {
            return View(new ShippingDetails());
        }

        public ViewResult MakeOrder()
        {
            Cart cart = GetCart();

            if (cart == null)
                return View("null");
            else if(cart.Lines.Count<CartLine>() == 0)
                return View("Корзина пуста");

            Models.Order order = new Models.Order();
            String address = Request.Form["Address"];
            String name = Request.Form["Name"];
            String telephone = Request.Form["Telephone"];

            order.Address = address;
            order.FIO = name;
            order.Telephone = telephone;
            order.Sum = (int)cart.ComputeTotalValue();
            order.Status = 0;
            db.Orders.Add(order);
            db.SaveChanges();
            Models.Order n = db.Orders.ToArray<Models.Order>()[ db.Orders.Count<Models.Order>()-1 ];
            foreach(var line in cart.Lines)
            {
                db.OrderedGoods.Add(new Models.OrderedGood()
                {
                    IdOrder = n.Id,
                    Count = line.Quantity,
                    IdGood = line.good.Id
                });
            }
            db.SaveChanges();
            return View();
        }
    }
}