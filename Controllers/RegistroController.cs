using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FePrototipo.Controllers
{
    public class RegistroController : BaseSecureController
    {
        // GET: Registro
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RegistroDocumentoElectronico()
        {
            return View();
        }

        public ActionResult RegistroAnulacion()
        {
            return View();
        }

        public ActionResult RegistroEvento()
        {
            return View();
        }

    }
}