using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Serilog;

namespace DataUpload.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Log.Logger.Information("Index Called");
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
