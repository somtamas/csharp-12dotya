using MySqlConnector;
using konyvtarekezelo.Model;
using System.Data;
using System.Runtime.InteropServices;

internal class Program
{
    public static FileReadDLL.ReadFromFile reader = new FileReadDLL.ReadFromFile();
    public static List<List<string>> adatok = new List<List<string>>();
    public static List<konyv> konyvek = new List<konyv>();
    public static List<Konyvdb> konyvekdb = new List<Konyvdb>();
    //public static DataTable dbadatok = new DataTable();
    public static readonly string connectionString = "server=localhost;user=root;database=data;";

    private static void Main(string[] args)
    {
        //File read
        adatBeolvasas("könyvek.csv", 6, ',', true);
        adatBetoltes(adatok);
        ComedyOsszegzes(konyvek);

        //Database
        DataBase.DBConnectionCheck(connectionString);
        var data = DataBase.GetData("mock_data", connectionString);
        konyvekdb = DataBase.AdatokbetoltesDB(data);
        //foreach (var item in konyvekdb)
        //{
        //    Console.WriteLine(item.Book_title);
        //}
        Console.ReadKey();
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
}