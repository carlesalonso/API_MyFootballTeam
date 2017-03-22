using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Security.Cryptography;
using System.Data.SqlClient;


namespace API_MyFootballTeam.Areas.API.Models
{
    public class UsuarioManager
    {
        public string hash;
        
        //Cadena de conexion
        private static string cadenaConexion = Utilidades.cadenaConexion;

        //public bool InsertUsuario (Usuario usuario)
        //{
        //    SqlConnection conexion = new SqlConnection(cadenaConexion);

        //    conexion.Open();

        //    string llamada;

        //    llamada = 


        //}

    }
}