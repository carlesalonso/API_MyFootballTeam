using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_MyFootballTeam.Areas.API.Models
{
    public class Entrenamiento
    {
        public int IdEntrenamiento { get; set; }
        public int Duracion { get; set; }
        public int Orden { get; set; }
        public int Ejercicio_IdEjercicio { get; set; }
        public int Equipo_IdEquipo { get; set; }
    }
}