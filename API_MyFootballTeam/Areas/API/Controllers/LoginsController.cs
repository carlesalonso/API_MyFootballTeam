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

        // GET      http://apimyfootballteamnuevawebapp.azurewebsites.net/API/Login     ?EmailUsuario=morgan&Password=123
        [HttpGet]
        public JsonResult Login(int? id, Login item)
        {
            switch (Request.HttpMethod)
            {
                case "GET":
                    
                    return Json(LoginManager.GetLogin(item), JsonRequestBehavior.AllowGet);

            }

            return Json(new { Error = true, Message = "Operación HTTP desconocida" });
        }

    }
}