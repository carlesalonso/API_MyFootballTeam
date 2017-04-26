using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace API_MyFootballTeam.Areas.API.Models
{
    public class EjercicioManager
    {
        //Cadena de conexion
        private static string cadenaConexion = Utilidades.cadenaConexion;

        //----------------------------------*********
        // Metodo para insertar un Ejercicio FUNCIONA
        //----------------------------------*********
        public bool InsertEjercicio(Ejercicio ejercicio)
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

            //Busco el id del usuario que tenga el token que me han enviado
            int idUsuario;
            string sqlQuery = "SELECT IdUsuario FROM Usuario WHERE Token = @Token";
            SqlCommand comandaBuscarId = new SqlCommand(sqlQuery, conexion);
            comandaBuscarId.Parameters.Add("@Token", System.Data.SqlDbType.NVarChar).Value = tokenLlamada; 
            SqlDataReader reader = comandaBuscarId.ExecuteReader();
            if (reader.Read())
            {
                idUsuario = reader.GetInt32(0);
            }
            else
            {
                return false;
            }
            reader.Close();

            sqlQuery = "INSERT INTO Ejercicio (NombreEjercicio, Descripcion, Foto, Usuario_IdUsuario) " +
                "VALUES (@NombreEjercicio, @Descripcion, @Foto, @Usuario_IdUsuario)";

            SqlCommand comandaInsert = new SqlCommand(sqlQuery, conexion);

            //Aqui sustituyo los valores que tienen el @ por los que le vamos a insertar
            //Segun rafa no hace falta poner el token porque no es un campo 
            comandaInsert.Parameters.Add("@NombreEjercicio", System.Data.SqlDbType.NVarChar).Value = ejercicio.NombreEjercicio;
            comandaInsert.Parameters.Add("@Descripcion", System.Data.SqlDbType.NVarChar).Value = ejercicio.Descripcion;
            comandaInsert.Parameters.Add("@Foto", System.Data.SqlDbType.NVarChar).Value = ejercicio.Foto;
            comandaInsert.Parameters.Add("@Usuario_IdUsuario", System.Data.SqlDbType.Int).Value = idUsuario;

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

        //-------------------------------------*********
        // Funcion para actualizar el Ejercicio FUNCIONA 
        //-------------------------------------*********
        //Este metodo recibe un objeto de la clase ejercicio, que tiene los datos del ejercicio ya cargados
        public bool UpdateEjercicio(Ejercicio ejercicio)
        {
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            conexion.Open();

            //Miro y cojo el token que hay en el header de las peticion 
            string tokenLlamada;
            tokenLlamada = System.Web.HttpContext.Current.Request.Headers["token"];

            // Compruebo que el token existe en la base de datos
            if (Token.BuscarTokenUsuario(tokenLlamada))
            {
                //Busco el id del usuario que tenga el token que me han enviado
                int idUsuario;
                string sqlQuery = "SELECT IdUsuario FROM Usuario WHERE Token = @Token";
                SqlCommand comandaBuscarId = new SqlCommand(sqlQuery, conexion);
                comandaBuscarId.Parameters.Add("@Token", System.Data.SqlDbType.NVarChar).Value = tokenLlamada;

                SqlDataReader reader = comandaBuscarId.ExecuteReader();

                if (reader.Read())
                {
                    idUsuario = reader.GetInt32(0);
                }
                else
                {
                    return false;
                }
                reader.Close();

                sqlQuery = "UPDATE Ejercicio SET NombreEjercicio = @NombreEjercicio, Descripcion = @Descripcion, Foto = @Foto WHERE IdEjercicio = @IdEjercicio";

                SqlCommand comandaUpdate = new SqlCommand(sqlQuery, conexion);

                // Aqui estoy sustituyendo cada valor que tiene antes un @ por los nuevos valores que me llegan por el objeto de la clase Usuario
                comandaUpdate.Parameters.Add("@IdEjercicio", System.Data.SqlDbType.NVarChar).Value = ejercicio.IdEjercicio;
                comandaUpdate.Parameters.Add("@NombreEjercicio", System.Data.SqlDbType.NVarChar).Value = ejercicio.NombreEjercicio;
                comandaUpdate.Parameters.Add("@Descripcion", System.Data.SqlDbType.NVarChar).Value = ejercicio.Descripcion;
                comandaUpdate.Parameters.Add("@Foto", System.Data.SqlDbType.NVarChar).Value = ejercicio.Foto;
                

                int res = comandaUpdate.ExecuteNonQuery();
                return (res == 1);
            }
            conexion.Close();
            return false;
        }

        //-------------------------------------*********
        // Metodo GET que devuelve un Ejercicio FUNCIONA
        //-------------------------------------*********
        //Este metodo tiene que recibir el token de la llamada y el ID del Ejercicio que busca
        public Ejercicio GetEjercicio(Ejercicio ejercicio)
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

            //Busco el id del usuario que tenga el token que me han enviado
            int idUsuario;
            string sqlQuery = "SELECT IdUsuario FROM Usuario WHERE Token = @Token";
            SqlCommand comandaBuscarId = new SqlCommand(sqlQuery, conexion);
            comandaBuscarId.Parameters.Add("@Token", System.Data.SqlDbType.NVarChar).Value = tokenLlamada;

            SqlDataReader reader = comandaBuscarId.ExecuteReader();

            if (reader.Read())
            {
                idUsuario = reader.GetInt32(0);
            }
            else
            {
                return null;
            }
            reader.Close();

            sqlQuery = "SELECT IdEjercicio, NombreEjercicio, Descripcion, Foto, Usuario_IdUsuario FROM Ejercicio WHERE IdEjercicio = @IdEjercicio AND Usuario_IdUsuario = @Usuario_IdUsuario";
            SqlCommand comandaSelect = new SqlCommand(sqlQuery, conexion);
            comandaSelect.Parameters.Add("@Usuario_IdUsuario", System.Data.SqlDbType.NVarChar).Value = idUsuario;
            comandaSelect.Parameters.Add("@IdEjercicio", System.Data.SqlDbType.Int).Value = ejercicio.IdEjercicio;
            reader = comandaSelect.ExecuteReader();
            if (reader.Read())
            {
                ejercicio = new Ejercicio();

                ejercicio.IdEjercicio = reader.GetInt32(0);
                ejercicio.NombreEjercicio = reader.GetString(1);
                ejercicio.Descripcion = reader.GetString(2);
                ejercicio.Foto = reader.GetString(3);
                ejercicio.Usuario_IdUsuario = reader.GetInt32(4);
            }
            reader.Close();
            conexion.Close();

            return ejercicio;
        }

        //-----------------------------------------------------------*********
        // Metodo GET que devuelve una lista con todos los Ejercicios FUNCIONA
        //-----------------------------------------------------------*********
        public List<Ejercicio> GetEjercicios()
        {
            string tokenLlamada;
            tokenLlamada = System.Web.HttpContext.Current.Request.Headers["Token"];
            if (Token.BuscarTokenUsuario(tokenLlamada) == false)
            {
                return null;
            }

            List<Ejercicio> lista = new List<Ejercicio>();
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            conexion.Open();

            //Busco el id del usuario que tenga el token que me han enviado
            int idUsuario;
            string sqlQuery = "SELECT IdUsuario FROM Usuario WHERE Token = @Token";
            SqlCommand comandaBuscarId = new SqlCommand(sqlQuery, conexion);
            comandaBuscarId.Parameters.Add("@Token", System.Data.SqlDbType.NVarChar).Value = tokenLlamada;

            SqlDataReader reader = comandaBuscarId.ExecuteReader();

            if (reader.Read())
            {
                idUsuario = reader.GetInt32(0);
            }
            else
            {
                return null;
            }
            reader.Close();

            sqlQuery = "SELECT IdEjercicio, NombreEjercicio, Descripcion, Foto, Usuario_IdUsuario FROM Ejercicio WHERE Usuario_IdUsuario = @Usuario_IdUsuario";
            SqlCommand comandaSelect = new SqlCommand(sqlQuery, conexion);
            comandaSelect.Parameters.Add("@Usuario_IdUsuario", System.Data.SqlDbType.NVarChar).Value = idUsuario;
            reader = comandaSelect.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                Ejercicio ejercicio = new Ejercicio();

                ejercicio.IdEjercicio = reader.GetInt32(0);
                ejercicio.NombreEjercicio = reader.GetString(1);
                ejercicio.Descripcion = reader.GetString(2);
                ejercicio.Foto = reader.GetString(3);
                ejercicio.Usuario_IdUsuario = reader.GetInt32(4);
                

                lista.Add(ejercicio);
            }

            reader.Close();

            conexion.Close();
            return lista;
        }

        //-------------------------------------*********
        // Funcion para eliminar a un Ejercicio FUNCIONA
        //-------------------------------------*********
        public bool DeleteEjercicio(Ejercicio ejercicio)
        {
            SqlConnection conexion = new SqlConnection(cadenaConexion);

            conexion.Open();

            string tokenLlamada;
            tokenLlamada = System.Web.HttpContext.Current.Request.Headers["token"];

            if (Token.BuscarTokenUsuario(tokenLlamada))
            {
                string sql = "DELETE FROM Ejercicio WHERE IdEjercicio = @IdEjercicio ";

                SqlCommand cmd = new SqlCommand(sql, conexion);
                cmd.Parameters.Add("@IdEjercicio", System.Data.SqlDbType.NVarChar).Value = ejercicio.IdEjercicio;

                int res = cmd.ExecuteNonQuery();

                conexion.Close();
                return (res == 1);
            }

            conexion.Close();
            return false;
        }

    }
}