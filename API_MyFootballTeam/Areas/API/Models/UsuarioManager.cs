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

        //--------------------------------*********
        // Metodo para insertar un Usuario FUNCIONA
        //--------------------------------*********
        public bool InsertUsuario(Usuario usuario)
        {
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            conexion.Open();
            
            string sqlQuery = "INSERT INTO Usuario (EmailUsuario, Password, NombreUsuario, ApellidoUsuario, TelefonoUsuario, NIF, IBAN) " +
                "VALUES (@EmailUsuario, @Password, @NombreUsuario, @ApellidoUsuario, @TelefonoUsuario, @NIF, @IBAN)";

            SqlCommand comandaInsert = new SqlCommand(sqlQuery, conexion);
            SqlCommand comandaBuscarId= new SqlCommand(sqlQuery, conexion);
            SqlCommand comandaUpdateHash = new SqlCommand(sqlQuery, conexion);

            // Con esta comanda hago el insert del nuevo usuario
            //Segun rafa no hace falta poner el token porque no es un campo 
            comandaInsert.Parameters.Add("@EmailUsuario", System.Data.SqlDbType.NVarChar).Value = usuario.EmailUsuario;
            comandaInsert.Parameters.Add("@Password", System.Data.SqlDbType.NVarChar).Value = usuario.Password;
            comandaInsert.Parameters.Add("@NombreUsuario", System.Data.SqlDbType.NVarChar).Value = usuario.NombreUsuario;
            comandaInsert.Parameters.Add("@ApellidoUsuario", System.Data.SqlDbType.NVarChar).Value = usuario.ApellidoUsuario;
            comandaInsert.Parameters.Add("@TelefonoUsuario", System.Data.SqlDbType.NVarChar).Value = usuario.TelefonoUsuario;
            comandaInsert.Parameters.Add("@NIF", System.Data.SqlDbType.NVarChar).Value = usuario.NIF;
            comandaInsert.Parameters.Add("@IBAN", System.Data.SqlDbType.NVarChar).Value = usuario.IBAN;
            
            int res = comandaInsert.ExecuteNonQuery();
            if (res == 0)
            {
                return false;
            }

            // Buscamos la id que hemos generado con este usuario y la hasheamos
            sqlQuery = "SELECT IdUsuario FROM Usuario WHERE EmailUsuario = @EmailUsuario";
            comandaBuscarId = new SqlCommand(sqlQuery, conexion);
            comandaBuscarId.Parameters.Add("@EmailUsuario", System.Data.SqlDbType.NVarChar).Value = usuario.EmailUsuario;
            SqlDataReader reader = comandaBuscarId.ExecuteReader();

            int Id;
            if (reader.Read())
            {
                Id = reader.GetInt32(0);
                hash = Utilidades.Hasheo(usuario.Password, Convert.ToString(Id));
            }
            reader.Close();

            // Actualizo la contraseña con el password hasheado
            sqlQuery = "UPDATE Usuario SET  Password = @Password WHERE EmailUsuario = @EmailUsuario";
            comandaUpdateHash= new SqlCommand(sqlQuery, conexion);

            comandaUpdateHash.Parameters.Add("@EmailUsuario", System.Data.SqlDbType.NVarChar).Value = usuario.EmailUsuario;
            comandaUpdateHash.Parameters.Add("@Password", System.Data.SqlDbType.NVarChar).Value = hash;

            res = comandaUpdateHash.ExecuteNonQuery();
            if (res == 0)
            {
                return false;
            }
            conexion.Close();
            
            return true;
        }
            
        //-----------------------------------**********
        // Funcion para actualizar el usuario FUNCIONA
        //-----------------------------------**********
        //Este metodo recibe un objeto de la clase usuario, que tiene los datos del usuario ya cargados
        public bool UpdateUsuario(Usuario usuario)
        {
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            conexion.Open();

            //Miro y cojo el token que hay en el header de las peticion 
            string tokenLlamada;
            tokenLlamada = System.Web.HttpContext.Current.Request.Headers["token"];

            // Compruebo que el token existe en la base de datos
            if (Token.BuscarTokenUsuario(tokenLlamada))
            {
                // vuelvo a hashear la contraseña con el password y la id
                hash = Utilidades.Hasheo(usuario.Password, Convert.ToString(usuario.IdUsuario));
                string sqlQuery = "UPDATE Usuario SET EmailUsuario = @EmailUsuario, Password = @Password, NombreUsuario = @NombreUsuario, ApellidoUsuario = @ApellidoUsuario, TelefonoUsuario = @TelefonoUsuario, NIF = @NIF, IBAN = @IBAN WHERE Token = @Token";

                SqlCommand comandaUpdate = new SqlCommand(sqlQuery, conexion);

                // Aqui estoy sustituyendo cada valor que tiene antes un @ por los nuevos valores que me llegan por el objeto de la clase Usuario
                comandaUpdate.Parameters.Add("@EmailUsuario", System.Data.SqlDbType.NVarChar).Value = usuario.EmailUsuario;
                comandaUpdate.Parameters.Add("@Password", System.Data.SqlDbType.NVarChar).Value = hash;
                comandaUpdate.Parameters.Add("@NombreUsuario", System.Data.SqlDbType.NVarChar).Value = usuario.NombreUsuario;
                comandaUpdate.Parameters.Add("@ApellidoUsuario", System.Data.SqlDbType.NVarChar).Value = usuario.ApellidoUsuario;
                comandaUpdate.Parameters.Add("@TelefonoUsuario", System.Data.SqlDbType.NVarChar).Value = usuario.TelefonoUsuario;
                comandaUpdate.Parameters.Add("@NIF", System.Data.SqlDbType.NVarChar).Value = usuario.NIF;
                comandaUpdate.Parameters.Add("@IBAN", System.Data.SqlDbType.NVarChar).Value = usuario.IBAN;
                comandaUpdate.Parameters.Add("@Token", System.Data.SqlDbType.NVarChar).Value = tokenLlamada;

                int res = comandaUpdate.ExecuteNonQuery();
                return (res == 1);
            }
            conexion.Close();
            return false;
        }

        //-----------------------------------*********
        // Metodo GET que devuelve un usuario FUNCIONA
        //-----------------------------------*********
        public Usuario GetUsuario(Usuario usuario)
        {
            // 1
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            conexion.Open();

            // 2
            //Miro y cojo el token que hay en el header de las peticion 
            string tokenLlamada;
            tokenLlamada = System.Web.HttpContext.Current.Request.Headers["token"];      

            // 3
            if (!(Token.BuscarTokenUsuario(tokenLlamada)))
            {
                return null;
            }

            // 4
            string sqlQuery = "SELECT IdUsuario, EmailUsuario, Password, NombreUsuario, ApellidoUsuario, TelefonoUsuario, NIF, IBAN, Token " +
                "FROM Usuario WHERE Token = @Token";
            SqlCommand comandaSelect = new SqlCommand(sqlQuery, conexion);
            comandaSelect.Parameters.Add("@Token", System.Data.SqlDbType.NVarChar).Value = tokenLlamada;
            SqlDataReader reader = comandaSelect.ExecuteReader();

            // 5
            if (reader.Read())
            {
                
                usuario = new Usuario();
                usuario.IdUsuario = reader.GetInt32(0);
                usuario.EmailUsuario = reader.GetString(1);
                usuario.Password = reader.GetString(2);
                usuario.NombreUsuario = reader.GetString(3);
                usuario.ApellidoUsuario = reader.GetString(4);
                usuario.TelefonoUsuario = reader.GetDecimal(5);
                usuario.NIF = reader.GetString(6);
                usuario.IBAN = reader.GetString(7);
                usuario.Token = reader.GetString(8);
            }
            reader.Close();
            conexion.Close();

            return usuario;
        }

        //---------------------------------------------------------**********
        // Metodo GET que devuelve una lista con todos los USUARIOS FUNCIONA
        //---------------------------------------------------------**********
        public List<Usuario> GetUsuarios()
        {

            string tokenLlamada;
            tokenLlamada = System.Web.HttpContext.Current.Request.Headers["Token"];

            if (Token.BuscarTokenUsuario(tokenLlamada)== false)
            {
                return null;
            }

            List<Usuario> lista = new List<Usuario>();

            SqlConnection conexion = new SqlConnection(cadenaConexion);

            conexion.Open();

            string sqlQuery = "SELECT IdUsuario, EmailUsuario, Password, NombreUsuario, ApellidoUsuario, TelefonoUsuario, NIF, IBAN, Token FROM Usuario ";

            SqlCommand comandaSelect = new SqlCommand(sqlQuery, conexion);

            SqlDataReader reader = comandaSelect.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

            while (reader.Read())
            {
                Usuario usuario = new Usuario();

                //Usuario usuario;
                usuario.IdUsuario = reader.GetInt32(0);
                usuario.EmailUsuario = reader.GetString(1);
                usuario.Password = reader.GetString(2);
                usuario.NombreUsuario = reader.GetString(3);
                usuario.ApellidoUsuario = reader.GetString(4);
                usuario.TelefonoUsuario = reader.GetDecimal(5);
                usuario.NIF = reader.GetString(6);
                usuario.IBAN = reader.GetString(7);
                //usuario.Token = reader.GetString(8);

                lista.Add(usuario);
            }

            reader.Close();

            conexion.Close();
            return lista;
        }

        //------------------------------------********
        // Funcion para eliminar a un USUARIO FUNCIONA
        //------------------------------------********
        public bool DeleteUsuario(Usuario usuario)
        {
            SqlConnection conexion = new SqlConnection(cadenaConexion);

            conexion.Open();

            string tokenLlamada;
            tokenLlamada = System.Web.HttpContext.Current.Request.Headers["token"];

            if (Token.BuscarTokenUsuario(tokenLlamada))
            {

                string sql = "DELETE FROM Usuario WHERE Token = @Token";

                SqlCommand cmd = new SqlCommand(sql, conexion);

                cmd.Parameters.Add("@Token", System.Data.SqlDbType.NVarChar).Value = tokenLlamada;

                int res = cmd.ExecuteNonQuery();

                conexion.Close();
                return (res == 1);
            }

            conexion.Close();

            return false;
        }
    }
}