using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteWpfApp.Controllers
{
    class StartController
    {
        private MainWindow mainWindow { get; set; }
        public StartController()
        {
            mainWindow = new MainWindow();
        }
    }
}
