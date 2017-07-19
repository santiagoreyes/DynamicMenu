using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FePrototipo.Domain
{
    public class Data
    {

        public IEnumerable<Navbar> getMenuByUserId(string puser) {
            var menu = new List<Navbar>();

            if (puser == "admin") return navbarItems();

            User user = getUsers().Where(x => x.user==puser).ElementAt(0);
            Rol rol = getRoles().Where(x => x.Id == user.idRol).ElementAt(0);
            RolMenu rolMenu = getRolMenus().Where(x => x.IdRol == rol.Id).ElementAt(0);
            var allMenu = navbarItems();
            // Agregar padre
            foreach (Navbar item in allMenu) {
                if (item.Id == rolMenu.IdMenu) {
                    menu.Add(item);
                }
            }
            // Agregar hijos
            foreach (Navbar item in allMenu)
            {
                if (item.parentId == rolMenu.IdMenu)
                {
                    menu.Add(item);
                }
            }

            return menu.ToList();

        }
        public IEnumerable<Navbar> navbarItems()
        {
            var menu = new List<Navbar>();

            menu.Add(new Navbar { Id = 5, parentId = 0, nameOption = "Afiliarse", controller = "Acreditar", action = "Index", imageClass = "fa fa-fw fa-dashboard", estatus = true, isParent = true });
            menu.Add(new Navbar { Id = 51, parentId = 5, nameOption = "Ambiente Pruebas", controller = "Acreditar", action = "AcreditarPruebas", imageClass = "fa fa-fw fa-bar-chart-o", estatus = true, isParent = false });
            menu.Add(new Navbar { Id = 52, parentId = 5, nameOption = "Ambiente Producción", controller = "Acreditar", action = "AcreditarProduccion", imageClass = "fa fa-fw fa-bar-chart-o", estatus = true, isParent = false });

            menu.Add(new Navbar { Id = 3, parentId = 0, nameOption = "Captura Datos", controller = "Privado", action = "Index", imageClass = "fa fa-fw fa-dashboard", estatus = true, isParent = true });
            menu.Add(new Navbar { Id = 31, parentId = 3, nameOption = "Documento Electronico", controller = "Registro", action = "RegistroDocumentoElectronico", imageClass = "fa fa-fw fa-bar-chart-o", estatus = true, isParent = false });
            menu.Add(new Navbar { Id = 32, parentId = 3, nameOption = "Anulacion", controller = "Registro", action = "RegistroAnulacion", imageClass = "fa fa-fw fa-bar-chart-o", estatus = true, isParent = false });
            menu.Add(new Navbar { Id = 33, parentId = 3, nameOption = "Evento", controller = "Registro", action = "RegistroEvento", imageClass = "fa fa-fw fa-bar-chart-o", estatus = true, isParent = false });


            menu.Add(new Navbar { Id = 1, parentId = 0, nameOption = "Consultas Operativas", controller = "Privado", action = "Index", imageClass = "fa fa-fw fa-dashboard", estatus = true, isParent = true });
            menu.Add(new Navbar { Id = 11, parentId = 1, nameOption = "Consulta Buzon", controller = "Consultas", action = "ConsultaBuzon", imageClass = "fa fa-fw fa-bar-chart-o", estatus = true, isParent = false});
            menu.Add(new Navbar { Id = 12, parentId = 1, nameOption = "Consulta Facturas Autorizadas", controller = "Consultas", action = "ConsultaFacturasAutorizadas", imageClass = "fa fa-fw fa-bar-chart-o", estatus = true, isParent = false });

            menu.Add(new Navbar { Id = 2, parentId = 0, nameOption = "Consultas Control", controller = "Privado", action = "Index", imageClass = "fa fa-fw fa-dashboard", estatus = true, isParent = true });
            menu.Add(new Navbar { Id = 21, parentId = 2, nameOption = "Consulta por Contribuyente", controller = "Consultas", action = "ConsultaPorContribuyente", imageClass = "fa fa-fw fa-bar-chart-o", estatus = true, isParent = false });
            

            menu.Add(new Navbar { Id = 4, parentId = 0, nameOption = "Consultas Participantes", controller = "Home", action = "Index", imageClass = "fa fa-fw fa-dashboard", estatus = true, isParent = true });
            menu.Add(new Navbar { Id = 41, parentId = 4, nameOption = "Facturas Emitidas", controller = "Consultas", action = "ConsultaFacturasEmitidas", imageClass = "fa fa-fw fa-bar-chart-o", estatus = true, isParent = false });
            menu.Add(new Navbar { Id = 42, parentId = 4, nameOption = "Facturas Recibidas", controller = "Consultas", action = "ConsultaFacturasRecibidas", imageClass = "fa fa-fw fa-bar-chart-o", estatus = true, isParent = false });


            return menu.ToList();
        }


        public IEnumerable<Rol> getRoles()
        {
            var roles = new List<Rol>();
            roles.Add(new Rol { Id = 1, nombre = "operador", estatus = true });
            roles.Add(new Rol { Id = 2, nombre = "fiscalizador", estatus = true });
            roles.Add(new Rol { Id = 3, nombre = "emisor", estatus = true });
            roles.Add(new Rol { Id = 4, nombre = "participante", estatus = true });
            roles.Add(new Rol { Id = 5, nombre = "contribuyente", estatus = true });

            return roles.ToList();
        }

        public IEnumerable<User> getUsers()
        {
            var users = new List<User>();
            users.Add(new User { Id = 0, user = "admin", password = "12345", estatus = true, RememberMe = false, idRol = 0 });
            users.Add(new User { Id = 1, user = "4-444-444", password = "12345", estatus = true, RememberMe = false, idRol = 1 });
            users.Add(new User { Id = 2, user = "8-888-888", password = "12345", estatus = true, RememberMe = false, idRol = 2 });
            users.Add(new User { Id = 3, user = "1-1-1", password = "12345", estatus = false, RememberMe = false, idRol = 3 });
            users.Add(new User { Id = 4, user = "2-2-2", password = "12345", estatus = false, RememberMe = false, idRol = 4 });
            users.Add(new User { Id = 4, user = "3-3-3", password = "12345", estatus = false, RememberMe = false, idRol = 5 });

            return users.ToList();
        }

        public IEnumerable<RolMenu> getRolMenus()
        {
            var rolMenus = new List<RolMenu>();
            rolMenus.Add(new RolMenu { IdRol = 1, IdMenu = 1 });
            rolMenus.Add(new RolMenu { IdRol = 2, IdMenu = 2 });
            rolMenus.Add(new RolMenu { IdRol = 3, IdMenu = 3 });
            rolMenus.Add(new RolMenu { IdRol = 4, IdMenu = 4 });
            rolMenus.Add(new RolMenu { IdRol = 5, IdMenu = 5 });

            return rolMenus.ToList();
        }
    }
}