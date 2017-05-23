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

        // GET      http://apimyfootballteamnuevawebapp.azurewebsites.net/API/Jugadores
        [HttpGet]
        public JsonResult Jugadores(int idEquipo)
        {
            return Json(JugadoresManager.GetJugadores(idEquipo), JsonRequestBehavior.AllowGet);
        }

        // POST     http://apimyfootballteamnuevawebapp.azurewebsites.net/API/Jugadores/Jugador     ?NombreJugador=Bale&FechaNacimiento=1990-12-30&Altura=1.85&Dorsal=11&TelefonoJugador=699556916&Posicion=Extremo&Lesion=0&Equipo_IdEquipo=1
        // PUT      http://apimyfootballteamnuevawebapp.azurewebsites.net/API/Jugadores/Jugador     ?IdJugador=3&NombreJugador=Bale&FechaNacimiento=1990-12-30&Altura=1.85&Dorsal=11&TelefonoJugador=699556916&Posicion=Extremo&Lesion=0&Equipo_IdEquipo=1
        // GET      http://apimyfootballteamnuevawebapp.azurewebsites.net/API/Jugadores/Jugador     ?IdJugador=3
        // DELETE   http://apimyfootballteamnuevawebapp.azurewebsites.net/API/Jugadores/Jugador     ?IdJugador=2
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