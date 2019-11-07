using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ToDoApp.Controllers
{
    public class HomeController : Controller
    {
        [AllowAnonymous] /// created This means that you don't need Authorization to view/interact with the homepage
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous] /// created
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [AllowAnonymous] /// created
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}