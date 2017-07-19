using FePrototipo.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FePrototipo.Controllers
{
  
    public class NavbarController : Controller
    {
        // GET: Navbar
        //[Authorize]
        public ActionResult Navbar()
        {

            if (User.Identity.IsAuthenticated)
            {
                var data = new Data();

                //var items = data.navbarItems().ToList();
                var items = data.getMenuByUserId(User.Identity.Name);

                return PartialView("_Navbar", items);

            }
            else {
                return PartialView("_Navbar", null);
            }
           
        }
    }
}