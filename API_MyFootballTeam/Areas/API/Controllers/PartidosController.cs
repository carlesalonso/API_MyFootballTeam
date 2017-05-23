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

        // GET      http://apimyfootballteamnuevawebapp.azurewebsites.net/API/Partidos      ?idEquipo=9
        [HttpGet]
        public JsonResult Partidos(int idEquipo)
        {
            return Json(PartidosManager.GetPartidos(idEquipo), JsonRequestBehavior.AllowGet);
        }

        // POST     http://apimyfootballteamnuevawebapp.azurewebsites.net/API/Partidos/Partido      ?FechaPartido=2017-04-25&Jornada=2&Rival=Juventus&Local=true&Equipo_IdEquipo=9
        // PUT      http://apimyfootballteamnuevawebapp.azurewebsites.net/API/Partidos/Partido      ?IdPartido=1&FechaPartido=2017-04-25&Jornada=1990-12-30&Rival=Juventus&Local=true&Equipo_IdEquipo=1
        // GET      http://apimyfootballteamnuevawebapp.azurewebsites.net/API/Partidos/Partido      ?IdPartido=1
        // DELETE   http://apimyfootballteamnuevawebapp.azurewebsites.net/API/Partidos/Partido      ?IdPartido=1
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