using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using API_MyFootballTeam.Areas.API.Models;

namespace API_MyFootballTeam.Areas.API.Controllers
{
    public class PartidosController : Controller
    {
        private PartidoManager PartidosManager;

        public PartidosController()
        {
            PartidosManager = new PartidoManager();
        }

        // GET /Api/Partidos
        [HttpGet]
        public JsonResult Partidos(int idEquipo)
        {
            return Json(PartidosManager.GetPartidos(idEquipo), JsonRequestBehavior.AllowGet);
        }

        // POST     /Api/Partidos/Partido       { NombreUsuario:"nombre", TelefonoUsuario:123456789 }
        // PUT      /Api/Partidos/Partido/3     { IdUsuario:3, NombreUsuario:"nombre", TelefonoUsuario:123456789 }
        // GET      /Api/Partidos/Partido/3
        // DELETE   /Api/Partidos/Partido/3
        public JsonResult Partido(int? id, Partido item)
        {
            switch (Request.HttpMethod)
            {
                case "POST":
                    return Json(PartidosManager.InsertPartido(item));

                case "PUT":
                    return Json(PartidosManager.UpdatePartido(item));

                case "GET":
                    return Json(PartidosManager.GetPartido(item), JsonRequestBehavior.AllowGet);

                case "DELETE":
                    return Json(PartidosManager.DeletePartido(item));
            }

            return Json(new { Error = true, Messege = "Operación HTTP desconocida" });
        }
    }
}