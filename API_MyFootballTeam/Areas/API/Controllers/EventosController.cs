using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using API_MyFootballTeam.Areas.API.Models;

namespace API_MyFootballTeam.Areas.API.Controllers
{
    public class EventosController : Controller
    {
        private EventoManager EventosManager;

        public EventosController()
        {
            EventosManager = new EventoManager();
        }

        // GET /Api/Eventos
        [HttpGet]
        public JsonResult Eventos(int idPartido)
        {
            return Json(EventosManager.GetEventos(idPartido), JsonRequestBehavior.AllowGet);
        }

        // POST     /Api/Eventos/Evento       { NombreUsuario:"nombre", TelefonoUsuario:123456789 }
        // PUT      /Api/Eventos/Evento/3     { IdUsuario:3, NombreUsuario:"nombre", TelefonoUsuario:123456789 }
        // GET      /Api/Eventos/Evento/3
        // DELETE   /Api/Eventos/Evento/3
        public JsonResult Evento(int? id, Evento item)
        {
            switch (Request.HttpMethod)
            {
                case "POST":
                    return Json(EventosManager.InsertEvento(item));

                case "PUT":
                    return Json(EventosManager.UpdateEvento(item));

                case "GET":
                    return Json(EventosManager.GetEvento(item), JsonRequestBehavior.AllowGet);

                case "DELETE":
                    return Json(EventosManager.DeleteEvento(item));
            }

            return Json(new { Error = true, Messege = "Operación HTTP desconocida" });
        }
    }
}