using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using FEDAL;

namespace FePrototipo.Models
{
 
    public class SearchCriteria {
        public Int32 Page { get; set; }
        public Int32 PageSize { get; set; }
        public Int32 TotalRecords { get; set; }

        public SearchCriteria() {
            Page = 1;
            PageSize = 4;
        }
    }

    public class BuzonSearchCriteria: SearchCriteria {
        /*
        [Display(Name = "Desde")]
        public DateTime Desde { get; set; }

        [Display(Name = "Hasta")]
        public DateTime Hasta { get; set; }
        */
        [Display(Name = "CUFE")]
        public String Cufe { get; set; }


        [Display(Name = "Estado")]
        public String Estado { get; set; }

        public PagedList.IPagedList<Buzon> Resultado  { get; set; }

    }

    public class DocumentosAutorizadosSearchCriteria : SearchCriteria
    {
        [Display(Name = "CUFE")]
        public String Cufe { get; set; }
        [Display(Name = "TIPO_OPERACION")]
        public String TipoOperacion { get; set; }
        [Display(Name = "TIPO_DOCUMENTO")]
        public String TipoDocumento { get; set; }

        public PagedList.IPagedList<DocumentoAutorizado> Resultado { get; set; }
    }

    public class ConsultaPorContribuyenteSearchCriteria : SearchCriteria
    {
        [Display(Name = "CUFE")]
        public String Cufe { get; set; }
        [Display(Name = "RUC_EMISOR")]
        public String RucEmisor { get; set; }
        [Display(Name = "RUC_RECEPTOR")]
        public String RucReceptor { get; set; }
        [Display(Name = "TIPO_OPERACION")]
        public String TipoOperacion { get; set; }
        [Display(Name = "TIPO_DOCUMENTO")]
        public String TipoDocumento { get; set; }

        public PagedList.IPagedList<DocumentoAutorizado> Resultado { get; set; }
    }



    ///// Formularios

    public class SolicitudPruebas {
        [Required]
        [Range(1, 100)]
        [Display(Name = "Número de Sucursales")]
        public string NumeroSucursales { get; set; }

        [Required]
        [Range(0, 1000)]
        [Display(Name = "Cantidad de Equipos Fiscales")]
        public string NumeroEquiposFiscalesActuales { get; set; }

    }

    public class SolicitudProduccion
    {
        [Required]
        [Display(Name = "Fecha prevista de inicio")]
        [DataType(DataType.Date)]
        public DateTime FechaPrevistaInicio { get; set; }

 
    }

}