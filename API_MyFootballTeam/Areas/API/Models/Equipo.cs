using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_MyFootballTeam.Areas.API.Models
{
    public class Equipo
    {
        public int IdEquipo { get; set; }
        public string NombreEquipo { get; set; }
        public string Direccion { get; set; }
        public string Categoria { get; set; }
        public string FotoEscudo { get; set; }
        public int Usuario_IdUsuario { get; set; }
    }
}