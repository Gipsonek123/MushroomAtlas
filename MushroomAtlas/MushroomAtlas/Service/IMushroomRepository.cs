using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MushroomAtlas.Model;

namespace MushroomAtlas.Service
{
    public interface IMushroomRepository
    {
        //dodawanie/usuwanie/filtrwoanie
        void Add(MushroomModel mushroom);
        void Remove(string mushroomId);
        void Filter(DataTable dataTable, string pick, string filterText);

        //detale
        void Details(string name);

        //wczytywanie z bazy danych
        DataTable GetAllAsDataTable();

        IEnumerable<MushroomModel> GetAllAsEnumerable();
    }
}
