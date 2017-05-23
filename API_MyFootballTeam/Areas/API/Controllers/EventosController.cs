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

        // GET      http://apimyfootballteamnuevawebapp.azurewebsites.net/API/Eventos       ?idPartido=58
        [HttpGet]
        public JsonResult Eventos(int idPartido)
        {
            return Json(EventosManager.GetEventos(idPartido), JsonRequestBehavior.AllowGet);
        }

        // POST     http://apimyfootballteamnuevawebapp.azurewebsites.net/API/Eventos/Evento        ?NombreEvento=Gol&Partido_IdPartido=39
        // PUT      http://apimyfootballteamnuevawebapp.azurewebsites.net/API/Eventos/Evento        ?IdEvento=8&Minuto=1&NombreEvento=Gol&Partido_IdPartido=2&Jugador_IdJugador=-1
        // GET      http://apimyfootballteamnuevawebapp.azurewebsites.net/API/Eventos/Evento        ?IdEvento=8
        // DELETE   http://apimyfootballteamnuevawebapp.azurewebsites.net/API/Eventos/Evento        ?IdEvento=5
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