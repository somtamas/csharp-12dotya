using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace konyvtarekezelo.Model
{
    internal class konyv
    {
        private int _id;
        private string _nev;
        private string _szerzo;
        private int _oldalszam;
        private string _kategoria;
        private int _ar;



        public int Id { get => _id; set => _id = value; }
        public string Nev { get => _nev; set => _nev = value; }
        public string Szerzo { get => _szerzo; set => _szerzo = value; }
        public int Oldalszam { get => _oldalszam; set => _oldalszam = value; }
        public string Kategoria { get => _kategoria; set => _kategoria = value; }
        public int Ar { get => _ar; set => _ar = value; }

        public konyv(int id, string nev, string szerzo, int oldalszam, string kategoria, int ar)
        {
            Id = id;
            Nev = nev;
            Szerzo = szerzo;
            Oldalszam = oldalszam;
            Kategoria = kategoria;
            Ar = ar;
        }

        public konyv()
        {
            
        }


        public override string ToString()
        {
            return $"{Id}, {Nev}, {Szerzo}, {Oldalszam}, {Kategoria}, {Ar}";
        }
    }
}
