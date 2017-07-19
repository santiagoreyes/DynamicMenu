using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FEDAL;

namespace FePrototipo.Controllers
{
    public class ConsultasController : BaseSecureController
    {
        // GET: Privado
       
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ConsultaBuzon(Models.BuzonSearchCriteria modelo)
        {

            ViewBag.Estados = new SelectList(new string[] { "RECIBIDO", "PROCESANDO", "RECHAZADO", "AUTORIZADO" });

            modelo.Resultado = db.Buzones.Where(
                x =>
                (modelo.Cufe == null || x.Cufe == modelo.Cufe)
                && (modelo.Estado == null || x.Estado == modelo.Estado)
                ).OrderByDescending(i => i.FechaHora).ToPagedList(modelo.Page, modelo.PageSize);
            return View(modelo);
         }

        
        public ActionResult VerMensajeBuzon(Guid id)
        {
            Buzon mensajeBuzon = db.Buzones.Where(
                x => (x.ID == id)).First();
            return View(mensajeBuzon);
        }

        public ActionResult ConsultaFacturasAutorizadas(Models.DocumentosAutorizadosSearchCriteria modelo)
        {
            ViewBag.TiposOperacion = new SelectList(new string[] { "B2B", "B2C"});
            ViewBag.TiposDocumento = new SelectList(new string[] { "FACTURA", "NOTA_CREDITO", "NOTA_DEBITO" });


            modelo.Resultado = db.DocumentoAutorizado.Where(
                x =>
                (modelo.Cufe == null || x.Cufe == modelo.Cufe)
                && (modelo.TipoOperacion == null || x.TipoOperacion == modelo.TipoOperacion)
                && (modelo.TipoDocumento == null || x.TipoDocumento == modelo.TipoDocumento)
                ).OrderByDescending(i => i.FechaHoraAutorizacion).ToPagedList(modelo.Page, modelo.PageSize);
            return View(modelo);
        }

        public ActionResult VerDocumentoAutorizado(Guid id)
        {
            DocumentoAutorizado documentoAutorizado = db.DocumentoAutorizado.Where(
                x => (x.ID == id)).First();
            return View(documentoAutorizado);
        }

        public ActionResult ConsultaPorContribuyente(Models.ConsultaPorContribuyenteSearchCriteria modelo)
        {

            ViewBag.TiposOperacion = new SelectList(new string[] { "B2B", "B2C" });
            ViewBag.TiposDocumento = new SelectList(new string[] { "FACTURA", "NOTA_CREDITO", "NOTA_DEBITO" });

            modelo.Resultado = db.DocumentoAutorizado.Where(
                x =>
                (modelo.Cufe == null || x.Cufe == modelo.Cufe)
                && (modelo.RucEmisor == null || x.RucEmisor == modelo.RucEmisor)
                && (modelo.RucReceptor == null || x.RucReceptor == modelo.RucReceptor)
                && (modelo.TipoOperacion == null || x.TipoOperacion == modelo.TipoOperacion)
                && (modelo.TipoDocumento == null || x.TipoDocumento == modelo.TipoDocumento)
                ).OrderByDescending(i => i.FechaHoraAutorizacion).ToPagedList(modelo.Page, modelo.PageSize);
            return View(modelo);
        }

        public ActionResult ConsultaFacturasEmitidas()
        {
            List<Buzon> datosBuzon = db.Buzones.ToList<Buzon>();
            return View(datosBuzon);
        }

        public ActionResult ConsultaFacturasRecibidas()
        {
            List<Buzon> datosBuzon = db.Buzones.ToList<Buzon>();
            return View(datosBuzon);
        }
    }
}