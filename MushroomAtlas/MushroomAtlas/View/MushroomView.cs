using MushroomAtlas.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MushroomAtlas.View
{
    public partial class MushroomView : Form, IMushroomView
    {
        private DataTable originalMushroomDataTable;
        public ErrorProvider ErrorProvider => errorProvider1;
        public Control ScientifNameControl => textBox1;
        public Control CommonNameControl => textBox2;
        public Control EdibilityControl => textBox3;
        public Control MushroomDescriptionControl => textBox4;
        public Control ClassNameControl => textBox5;
        public Control GenusNameControl => textBox6;
        public Control FamilyNameControl => textBox7;
        public Control RowNameControl => textBox8;
        public Control HabitatNameControl => textBox9;
        public Control TypeNameControl => textBox10;

        public MushroomView()
        {
            InitializeComponent();
            _associateAndRaiseViewEvents();
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoGenerateColumns = true; 
            dataGridView1.ReadOnly = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false; 
            dataGridView1.DoubleClick += DataGridView1_DoubleClick;
        }

        private void _associateAndRaiseViewEvents()
        {
            button1.Click += (sender, e) =>
            {
                AddNewMusroomMushroom?.Invoke(this, EventArgs.Empty);
            };
            button2.Click += (sender, e) =>
            {
                RemoveMusroomMushroom?.Invoke(this, EventArgs.Empty);
            };
            textBox11.TextChanged += (sender, e) =>
            {
                FilterMushroomMushroom?.Invoke(this, e);
            };
        }

        private void DataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                row = dataGridView1.SelectedRows[0];
                DetailsMushroomMushroom?.Invoke(this, EventArgs.Empty);
            }
        }

        public string ScientificName { get => textBox1.Text; set => textBox1.Text = value; }
        public string CommonName { get => textBox2.Text; set => textBox2.Text = value; }
        public string Edibility { get => textBox3.Text; set => textBox3.Text = value; }
        public string MushroomDescription { get => textBox4.Text; set => textBox4.Text = value; }
        public string HabitatName { get => textBox5.Text; set => textBox5.Text = value; }
        public string GenusName { get => textBox6.Text; set => textBox6.Text = value; }
        public string FamilyName { get => textBox7.Text; set => textBox7.Text = value; }
        public string RowName { get => textBox8.Text; set => textBox8.Text = value; }
        public string ClassName { get => textBox9.Text; set => textBox9.Text = value; }
        public string TypeName { get => textBox10.Text; set => textBox10.Text = value; }
        public string Pick { get => comboBox1.Text; set => comboBox1.Text = value; }
        public string FilterText { get => textBox11.Text; set => textBox11.Text = value; }
        public DataGridViewRow row { get; set; }
        public DataGridView DataGridView => dataGridView1;


        public int SelectedRowIndex
        {
            get { return dataGridView1.SelectedRows.Count > 0 ? dataGridView1.SelectedRows[0].Index : -1; }
            set { }
        }

        public event EventHandler AddNewMusroomMushroom;
        public event EventHandler RemoveMusroomMushroom;
        public event EventHandler DetailsMushroomMushroom;
        public event EventHandler FilterMushroomMushroom;

        public void SetEventListDataSource(DataTable eventDataTable)
        {
            originalMushroomDataTable = eventDataTable;
            dataGridView1.DataSource = eventDataTable;
        }

    }
}
