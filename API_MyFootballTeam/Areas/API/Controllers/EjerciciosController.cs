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

        // GET      http://apimyfootballteamnuevawebapp.azurewebsites.net/API/Ejercicios
        [HttpGet]
        public JsonResult Ejercicios()
        {
            return Json(EjerciciosManager.GetEjercicios(), JsonRequestBehavior.AllowGet);
        }

        // POST     http://apimyfootballteamnuevawebapp.azurewebsites.net/API/Ejercicios/Ejercicio      ?NombreEjercicio=Correr&Descripcion=De punta a punta del campo&Foto=Morgan.png&Usuario_IdUsuario=24
        // PUT      http://apimyfootballteamnuevawebapp.azurewebsites.net/API/Ejercicios/Ejercicio      ?IdEjercicio=1&NombreEjercicio=Correr&Descripcion=De punta a punta del campo&Foto=Morgan.png
        // GET      http://apimyfootballteamnuevawebapp.azurewebsites.net/API/Ejercicios/Ejercicio      ?IdEjercicio=1
        // DELETE   http://apimyfootballteamnuevawebapp.azurewebsites.net/API/Ejercicios/Ejercicio      ?IdEjercicio=1
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