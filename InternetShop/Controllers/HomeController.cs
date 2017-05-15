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
            var Items = db.Goods;
            return View(Items);
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Auth()
        {
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

        public ActionResult Cart()
        {
            return View();
        }
    }
}