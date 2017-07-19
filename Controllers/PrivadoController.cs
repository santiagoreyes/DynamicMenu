using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FePrototipo.Controllers
{
    public class PrivadoController : BaseSecureController
    {
        // GET: Privado
       
        public ActionResult Index()
        {
            return View();
        }
    }
}