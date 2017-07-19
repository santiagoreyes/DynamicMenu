using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using FEDominio;
namespace FEBLL
{
    public class ValidarSchema
    {
        private string feNameSpace = "";
        private string xsdFile = "fePty.xsd";
        private ResultadoValidaciones resultadoValidaciones;

        // TODO: REVISAR PARA CAMBIAR A XmlSchemaValidator Push-Based Validation

        public ResultadoValidaciones  validar(string documentoElectronicoXml, ResultadoValidaciones resultadoValidaciones) {
            this.resultadoValidaciones = resultadoValidaciones;
            XmlReaderSettings feSettings = new XmlReaderSettings();
            string lxsd = GetResourceTextFile(xsdFile);
            feSettings.Schemas.Add(feNameSpace, XmlReader.Create(new StringReader(lxsd)));
            feSettings.ValidationType = ValidationType.Schema;
            feSettings.ValidationEventHandler += new ValidationEventHandler(feSettingsValidationEventHandler);

            XmlReader deXml = XmlReader.Create(new StringReader(documentoElectronicoXml), feSettings);

            while (deXml.Read()) { }

            return this.resultadoValidaciones;

        }

        private void feSettingsValidationEventHandler(object sender, ValidationEventArgs e)
        {
            if (e.Severity == XmlSeverityType.Warning)
            {
                resultadoValidaciones.agregarEvento(new FEDominio.EventoValidacion { error = e.Message, accion = "WARNING" });
            }
            else if (e.Severity == XmlSeverityType.Error)
            {
                resultadoValidaciones.agregarEvento(new FEDominio.EventoValidacion { error = e.Message, accion = "ERROR" });
            }
        }


        private string GetResourceTextFile(string filename)
        {
            string result = string.Empty;
            string[] resources = GetType().Assembly.GetManifestResourceNames();
            using (Stream stream = this.GetType().Assembly.
                       GetManifestResourceStream("FEBLL.archivos." + filename))
            {
                using (StreamReader sr = new StreamReader(stream))
                {
                    result = sr.ReadToEnd();
                }
            }
            return result;
        }
    }
}
