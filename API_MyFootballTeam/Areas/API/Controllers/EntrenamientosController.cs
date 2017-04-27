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

        // GET /Api/Entrenamientos
        [HttpGet]
        public JsonResult Entrenamientos(int idEquipo)
        {
            return Json(EntrenamientosManager.GetEntrenamientos(idEquipo), JsonRequestBehavior.AllowGet);
        }

        // POST     /Api/Entrenamientos/Entrenamiento       { NombreUsuario:"nombre", TelefonoUsuario:123456789 }
        // PUT      /Api/Entrenamientos/Entrenamiento/3     { IdUsuario:3, NombreUsuario:"nombre", TelefonoUsuario:123456789 }
        // GET      /Api/Entrenamientos/Entrenamiento/3
        // DELETE   /Api/Entrenamientos/Entrenamiento/3
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