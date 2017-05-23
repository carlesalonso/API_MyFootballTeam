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

        // GET      http://apimyfootballteamnuevawebapp.azurewebsites.net/API/Equipos
        [HttpGet]
        public JsonResult Equipos()
        {
            return Json(EquiposManager.GetEquipos(), JsonRequestBehavior.AllowGet);
        }

        // POST     http://apimyfootballteamnuevawebapp.azurewebsites.net/API/Equipos/Equipo        ?NombreEquipo=Atletico de Madrid&Direccion=Alarona&Categoria=PrimeraDivision
        // PUT      http://apimyfootballteamnuevawebapp.azurewebsites.net/API/Equipos/Equipo        ?IdEquipo=9&NombreEquipo=Real Betis&Direccion=Alarona&Categoria=Primera Division&FotoEscudo=Escudopmg
        // GET      http://apimyfootballteamnuevawebapp.azurewebsites.net/API/Equipos/Equipo        ?IdEquipo=1
        // DELETE   http://apimyfootballteamnuevawebapp.azurewebsites.net/API/Equipos/Equipo        ?IdEquipo=7
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