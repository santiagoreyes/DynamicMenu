using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace FEDAL
{

        [Table("BUZON")]
        public class Buzon
        {
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public Guid ID { get; set; }
            [Column("CUFE")]
            public string Cufe { get; set; }
            [Column("FECHA_HORA")]
            public DateTime FechaHora { get; set; }
            [Column("MENSAJE_ENTRADA")]
            public string MensajeEntrada { get; set; }
            [Column("MENSAJE_RESPUESTA")]
            public string MensajeRespuesta { get; set; }
            [Column("ESTADO")]
            public string Estado { get; set; }
        }

        [Table("DOCUMENTO_AUTORIZADOS")]
        public class DocumentoAutorizado
        {
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public Guid ID { get; set; }
            [Column("CUFE")]
            public string Cufe { get; set; }
            [Column("DOCUMENTO_XML")]
            public string DocumentoXml { get; set; }
            //[Column("CODIGOAUTORIZACION")]
            //public string CodigoAutorizacion { get; set; }
            [Column("FECHAHORA_AUTORIZACION")]
            public DateTime FechaHoraAutorizacion { get; set; }
 
            [Column("TIPO_DOCUMENTO")]
            public string TipoDocumento { get; set; }
            [Column("TIPO_FACTURA")]
            public string TipoFactura { get; set; }
            [Column("TIPO_OPERACION")]
            public string  TipoOperacion { get; set; }
            [Column("FECHAHORA_EMISION")]
            public DateTime FechaHora_Emision  { get; set; }
            [Column("RUC_EMISOR")]
            public string  RucEmisor{ get; set; }
            [Column("RUC_RECEPTOR")]
            public string  RucReceptor { get; set; }
            [Column("FECHA_OPERACION")]
            public Int32 FechaOperacionNumero { get; set; }
            [Column("PERIODO")]
            public Int32 Periodo { get; set; }            
            [Column("MONTO_OPERACION")]
            public Decimal MontoOperacion { get; set; }
            [Column("MONTO_IMPUESTOS")]
            public Decimal  MontoImpuestos { get; set; }
            [Column("MONTO_TOTAL")]
            public Decimal  MontoTotal{ get; set; }

            [Column("IDBUZON")]
            public Guid IdBuzon { get; set; }

    }

 

    [Table("RECHAZOS")]
    public class DocumentoRechazado
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ID { get; set; }

        [Column("CUFE")]
        public string Cufe { get; set; }
        [Column("FECHA_HORA")]
        public DateTime FechaHoraRechazo { get; set; }
        [Column("MENSAJE_RECHAZO")]
        public string MensajeRechazo { get; set; }

        [Column("IDBUZON")]
        public Guid IdBuzon { get; set; }
    }

    [Table("RUCS_ACREDITADOS")]
    public class RucAcreditado
    {
        [Key]
        [Column("RUC")]
        public string Ruc { get; set; }
        [Column("FECHA_HORA")]
        public DateTime FechaHoraActualizacion { get; set; }
        
    }

    [Table("RUCS_VALIDOS")]
    public class RucValido
    {
        [Key]
        [Column("RUC")]
        public string Ruc { get; set; }
    }

    [Table("DOCUMENTOS_EVENTOS")]
    public class DocumentoEventos
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ID { get; set; }

        [Column("TIPO_EVENTO")]
        public string TipoEvento { get; set; }
        public DateTime FechaHoraEvento { get; set; }

        public virtual DocumentoAutorizado DocumentoAutorizadoReceptor { get; set; }

        public virtual DocumentoAutorizado DocumentoAutorizadoEmisor { get; set; }

    }

}
