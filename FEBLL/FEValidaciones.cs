using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FEDominio;
using System.Xml.XPath;
using System.Xml;
using System.IO;

namespace FEBLL
{
    public class FEValidaciones
    {

        public FEValidaciones() {

        }

        public ResultadoValidaciones ValidarDocumentoElectronico(MensajeDE mensajeDE)
        {
            ResultadoValidaciones resultadoValidaciones = new ResultadoValidaciones(mensajeDE.identificadorMensajeDE);
            try
            {
                resultadoValidaciones = ValidarCertificado(mensajeDE.contenidoXml, resultadoValidaciones);
                resultadoValidaciones = ValidarEsquema(mensajeDE.contenidoXml, resultadoValidaciones);
                // Si el esquema es valido se puede obtener los datos en un objeto
                // Dependiendo de las validaciones ver si esto va mas abajo en la cola..
                if (resultadoValidaciones.resultadoValidacion == ResultadoValidacionEnum.OK) {
                    resultadoValidaciones.documentoElectronicoResumen = this.obtenerResumen(mensajeDE.contenidoXml);
                    resultadoValidaciones = ValidarDatosGenerales(resultadoValidaciones.documentoElectronicoResumen, resultadoValidaciones);
                    resultadoValidaciones = ValidarBDD(resultadoValidaciones.documentoElectronicoResumen, resultadoValidaciones);
                }
                

            }
            catch (ValidacionException ex) {
                resultadoValidaciones.agregarEvento(new EventoValidacion { error = ex.Message, accion = "Muchos errores" });
            }
            catch (Exception ex)
            {
                resultadoValidaciones.agregarEvento(new EventoValidacion { error = ex.Message, accion = "Error en validacion, comunicarse." });
            }
            return resultadoValidaciones;

        }

        protected ResultadoValidaciones ValidarCertificado(string xmlDocumentoElectronico, ResultadoValidaciones resultadoValidaciones) {

            return resultadoValidaciones;
        }

        protected ResultadoValidaciones ValidarEsquema(string xmlDocumentoElectronico, ResultadoValidaciones resultadoValidaciones)
        {
            ValidarSchema validarSchema = new ValidarSchema();
            validarSchema.validar(xmlDocumentoElectronico, resultadoValidaciones);
            return resultadoValidaciones;
        }

        protected ResultadoValidaciones ValidarFirma(string xmlDocumentoElectronico, ResultadoValidaciones resultadoValidaciones)
        {

            return resultadoValidaciones;
        }

        protected ResultadoValidaciones ValidarDatosGenerales(FEDominio.DocumentoElectronicoResumen documentoElectronicoResumen, ResultadoValidaciones resultadoValidaciones)
        {
            if (documentoElectronicoResumen == null) throw new ValidacionException("No se obtuvo resumen");
            // Validar cufe 
            if (documentoElectronicoResumen.Cufe.Trim() != resultadoValidaciones.identificadorMensajeDE.cufe.Trim())
                resultadoValidaciones.agregarEvento(new EventoValidacion { error = "Cufe no coincide", accion = "Corregir en xml" });
            // Validar fecha en rango
            if (documentoElectronicoResumen.FechaHoraEmision < DateTime.Now.AddDays(-1) 
                    || documentoElectronicoResumen.FechaHoraEmision >= DateTime.Now)
                resultadoValidaciones.agregarEvento(new EventoValidacion { error = "Fecha Hora emision no cumple con rango", accion = "Corregir en xml" });
            return resultadoValidaciones;

        }

        protected ResultadoValidaciones ValidarCalculos(string xmlDocumentoElectronico, ResultadoValidaciones resultadoValidaciones)
        {

            return resultadoValidaciones;
        }

        protected ResultadoValidaciones ValidarBDD(DocumentoElectronicoResumen documentoElectronicoResumen, ResultadoValidaciones resultadoValidaciones)
        {
            //FEDAL.FEContext ctx = new FEDAL.FEContext();
            string resultado = null;

            using (var ctx = new FEDAL.FEContext())
            {
                if (documentoElectronicoResumen.TipoOperacion == "B2B")
                {
                    resultado =
    ctx.Database.SqlQuery<string>("select VALIDACIONES_FE(:p0,:p1,:p2) from dual",
    documentoElectronicoResumen.Cufe,
    documentoElectronicoResumen.RucEmisor,
    documentoElectronicoResumen.RucReceptor
    ).FirstOrDefault().ToString();
                }
                else {
                    resultado =
    ctx.Database.SqlQuery<string>("select VALIDACIONES_FE(:p0,:p1) from dual",
    documentoElectronicoResumen.Cufe,
    documentoElectronicoResumen.RucEmisor    ).FirstOrDefault().ToString();
                }

            }
            if (!resultado.Equals("0",StringComparison.Ordinal)) {
                resultadoValidaciones.agregarEvento(new EventoValidacion { error = resultado, accion = "Error BD" });
            }
             return resultadoValidaciones;
        }


        /// ser si se pasa a clase utilitaria
        /// 
        protected DocumentoElectronicoResumen obtenerResumen(string documentoElectronicoXml)
        {
            DocumentoElectronicoResumen documentoElectronicoResumen = new DocumentoElectronicoResumen();
            XPathDocument document = new XPathDocument(XmlReader.Create(new StringReader(documentoElectronicoXml)));

            XPathNavigator navigator = document.CreateNavigator();

            documentoElectronicoResumen.TipoDocumento = 
                navigator.SelectSingleNode("/ptyDE/factura/identificacion/tipoDocumento").Value;

            documentoElectronicoResumen.TipoOperacion =
                navigator.SelectSingleNode("/ptyDE/factura/identificacion/tipoOperacion").Value;

            documentoElectronicoResumen.TipoFactura =
            navigator.SelectSingleNode("/ptyDE/factura/identificacion/tipoFactura").Value;

            documentoElectronicoResumen.Cufe = 
                navigator.SelectSingleNode("/ptyDE/factura/identificacion/codigoUnicoDocumento").Value;

            string fechaHoraEmision = navigator.SelectSingleNode("/ptyDE/factura/identificacion/fechaHoraEmision").Value;
            DateTime lFechaHoraEmision = DateTimeOffset.Parse(fechaHoraEmision).UtcDateTime;
            documentoElectronicoResumen.FechaHoraEmision = lFechaHoraEmision;

            documentoElectronicoResumen.RucEmisor =
                navigator.SelectSingleNode("/ptyDE/factura/emisor/ruc").Value;
            documentoElectronicoResumen.RucReceptor =
                navigator.SelectSingleNode("/ptyDE/factura/receptor/ruc").Value;

            documentoElectronicoResumen.montoOperacion =
                Decimal.Parse(navigator.SelectSingleNode("/ptyDE/factura/totales/subtotal").Value);

            documentoElectronicoResumen.montoImpuesto =
                Decimal.Parse(navigator.SelectSingleNode("/ptyDE/factura/totales/isrTotal").Value)+
                Decimal.Parse(navigator.SelectSingleNode("/ptyDE/factura/totales/itbmsTotal").Value);

            documentoElectronicoResumen.montoTotal =
                Decimal.Parse(navigator.SelectSingleNode("/ptyDE/factura/totales/total").Value);
            
            return documentoElectronicoResumen;
        }

    }

}
