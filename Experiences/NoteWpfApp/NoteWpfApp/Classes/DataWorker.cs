using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using NoteWpfApp.Models;

namespace NoteWpfApp.Classes
{
    public class DataWorker
    {
        const string pathToXml = "D:/MyNote.xml"; 
        public void SaveData(XmlModel data)
        {
            if (!File.Exists(pathToXml))
            {
                XmlTextWriter textWritter = new XmlTextWriter(pathToXml, Encoding.UTF8);
                textWritter.WriteStartDocument();
                textWritter.WriteStartElement("head");
                textWritter.WriteEndElement();
                textWritter.Close();
            }
            AddDocument(data).Save(pathToXml);
        }
        public XmlModel LoadData()
        {
            return()
        }

        public XmlDocument AddDocument(XmlModel data)
        {
            XmlDocument document = new XmlDocument();
            document.Load(pathToXml);

            XmlNode element = document.CreateElement("data");
            document.DocumentElement.AppendChild(element);
            XmlAttribute attribute = document.CreateAttribute("date");
            attribute.Value = data.Date.ToShortDateString();
            element.Attributes.Append(attribute);
            XmlNode subElement1 = document.CreateElement("spendings");
            element.AppendChild(subElement1);
            foreach (var spending in data.Spendings)
            {
                var subElement2 = document.CreateElement("spending");
                subElement1.AppendChild(subElement2);
                var subElement3 = document.CreateElement("amount");
                subElement3.InnerText = spending.Amount.ToString();
                subElement2.AppendChild(subElement3);
                var subElement4 = document.CreateElement("message");
                subElement3.InnerText = spending.Message.ToString();
                subElement2.AppendChild(subElement4);
            }
            XmlNode subElementNotes = document.CreateElement("notes");
            element.AppendChild(subElementNotes);
            foreach (var note in data.Notes)
            {
                var subElementNote = document.CreateElement("note");
                subElementNote.InnerText = note.Message;
                subElementNotes.AppendChild(subElementNote);
            }
            return document;
        }
    }
}
