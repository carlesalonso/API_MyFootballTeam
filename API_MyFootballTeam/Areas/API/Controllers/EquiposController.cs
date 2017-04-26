using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using API_MyFootballTeam.Areas.API.Models;

namespace API_MyFootballTeam.Areas.API.Controllers
{
    public class EquiposController : Controller
    {
        private EquipoManager EquiposManager;

        public EquiposController()
        {
            EquiposManager = new EquipoManager();
        }

        // GET /Api/Equipos
        [HttpGet]
        public JsonResult Equipos()
        {
            return Json(EquiposManager.GetEquipos(), JsonRequestBehavior.AllowGet);
        }

        // POST     /Api/Equipos/Equipo       { NombreUsuario:"nombre", TelefonoUsuario:123456789 }
        // PUT      /Api/Equipos/Equipo/3     { IdUsuario:3, NombreUsuario:"nombre", TelefonoUsuario:123456789 }
        // GET      /Api/Equipos/Equipo/3
        // DELETE   /Api/Equipos/Equipo/3
        public JsonResult Equipo(int? id, Equipo item)
        {
            switch (Request.HttpMethod)
            {
                case "POST":
                    return Json(EquiposManager.InsertEquipo(item));

                case "PUT":
                    return Json(EquiposManager.UpdateEquipo(item));

                case "GET":
                    return Json(EquiposManager.GetEquipo(item), JsonRequestBehavior.AllowGet);

                case "DELETE":
                    return Json(EquiposManager.DeleteEquipo(item));
            }

            return Json(new { Error = true, Messege = "Operación HTTP desconocida" });
        }
    }
}