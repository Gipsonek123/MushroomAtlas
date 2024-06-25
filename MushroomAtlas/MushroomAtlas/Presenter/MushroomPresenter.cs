using System;
using System.Collections.Generic;
using System.Data;
using MushroomAtlas.Model;
using MushroomAtlas.Service;
using MushroomAtlas.View;

namespace MushroomAtlas.Presenter
{
    public class MushroomPresenter 
    {
        private readonly IMushroomRepository _repository;
        private readonly IMushroomView _view;
        private DataTable _mushroomDataTable;

        public MushroomPresenter(IMushroomView view, IMushroomRepository repository)
        {
            _view = view;
            _repository = repository;

            _view.AddNewMusroomMushroom += AddMushroom;
            _view.RemoveMusroomMushroom += RemoveMushroom;
            _view.DetailsMushroomMushroom += DetailsMushroom;
            _view.FilterMushroomMushroom += FilterMushroom;

            LoadAllMushroomList();
        }

        private void LoadAllMushroomList()
        {
            _mushroomDataTable = _repository.GetAllAsDataTable();
            _view.SetEventListDataSource(_mushroomDataTable);
        }

        private void AddMushroom(object sender, EventArgs e)
        {
            string scientificName = _view.ScientificName;
            string commonName = _view.CommonName;
            string edibility = _view.Edibility;
            string habitatName = _view.HabitatName;
            string genusName = _view.GenusName;
            string familyName = _view.FamilyName;
            string rowName = _view.RowName;
            string className = _view.ClassName;
            string typeName = _view.TypeName;
            string mushroomDescription = _view.MushroomDescription;

            _view.ErrorProvider.SetError(_view.ScientifNameControl, "");
            _view.ErrorProvider.SetError(_view.CommonNameControl, "");
            _view.ErrorProvider.SetError(_view.EdibilityControl, "");
            _view.ErrorProvider.SetError(_view.HabitatNameControl, "");
            _view.ErrorProvider.SetError(_view.GenusNameControl, "");
            _view.ErrorProvider.SetError(_view.FamilyNameControl, "");
            _view.ErrorProvider.SetError(_view.RowNameControl, "");
            _view.ErrorProvider.SetError(_view.ClassNameControl, "");
            _view.ErrorProvider.SetError(_view.TypeNameControl, "");
            _view.ErrorProvider.SetError(_view.MushroomDescriptionControl, "");

            bool isValid = true;
            if (string.IsNullOrWhiteSpace(scientificName))
            {
                _view.ErrorProvider.SetError(_view.ScientifNameControl, "To pole nie może być puste.");
                isValid = false;
            }
            if (string.IsNullOrWhiteSpace(commonName))
            {
                _view.ErrorProvider.SetError(_view.CommonNameControl, "To pole nie może być puste.");
                isValid = false;
            }
            if (string.IsNullOrWhiteSpace(edibility))
            {
                _view.ErrorProvider.SetError(_view.EdibilityControl, "To pole nie może być puste.");
                isValid = false;
            }
            else
            {
                // Validate edibility
                var validEdibilityValues = new List<string> { "Jadalny", "Trujący", "Niejadalny" };
                if (!validEdibilityValues.Contains(edibility))
                {
                    _view.ErrorProvider.SetError(_view.EdibilityControl, "Wartość jadalności musi być jedną z następujących: Jadalny, Trujący, Niejadalny.");
                    isValid = false;
                }
            }
            if (string.IsNullOrWhiteSpace(habitatName))
            {
                _view.ErrorProvider.SetError(_view.HabitatNameControl, "To pole nie może być puste.");
                isValid = false;
            }
            if (string.IsNullOrWhiteSpace(genusName))
            {
                _view.ErrorProvider.SetError(_view.GenusNameControl, "To pole nie może być puste.");
                isValid = false;
            }
            if (string.IsNullOrWhiteSpace(familyName))
            {
                _view.ErrorProvider.SetError(_view.FamilyNameControl, "To pole nie może być puste.");
                isValid = false;
            }
            if (string.IsNullOrWhiteSpace(rowName))
            {
                _view.ErrorProvider.SetError(_view.RowNameControl, "To pole nie może być puste.");
                isValid = false;
            }
            if (string.IsNullOrWhiteSpace(className))
            {
                _view.ErrorProvider.SetError(_view.ClassNameControl, "To pole nie może być puste.");
                isValid = false;
            }
            if (string.IsNullOrWhiteSpace(typeName))
            {
                _view.ErrorProvider.SetError(_view.TypeNameControl, "To pole nie może być puste.");
                isValid = false;
            }
            if (string.IsNullOrWhiteSpace(mushroomDescription))
            {
                _view.ErrorProvider.SetError(_view.MushroomDescriptionControl, "To pole nie może być puste.");
                isValid = false;
            }

            foreach (DataRow row in _mushroomDataTable.Rows)
            {
                if (row["Nazwa Naukowa"].ToString() == scientificName && row["Nazwa Potoczna"].ToString() == commonName)
                {
                    _view.ErrorProvider.SetError(_view.ScientifNameControl, "Istnieje już grzyb o takiej nazwie.");
                    _view.ErrorProvider.SetError(_view.CommonNameControl, "Istnieje już grzyb o takiej nazwie.");
                    isValid = false;
                    break;
                }
            }

            if (!isValid)
            {
                return;
            }

            var newMushroom = new MushroomModel(scientificName, commonName, edibility, habitatName, genusName, familyName, rowName, className, typeName, mushroomDescription);
            _repository.Add(newMushroom);
            LoadAllMushroomList();
        }


        private void RemoveMushroom(object sender, EventArgs e)
        {
            if (_view.SelectedRowIndex >= 0)
            {
                string scientificName = _mushroomDataTable.Rows[_view.SelectedRowIndex]["Nazwa Naukowa"].ToString();
                _repository.Remove(scientificName);
                LoadAllMushroomList();
            }
        }

        private void DetailsMushroom(object sender, EventArgs e)
        {
            if (_view.SelectedRowIndex >= 0)
            {
                string scientificName = _mushroomDataTable.Rows[_view.SelectedRowIndex]["Nazwa Naukowa"].ToString();
                _repository.Details(scientificName);
                LoadAllMushroomList();
            }
        }

        private void FilterMushroom(object sender, EventArgs e)
        {
            string filterText = _view.FilterText;
            string pick = _view.Pick;
            _repository.Filter(_mushroomDataTable, pick, filterText);
        }
    }
}