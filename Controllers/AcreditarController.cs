using FePrototipo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FePrototipo.Controllers
{
    public class AcreditarController : BaseSecureController
    {
        // GET: Registro
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AcreditarPruebas()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AcreditarPruebas(SolicitudPruebas solicitudPruebas)
        {
            if (ModelState.IsValid) {
                // Grabar pruebas
                return View();
            }
            return View(solicitudPruebas);
        }

        public ActionResult AcreditarProduccion()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AcreditarProduccion(SolicitudProduccion solicitudProduccion)
        {
            if (ModelState.IsValid)
            {
                // Grabar pruebas
                ViewBag.Mensaje = "Puede empezar a facturar electrónicamente.";
                return View();
            }
            return View(solicitudProduccion);
        }


    }
}