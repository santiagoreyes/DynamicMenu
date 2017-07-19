using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEDominio
{

    public enum ResultadoValidacionEnum
    {
        OK,
        FALLO
    }

    public enum ResultadoProcesoEnum
    {
        NOEXISTE,
        ENPROCESO,
        FINALIZADO
    }

    public class EventoValidacion
    {
        public string error { get; set; }
        public string accion { get; set; }
    }

    public class ResultadoValidaciones
    {
        private int maxErrores = 5;
        public IdentificadorMensajeDE identificadorMensajeDE { get; set; }
        //public string Cufe { get; set; }
        public ResultadoValidacionEnum resultadoValidacion { get; set; }
        public List<EventoValidacion> eventosValidacion { get; set; }

        public DocumentoElectronicoResumen documentoElectronicoResumen { get; set; }

        public ResultadoValidaciones() { }
        public ResultadoValidaciones(IdentificadorMensajeDE identificadorMensajeDE)
        {
            this.identificadorMensajeDE = identificadorMensajeDE;
            this.resultadoValidacion = ResultadoValidacionEnum.OK;
            this.eventosValidacion = new List<EventoValidacion>();
            
        }

        public void agregarEvento(EventoValidacion eventoValidacion)
        {
            eventosValidacion.Add(eventoValidacion);
            if (this.getErrorCount()>0) this.resultadoValidacion = ResultadoValidacionEnum.FALLO;
            /*
            if (eventosValidacion.Count() > this.maxErrores)
            {
                throw new ValidacionException(string.Format("Max errores alcanzado: {0}", this.maxErrores));
            }*/
        }

        public int getErrorCount()
        {
            return this.eventosValidacion.Count();
        }
    }

    public class ValidacionException : Exception
    {
        public ValidacionException(string mensaje) : base(mensaje) { }
        public ValidacionException(string mensaje, Exception e) : base(mensaje, e) { }
    }


    public class Autorizacion
    {
        public string IdAutorizacion { get; set; }
        public DateTime fechaHoraAutorizacion { get; set; }
        public IdentificadorMensajeDE identificadorMensajeDE { get; set; }
        

        public Autorizacion() { }
        public Autorizacion(IdentificadorMensajeDE identificadorMensajeDE)
        {
            this.identificadorMensajeDE = identificadorMensajeDE;
        }

        public string ToXML()
        {
            return FEUtil.XmlHelper.SerializeToXmlString(this);
        }
    }

    public class Rechazo
    {
        public string IdRechazo { get; set; }
        public IdentificadorMensajeDE identificadorMensajeDE { get; set; }

        public Rechazo() { }
        public Rechazo(IdentificadorMensajeDE identificadorMensajeDE)
        {
            this.identificadorMensajeDE = identificadorMensajeDE;
        }

        public string ToXML()
        {
            return FEUtil.XmlHelper.SerializeToXmlString(this);
        }
    }

    public class ResultadoProceso {
        public ResultadoValidaciones resultadoValidaciones { get; set; }
        public ResultadoProcesoEnum resultadoProceso { get; set; }

        public Autorizacion autorizacion { get; set; }
        public Rechazo rechazo { get; set; }

        public ResultadoProceso() { }
        public ResultadoProceso(ResultadoValidaciones resultadoValidaciones)
        {
           this.resultadoValidaciones = resultadoValidaciones;
            this.autorizacion = null;
            this.rechazo = null;
            
        }

        public string ToXML()
        {
            return FEUtil.XmlHelper.SerializeToXmlString(this);
        }

        //public int getIdBuzon() { return this.resultadoValidaciones.identificadorMensajeDE.idBuzon; }

    }

    public class RespuestaAutorizacion {
        public ResultadoProceso resultadoProceso { get; set; }
        public string mensajeRespuesta { get; set; } // Revisar y crear clase mensaje

        public RespuestaAutorizacion() { }
        public RespuestaAutorizacion(ResultadoProceso resultadoProceso) {
            this.resultadoProceso = resultadoProceso;
            if (resultadoProceso.resultadoProceso == FEDominio.ResultadoProcesoEnum.FINALIZADO)
            {
                if (resultadoProceso.resultadoValidaciones.resultadoValidacion == FEDominio.ResultadoValidacionEnum.OK)
                {
                    mensajeRespuesta = "FINALIZADO:OK";
                }
                if (resultadoProceso.resultadoValidaciones.resultadoValidacion == FEDominio.ResultadoValidacionEnum.FALLO)
                {
                    mensajeRespuesta = "FINALIZADO:RECHAZO";
                }
            }
            else if (resultadoProceso.resultadoProceso == FEDominio.ResultadoProcesoEnum.ENPROCESO)
            {
                mensajeRespuesta = "EN PROCESO";
            }
            else if (resultadoProceso.resultadoProceso == FEDominio.ResultadoProcesoEnum.NOEXISTE)
            {
                mensajeRespuesta = "NO EXISTE";
            }
        }

        public string ToXML() {
            return FEUtil.XmlHelper.SerializeToXmlString(this);
        }
    }

    public class IdentificadorMensajeDE {
        public string cufe { get; set; }
        public Guid idBuzon { get; set; }
        public IdentificadorMensajeDE() { }
        public IdentificadorMensajeDE(string cufe) {
            this.cufe = cufe;
        }
        
    }

    public class MensajeDE {
        public string contenidoXml { get; set; }
        public IdentificadorMensajeDE identificadorMensajeDE { get; set; }
        public MensajeDE(string cufe, string contenidoXml) {
            this.identificadorMensajeDE = new IdentificadorMensajeDE (cufe);
            this.contenidoXml = contenidoXml;
        }

        //public void setIdBuzon(int idBuzon) {
        //    this.identificadorMensajeDE.idBuzon = idBuzon;
        //}
    }




    public class DocumentoElectronicoResumen {
        public String Cufe;
        public String TipoDocumento;

        public String TipoOperacion;
        public String TipoFactura;
        public String RucEmisor;
        public String RucReceptor;
        public DateTime FechaHoraEmision;

        public Decimal montoOperacion;
        public Decimal montoImpuesto;
        public Decimal montoTotal;

        public DocumentoElectronicoResumen() {

        }
    }
    //// SECCION DEFINICION DOCUMENTOELECTRONICO
    //// REVISAR SI GENERAR O NO
    public class DocumentoElectronico {


    }


}
