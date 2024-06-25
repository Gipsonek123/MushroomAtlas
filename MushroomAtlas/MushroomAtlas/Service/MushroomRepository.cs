using MySql.Data.MySqlClient;
using MushroomAtlas.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace MushroomAtlas.Service
{
    public class MushroomRepository : IMushroomRepository
    {
        private readonly string connectionString;

        public MushroomRepository()
        {
            connectionString = "Server=localhost;Database=leksykongrzybow;User ID=root;SslMode=none;";
        }

        public IEnumerable<MushroomModel> GetAllAsEnumerable()
        {
            var mushrooms = new List<MushroomModel>();

            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string selectCommand = "SELECT * FROM grzyb, jadalnosc, siedlisko, rodzaj, rodzina, rzad, klasa, typ WHERE grzyb.ID_Jadalnosci=jadalnosc.ID_Jadalnosci and grzyb.ID_Siedliska=siedlisko.ID_Siedliska and grzyb.ID_Rodzaju=rodzaj.ID_Rodzaju and rodzaj.ID_Rodziny=rodzina.ID_Rodziny and rodzina.ID_Rzedu=rzad.ID_Rzedu and rzad.ID_Klasy=klasa.ID_Klasy and klasa.ID_Typu=typ.ID_Typu";

                using (var command = new MySqlCommand(selectCommand, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        mushrooms.Add(new MushroomModel
                        {
                            ScientificName = reader.GetString("Nazwa_Naukowa"),
                            CommonName = reader.GetString("Nazwa_Potoczna"),
                            EdibilityStatus = reader.GetString("Status_Jadalnosci"),
                            HabitatName = reader.GetString("Nazwa_Siedliska"),
                            GenusName = reader.GetString("Nazwa_Rodzaju"),
                            FamilyName = reader.GetString("Nazwa_Rodziny"),
                            RowName = reader.GetString("Nazwa_Rzedu"),
                            ClassName = reader.GetString("Nazwa_Klasy"),
                            TypeName = reader.GetString("Nazwa_Typu"),
                            MushroomDescription = reader.GetString("Opis")
                        });
                    }
                }
            }

            return mushrooms;
        }

        public DataTable GetAllAsDataTable()
        {
            var mushroomsTable = new DataTable();
            mushroomsTable.Columns.Add("Nazwa Naukowa", typeof(string));
            mushroomsTable.Columns.Add("Nazwa Potoczna", typeof(string));
            mushroomsTable.Columns.Add("Jadalność", typeof(string));
            mushroomsTable.Columns.Add("Nazwa Siedliska", typeof(string));
            mushroomsTable.Columns.Add("Nazwa Rodzaju", typeof(string));
            mushroomsTable.Columns.Add("Nazwa Rodziny", typeof(string));
            mushroomsTable.Columns.Add("Nazwa Rzędu", typeof(string));
            mushroomsTable.Columns.Add("Nazwa Klasy", typeof(string));
            mushroomsTable.Columns.Add("Nazwa Typu", typeof(string));

            foreach (var mushroom in GetAllAsEnumerable())
            {
                mushroomsTable.Rows.Add(mushroom.ScientificName, mushroom.CommonName, mushroom.EdibilityStatus, mushroom.HabitatName, mushroom.GenusName, mushroom.FamilyName, mushroom.RowName, mushroom.ClassName, mushroom.TypeName);
            }

            return mushroomsTable;
        }

        public void Add(MushroomModel mushroom)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                int typeId = GetOrInsertId(connection, "typ", "Nazwa_Typu", mushroom.TypeName, "ID_Typu", mushroom.TypeDescription);

                int classId = GetOrInsertId(connection, "klasa", "Nazwa_Klasy", mushroom.ClassName, "ID_Klasy", mushroom.ClassDescription, "ID_Typu", typeId);

                int rowId = GetOrInsertId(connection, "rzad", "Nazwa_Rzedu", mushroom.RowName, "ID_Rzedu", mushroom.RowDescription, "ID_Klasy", classId);

                int familyId = GetOrInsertId(connection, "rodzina", "Nazwa_Rodziny", mushroom.FamilyName, "ID_Rodziny", mushroom.FamilyDescription, "ID_Rzedu", rowId);

                int genusId = GetOrInsertId(connection, "rodzaj", "Nazwa_Rodzaju", mushroom.GenusName, "ID_Rodzaju", mushroom.GenusDescription, "ID_Rodziny", familyId);

                int habitatId = GetOrInsertId(connection, "siedlisko", "Nazwa_Siedliska", mushroom.HabitatName, "ID_Siedliska");

                int edibilityId = GetOrInsertId(connection, "jadalnosc", "Status_Jadalnosci", mushroom.EdibilityStatus, "ID_Jadalnosci", mushroom.EdibilityDescription);

                string insertMushroomCommand = "INSERT INTO grzyb (Nazwa_Naukowa, Nazwa_Potoczna, Opis, ID_Rodzaju, ID_Siedliska, ID_Jadalnosci) " +
                                               "VALUES (@ScientificName, @CommonName, @MushroomDescription, @GenusId, @HabitatId, @EdibilityId)";

                using (var command = new MySqlCommand(insertMushroomCommand, connection))
                {
                    command.Parameters.AddWithValue("@ScientificName", mushroom.ScientificName);
                    command.Parameters.AddWithValue("@CommonName", mushroom.CommonName);
                    command.Parameters.AddWithValue("@MushroomDescription", mushroom.MushroomDescription);
                    command.Parameters.AddWithValue("@GenusId", genusId);
                    command.Parameters.AddWithValue("@HabitatId", habitatId);
                    command.Parameters.AddWithValue("@EdibilityId", edibilityId);
                    command.ExecuteNonQuery();
                }
            }
        }

        private int GetOrInsertId(MySqlConnection connection, string tableName, string nameColumn, string nameValue, string idColumn, string descriptionValue = null, string foreignKeyColumn = null, int? foreignKeyValue = null)
        {
            string selectCommandText = $"SELECT {idColumn} FROM {tableName} WHERE {nameColumn} = @name";
            using (var selectCommand = new MySqlCommand(selectCommandText, connection))
            {
                selectCommand.Parameters.AddWithValue("@name", nameValue);
                var result = selectCommand.ExecuteScalar();
                if (result != null)
                {
                    return Convert.ToInt32(result);
                }
            }

            string insertCommandText = $"INSERT INTO {tableName} ({nameColumn}, Opis{(foreignKeyColumn != null ? ", " + foreignKeyColumn : "")}) " +
                                       $"VALUES (@name, @description{(foreignKeyColumn != null ? ", @foreignKey" : "")}); " +
                                       $"SELECT LAST_INSERT_ID();";

            using (var insertCommand = new MySqlCommand(insertCommandText, connection))
            {
                insertCommand.Parameters.AddWithValue("@name", nameValue);
                insertCommand.Parameters.AddWithValue("@description", descriptionValue ?? (object)DBNull.Value);
                if (foreignKeyColumn != null)
                {
                    insertCommand.Parameters.AddWithValue("@foreignKey", foreignKeyValue);
                }
                return Convert.ToInt32(insertCommand.ExecuteScalar());
            }
        }


        public void Remove(string mushroomId)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                string deleteCommand = "DELETE FROM grzyb WHERE Nazwa_Naukowa = @Id";

                using (var command = new MySqlCommand(deleteCommand, connection))
                {
                    command.Parameters.AddWithValue("@Id", mushroomId);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Filter(DataTable dataTable, string pick, string filterText)
        {
            dataTable.DefaultView.RowFilter = string.Format("[{0}] LIKE '%{1}%'", pick, filterText);
        }

        public void Details(string name)
        {
            using (var connection = new MySqlConnection(connectionString))
            {
                string desc=string.Empty;
                connection.Open();

                string selectCommand = "SELECT Nazwa_Potoczna, Opis FROM grzyb WHERE Nazwa_Naukowa = @Id";

                using (var command = new MySqlCommand(selectCommand, connection))
                {
                    command.Parameters.AddWithValue("@Id", name);
                    command.ExecuteNonQuery();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            desc = desc + reader.GetString("Nazwa_Potoczna") + "\n";
                            desc = desc + reader.GetString("Opis") + "\n";
                        }
                        MessageBox.Show(desc);
                    }
                }
                
            }
        }
    }
}