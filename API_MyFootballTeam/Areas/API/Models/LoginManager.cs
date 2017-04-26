using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;

namespace API_MyFootballTeam.Areas.API.Models
{
    public class LoginManager
    {
        public string hash;

        //Cadena de conexion
        private static string cadenaConexion = Utilidades.cadenaConexion;

        //---------------------------------------------------------------**********
        // Metodo que comprueba que el usuario existe en la base de datos FUNCIONA
        //---------------------------------------------------------------**********
        public string GetLogin(Login Item)
        {
            string usuarioExiste = "false";

            SqlConnection conexion = new SqlConnection(cadenaConexion);
            try
            {
                conexion.Open();
            }
            catch
            {
                return "No hay conexion";          
            }

            string sqlQuery = "SELECT IdUsuario, Password FROM Usuario WHERE EmailUsuario = @EmailUsuario";

            SqlCommand comanda = new SqlCommand(sqlQuery, conexion);
            
            comanda.Parameters.Add("@EmailUsuario", System.Data.SqlDbType.NVarChar).Value = Item.EmailUsuario;

            SqlDataReader reader = comanda.ExecuteReader(System.Data.CommandBehavior.CloseConnection);


            if (reader.Read())
            {
                Login login = new Login();
                login.Id = reader.GetInt32(0);
                login.Password = reader.GetString(1);

                if (Utilidades.Hasheo(Item.Password, Convert.ToString(login.Id)) == login.Password)
                {
                    // Aqui creo el token del usuario con el email, la fecha actual y el id
                    string token = Utilidades.Hasheo(login.EmailUsuario + DateTime.Now, Convert.ToString(login.Id));

                    ActualizarToken(token, login.Id);
                    
                    reader.Close();

                    return token;
                }
                
            }

            reader.Close();
            //usuarioExiste = "false";
            return usuarioExiste;
        }

        //------------------------------------------------------------------------**********
        // Funcion que introduce el token del usuario logueado en la base de datos FUNCIONA
        //------------------------------------------------------------------------**********
        public void ActualizarToken(string token, int id)
        {
            SqlConnection conexion = new SqlConnection(cadenaConexion);

            conexion.Open();

            string sqlQuery = "UPDATE Usuario SET Token = @Token WHERE IdUsuario = @IdUsuario";

            SqlCommand comanda = new SqlCommand(sqlQuery, conexion);

            comanda.Parameters.Add("@IdUsuario", System.Data.SqlDbType.Int).Value = id;
            comanda.Parameters.Add("@Token", System.Data.SqlDbType.NVarChar).Value = token;

            int res = comanda.ExecuteNonQuery();

            conexion.Close();
        }      
    }
}