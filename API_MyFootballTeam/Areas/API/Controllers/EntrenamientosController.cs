using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using API_MyFootballTeam.Areas.API.Models;

namespace API_MyFootballTeam.Areas.API.Controllers
{
    public class EntrenamientosController : Controller
    {
        private EntrenamientoManager EntrenamientosManager;

        public EntrenamientosController()
        {
            EntrenamientosManager = new EntrenamientoManager();
        }

        // GET http://apimyfootballteamnuevawebapp.azurewebsites.net/API/Entrenamientos     ?idEquipo=1
        [HttpGet]
        public JsonResult Entrenamientos(int idEquipo)
        {
            return Json(EntrenamientosManager.GetEntrenamientos(idEquipo), JsonRequestBehavior.AllowGet);
        }

        // POST     http://apimyfootballteamnuevawebapp.azurewebsites.net/API/Entrenamientos/Entrenamiento      ?Duracion=20&Orden=2&Ejercicio_IdEjercicio=3&Equipo_IdEquipo=1
        // PUT      http://apimyfootballteamnuevawebapp.azurewebsites.net/API/Entrenamientos/Entrenamiento      ?IdEntrenamiento=1&Duracion=15&Orden=1&Ejercicio_IdEjercicio=2&Equipo_IdEquipo=1
        // GET      http://apimyfootballteamnuevawebapp.azurewebsites.net/API/Entrenamientos/Entrenamiento      ?IdEntrenamiento=1
        // DELETE   http://apimyfootballteamnuevawebapp.azurewebsites.net/API/Entrenamientos/Entrenamiento      ?IdEntrenamiento=2
        public JsonResult Entrenamiento(int? id, Entrenamiento item)
        {
            switch (Request.HttpMethod)
            {
                case "POST":
                    return Json(EntrenamientosManager.InsertEntrenamiento(item));

                case "PUT":
                    return Json(EntrenamientosManager.UpdateEntrenamiento(item));

                case "GET":
                    return Json(EntrenamientosManager.GetEntrenamiento(item), JsonRequestBehavior.AllowGet);

                case "DELETE":
                    return Json(EntrenamientosManager.DeleteEntrenamiento(item));
            }

            return Json(new { Error = true, Messege = "Operación HTTP desconocida" });
        }
    }
}