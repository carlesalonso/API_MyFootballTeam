using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_MyFootballTeam.Areas.API.Models
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string EmailUsuario { get; set; }
        public string Password { get; set; }
        public string NombreUsuario { get; set; }
        public string ApellidoUsuario { get; set; }
        public decimal TelefonoUsuario { get; set; }
        public string NIF { get; set; }
        public string IBAN { get; set; }
        public string Token { get; set; }
    }
}