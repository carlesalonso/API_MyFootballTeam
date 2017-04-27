using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace API_MyFootballTeam.Areas.API.Models
{
    public class EntrenamientoManager
    {
        // Esta Clase es una M/N de Ejercicio y Equipo 

        //Cadena de conexion
        private static string cadenaConexion = Utilidades.cadenaConexion;

        //--------------------------------------*********
        // Metodo para insertar un Entrenamiento 
        //--------------------------------------*********
        public bool InsertEntrenamiento(Entrenamiento entrenamiento)
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

            string sqlQuery = "INSERT INTO Entrenamiento (Duracion, Orden, Ejercicio_IdEjercicio, Equipo_IdEquipo) " +
                "VALUES (@Duracion, @Orden, @Ejercicio_IdEjercicio, @Equipo_IdEquipo)";

            SqlCommand comandaInsert = new SqlCommand(sqlQuery, conexion);

            //Aqui sustituyo los valores que tienen el @ por los que le vamos a insertar
            comandaInsert.Parameters.Add("@Duracion", System.Data.SqlDbType.Int).Value = entrenamiento.Duracion;
            comandaInsert.Parameters.Add("@Orden", System.Data.SqlDbType.Int).Value = entrenamiento.Orden;
            comandaInsert.Parameters.Add("@Ejercicio_IdEjercicio", System.Data.SqlDbType.Int).Value = entrenamiento.Ejercicio_IdEjercicio;
            comandaInsert.Parameters.Add("@Equipo_IdEquipo", System.Data.SqlDbType.Int).Value = entrenamiento.Equipo_IdEquipo;

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

        //------------------------------------------*********
        // Funcion para actualizar el Entrenamiento 
        //------------------------------------------*********
        //Este metodo recibe un objeto de la clase Entrenamiento, que tiene los datos del evento ya cargados
        public bool UpdateEntrenamiento(Entrenamiento entrenamiento)
        {
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            conexion.Open();

            //Miro y cojo el token que hay en el header de las peticion 
            string tokenLlamada;
            tokenLlamada = System.Web.HttpContext.Current.Request.Headers["token"];

            // Compruebo que el token existe en la base de datos
            if (Token.BuscarTokenUsuario(tokenLlamada))
            {

                string sqlQuery = "UPDATE Entrenamiento SET Duracion = @Duracion, Orden = @Orden, Ejercicio_IdEjercicio = @Ejercicio_IdEjercicio, Equipo_IdEquipo = @Equipo_IdEquipo WHERE IdEntrenamiento = @IdEntrenamiento";

                SqlCommand comandaUpdate = new SqlCommand(sqlQuery, conexion);

                // Aqui estoy sustituyendo cada valor que tiene antes un @ por los nuevos valores que me llegan por el objeto de la clase Usuario
                comandaUpdate.Parameters.Add("@IdEntrenamiento", System.Data.SqlDbType.Int).Value = entrenamiento.IdEntrenamiento;
                comandaUpdate.Parameters.Add("@Duracion", System.Data.SqlDbType.Int).Value = entrenamiento.Duracion;
                comandaUpdate.Parameters.Add("@Orden", System.Data.SqlDbType.Int).Value = entrenamiento.Orden;
                comandaUpdate.Parameters.Add("@Ejercicio_IdEjercicio", System.Data.SqlDbType.Int).Value = entrenamiento.Ejercicio_IdEjercicio;
                comandaUpdate.Parameters.Add("@Equipo_IdEquipo", System.Data.SqlDbType.Int).Value = entrenamiento.Equipo_IdEquipo;

                int res = comandaUpdate.ExecuteNonQuery();
                return (res == 1);
            }
            conexion.Close();
            return false;
        }

        //------------------------------------------*********
        // Metodo GET que devuelve un Entrenamiento 
        //------------------------------------------*********
        //Este metodo tiene que recibir el token de la llamada y el nombre del Evento que busca
        public Entrenamiento GetEntrenamiento(Entrenamiento entrenamiento)
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

            string sqlQuery = "SELECT IdEntrenamiento, Duracion, Orden, Ejercicio_IdEjercicio, Equipo_IdEquipo FROM Entrenamiento WHERE IdEntrenamiento = @IdEntrenamiento";
            SqlCommand comandaSelect = new SqlCommand(sqlQuery, conexion);
            comandaSelect.Parameters.Add("@IdEntrenamiento", System.Data.SqlDbType.Int).Value = entrenamiento.IdEntrenamiento;
            SqlDataReader reader = comandaSelect.ExecuteReader();
            if (reader.Read())
            {
                entrenamiento = new Entrenamiento();

                entrenamiento.IdEntrenamiento = reader.GetInt32(0);
                entrenamiento.Duracion = reader.GetInt32(1);
                entrenamiento.Orden = reader.GetInt32(2);
                entrenamiento.Ejercicio_IdEjercicio = reader.GetInt32(3);
                entrenamiento.Equipo_IdEquipo = reader.GetInt32(4);
                
            }
            reader.Close();
            conexion.Close();

            return entrenamiento;
        }

        //----------------------------------------------------------------------------********* 
        // Metodo GET que devuelve una lista con todos los Entrenamientos de un equipo 
        //----------------------------------------------------------------------------*********
        public List<Entrenamiento> GetEntrenamientos(int idEquipo)
        {
            string tokenLlamada;
            tokenLlamada = System.Web.HttpContext.Current.Request.Headers["Token"];
            if (Token.BuscarTokenUsuario(tokenLlamada) == false)
            {
                return null;
            }

            List<Entrenamiento> lista = new List<Entrenamiento>();
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            conexion.Open();

            string sqlQuery = "SELECT IdEntrenamiento, Duracion, Orden, Ejercicio_IdEjercicio, Equipo_IdEquipo FROM Entrenamiento WHERE Equipo_IdEquipo = @Equipo_IdEquipo";
            SqlCommand comandaSelect = new SqlCommand(sqlQuery, conexion);
            comandaSelect.Parameters.Add("@Equipo_IdEquipo", System.Data.SqlDbType.Int).Value = idEquipo;
            SqlDataReader reader = comandaSelect.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                Entrenamiento entrenamiento = new Entrenamiento();

                entrenamiento.IdEntrenamiento = reader.GetInt32(0);
                entrenamiento.Duracion = reader.GetInt32(1);
                entrenamiento.Orden = reader.GetInt32(2);
                entrenamiento.Ejercicio_IdEjercicio = reader.GetInt32(3);
                entrenamiento.Equipo_IdEquipo = reader.GetInt32(4);
                
                lista.Add(entrenamiento);
            }

            reader.Close();
            conexion.Close();

            return lista;
        }

        //------------------------------------------*********
        // Funcion para eliminar a un Entrenamiento 
        //------------------------------------------*********
        public bool DeleteEntrenamiento(Entrenamiento entrenamiento)
        {
            SqlConnection conexion = new SqlConnection(cadenaConexion);

            conexion.Open();

            string tokenLlamada;
            tokenLlamada = System.Web.HttpContext.Current.Request.Headers["token"];

            if (Token.BuscarTokenUsuario(tokenLlamada))
            {
                string sql = "DELETE FROM Entrenamiento WHERE IdEntrenamiento = @IdEntrenamiento ";

                SqlCommand cmd = new SqlCommand(sql, conexion);
                cmd.Parameters.Add("@IdEntrenamiento", System.Data.SqlDbType.NVarChar).Value = entrenamiento.IdEntrenamiento;

                int res = cmd.ExecuteNonQuery();

                conexion.Close();
                return (res == 1);
            }

            conexion.Close();
            return false;
        }
    }
}