using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using API_MyFootballTeam.Areas.API.Models;

namespace API_MyFootballTeam.Areas.API.Controllers
{
    public class JugadoresController : Controller
    {
        private JugadorManager JugadoresManager;

        public JugadoresController()
        {
            JugadoresManager = new JugadorManager();
        }

        // GET /Api/Jugadores
        [HttpGet]
        public JsonResult Jugadores(int idEquipo)
        {
            return Json(JugadoresManager.GetJugadores(idEquipo), JsonRequestBehavior.AllowGet);
        }

        // POST     /Api/Jugadores/Jugador       { NombreUsuario:"nombre", TelefonoUsuario:123456789 }
        // PUT      /Api/Jugadores/Jugador/3     { IdUsuario:3, NombreUsuario:"nombre", TelefonoUsuario:123456789 }
        // GET      /Api/Jugadores/Jugador/3
        // DELETE   /Api/Jugadores/Jugador/3
        public JsonResult Jugador(int? id, Jugador item)
        {
            switch (Request.HttpMethod)
            {
                case "POST":
                    return Json(JugadoresManager.InsertJugador(item));

                case "PUT":
                    return Json(JugadoresManager.UpdateJugador(item));

                case "GET":
                    return Json(JugadoresManager.GetJugador(item), JsonRequestBehavior.AllowGet);

                case "DELETE":
                    return Json(JugadoresManager.DeleteJugador(item));
            }

            return Json(new { Error = true, Messege = "Operación HTTP desconocida" });
        }
    }
}