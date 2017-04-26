using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace API_MyFootballTeam.Areas.API.Models
{
    public class JugadorManager
    {
        //Cadena de conexion
        private static string cadenaConexion = Utilidades.cadenaConexion;

        //--------------------------------*********
        // Metodo para insertar un Jugador FUNCIONA
        //--------------------------------*********
        public bool InsertJugador(Jugador jugador)
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

            string sqlQuery = "INSERT INTO Jugador (NombreJugador, FechaNacimiento, Altura, Dorsal, TelefonoJugador, Posicion, Lesion, Equipo_IdEquipo) " +
                "VALUES (@NombreJugador, @FechaNacimiento, @Altura, @Dorsal, @TelefonoJugador, @Posicion, @Lesion, @Equipo_IdEquipo)";

            SqlCommand comandaInsert = new SqlCommand(sqlQuery, conexion);

            //Aqui sustituyo los valores que tienen el @ por los que le vamos a insertar
            
            comandaInsert.Parameters.Add("@NombreJugador", System.Data.SqlDbType.NVarChar).Value = jugador.NombreJugador;
            comandaInsert.Parameters.Add("@FechaNacimiento", System.Data.SqlDbType.Date).Value = jugador.FechaNacimiento;
            comandaInsert.Parameters.Add("@Altura", System.Data.SqlDbType.Float).Value = jugador.Altura;
            comandaInsert.Parameters.Add("@Dorsal", System.Data.SqlDbType.Int).Value = jugador.Dorsal;
            comandaInsert.Parameters.Add("@TelefonoJugador", System.Data.SqlDbType.Float).Value = jugador.TelefonoJugador;
            comandaInsert.Parameters.Add("@Posicion", System.Data.SqlDbType.NVarChar).Value = jugador.Posicion;
            comandaInsert.Parameters.Add("@Lesion", System.Data.SqlDbType.Bit).Value = jugador.Lesion;
            comandaInsert.Parameters.Add("@Equipo_IdEquipo", System.Data.SqlDbType.Int).Value = jugador.Equipo_IdEquipo;

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
        // Funcion para actualizar el Jugador FUNCIONA
        //-----------------------------------*********
        //Este metodo recibe un objeto de la clase Jugador, que tiene los datos del jugador ya cargados
        public bool UpdateJugador(Jugador jugador)
        {
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            conexion.Open();

            //Miro y cojo el token que hay en el header de las peticion 
            string tokenLlamada;
            tokenLlamada = System.Web.HttpContext.Current.Request.Headers["token"];

            // Compruebo que el token existe en la base de datos
            if (Token.BuscarTokenUsuario(tokenLlamada))
            {
              
                string sqlQuery = "UPDATE Jugador SET NombreJugador = @NombreJugador, FechaNacimiento = @FechaNacimiento, Altura = @Altura, Dorsal = @Dorsal, TelefonoJugador = @TelefonoJugador, Posicion = @Posicion, Lesion = @Lesion, Equipo_IdEquipo = @Equipo_IdEquipo WHERE IdJugador = @IdJugador";

                SqlCommand comandaUpdate = new SqlCommand(sqlQuery, conexion);

                // Aqui estoy sustituyendo cada valor que tiene antes un @ por los nuevos valores que me llegan por el objeto de la clase Usuario
                comandaUpdate.Parameters.Add("@IdJugador", System.Data.SqlDbType.Int).Value = jugador.IdJugador;
                comandaUpdate.Parameters.Add("@NombreJugador", System.Data.SqlDbType.NVarChar).Value = jugador.NombreJugador;
                comandaUpdate.Parameters.Add("@FechaNacimiento", System.Data.SqlDbType.Date).Value = jugador.FechaNacimiento;
                comandaUpdate.Parameters.Add("@Altura", System.Data.SqlDbType.Float).Value = jugador.Altura;
                comandaUpdate.Parameters.Add("@Dorsal", System.Data.SqlDbType.Int).Value = jugador.Dorsal;
                comandaUpdate.Parameters.Add("@TelefonoJugador", System.Data.SqlDbType.Float).Value = jugador.TelefonoJugador;
                comandaUpdate.Parameters.Add("@Posicion", System.Data.SqlDbType.NVarChar).Value = jugador.Posicion;
                comandaUpdate.Parameters.Add("@Lesion", System.Data.SqlDbType.Bit).Value = jugador.Lesion;
                comandaUpdate.Parameters.Add("@Equipo_IdEquipo", System.Data.SqlDbType.Int).Value = jugador.Equipo_IdEquipo;

                int res = comandaUpdate.ExecuteNonQuery();
                return (res == 1);
            }
            conexion.Close();
            return false;
        }

        //----------------------------------------------------------********* 
        // Metodo GET que devuelve una lista con todos los Jugadores FUNCIONA
        //----------------------------------------------------------*********
        public List<Jugador> GetJugadores(int idEquipo)
        {
            string tokenLlamada;
            tokenLlamada = System.Web.HttpContext.Current.Request.Headers["Token"];
            if (Token.BuscarTokenUsuario(tokenLlamada) == false)
            {
                return null;
            }

            List<Jugador> lista = new List<Jugador>();
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            conexion.Open();

            string sqlQuery = "SELECT IdJugador, NombreJugador, FechaNacimiento, Altura, Dorsal, TelefonoJugador, Posicion, Lesion, Equipo_IdEquipo FROM Jugador WHERE Equipo_IdEquipo = @Equipo_IdEquipo";
            SqlCommand comandaSelect = new SqlCommand(sqlQuery, conexion);
            comandaSelect.Parameters.Add("@Equipo_IdEquipo", System.Data.SqlDbType.NVarChar).Value = idEquipo;
            SqlDataReader reader = comandaSelect.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                Jugador jugador = new Jugador();

                jugador.IdJugador = reader.GetInt32(0);
                jugador.NombreJugador = reader.GetString(1);
                jugador.FechaNacimiento = reader.GetDateTime(2);
                jugador.Altura = reader.GetFloat(3);
                jugador.Dorsal = reader.GetInt32(4);
                jugador.TelefonoJugador = reader.GetDecimal(5);
                jugador.Posicion = reader.GetString(6);
                jugador.Lesion = reader.GetBoolean(7);
                jugador.Equipo_IdEquipo = reader.GetInt32(8);

                lista.Add(jugador);
            }

            reader.Close();
            conexion.Close();

            return lista;
        }

        //-----------------------------------*********
        // Metodo GET que devuelve un Jugador Funciona
        //-----------------------------------*********
        //Este metodo tiene que recibir el token de la llamada y el nombre del equipo que busca
        public Jugador GetJugador (Jugador jugador)
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

            string sqlQuery = "SELECT IdJugador, NombreJugador, FechaNacimiento, Altura, Dorsal, TelefonoJugador, Posicion, Lesion, Equipo_IdEquipo FROM Jugador WHERE IdJugador = @IdJugador";
            SqlCommand comandaSelect = new SqlCommand(sqlQuery, conexion);
            
            comandaSelect.Parameters.Add("@IdJugador", System.Data.SqlDbType.NVarChar).Value = jugador.IdJugador;
            SqlDataReader reader = comandaSelect.ExecuteReader();
            if (reader.Read())
            {
                jugador = new Jugador();

                jugador.IdJugador = reader.GetInt32(0);
                jugador.NombreJugador = reader.GetString(1);
                jugador.FechaNacimiento = reader.GetDateTime(2);
                jugador.Altura = reader.GetFloat(3);
                jugador.Dorsal = reader.GetInt32(4);
                jugador.TelefonoJugador = reader.GetDecimal(5);
                jugador.Posicion = reader.GetString(6);
                jugador.Lesion = reader.GetBoolean(7);
                jugador.Equipo_IdEquipo = reader.GetInt32(8);
            }
            reader.Close();
            conexion.Close();

            return jugador;
        }

        //-----------------------------------*********
        // Funcion para eliminar a un Jugador FUNCIONA
        //-----------------------------------*********
        public bool DeleteJugador(Jugador jugador)
        {
            SqlConnection conexion = new SqlConnection(cadenaConexion);

            conexion.Open();

            string tokenLlamada;
            tokenLlamada = System.Web.HttpContext.Current.Request.Headers["token"];

            if (Token.BuscarTokenUsuario(tokenLlamada))
            {
                string sql = "DELETE FROM Jugador WHERE IdJugador = @IdJugador ";

                SqlCommand cmd = new SqlCommand(sql, conexion);
                cmd.Parameters.Add("@IdJugador", System.Data.SqlDbType.NVarChar).Value = jugador.IdJugador;

                int res = cmd.ExecuteNonQuery();

                conexion.Close();
                return (res == 1);
            }

            conexion.Close();
            return false;
        }
    }
}