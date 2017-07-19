using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FePrototipo.Domain
{
    public class Navbar
    {
        public int Id { get; set; }
        public string nameOption { get; set; }
        public string controller { get; set; }
        public string action { get; set; }
        public string area { get; set; }
        public string imageClass { get; set; }
        public string activeli { get; set; }
        public bool estatus { get; set; }
        public int parentId { get; set; }
        public bool isParent { get; set; }
        public bool hasChild { get; set; }
    }

    public class User
    {
        public int Id { get; set; }
        public string user { get; set; }
        public string password { get; set; }
        public bool estatus { get; set; }
        public bool RememberMe { get; set; }
        public int idRol { get; set; }
    }

    public class Rol
    {
        public int Id { get; set; }
        public string nombre { get; set; }
        public bool estatus { get; set; }
    }

    public class RolMenu
    {
        public int IdRol { get; set; }
        public int IdMenu { get; set; }
    }



}