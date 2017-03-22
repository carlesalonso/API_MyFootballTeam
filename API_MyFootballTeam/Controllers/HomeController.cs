using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace API_MyFootballTeam.Controllers
{
    public class HomeController : Controller
    {
       // public string cn = ConfigurationManager.AppSettings("MyTeamCS");
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
