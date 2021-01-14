using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Tools.XML
{
    public static class Serializer
    {
        static Serializer()
        {
        }

        public static T Deserialize<T>(string xml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            StringReader stringReader = new StringReader(xml);
            return (T)serializer.Deserialize(stringReader);
        }

        public static string Serialize<T>(T obj)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            string xml = "";
            using (Stream stream = new MemoryStream())
            {
                serializer.Serialize(stream, obj);
                stream.Position = 0;
                var sr = new StreamReader(stream);
                xml = sr.ReadToEnd();
            }
            return xml;
        }

        public static T LoadXML<T>(string xmlFile, string xsdFile) where T : class
        {
            T returnType = null;

            try
            {
                // Check Schema
                XmlSchemaSet schema = new XmlSchemaSet();
                schema.Add("", xsdFile);
                XmlReader rd = XmlReader.Create(xmlFile);
                XDocument doc = XDocument.Load(rd);
                doc.Validate(schema, null);
                // Deserialize
                returnType = Serializer.Deserialize<T>(Files.ReadFile(xmlFile));

            } catch (XmlSchemaValidationException xmlE)
            {
            }
            catch (Exception e)
            {
            }

            return returnType;
        } 

        public static void SaveXML<T>(string xmlFile, T obj)
        {
            Files.WriteFile(xmlFile, Serializer.Serialize<T>(obj));
        }
    }
}
