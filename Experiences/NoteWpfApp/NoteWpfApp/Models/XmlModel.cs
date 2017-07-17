using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteWpfApp.Models
{
    public class SpendingModel
    {
        public decimal Amount { get; set; }
        public string Message { get; set; }
    }

    public class NoteModel
    {
        public string Message { get; set; }
    }

    public class XmlModel
    {
        public List<SpendingModel> Spendings { get; set; }
        public List<NoteModel> Notes { get; set; }
        public DateTime Date { get; set; }
    }
}
