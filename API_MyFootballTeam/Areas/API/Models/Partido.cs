using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_MyFootballTeam.Areas.API.Models
{
    public class Partido
    {
        public int IdPartido { get; set; }
        public DateTime FechaPartido { get; set; }
        public int Jornada { get; set; }
        public string Rival { get; set; }
        public Boolean Local { get; set; }
        public int Equipo_IdEquipo { get; set; }
    }
}