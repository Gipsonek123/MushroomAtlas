namespace MushroomAtlas.Model
{
    public interface IMushroomModel
    {
        string ClassDescription { get; set; }
        string ClassName { get; set; }
        string CommonName { get; set; }
        string EdibilityStatus { get; set; }
        string EdibilityDescription { get; set; }
        string FamilyDescription { get; set; }
        string FamilyName { get; set; }
        string GenusDescription { get; set; }
        string GenusName { get; set; }
        string HabitatDescription { get; set; }
        string HabitatName { get; set; }
        string MushroomDescription { get; set; }
        string RowDescription { get; set; }
        string RowName { get; set; }
        string ScientificName { get; set; }
        string TypeDescription { get; set; }
        string TypeName { get; set; }
    }
}