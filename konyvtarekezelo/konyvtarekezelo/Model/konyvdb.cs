using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace konyvtarekezelo.Model
{
    internal class konyvdb
    {

        private int _id;
        private string _book_title;
        private string _author_name;
        private string _genre;
        private int _page_count;
        private int _price;

        public int Id { get => _id; set => _id = value; }
        public string Book_title { get => _book_title; set => _book_title = value; }
        public string Author_name { get => _author_name; set => _author_name = value; }
        public string Genre { get => _genre; set => _genre = value; }
        public int Page_count { get => _page_count; set => _page_count = value; }
        public int Price { get => _price; set => _price = value; }


        public konyvdb(int id, string book_title, string author_name, string genre, int page_count, int price)
        {
            Id = id;
            Book_title = book_title;
            Author_name = author_name;
            Genre = genre;
            Page_count = page_count;
            Price = price;
        }

        public konyvdb()
        {
            
        }
    }
}
