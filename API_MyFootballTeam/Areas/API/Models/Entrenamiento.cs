using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_MyFootballTeam.Areas.API.Models
{
    public class Entrenamiento
    {
        public int IdEntrenamiento { get; set; }
        public Boolean AsistenciaEntreno { get; set; }
        public int Equipo_IdEquipo { get; set; }
    }
}