using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace API_MyFootballTeam.Areas.API
{
    public class APIAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "API";            
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            //-------------------------------------------------
            //--------------------- Usuario -------------------
            //-------------------------------------------------

            // Esta ruta lleva a los metodos que tratan un elemento
            // y necesitan un valor para trabajar 
            context.MapRoute(
                "APIUsuario",
                "API/Usuarios/Usuario/{id}",
                new
                {
                    controller = "Usuarios",
                    action = "Usuario",
                    id = UrlParameter.Optional
                }
            );

            //Esta ruta lleva al metodo que devuelve todos los usuarios
            context.MapRoute(
                "APIUsuarios",
                "API/Usuarios",
                new
                {
                    controller = "Usuarios",
                    action = "Usuarios"
                }
            );

            //-------------------------------------------------
            //--------------------- Login ---------------------
            //-------------------------------------------------

            context.MapRoute(
                "APILogin",
                //"API/Login/{EmailUsuario}/{Password}",
                "API/Login/{id}",
                new
                {
                    controller = "Logins",
                    action = "Login",
                    id = UrlParameter.Optional,
                    //EmailUsuario = UrlParameter.Optional,
                    //Password = UrlParameter.Optional
                }
            );

            //context.MapRoute(
            //    "API_default",
            //    "API/{controller}/{action}/{id}",
            //    new { action = "Index", id = UrlParameter.Optional }
            //);

            //-------------------------------------------------
            //--------------------- Equipo -------------------
            //-------------------------------------------------

            //Esta ruta lleva al los metodos que necesitan un id o que solo hace una accion
            context.MapRoute(
                "APIEquipo",
                "API/Equipos/Equipo/{id}",
                new
                {
                    controller = "Equipos",
                    action = "Equipo",
                    id = UrlParameter.Optional
                }
            );

            //Esta ruta lleva al metodo que devuelve todos los usuarios
            context.MapRoute(
                "APIEquipos",
                "API/Equipos",
                new
                {
                    controller = "Equipos",
                    action = "Equipos"
                }
            );

            //---------------------------------------------------
            //--------------------- Ejercicio -------------------
            //---------------------------------------------------

            //Esta ruta lleva al los metodos que necesitan un id o que solo hace una accion
            context.MapRoute(
                "APIEjercicio",
                "API/Ejercicios/Ejercicio/{id}",
                new
                {
                    controller = "Ejercicios",
                    action = "Ejercicio",
                    id = UrlParameter.Optional
                }
            );

            //Esta ruta lleva al metodo que devuelve todos los usuarios
            context.MapRoute(
                "APIEjercicios",
                "API/Ejercicios",
                new
                {
                    controller = "Ejercicios",
                    action = "Ejercicios"
                }
            );

            //-------------------------------------------------
            //--------------------- Jugador -------------------
            //-------------------------------------------------

            //Esta ruta lleva al los metodos que necesitan un id o que solo hace una accion
            context.MapRoute(
                "APIJugador",
                "API/Jugadores/Jugador/{id}",
                new
                {
                    controller = "Jugadores",
                    action = "Jugador",
                    id = UrlParameter.Optional
                }
            );

            //Esta ruta lleva al metodo que devuelve todos los usuarios
            context.MapRoute(
                "APIJugadores",
                "API/Jugadores",
                new
                {
                    controller = "Jugadores",
                    action = "Jugadores"
                }
            );

            //-------------------------------------------------
            //--------------------- Partido -------------------
            //-------------------------------------------------

            //Esta ruta lleva al los metodos que necesitan un id o que solo hace una accion
            context.MapRoute(
                "APIPartido",
                "API/Partidos/Partido/{id}",
                new
                {
                    controller = "Partidos",
                    action = "Partido",
                    id = UrlParameter.Optional
                }
            );

            //Esta ruta lleva al metodo que devuelve todos los usuarios
            context.MapRoute(
                "APIPartidos",
                "API/Partidos",
                new
                {
                    controller = "Partidos",
                    action = "Partidos"
                }
            );

            //-------------------------------------------------
            //--------------------- Evento --------------------
            //-------------------------------------------------

            //Esta ruta lleva al los metodos que necesitan un id o que solo hace una accion
            context.MapRoute(
                "APIEvento",
                "API/Eventos/Evento/{id}",
                new
                {
                    controller = "Eventos",
                    action = "Evento",
                    id = UrlParameter.Optional
                }
            );

            //Esta ruta lleva al metodo que devuelve todos los usuarios
            context.MapRoute(
                "APIEventos",
                "API/Eventos",
                new
                {
                    controller = "Eventos",
                    action = "Eventos"
                }
            );
        }
    }
}