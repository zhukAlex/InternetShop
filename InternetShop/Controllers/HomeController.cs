using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InternetShop.Controllers
{
    public class HomeController : Controller
    {
        private Models.ShopEntities db = new Models.ShopEntities();
        private bool auth = false;
        public ActionResult Index()
        {
            var cookie = new HttpCookie("auth");
            cookie.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(cookie);
            var Item = new Models.Good()
            {
                Count = 3,
                Description = "LALALA",
                Price = 200
            };
            db.Goods.Add(Item);
         //   db.SaveChanges();
            var Items = db.Goods;
            Console.WriteLine(Item.Count);
            return View(Items);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Auth()
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
                return Content("Ты сука не одмен!");

            var Items = db.Goods;
            return View(Items);
        }

        public ActionResult Orders()
        {
            if (Request.Cookies["auth"] == null)
                return Content("Ты сука не одмен!");
            return View();
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
                    return Content("hohohoohohohoho!");
            }
            return View(Item);
        }


        public ActionResult Success(int id)
        {
            var Item = db.Goods.FirstOrDefault(x => x.Id == id);
            var name = Request.Form["name"];
            var description = Request.Form["description"];
            int price = Int32.Parse( Request.Form["price"] );
            int count = Int32.Parse( Request.Form["count"] );
            if (name == null || description == null)
                return Content("post error");
            if (Item == null)
                return Content("hohohoohohohoho!");
            Item.Name = Request.Form["name"];
            Item.Description = description;
            Item.Price = price;
            Item.Count = count;
            db.SaveChanges();
            return View();
        }
    }
}