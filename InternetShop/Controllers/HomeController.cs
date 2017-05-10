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
        public ActionResult Index()
        {
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
            ViewBag.Message = "Your contact page.";
            ViewBag.Title = "LOLLLLLLLLLLlll";
            return View();
        }

        public ActionResult Auth()
        {
            ViewBag.Message = "Your contact page.";
            ViewBag.Title = "LOLLLLLLLLLLlll";
            return View();
        }

        public ActionResult Admin()
        {
            ViewBag.Message = "Your contact page.";
            ViewBag.Title = "LOLLLLLLLLLLlll";
            return View();
        }


    }
}