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
        private string _book_title;
        private string _author_name;
        private string _genre;
        private int _page_count;
        private int _price;
        private int _mufajdb;

        public int Id { get => _id; set => _id = value; }
        public string Book_title { get => _book_title; set => _book_title = value; }
        public string Author_name { get => _author_name; set => _author_name = value; }
        public string Genre { get => _genre; set => _genre = value; }
        public int Page_count { get => _page_count; set => _page_count = value; }
        public int Price
        {
            get { return _price; }
            set
            {
                if (value >= 1000 && value <= 5000)
                {
                    _price = value;
                }
            }
        }
        public int MufajDb
        {
            get { return _mufajdb; }
            set { _mufajdb = value; }
        }


        public konyv(int id, string book_title, string author_name, string genre, int page_count, int price)
        {
            Id = id;
            Book_title = book_title;
            Author_name = author_name;
            Genre = genre;
            Page_count = page_count;
            Price = price;
        }

        public konyv(int id, string genre, int mufajdb)
        {
            Id = id;
            Genre = genre;
            MufajDb = mufajdb;
        }

        public konyv()
        {

        }

        public bool Olcso()
        {
            if (Price <= 1300)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool RovidFikcio()
        {
            return Page_count < 200 && Genre == "fiction" ? true : false;
        }


        public override string ToString()
        {
            return $"Könyv címe: {Book_title}, szerző neve: {Author_name}, műfaja: {Genre}, oldalak száma: {Page_count}, ára: {Price}";
        }
    }
}