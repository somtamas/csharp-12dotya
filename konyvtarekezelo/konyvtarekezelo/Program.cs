using MySqlConnector;
using konyvtarekezelo.Model;
using System.Data;
using System.Runtime.InteropServices;

internal class Program
{
    public static FileReadDLL.ReadFromFile reader = new FileReadDLL.ReadFromFile();
    public static List<List<string>> adatok = new List<List<string>>();
    public static List<konyv> konyvek = new List<konyv>();
    public static List<konyv> konyvekdb = new List<konyv>();
    public static DataTable dbadatok = new DataTable();
    public static readonly string connectionString = "server=localhost;user=root;database=mock_data;";

    private static void Main(string[] args)
    {
        adatBeolvasas("könyvek.csv", 6, ',', true);
        adatBetoltes(adatok);

        ComedyOsszegzes(konyvek);

        AdatokbetoltesDB(dbadatok);
        DBCheck(connectionString);
        SelectFromTable("sokkonyv", connectionString);
    }

    private static void ComedyOsszegzes(List<konyv> konyvek)
    {
        var comedy = konyvek.FindAll(x => x.Genre == "comedy");
        Console.WriteLine(comedy);
    }

    private static void adatBetoltes(List<List<string>> adatok)
    {
        foreach (var item in adatok)
        {
            konyvek.Add(new konyv(int.Parse(item[0]), item[1], item[2], item[3], int.Parse(item[4]), int.Parse(item[5])));
        }
    }

    private static void adatBeolvasas(string v1, int v2, char v3, bool v4)
    {
        adatok = reader.FileRead(v1, v2, v3, v4);
    }

    private static void AdatokbetoltesDB(DataTable dbadatok)
    {
        foreach (DataRow row in dbadatok.Rows)
        {
            Konyvdb konyv = new Konyvdb();
            konyv.Id = row.Field<int>(0);
            konyv.Book_title = row.Field<string>(1);
            konyv.Author_name = row.Field<string>(2);
            konyv.Genre = row.Field<string>(3);
            konyv.Page_count = row.Field<int>(4);
            konyv.Price = row.Field<int>(5);
        }
    }



    private static void SelectFromTable(string tablename, string connectionString)
    {
        dbadatok = DataBase.GetData(tablename, connectionString);
        Console.WriteLine(".");
    }

    private static void DBCheck(string connectionString)
    {
        DataBase.DBConnectionCheck(connectionString);
    }
}