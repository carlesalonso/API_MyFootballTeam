using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace API_MyFootballTeam.Areas.API.Models
{
    public class EquipoManager
    {
        //Cadena de conexion
        private static string cadenaConexion = Utilidades.cadenaConexion;
        //private static string cadenaConexion = Utilidades.cadenaConexion;


        //-------------------------------**********
        // Metodo para insertar un Equipo Funciona
        //-------------------------------**********
        public bool InsertEquipo(Equipo equipo)
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
            

            sqlQuery = "INSERT INTO Equipo (NombreEquipo, Direccion, Categoria, Usuario_IdUsuario) VALUES (@NombreEquipo, @Direccion, @Categoria, @Usuario_IdUsuario)";

            SqlCommand comandaInsert = new SqlCommand(sqlQuery, conexion);


            //Aqui sustituyo los valores que tienen el @ por los que le vamos a insertar
            //Segun rafa no hace falta poner el token porque no es un campo 
            comandaInsert.Parameters.Add("@NombreEquipo", System.Data.SqlDbType.NVarChar).Value = equipo.NombreEquipo;
            comandaInsert.Parameters.Add("@Direccion", System.Data.SqlDbType.NVarChar).Value = equipo.Direccion;
            comandaInsert.Parameters.Add("@Categoria", System.Data.SqlDbType.NVarChar).Value = equipo.Categoria;
            //if (equipo.FotoEscudo == null)
            //{
            //    comandaInsert.Parameters.Add("@FotoEscudo", System.Data.SqlDbType.NVarChar).Value = DBNull.Value;
            //}
            //else
            //{
            //    comandaInsert.Parameters.Add("@FotoEscudo", System.Data.SqlDbType.NVarChar).Value = equipo.FotoEscudo;
            //}
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

        //----------------------------------**********
        // Funcion para actualizar el EQUIPO FUNCIONA
        //----------------------------------**********
        //Este metodo recibe un objeto de la clase usuario, que tiene los datos del usuario ya cargados
        public bool UpdateEquipo(Equipo equipo)
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

                sqlQuery = "UPDATE Equipo SET NombreEquipo = @NombreEquipo, Direccion = @Direccion, Categoria = @Categoria, FotoEscudo = @FotoEscudo WHERE Usuario_IdUsuario = @idUsuario AND IdEquipo = @IdEquipo";

                SqlCommand comandaUpdate = new SqlCommand(sqlQuery, conexion);

                // Aqui estoy sustituyendo cada valor que tiene antes un @ por los nuevos valores que me llegan por el objeto de la clase Usuario
                comandaUpdate.Parameters.Add("@IdEquipo", System.Data.SqlDbType.NVarChar).Value = equipo.IdEquipo;
                comandaUpdate.Parameters.Add("@NombreEquipo", System.Data.SqlDbType.NVarChar).Value = equipo.NombreEquipo;
                comandaUpdate.Parameters.Add("@Direccion", System.Data.SqlDbType.NVarChar).Value = equipo.Direccion;
                comandaUpdate.Parameters.Add("@Categoria", System.Data.SqlDbType.NVarChar).Value = equipo.Categoria;
                if (equipo.FotoEscudo == null)
                {
                    comandaUpdate.Parameters.Add("@FotoEscudo", System.Data.SqlDbType.NVarChar).Value = DBNull.Value;
                }
                else
                {
                    comandaUpdate.Parameters.Add("@FotoEscudo", System.Data.SqlDbType.NVarChar).Value = equipo.FotoEscudo;
                }
                comandaUpdate.Parameters.Add("@idUsuario", System.Data.SqlDbType.NVarChar).Value = idUsuario;

                int res = comandaUpdate.ExecuteNonQuery();
                return (res == 1);
            }
            conexion.Close();
            return false;
        }


        //-----------------------------------*********
        // Metodo GET que devuelve un Equipo  FUNCIONA
        //-----------------------------------*********
        //Este metodo tiene que recibir el token de la llamada y el ID del equipo que busca
        public Equipo GetEquipo(Equipo equipo)
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

            sqlQuery = "SELECT IdEquipo, NombreEquipo, Direccion, Categoria, FotoEscudo, Usuario_IdUsuario FROM Equipo WHERE IdEquipo = @IdEquipo AND Usuario_IdUsuario = @Usuario_IdUsuario";
            SqlCommand comandaSelect = new SqlCommand(sqlQuery, conexion);
            comandaSelect.Parameters.Add("@IdEquipo", System.Data.SqlDbType.NVarChar).Value = equipo.IdEquipo;
            comandaSelect.Parameters.Add("@Usuario_IdUsuario", System.Data.SqlDbType.NVarChar).Value = idUsuario;
            reader = comandaSelect.ExecuteReader();
            if (reader.Read())
            {
                equipo = new Equipo();
    
                equipo.IdEquipo = reader.GetInt32(0);
                equipo.NombreEquipo = reader.GetString(1);
                equipo.Direccion = reader.GetString(2);
                equipo.Categoria = reader.GetString(3);
                try
                {
                    equipo.FotoEscudo = reader.GetString(4);
                }
                catch (Exception)
                {
                    equipo.FotoEscudo = null;
                }
                equipo.Usuario_IdUsuario = reader.GetInt32(5);
            }
            reader.Close();
            conexion.Close();

            return equipo;
        }

        //--------------------------------------------------------********
        // Metodo GET que devuelve una lista con todos los Equipos FUNCIONA
        //--------------------------------------------------------********
        public List<Equipo> GetEquipos()
        {
            string tokenLlamada;
            tokenLlamada = System.Web.HttpContext.Current.Request.Headers["Token"];
            if (Token.BuscarTokenUsuario(tokenLlamada) == false)
            {
                return null;
            }

            List<Equipo> lista = new List<Equipo>();
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

            sqlQuery = "SELECT IdEquipo, NombreEquipo, Direccion, Categoria, FotoEscudo, Usuario_IdUsuario FROM Equipo WHERE Usuario_IdUsuario = @Usuario_IdUsuario";
            SqlCommand comandaSelect = new SqlCommand(sqlQuery, conexion);
            comandaSelect.Parameters.Add("@Usuario_IdUsuario", System.Data.SqlDbType.NVarChar).Value = idUsuario;
            reader = comandaSelect.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            while (reader.Read())
            {
                Equipo equipo = new Equipo();

                equipo.IdEquipo = reader.GetInt32(0);
                equipo.NombreEquipo = reader.GetString(1);
                equipo.Direccion = reader.GetString(2);
                equipo.Categoria = reader.GetString(3);
                try
                {
                    equipo.FotoEscudo = reader.GetString(4);
                }
                catch (Exception)
                {
                    equipo.FotoEscudo = null;
                }
                equipo.Usuario_IdUsuario = reader.GetInt32(5);

                lista.Add(equipo);
            }

            reader.Close();

            conexion.Close();
            return lista;
        }

        //----------------------------------********
        // Funcion para eliminar a un EQUIPO Funciona
        //----------------------------------********
        public bool DeleteEquipo(Equipo equipo)
        {
            SqlConnection conexion = new SqlConnection(cadenaConexion);

            conexion.Open();

            string tokenLlamada;
            tokenLlamada = System.Web.HttpContext.Current.Request.Headers["token"];

            if (Token.BuscarTokenUsuario(tokenLlamada))
            {
                string sql = "DELETE FROM Equipo WHERE IdEquipo = @IdEquipo ";

                SqlCommand cmd = new SqlCommand(sql, conexion);
                cmd.Parameters.Add("@IdEquipo", System.Data.SqlDbType.NVarChar).Value = equipo.IdEquipo;

                int res = cmd.ExecuteNonQuery();

                conexion.Close();
                return (res == 1);
            }

            conexion.Close();
            return false;
        }
    }
}