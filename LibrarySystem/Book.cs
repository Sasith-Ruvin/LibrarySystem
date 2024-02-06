using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem
{
    public class Book
    {
        public string BookID { get; }
        public string Title { get;}
        public string Author { get;}
        public int ISBN { get; } 
        public bool Availability { get; set; }
        public string Status => Availability ? "Available" : "On Loan";

        public Book(string bookID, string title, string author, int iSBN)
        {
            BookID = bookID;
            Title = title;
            Author = author;
            ISBN = iSBN;
            Availability = true;
        }
    }
}
