using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using API_MyFootballTeam.Areas.API.Models;

namespace API_MyFootballTeam.Areas.API.Controllers
{
    public class LoginsController : Controller
    {
        private LoginManager LoginManager;

        public LoginsController()
        {
            LoginManager = new LoginManager();
        }

        // POST /Api/Login/Usuario { Nombre:"nombre", Telefono:123456789 }
        [HttpGet]
        public JsonResult Login(int? id, Login item)
        {
            switch (Request.HttpMethod)
            {
                case "GET":
                    //return Json(LoginManager.GetLogin(email, password), JsonRequestBehavior.AllowGet);
                    return Json(LoginManager.GetLogin(item), JsonRequestBehavior.AllowGet);

            }

            return Json(new { Error = true, Message = "Operación HTTP desconocida" });
        }

    }
}