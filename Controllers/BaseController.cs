using FePrototipo.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FePrototipo.Controllers
{

    [Authorize]
    public class BaseController : Controller
    {
        protected int pageSize = 4;

    }
}