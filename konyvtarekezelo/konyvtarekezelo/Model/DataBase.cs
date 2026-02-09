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

    }
}
