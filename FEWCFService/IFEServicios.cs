using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Xml;

namespace FEWCFService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IFEServicios
    {

 
        [OperationContract]
        RespuestaMensaje solicitarAutorizacionDE(MensajeDocumentoElectronico mensajeDE);

        [OperationContract]
        RespuestaMensaje enviarDE(MensajeDocumentoElectronico mensajeDE);

        [OperationContract]
        RespuestaMensaje obtenerResultadoDE(MensajeDocumentoElectronico mensajeDE);

    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class MensajeDocumentoElectronico
    {
        string cufe = null;
        string documentoElectronico = null;

        [DataMember]
        public string Cufe
        {
            get { return cufe; }
            set { cufe = value; }
        }
        [DataMember]
        public string DocumentoElectronico
        {
            get { return documentoElectronico; }
            set { documentoElectronico = value; }
        }
    }

    [DataContract]
    public class RespuestaMensaje
    {
        private FEDominio.RespuestaAutorizacion respuesta = null;

        [DataMember]
        public FEDominio.RespuestaAutorizacion Respuesta
        {
            get { return respuesta; }
            set { respuesta = value; }
        }

        public RespuestaMensaje(FEDominio.RespuestaAutorizacion respuestaAutorizacion) {
            this.respuesta = respuestaAutorizacion;
        }
    }
}
