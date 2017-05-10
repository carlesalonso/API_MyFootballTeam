using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace API_MyFootballTeam.Areas.API.Models
{
    public class EventoManager
    {
        //Cadena de conexion
        private static string cadenaConexion = Utilidades.cadenaConexion;

        //--------------------------------*********
        // Metodo para insertar un Evento FUNCIONA
        //--------------------------------*********
        public bool InsertEvento(Evento evento)
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

            //string sqlQuery = "INSERT INTO Evento (Minuto, NombreEvento, Partido_IdPartido, Jugador_IdJugador) " +
            //    "VALUES (@Minuto, @NombreEvento, @Partido_IdPartido, @Jugador_IdJugador)";
            string sqlQuery = "INSERT INTO Evento (NombreEvento, Partido_IdPartido) " +
                "VALUES (@NombreEvento, @Partido_IdPartido)";

            SqlCommand comandaInsert = new SqlCommand(sqlQuery, conexion);

            //Aqui sustituyo los valores que tienen el @ por los que le vamos a insertar

            //comandaInsert.Parameters.Add("@Minuto", System.Data.SqlDbType.Int).Value = evento.Minuto;
            comandaInsert.Parameters.Add("@NombreEvento", System.Data.SqlDbType.NVarChar).Value = evento.NombreEvento;
            comandaInsert.Parameters.Add("@Partido_IdPartido", System.Data.SqlDbType.Int).Value = evento.Partido_IdPartido;
            //if (evento.Jugador_IdJugador == -1)
            //{
            //    comandaInsert.Parameters.Add("@Jugador_IdJugador", System.Data.SqlDbType.Int).Value = DBNull.Value;
            //}
            //else
            //{
            //    comandaInsert.Parameters.Add("@Jugador_IdJugador", System.Data.SqlDbType.Int).Value = evento.Jugador_IdJugador;
            //}


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
        // Funcion para actualizar el Evento FUNCIONA
        //-----------------------------------*********
        //Este metodo recibe un objeto de la clase Evento, que tiene los datos del evento ya cargados
        public bool UpdateEvento(Evento evento)
        {
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            conexion.Open();

            //Miro y cojo el token que hay en el header de las peticion 
            string tokenLlamada;
            tokenLlamada = System.Web.HttpContext.Current.Request.Headers["token"];

            // Compruebo que el token existe en la base de datos
            if (Token.BuscarTokenUsuario(tokenLlamada))
            {

                string sqlQuery = "UPDATE Evento SET Minuto = @Minuto, NombreEvento = @NombreEvento, Partido_IdPartido = @Partido_IdPartido, Jugador_IdJugador = @Jugador_IdJugador WHERE IdEvento = @IdEvento";

                SqlCommand comandaUpdate = new SqlCommand(sqlQuery, conexion);

                // Aqui estoy sustituyendo cada valor que tiene antes un @ por los nuevos valores que me llegan por el objeto de la clase Usuario
                comandaUpdate.Parameters.Add("@IdEvento", System.Data.SqlDbType.Int).Value = evento.IdEvento;
                comandaUpdate.Parameters.Add("@Minuto", System.Data.SqlDbType.Int).Value = evento.Minuto;
                comandaUpdate.Parameters.Add("@NombreEvento", System.Data.SqlDbType.NVarChar).Value = evento.NombreEvento;
                comandaUpdate.Parameters.Add("@Partido_IdPartido", System.Data.SqlDbType.Int).Value = evento.Partido_IdPartido;
                if (evento.Jugador_IdJugador == -1)
                {
                    comandaUpdate.Parameters.Add("@Jugador_IdJugador", System.Data.SqlDbType.Int).Value = DBNull.Value;
                }
                else
                {
                    comandaUpdate.Parameters.Add("@Jugador_IdJugador", System.Data.SqlDbType.Int).Value = evento.Jugador_IdJugador;
                }

                int res = comandaUpdate.ExecuteNonQuery();
                return (res == 1);
            }
            conexion.Close();
            return false;
        }

        //-----------------------------------*********
        // Metodo GET que devuelve un Evento 
        //-----------------------------------*********
        //Este metodo tiene que recibir el token de la llamada y el nombre del Evento que busca
        public Evento GetEvento(Evento evento)
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

            string sqlQuery = "SELECT IdEvento, Minuto, NombreEvento, Partido_IdPartido, Jugador_IdJugador FROM Evento WHERE IdEvento = @IdEvento";
            SqlCommand comandaSelect = new SqlCommand(sqlQuery, conexion);
            comandaSelect.Parameters.Add("@IdEvento", System.Data.SqlDbType.Int).Value = evento.IdEvento;
            SqlDataReader reader = comandaSelect.ExecuteReader();
            if (reader.Read())
            {
                evento = new Evento();

                evento.IdEvento = reader.GetInt32(0);
                evento.Minuto = reader.GetInt32(1);
                evento.NombreEvento = reader.GetString(2);
                evento.Partido_IdPartido = reader.GetInt32(3);
                try
                {
                    evento.Jugador_IdJugador = reader.GetInt32(4);
                }
                catch (Exception)
                {
                    evento.Jugador_IdJugador = -1;
                }
            }
            reader.Close();
            conexion.Close();

            return evento;
        }

        //----------------------------------------------------------------------********* 
        // Metodo GET que devuelve una lista con todos los EVENTOS de un Partido 
        //----------------------------------------------------------------------*********
        public List<Evento> GetEventos(int idPartido)
        {
            string tokenLlamada;
            tokenLlamada = System.Web.HttpContext.Current.Request.Headers["Token"];
            if (Token.BuscarTokenUsuario(tokenLlamada) == false)
            {
                return null;
            }

            List<Evento> lista = new List<Evento>();
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            conexion.Open();

            string sqlQuery = "SELECT IdEvento, Minuto, NombreEvento, Partido_IdPartido, Jugador_IdJugador FROM Evento WHERE Partido_IdPartido = @Partido_IdPartido";
            SqlCommand comandaSelect = new SqlCommand(sqlQuery, conexion);
            comandaSelect.Parameters.Add("@Partido_IdPartido", System.Data.SqlDbType.Int).Value = idPartido;
            SqlDataReader reader = comandaSelect.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                Evento evento = new Evento();

                evento.IdEvento = reader.GetInt32(0);
                evento.Minuto = reader.GetInt32(1);
                evento.NombreEvento = reader.GetString(2);
                evento.Partido_IdPartido = reader.GetInt32(3);
                try
                {
                    evento.Jugador_IdJugador = reader.GetInt32(4);
                }
                catch (Exception)
                {
                    evento.Jugador_IdJugador = -1;
                }


                lista.Add(evento);
            }

            reader.Close();
            conexion.Close();

            return lista;
        }

        //-----------------------------------*********
        // Funcion para eliminar a un Evento 
        //-----------------------------------*********
        public bool DeleteEvento(Evento evento)
        {
            SqlConnection conexion = new SqlConnection(cadenaConexion);

            conexion.Open();

            string tokenLlamada;
            tokenLlamada = System.Web.HttpContext.Current.Request.Headers["token"];

            if (Token.BuscarTokenUsuario(tokenLlamada))
            {
                string sql = "DELETE FROM Evento WHERE IdEvento = @IdEvento ";

                SqlCommand cmd = new SqlCommand(sql, conexion);
                cmd.Parameters.Add("@IdEvento", System.Data.SqlDbType.NVarChar).Value = evento.IdEvento;

                int res = cmd.ExecuteNonQuery();

                conexion.Close();
                return (res == 1);
            }

            conexion.Close();
            return false;
        }
    }
}