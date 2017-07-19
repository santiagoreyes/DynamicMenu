using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FEDAL;
using FEDominio;


namespace FEBLL
{
    public class FEProcesar
    {
        protected FEDAL.FEContext db = new FEDAL.FEContext();


        public FEDominio.RespuestaAutorizacion solicitarAutorizacionDE(MensajeDE mensajeDE)
        {

            mensajeDE = InsertarBuzon(mensajeDE);
            FEDominio.ResultadoProceso resultadoProceso = this.procesar(mensajeDE);
            FEDominio.RespuestaAutorizacion respuestaAutorizacion = this.generarRespuestaAutorizacion(resultadoProceso);
            return respuestaAutorizacion;
        }

        

        protected FEDominio.ResultadoProceso procesar(MensajeDE mensajeDE) {

            FEDominio.ResultadoValidaciones resultadoValidaciones = this.validar(mensajeDE);
            ResultadoProceso resultadoProceso = new ResultadoProceso(resultadoValidaciones);
            
            if (resultadoValidaciones.resultadoValidacion == FEDominio.ResultadoValidacionEnum.OK) {
                resultadoProceso.autorizacion = this.generarAutorizacion(mensajeDE, resultadoProceso);
            }
            if (resultadoValidaciones.resultadoValidacion == FEDominio.ResultadoValidacionEnum.FALLO)
            {
                resultadoProceso.rechazo = this.generarRechazo(mensajeDE, resultadoProceso);
            }
            resultadoProceso.resultadoProceso = ResultadoProcesoEnum.FINALIZADO;
            return resultadoProceso;
        }

        protected Autorizacion generarAutorizacion(MensajeDE mensajeDE,ResultadoProceso resultadoProceso) {
            /*Autorizacion autorizacion = new Autorizacion(resultadoValidaciones.identificadorMensajeDE);
            autorizacion.IdAutorizacion = Guid.NewGuid().ToString("n").Substring(0, 30); // ajustar
            autorizacion.fechaHoraAutorizacion = DateTime.Now;*/
            Autorizacion autorizacion = InsertarAutorizacion(mensajeDE, resultadoProceso);
            return autorizacion;
        }

        protected Rechazo generarRechazo(MensajeDE mensajeDE, ResultadoProceso resultadoProceso)
        {
            /*
            Rechazo rechazo = new Rechazo(resultadoValidaciones.identificadorMensajeDE);
            rechazo.IdRechazo = Guid.NewGuid().ToString("n").Substring(0, 30); // ajustar
            */
            Rechazo rechazo = InsertarRechazo(mensajeDE, resultadoProceso);
            return rechazo;
        }

        protected FEDominio.RespuestaAutorizacion generarRespuestaAutorizacion(FEDominio.ResultadoProceso resultadoProceso) {
            FEDominio.RespuestaAutorizacion respuestaAutorizacion = new FEDominio.RespuestaAutorizacion(resultadoProceso);
            
            /// FALTA GUARDAR LA RESPUESTA Y DESAGREGAR LOS DATOS EN TABLA PLANA
            return respuestaAutorizacion;
        }

        protected FEDominio.ResultadoValidaciones validar(MensajeDE mensajeDE) {

            FEValidaciones validaciones = new FEValidaciones();
            return validaciones.ValidarDocumentoElectronico(mensajeDE);
        }


        /* Metodos para Acceso a la Base de Datos */


        protected MensajeDE InsertarBuzon(MensajeDE mensajeDE)
        {

            FEDAL.Buzon pBuzon = new FEDAL.Buzon
            {
                Cufe = mensajeDE.identificadorMensajeDE.cufe,
                MensajeEntrada = mensajeDE.contenidoXml,
                MensajeRespuesta = "RECIBIDO",
                Estado = "RECIBIDO",
                FechaHora = DateTime.Now
            };
            db.Buzones.Add(pBuzon);
            db.SaveChanges();
            mensajeDE.identificadorMensajeDE.idBuzon = pBuzon.ID;
            return mensajeDE;
        }

        protected Autorizacion InsertarAutorizacion(MensajeDE mensajeDE,ResultadoProceso resultadoProceso) {
            DocumentoElectronicoResumen lDER = resultadoProceso.resultadoValidaciones.documentoElectronicoResumen;
            string strFechaHora = lDER.FechaHoraEmision.ToString("yyyyMMdd");
            FEDAL.DocumentoAutorizado documentoAutorizado = new FEDAL.DocumentoAutorizado
            {
                Cufe = resultadoProceso.resultadoValidaciones.identificadorMensajeDE.cufe,
                DocumentoXml = mensajeDE.contenidoXml,
                //CodigoAutorizacion = Guid.NewGuid().ToString(),// revisar si es el mismo ID o es un algoritmo
                FechaHoraAutorizacion = DateTime.Now,
                FechaHora_Emision = lDER.FechaHoraEmision,
                
                FechaOperacionNumero = Int32.Parse(strFechaHora),
                RucReceptor = lDER.RucReceptor,
                RucEmisor = lDER.RucEmisor,
                TipoDocumento = lDER.TipoDocumento,
                TipoFactura = "FACTURA", // Arreglar
                TipoOperacion = lDER.TipoOperacion,
                Periodo = Int32.Parse(lDER.FechaHoraEmision.ToString("yyyyMM")),
                MontoOperacion = lDER.montoOperacion,
                MontoImpuestos = lDER.montoImpuesto,
                MontoTotal = lDER.montoTotal,
                IdBuzon = resultadoProceso.resultadoValidaciones.identificadorMensajeDE.idBuzon

            };
            db.DocumentoAutorizado.Add(documentoAutorizado);
            db.SaveChanges();

            Autorizacion autorizacion = new Autorizacion();
            autorizacion.identificadorMensajeDE = mensajeDE.identificadorMensajeDE;
            autorizacion.fechaHoraAutorizacion = documentoAutorizado.FechaHoraAutorizacion;
            autorizacion.IdAutorizacion = documentoAutorizado.ID.ToString();
            return autorizacion;
        }

        protected Rechazo InsertarRechazo(MensajeDE mensajeDE, ResultadoProceso resultadoProceso)
        {
            DocumentoElectronicoResumen lDER = resultadoProceso.resultadoValidaciones.documentoElectronicoResumen;
            FEDAL.DocumentoRechazado documentoRechazado = new FEDAL.DocumentoRechazado
            {
                Cufe = resultadoProceso.resultadoValidaciones.identificadorMensajeDE.cufe,
                FechaHoraRechazo = DateTime.Now,
                MensajeRechazo = resultadoProceso.ToXML(),
                IdBuzon = resultadoProceso.resultadoValidaciones.identificadorMensajeDE.idBuzon

            };
            db.DocumentoRechazado.Add(documentoRechazado);
            db.SaveChanges();

            Rechazo rechazo = new Rechazo();
            rechazo.identificadorMensajeDE = mensajeDE.identificadorMensajeDE;
            return rechazo;
        }



    }
}
