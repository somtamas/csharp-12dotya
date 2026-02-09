using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace konyvtarekezelo.Model
{
    internal class DataBase
    {
        private static string connectionString;
        private static string table;
        private static string queryParameters;


        public static void DBConnectionCheck(string connectionString)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    Console.WriteLine("Sikeres csatlakozás");
                }
            }
            catch (Exception ex)
            {
                {
                    Console.WriteLine("Sikertelen csatlakozás: " + ex.Message);
                }
            }

        }


        public static DataTable GetData(string tabelname, string connectionString)
        {
            using var connection = new MySqlConnection(connectionString);
            connection.Open();

            using var command = new MySqlCommand("select * from " + tabelname, connection);

            using var reader = command.ExecuteReader();
            var dataTable = new DataTable();

            dataTable.Load(reader);


            return dataTable;
        }


        public static List<Konyvdb> AdatokbetoltesDB(DataTable dbadatok)
        {
            List<Konyvdb> konyvek = new List<Konyvdb>();
            foreach (DataRow row in dbadatok.Rows)
            {
                Konyvdb k = new Konyvdb();
                k.Id = row.Field<int>(0);
                k.Book_title = row.Field<string>(1);
                k.Author_name = row.Field<string>(2);
                k.Genre = row.Field<string>(3);
                k.Page_count = row.Field<int>(4);
                k.Price = row.Field<int>(5);
                konyvek.Add(k);
            }

            return konyvek;
        }

    }
}
