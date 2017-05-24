using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InternetShop.Controllers
{
    public class AdminController : Controller
    {
        private Models.ShopEntities db = new Models.ShopEntities();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Goods()
        {
            string password = Request.Form["password"];
            string login = Request.Form["login"];
            var cookie = new HttpCookie("auth")
            {
                Name = "auth",
                Value = DateTime.Now.ToString("dd.MM.yyyy"),
                Expires = DateTime.Now.AddDays(30),
            };
            if (login == "admin" && password == "12345")
            {
                Response.SetCookie(cookie);
            }
            if (Request.Cookies["auth"] == null)
                return Content("Вы не админ!");

            var Items = db.Goods;
            return View(Items);
        }

        public ActionResult Orders()
        {
            if (Request.Cookies["auth"] == null)
                return Content("Вы не админ!");

            var orders = db.Orders;
            var orderedGoods = db.OrderedGoods;
            return View(orders);
        }

        public ActionResult Select(int id)
        {
            InternetShop.Models.Good Item = null;
            if (id == 0)
            {
                Item = new InternetShop.Models.Good();

                db.Goods.Add(Item);
                db.SaveChanges();
            }
            else
            {
                Item = db.Goods.FirstOrDefault(x => x.Id == id);
                if (Item == null)
                    return Content("Ошибка!");
            }
            return View(Item);
        }


        public ActionResult Success(int id)
        {
            var Item = db.Goods.FirstOrDefault(x => x.Id == id);
            var name = Request.Form["name"];
            var description = Request.Form["description"];
            int price = Int32.Parse(Request.Form["price"]);
            int count = Int32.Parse(Request.Form["count"]);
            String img = Request.Form["photo"];
            if (name == null || description == null)
                return Content("post error");
            if (Item == null)
                return Content("Ошибка!");
            if (img == null)
                return Content("post error");
            Item.Name = Request.Form["name"];
            Item.Description = description;
            Item.Price = price;
            Item.Image = "/img/" + img;
            Item.Count = count;
            db.SaveChanges();
            return View();
        }

        public ActionResult Cart()
        {
            return View();
        }
    }
}