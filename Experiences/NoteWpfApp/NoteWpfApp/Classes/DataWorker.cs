using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using NoteWpfApp.Models;
using System;
using System.Collections.Generic;

namespace NoteWpfApp.Classes
{
    /// <summary>
    /// Незавершенный,необходимо обработать ситуацию повторного добавления таой же даты
    /// </summary>
    public class DataWorker
    {
        string pathToXml { get; set; }

        public DataWorker(string path)
        {
            pathToXml = path;
        }

        public DataWorker()
        {
            pathToXml = "D:/MyNote.xml";
        }

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

            CreateDocument(data).Save(pathToXml);
        }

        public List<XmlModel> LoadData()
        {
            List<XmlModel> datas = new List<XmlModel>();
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(pathToXml);
            XmlElement xRoot = xDoc.DocumentElement;
            foreach (XmlNode xnode in xRoot)
            {
                //получаем data
                XmlModel data = new XmlModel();
                data.Notes = new List<NoteModel>();
                data.Spendings = new List<SpendingModel>();
                // получаем атрибут date
                if (xnode.Attributes.Count > 0)
                {
                    XmlNode attr = xnode.Attributes.GetNamedItem("date");
                    if (attr != null)
                        data.Date = DateTime.Parse(attr.Value);
                }
                // обходим все дочерние узлы элемента spendings
                foreach (XmlNode childnode in xnode.ChildNodes)
                {
                    if (childnode.Name == "spending")
                    {
                        SpendingModel spending = new SpendingModel();
                        foreach (XmlNode child in childnode.ChildNodes)
                        {
                            if (child.Name == "amount")
                            {
                                spending.Message = child.Value;
                            }
                            if (child.Name == "message")
                            {
                                spending.Message = child.Value;
                            }
                        }
                        data.Spendings.Add(spending);
                    }
                    else
                    {
                        NoteModel note = new NoteModel();
                        foreach (XmlNode child in childnode.ChildNodes)
                        {
                            note.Message = child.Value;
                        }
                        data.Notes.Add(note);
                    }
                }
                datas.Add(data);
            }
            return datas;
        }

        public XmlDocument CreateDocument(XmlModel data)
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
                subElementNotes.AppendChild(subElementNote);
                var subElementNoteMessage = document.CreateElement("message");
                subElementNoteMessage.InnerText = note.Message;
                subElementNote.AppendChild(subElementNoteMessage);
            }
            return document;
        }

        public XmlDocument ChangeDocument()
        {

        }
    }
}
