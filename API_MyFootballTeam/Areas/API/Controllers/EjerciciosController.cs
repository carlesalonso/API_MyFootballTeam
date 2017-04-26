using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using API_MyFootballTeam.Areas.API.Models;

namespace API_MyFootballTeam.Areas.API.Controllers
{
    public class EjerciciosController : Controller
    {
        private EjercicioManager EjerciciosManager;

        public EjerciciosController()
        {
            EjerciciosManager = new EjercicioManager();
        }

        // GET /Api/Ejercicios
        [HttpGet]
        public JsonResult Ejercicios()
        {
            return Json(EjerciciosManager.GetEjercicios(), JsonRequestBehavior.AllowGet);
        }

        // POST     /Api/Ejercicios/Ejercicio       { NombreUsuario:"nombre", TelefonoUsuario:123456789 }
        // PUT      /Api/Ejercicios/Ejercicio/3     { IdUsuario:3, NombreUsuario:"nombre", TelefonoUsuario:123456789 }
        // GET      /Api/Ejercicios/Ejercicio/3
        // DELETE   /Api/Ejercicios/Ejercicio/3
        public JsonResult Ejercicio(int? id, Ejercicio item)
        {
            switch (Request.HttpMethod)
            {
                case "POST":
                    return Json(EjerciciosManager.InsertEjercicio(item));

                case "PUT":
                    return Json(EjerciciosManager.UpdateEjercicio(item));

                case "GET":
                    return Json(EjerciciosManager.GetEjercicio(item), JsonRequestBehavior.AllowGet);

                case "DELETE":
                    return Json(EjerciciosManager.DeleteEjercicio(item));
            }

            return Json(new { Error = true, Messege = "Operación HTTP desconocida" });
        }
    }
}