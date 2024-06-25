using MushroomAtlas.Presenter;
using MushroomAtlas.Service;
using MushroomAtlas.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MushroomAtlas.Model;

namespace MushroomAtlas
{
    static class Program
    {
        /// <summary>
        /// Główny punkt wejścia dla aplikacji.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            IMushroomRepository mushroomRepository = new MushroomRepository();
            IMushroomView mushroomView = new MushroomView();
            new MushroomPresenter(mushroomView, mushroomRepository);

            Form mushroomForm = (Form)mushroomView;
            mushroomForm.WindowState = FormWindowState.Maximized;

            Application.Run((Form)mushroomView);
        }
    }
}
