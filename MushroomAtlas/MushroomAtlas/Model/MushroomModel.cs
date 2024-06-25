using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace MushroomAtlas.Model
{
    public class MushroomModel : IMushroomModel
    {
        //grzyb
        public string ScientificName { get; set; }
        public string CommonName { get; set; }
        public string MushroomDescription { get; set; }
        //jadalnosc
        public string EdibilityStatus { get; set; }
        public string EdibilityDescription { get; set; }
        //klasa
        public string ClassName { get; set; }
        public string ClassDescription { get; set; }
        //rodzaj
        public string GenusName { get; set; }
        public string GenusDescription { get; set; }
        //rodzina
        public string FamilyName { get; set; }
        public string FamilyDescription { get; set; }
        //rząd
        public string RowName { get; set; }
        public string RowDescription { get; set; }
        //siedlisko
        public string HabitatName { get; set; }
        public string HabitatDescription { get; set; }
        //typ
        public string TypeName { get; set; }
        public string TypeDescription { get; set; }
        //konstruktory
        public MushroomModel(string scientificName, string commonName,  string edibility, string habitatName, string genusName, string familyName, string rowName, string className, string typeName, string mushroomDescription)
        {
            ScientificName = scientificName;
            CommonName = commonName;
            EdibilityStatus = edibility;
            HabitatName = habitatName;
            GenusName = genusName;
            FamilyName = familyName;
            RowName = rowName;
            ClassName = className;
            TypeName = typeName;
            MushroomDescription = mushroomDescription;
        }
        public MushroomModel()
        {

        }
    }
}
