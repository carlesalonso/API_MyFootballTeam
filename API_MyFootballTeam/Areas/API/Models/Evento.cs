using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_MyFootballTeam.Areas.API.Models
{
    public class Evento
    {
        public int IdEvento { get; set; }
        public int Minuto { get; set; }
        public string NombreEvento { get; set; }
        public int Partido_IdPartido { get; set; }
        public int?  Jugador_IdJugador { get; set; }
        
    }
}