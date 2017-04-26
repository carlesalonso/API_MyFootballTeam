using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_MyFootballTeam.Areas.API.Models
{
    public class Jugador
    {
        public int IdJugador { get; set; }
        public string NombreJugador { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public float Altura { get; set; }
        public int Dorsal { get; set; }
        public decimal TelefonoJugador { get; set; }
        public string Posicion { get; set; }
        public Boolean Lesion { get; set; }
        public int Equipo_IdEquipo { get; set; }
    }
}