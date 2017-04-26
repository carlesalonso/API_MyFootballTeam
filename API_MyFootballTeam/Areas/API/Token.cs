using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace API_MyFootballTeam.Areas.API
{
    public class Token
    {
        private static string cadenaConexion = Utilidades.cadenaConexion;

        public static bool BuscarTokenUsuario(string token)
        {
            SqlConnection conexion = new SqlConnection(cadenaConexion);

            conexion.Open();

            string sql = "SELECT Token FROM Usuario where Token ='" + token + "'";

            SqlCommand cmd = new SqlCommand(sql, conexion);

            SqlDataReader reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

            if (reader.Read())
            {
                reader.Close();
                return true;
            }
            else
            {
                reader.Close();
                return false;
            }
        }
    }
}