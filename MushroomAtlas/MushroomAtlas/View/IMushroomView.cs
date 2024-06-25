using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;

namespace MushroomAtlas.View
{
    public interface IMushroomView
    {
        string ScientificName { get; set; }
        string CommonName { get; set; }
        string Edibility { get; set; }
        string MushroomDescription { get; set; }
        string HabitatName { get; set; }
        string GenusName { get; set;}
        string FamilyName { get; set; }
        string RowName { get; set; }
        string ClassName { get; set; }
        string TypeName { get; set; }

        string Pick {  get; set; }
        string FilterText { get; set; }
        DataGridViewRow row { get; set; }
        DataGridView DataGridView { get; }
        Control ScientifNameControl { get; }
        Control CommonNameControl { get; }
        Control EdibilityControl { get; }
        Control MushroomDescriptionControl { get; }
        Control ClassNameControl { get; }
        Control GenusNameControl { get; }
        Control FamilyNameControl { get; }
        Control RowNameControl { get; }
        Control HabitatNameControl { get; }
        Control TypeNameControl { get; }
        ErrorProvider ErrorProvider { get; }

        event EventHandler AddNewMusroomMushroom;
        event EventHandler RemoveMusroomMushroom;
        event EventHandler DetailsMushroomMushroom;
        event EventHandler FilterMushroomMushroom;

        void SetEventListDataSource(DataTable eventDataTable);
        int SelectedRowIndex { get; set; }
    }
}
