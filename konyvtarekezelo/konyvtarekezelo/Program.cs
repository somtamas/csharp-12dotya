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
    public static DataTable dbadatok = new DataTable();
    public static readonly string connectionString = "server=localhost;user=root;database=data;";

    private static void Main(string[] args)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        //File read
        adatBeolvasas("könyvek.csv", 6, ',', true);
        adatBetoltes(adatok);
        Console.WriteLine($"Kilépéshez nyomd meg az ESC billentyűt!\nA menú-ért nyomd meg az M billentyűt!\nA műfajok összegzéséhez nyomd meg a G billentyűt!{Environment.NewLine}Az olcsó és rövid fikció könyvekért nyomd meg a H billentyűt!\nA statisztikáért nyomd meg az S billentyűt!");
        Lepeget();


        //Database
        /* DataBase.DBConnectionCheck(connectionString);
         var data = DataBase.GetData("mock_data", connectionString);
         konyvekdb = DataBase.AdatokbetoltesDB(data);*/
        //foreach (var item in konyvekdb)
        //{
        //    Console.WriteLine(item.Book_title);
        //}
        Console.ReadKey();
    }

    private static void Lepeget()
    {
        int osszadat;
        int atlagar;
        List<konyv> osszmufajok = new List<konyv>();
        ConsoleKeyInfo keyInfo = Console.ReadKey();
        if (keyInfo.Key == ConsoleKey.M)
        {
            Console.WriteLine();
            Console.WriteLine("Menü...");
            Thread.Sleep(1000);
            Console.Clear();
            Console.WriteLine($"Kilépéshez nyomd meg az ESC billentyűt!\nA menú-ért nyomd meg az M billentyűt!\nA műfajok összegzéséhez nyomd meg a G billentyűt!{Environment.NewLine}Az olcsó és rövid fikció könyvekért nyomd meg a H billentyűt!\nA statisztikáért nyomd meg az S billentyűt!");
            Lepeget();
        }
        if (keyInfo.Key == ConsoleKey.Escape)
        {
            Console.WriteLine();
            Console.WriteLine("KKilépés...");
            Thread.Sleep(1000);
            Environment.Exit(0);
        }
        if (keyInfo.Key == ConsoleKey.G)
        {
            Console.WriteLine();
            Console.WriteLine("Műfajok összegzése...");
            Thread.Sleep(1000);
            MufajOsszegzes(konyvek, out osszadat, out atlagar, ref osszmufajok);
        }
        if (keyInfo.Key == ConsoleKey.H)
        {
            Console.WriteLine();
            Console.WriteLine("Olcsó és rövid fikció könyvek...");
            Thread.Sleep(1000);
            OlcsoesRovidfiktciok(konyvek);
        }
        if (keyInfo.Key == ConsoleKey.S)
        {
            Console.WriteLine();
            Console.WriteLine("Statisztika...");
            Thread.Sleep(1000);
            Statisztika(konyvek);
        }
    }

    private static void Statisztika(List<konyv> konyvek)
    {
        Console.Clear();
        int count = 0;
        foreach (var item in konyvek)
        {
            count += 1;
        }
        Console.WriteLine($"Hány könyv van a könyvtárban: {count}");
        Console.WriteLine();
        Console.WriteLine("A legdrágább könyv:");
        konyv legdragabbar = new konyv();
        foreach (var item in konyvek)
        {
            if (item.Price > legdragabbar.Price)
            {
                legdragabbar = item;
            }
        }
        Console.WriteLine(legdragabbar.ToString());
        Console.WriteLine();
        Console.WriteLine("A leghosszabb könyv:");
        konyv leghoszabbkonyv = new konyv();
        foreach (var item in konyvek)
        {
            if (item.Page_count > leghoszabbkonyv.Page_count)
            {
                leghoszabbkonyv = item;
            }
        }
        Console.WriteLine(leghoszabbkonyv.ToString());
        Lepeget();
    }

    private static void OlcsoesRovidfiktciok(List<konyv> konyvek)
    {
        Console.Clear();
        Console.BackgroundColor = ConsoleColor.Magenta;
        Console.ForegroundColor = ConsoleColor.DarkBlue;
        Console.WriteLine("Legolcsóbb könyvek:");
        foreach (var item in konyvek)
        {
            if (item.Olcso())
            {
                Console.WriteLine(item.ToString());
            }
        }
        Console.ResetColor();
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.BackgroundColor = ConsoleColor.Gray;
        Console.WriteLine("Rövid fikcionális könyvek:");
        foreach (var item in konyvek)
        {
            if (item.RovidFikcio())
            {
                Console.WriteLine(item.ToString());
            }
        }
        Console.ResetColor();
        Console.WriteLine();
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.Green;
        Lepeget();
    }

    private static void MufajOsszegzes(List<konyv> konyvek, out int osszadat, out int atlagar, ref List<konyv> osszmufajok)
    {
        Console.Clear();
        int mystery = 0;
        int osszmystery = 0;
        int fiction = 0;
        int osszfiction = 0;
        int romance = 0;
        int osszromance = 0;
        int non_fiction = 0;
        int ossznonfiction = 0;
        int sci_fi = 0;
        int osszsci_fi = 0;
        foreach (var item in konyvek)
        {
            if (item.Genre == "mystery")
            {
                mystery++;
                osszmystery += item.Price;
            }
            else if (item.Genre == "fiction")
            {
                fiction++;
                osszfiction += item.Price;
            }
            else if (item.Genre == "romance")
            {
                romance++;
                osszromance += item.Price;
            }
            else if (item.Genre == "non-fiction")
            {
                non_fiction++;
                ossznonfiction += item.Price;
            }
            else if (item.Genre == "sci-fi")
            {
                sci_fi++;
                osszsci_fi += item.Price;
            }
        }
        Console.WriteLine("|{0, -10}|{1, -10}|{2, -10}|", "Műfaj", "Darab", "Átlagár(Ft)");
        Console.WriteLine("|{0, -10}|{1, -10}|{2, -11}|", "Rejtély", mystery, Math.Round(Convert.ToDouble(osszmystery) / Convert.ToDouble(mystery), 0));
        Console.WriteLine("|{0, -10}|{1, -10}|{2, -11}|", "Fikció", fiction, Math.Round(Convert.ToDouble(osszfiction) / Convert.ToDouble(fiction), 0));
        Console.WriteLine("|{0, -10}|{1, -10}|{2, -11}|", "Nem-fikció", non_fiction, Math.Round(Convert.ToDouble(ossznonfiction) / Convert.ToDouble(non_fiction), 0));
        Console.WriteLine("|{0, -10}|{1, -10}|{2, -11}|", "Romantika", romance, Math.Round(Convert.ToDouble(osszromance) / Convert.ToDouble(romance), 0));
        Console.WriteLine("|{0, -10}|{1, -10}|{2, -11}|", "Sci-fi", sci_fi, Math.Round(Convert.ToDouble(osszsci_fi) / Convert.ToDouble(sci_fi), 0));
        osszadat = mystery + fiction + romance + non_fiction + sci_fi;
        atlagar = (osszmystery + osszfiction + osszromance + ossznonfiction + osszsci_fi) / (mystery + fiction + romance + non_fiction + sci_fi);
        osszmufajok.Add(new konyv(1, "Rejtély", mystery));
        osszmufajok.Add(new konyv(2, "Fikció", fiction));
        osszmufajok.Add(new konyv(3, "Romantika", romance));
        osszmufajok.Add(new konyv(4, "Nem-fikció", non_fiction));
        osszmufajok.Add(new konyv(5, "Sci-fi", sci_fi));
        Lepeget();
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