using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace API_MyFootballTeam.Areas.API.Models
{
    public class PartidoManager
    {
        //Cadena de conexion
        private static string cadenaConexion = Utilidades.cadenaConexion;

        //--------------------------------*********
        // Metodo para insertar un Partido FUNCIONA
        //--------------------------------*********
        public bool InsertPartido(Partido partido)
        {
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            conexion.Open();

            //Miro y cojo el token que hay en el header de las peticion 
            string tokenLlamada;
            tokenLlamada = System.Web.HttpContext.Current.Request.Headers["token"];
            if (!(Token.BuscarTokenUsuario(tokenLlamada)))
            {
                return false;
            }

            string sqlQuery = "INSERT INTO Partido (FechaPartido, Jornada, Rival, Local, Equipo_IdEquipo) " +
                "VALUES (@FechaPartido, @Jornada, @Rival, @Local, @Equipo_IdEquipo)";

            SqlCommand comandaInsert = new SqlCommand(sqlQuery, conexion);

            //Aqui sustituyo los valores que tienen el @ por los que le vamos a insertar

            comandaInsert.Parameters.Add("@FechaPartido", System.Data.SqlDbType.Date).Value = partido.FechaPartido;
            comandaInsert.Parameters.Add("@Jornada", System.Data.SqlDbType.Int).Value = partido.Jornada;
            comandaInsert.Parameters.Add("@Rival", System.Data.SqlDbType.NVarChar).Value = partido.Rival;
            comandaInsert.Parameters.Add("@Local", System.Data.SqlDbType.Bit).Value = partido.Local;
            comandaInsert.Parameters.Add("@Equipo_IdEquipo", System.Data.SqlDbType.Int).Value = partido.Equipo_IdEquipo;

            int res = comandaInsert.ExecuteNonQuery();

            conexion.Close();
            if (res == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //-----------------------------------*********
        // Funcion para actualizar el Partido FUNCIONA
        //-----------------------------------*********
        //Este metodo recibe un objeto de la clase Jugador, que tiene los datos del jugador ya cargados
        public bool UpdatePartido(Partido partido)
        {
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            conexion.Open();

            //Miro y cojo el token que hay en el header de las peticion 
            string tokenLlamada;
            tokenLlamada = System.Web.HttpContext.Current.Request.Headers["token"];

            // Compruebo que el token existe en la base de datos
            if (Token.BuscarTokenUsuario(tokenLlamada))
            {

                string sqlQuery = "UPDATE Partido SET FechaPartido = @FechaPartido, Jornada = @Jornada, Rival = @Rival, Local = @Local, Equipo_IdEquipo = @Equipo_IdEquipo WHERE IdPartido = @IdPartido";

                SqlCommand comandaUpdate = new SqlCommand(sqlQuery, conexion);

                // Aqui estoy sustituyendo cada valor que tiene antes un @ por los nuevos valores que me llegan por el objeto de la clase Usuario
                comandaUpdate.Parameters.Add("@IdPartido", System.Data.SqlDbType.Int).Value = partido.IdPartido;
                comandaUpdate.Parameters.Add("@FechaPartido", System.Data.SqlDbType.Date).Value = partido.FechaPartido;
                comandaUpdate.Parameters.Add("@Jornada", System.Data.SqlDbType.Int).Value = partido.Jornada;
                comandaUpdate.Parameters.Add("@Rival", System.Data.SqlDbType.NVarChar).Value = partido.Rival;
                comandaUpdate.Parameters.Add("@Local", System.Data.SqlDbType.Bit).Value = partido.Local;
                comandaUpdate.Parameters.Add("@Equipo_IdEquipo", System.Data.SqlDbType.Int).Value = partido.Equipo_IdEquipo;

                int res = comandaUpdate.ExecuteNonQuery();
                return (res == 1);
            }
            conexion.Close();
            return false;
        }

        //-----------------------------------*********
        // Metodo GET que devuelve un Partido FUNCIONA
        //-----------------------------------*********
        //Este metodo tiene que recibir el token de la llamada y el nombre del equipo que busca
        public Partido GetPartido(Partido partido)
        {
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            conexion.Open();

            //Miro y cojo el token que hay en el header de las peticion 
            string tokenLlamada;
            tokenLlamada = System.Web.HttpContext.Current.Request.Headers["token"];
            //Compruebo si el usuario esta logueado
            if (!(Token.BuscarTokenUsuario(tokenLlamada)))
            {
                return null;
            }

            string sqlQuery = "SELECT IdPartido, FechaPartido, Jornada, Rival, Local, Equipo_IdEquipo FROM Partido WHERE IdPartido = @IdPartido";
            SqlCommand comandaSelect = new SqlCommand(sqlQuery, conexion);
            comandaSelect.Parameters.Add("@IdPartido", System.Data.SqlDbType.Int).Value = partido.IdPartido;
            SqlDataReader reader = comandaSelect.ExecuteReader();
            if (reader.Read())
            {
                partido = new Partido();

                partido.IdPartido = reader.GetInt32(0);
                partido.FechaPartido = reader.GetDateTime(1);
                partido.Jornada = reader.GetInt32(2);
                partido.Rival = reader.GetString(3);
                partido.Local = reader.GetBoolean(4);
                partido.Equipo_IdEquipo = reader.GetInt32(5);
            }
            reader.Close();
            conexion.Close();

            return partido;
        }

        //----------------------------------------------------------------------********* 
        // Metodo GET que devuelve una lista con todos los PARTIDOS de un equipo FUNCIONA
        //----------------------------------------------------------------------*********
        public List<Partido> GetPartidos(int idEquipo)
        {
            string tokenLlamada;
            tokenLlamada = System.Web.HttpContext.Current.Request.Headers["Token"];
            if (Token.BuscarTokenUsuario(tokenLlamada) == false)
            {
                return null;
            }

            List<Partido> lista = new List<Partido>();
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            conexion.Open();

            string sqlQuery = "SELECT IdPartido, FechaPartido, Jornada, Rival, Local, Equipo_IdEquipo FROM Partido WHERE Equipo_IdEquipo = @Equipo_IdEquipo";
            SqlCommand comandaSelect = new SqlCommand(sqlQuery, conexion);
            comandaSelect.Parameters.Add("@Equipo_IdEquipo", System.Data.SqlDbType.NVarChar).Value = idEquipo;
            SqlDataReader reader = comandaSelect.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                Partido partido = new Partido();

                partido.IdPartido = reader.GetInt32(0);
                partido.FechaPartido = reader.GetDateTime(1);
                partido.Jornada = reader.GetInt32(2);
                partido.Rival = reader.GetString(3);
                partido.Local = reader.GetBoolean(4);
                partido.Equipo_IdEquipo = reader.GetInt32(5);
                
                lista.Add(partido);
            }

            reader.Close();
            conexion.Close();

            return lista;
        }

        //-----------------------------------*********
        // Funcion para eliminar a un Partido FUNCIONA
        //-----------------------------------*********
        public bool DeletePartido(Partido partido)
        {
            SqlConnection conexion = new SqlConnection(cadenaConexion);

            conexion.Open();

            string tokenLlamada;
            tokenLlamada = System.Web.HttpContext.Current.Request.Headers["token"];

            if (Token.BuscarTokenUsuario(tokenLlamada))
            {
                string sql = "DELETE FROM Partido WHERE IdPartido = @IdPartido ";

                SqlCommand cmd = new SqlCommand(sql, conexion);
                cmd.Parameters.Add("@IdPartido", System.Data.SqlDbType.NVarChar).Value = partido.IdPartido;

                int res = cmd.ExecuteNonQuery();

                conexion.Close();
                return (res == 1);
            }

            conexion.Close();
            return false;
        }
    }
}