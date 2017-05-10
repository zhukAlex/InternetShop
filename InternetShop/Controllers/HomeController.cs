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
          //  db.SaveChanges();
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
            return View();
        }

        public ActionResult Orders()
        {
            if (Request.Cookies["auth"] == null)
                return Content("Ты сука не одмен!");
            return View();
        }
    }
}