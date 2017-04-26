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

        // GET /Api/Usuarios
        [HttpGet]
        public JsonResult Usuarios()
        {
            return Json(UsuariosManager.GetUsuarios(), JsonRequestBehavior.AllowGet);
        }

        // POST     /Api/Usuarios/Usuario       { NombreUsuario:"nombre", TelefonoUsuario:123456789 }
        // PUT      /Api/Usuarios/Usuario/3     { IdUsuario:3, NombreUsuario:"nombre", TelefonoUsuario:123456789 }
        // GET      /Api/Usuarios/Usuario/3
        // DELETE   /Api/Usuarios/Usuario/3
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