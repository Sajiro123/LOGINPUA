using LN.EntidadesLN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ENTIDADES;
using LOGINPUA.Models;
using AutoMapper;
using LOGINPUA.Util.Seguridad;
using ENTIDADES.InnerJoin;

namespace LOGINPUA.Controllers
{
    public class UsuariosController : Controller
    {
        //private readonly LoginLN _LoginLN;
        //public UsuariosController()
        //{
        //    _LoginLN = new LoginLN();

        //}

        public ActionResult Login(string m, string u)
        {

            ViewBag.mensaje = m;
            ViewBag.usuario = u;
            return View();
        }
      
    }
}