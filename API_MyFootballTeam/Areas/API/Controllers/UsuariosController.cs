using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using API_MyFootballTeam.Areas.API.Models;

namespace API_MyFootballTeam.Areas.API.Controllers
{
    public class UsuariosController : Controller
    {
        private UsuarioManager UsuariosManager;

        public UsuariosController()
        {
            UsuariosManager = new UsuarioManager();
        }

        // GET      http://apimyfootballteamnuevawebapp.azurewebsites.net/API/Usuarios
        [HttpGet]
        public JsonResult Usuarios()
        {
            return Json(UsuariosManager.GetUsuarios(), JsonRequestBehavior.AllowGet);
        }

        // POST     http://apimyfootballteamnuevawebapp.azurewebsites.net/API/Usuarios/Usuario      ?EmailUsuario=usuario2&Password=123&NombreUsuario=Morgan&ApellidoUsuario=Quintana&TelefonoUsuario=123456789
        // PUT      http://apimyfootballteamnuevawebapp.azurewebsites.net/API/Usuarios/Usuario      ?EmailUsuario=morgan.cordoba@hotmail.com&Password=123&NombreUsuario=MorganManuel&ApellidoUsuario=Quintana&TelefonoUsuario=123456789&NIF=123&IBAN=123
        // GET      http://apimyfootballteamnuevawebapp.azurewebsites.net/API/Usuarios/Usuario
        // DELETE   http://apimyfootballteamnuevawebapp.azurewebsites.net/API/Usuarios/Usuario
        public JsonResult Usuario(int? id, Usuario item)
        {
            switch (Request.HttpMethod)
            {
                case "POST":
                    return Json(UsuariosManager.InsertUsuario(item));

                case "PUT":
                    return Json(UsuariosManager.UpdateUsuario(item));

                case "GET":
                    return Json(UsuariosManager.GetUsuario(item), JsonRequestBehavior.AllowGet);

                case "DELETE":
                    return Json(UsuariosManager.DeleteUsuario(item));
            }

            return Json(new { Error = true, Messege = "Operación HTTP desconocida" });
        }
    }
}