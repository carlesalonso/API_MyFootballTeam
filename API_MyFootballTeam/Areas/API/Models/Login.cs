using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_MyFootballTeam.Areas.API.Models
{
    public class Login
    {
        public int Id { get; set; }
        public string EmailUsuario { get; set; }
        public string Password { get; set; }  
    }
}