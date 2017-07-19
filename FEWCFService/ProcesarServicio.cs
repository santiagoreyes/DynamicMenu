using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace FEWCFService
{
    public class ProcesarServicio
    {
        

        public RespuestaMensaje solicitarAutorizacionDE(MensajeDocumentoElectronico mensajeDocumentoElectronico) {
            FEBLL.FEProcesar procesar = new FEBLL.FEProcesar();
            FEDominio.MensajeDE mensajeDE = new FEDominio.MensajeDE(mensajeDocumentoElectronico.Cufe, mensajeDocumentoElectronico.DocumentoElectronico);
            FEDominio.RespuestaAutorizacion respuestaAutorizacion = procesar.solicitarAutorizacionDE(mensajeDE);
            return new RespuestaMensaje(respuestaAutorizacion);
        }

        public RespuestaMensaje enviarDE(MensajeDocumentoElectronico mensajeDE)
        {
            throw new NotImplementedException();
        }

        public RespuestaMensaje obtenerResultadoDE(MensajeDocumentoElectronico mensajeDE)
        {
            throw new NotImplementedException();
        }

    }
}