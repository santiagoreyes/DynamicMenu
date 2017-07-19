using FePrototipo.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FEDAL;

namespace FePrototipo.Controllers
{

    [Authorize]
    public class BaseSecureController : BaseController
    {
        protected FEContext db = new FEContext();
    }
}