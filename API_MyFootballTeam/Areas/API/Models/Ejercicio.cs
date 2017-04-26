using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_MyFootballTeam.Areas.API.Models
{
    public class Ejercicio
    {
        public int IdEjercicio { get; set; }
        public string NombreEjercicio { get; set; }
        public string Descripcion { get; set; }
        public string Foto { get; set; }
        public int Usuario_IdUsuario { get; set; }
    }
}