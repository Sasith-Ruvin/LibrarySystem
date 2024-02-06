using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem
{
    internal class Program
    {
        public static Library library = new Library();
        public static Member_Manager memberManager = new Member_Manager(library);
        public static Member_UI member_UI = new Member_UI(memberManager, library);
        public static Librarian_Manager librarian_Manager = new Librarian_Manager(library);
        public static Librarian_UI librarian_UI = new Librarian_UI(librarian_Manager, library);


        static void MainMenu()
        {
            Console.WriteLine("\n============================");
            Console.WriteLine("Main Login Menu");
            Console.WriteLine("==============================");
            Console.WriteLine("\nSelect a login option");
            Console.WriteLine("\n1.Member Login");
            Console.WriteLine("2.Librarian Login");
            Console.WriteLine("3.Exit");
            Console.Write("\nEnter Your Choice: ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    member_UI.MemberLoginMenu();
                    
                    break;
                case "2":
                    librarian_UI.LibrarianLoginMenu();
                    break;
                case "3":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid Input, Please Enter a Number between 1-3 to select an option");
                   
                    break;

            }
        }
        static void Main(string[] args)
        {
            library.LoadMembersFromFile();
            Console.WriteLine("Welcome to Library Management System");
            library.AddAndSaveLibrarian("ST001","admin01","admin123",new DateTime(2023,12,1), 20000.00,"Anne","Croft","Female","Main Street",new DateTime(2000,1,1));
            library.AddBook(new Book("B001", "Lord of the Rings", "J.R.R Tolkien", 123456789));
            library.AddBook(new Book("B002", "The Hobbit", "J.R.R Tolkien", 123456789));

            Member exampleMember = library.Members.FirstOrDefault(member => member.MemberID == "MEM001");
            Book exampleBook = library.AvailableBooks.FirstOrDefault(book => book.BookID == "B001");

            if (exampleMember != null && exampleBook != null)
            {
                Loan exampleLoan = new Loan(exampleMember, exampleBook, DateTime.Now, DateTime.Now.AddDays(14));
                exampleMember.PersonalLoans.Add(exampleLoan);
                Loan.AddLoan(exampleLoan);

                exampleBook.Availability = false;
            }
            MainMenu();
            library.SaveMembersToFile();
            
        }
    }
}
