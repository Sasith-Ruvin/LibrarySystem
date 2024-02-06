using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem
{
    public class Librarian_UI
    {
        private Librarian_Manager librarianmanager;
        private Library library;

        public Librarian_UI(Librarian_Manager librarianmanager, Library library)
        {
            this.librarianmanager = librarianmanager;
            this.library = library;
        }

        //Display the Login Options to Librarian
        public void LibrarianLoginMenu()
        {
            Console.WriteLine("\n============================");
            Console.WriteLine("Librarian Login Menu");
            Console.WriteLine("==============================");
            Console.WriteLine("\n1.Login");
            Console.WriteLine("2.Exit");
            Console.Write("Enter Your Choice: ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    LibrarianLogin();
                    break;
                case "2":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid Input..Enter a Number 1-2");
                    LibrarianLoginMenu();
                    break;
            }
        }

        //Display the menu to Librarian to enter Login Details
        public void LibrarianLogin()
        {
            Console.WriteLine("\n============================");
            Console.WriteLine("Librarian Login");
            Console.WriteLine("==============================");
            Console.Write("\nEnter Username: ");
            string adminUsername = Console.ReadLine();

            Console.Write("Enter Password: ");
            string adminPassword = Console.ReadLine();

            bool adminLoginResult = librarianmanager.ValidateAdminLogin(adminUsername, adminPassword);
            if (adminLoginResult)
            {
                LibrarianMenu(librarianmanager.GetLoggedAdmin());
            }
            else
            {
                LibrarianLoginMenu();
            }
        }

        //Display the Main Options to Librarian
        public void LibrarianMenu(Librarian librarian)
        {
            while (true)
            {
                Console.WriteLine("\n=================================");
                Console.WriteLine("Librarian Menu");
                Console.WriteLine("===================================");
                Console.WriteLine("\n1.Your Details");
                Console.WriteLine("2.Check Members");
                Console.WriteLine("3.Display Books on Loan");
                Console.WriteLine("4.Display Available Books");
                Console.WriteLine("5.Add a Book");
                Console.WriteLine("6.Remove a Book");
                Console.WriteLine("7.Return a Book");
                Console.WriteLine("8.Remove a Member");
                Console.WriteLine("9.Log out");
                Console.Write("Enter Your Choice: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        librarian.ToString(); 
                        break;
                    case "2":
                        library.DisplayMembers();
                        break;
                    case "3":
                        library.DisplayAllLoans();
                        break;
                    case "4":
                        library.DisplayAvailableBooks();
                        break;
                    case "5":
                        AddBookMenu();
                        break;
                    case "6":
                        RemoveBookMenu();
                        break;
                    case "7":
                        library.DisplayMembers();
                        ReturnBookMenu();
                        break;
                    case "8":
                        RemoveMemberMenu();
                        break;
                    case "9":
                        Console.WriteLine("Logging Out...");
                        LibrarianLoginMenu();
                        break;
                    default:
                        Console.WriteLine("Enter a Number Between 1-9.Please try again");
                        break;                       
                }             
            }
        }

        //Display the Return Book Menu and take user inputs
        public void ReturnBookMenu()
        {
            Console.Write("Enter Member ID: ");
            string returnMemberID = Console.ReadLine();

            Member member = library.Members.Find(m => m.MemberID == returnMemberID);

            if (member == null)
            {
                Console.WriteLine("Member with Entered ID is not Found!");
                return;
            }
            else
            {
                if(member.PersonalLoans.Count == 0)
                {
                    Console.WriteLine("Member Does not have any Loans");
                    return;
                }
                else
                {
                    Console.WriteLine("\nPersonal Loans:");
                    Console.WriteLine($"\nMember: {member.FirstName} {member.LastName}");
                    Console.WriteLine("==============================================================================");
                    Console.WriteLine($"|{"Book ID",-25} | {"Title",-25} | {"Due Date",-20}");
                    Console.WriteLine("==============================================================================");

                    foreach (Loan loan in member.PersonalLoans)
                    {
                        Console.WriteLine($"{loan.BorrowedBook.BookID} | {loan.BorrowedBook.Title} | {loan.DueDate}");
                        Console.WriteLine("------------------------------------------------------------------------------");
                    }
                    Console.Write("Enter Book ID: ");
                    string returnBookID = Console.ReadLine();
                    librarianmanager.ReturnBook(returnMemberID, returnBookID);
                }      
            }           
        }

        //Display the AddBook Menu and take user inputs
        public void AddBookMenu()
        {
            Console.WriteLine("\n=================================");
            Console.WriteLine("Add a Book");
            Console.WriteLine("===================================");


            Console.Write("\nEnter Book ID: ");
            string addedbookID = Console.ReadLine();

            Console.Write("Enter Book Title: ");
            string addedBookTitle = Console.ReadLine();

            Console.Write("Enter Book Author: ");
            string addedBookAuthor = Console.ReadLine();


            Console.Write("Enter Book ISBN: ");
            int isbn;
            while (true)
            {
                if(int.TryParse(Console.ReadLine(), out isbn))
                {
                    break;
                   
                }
                else
                {
                    Console.WriteLine("ISBN can only contain Numbers. Please try agai");
                }
            }
            librarianmanager.AddBooksToLibrary(addedbookID, addedBookTitle, addedBookAuthor, isbn);

        }

        //Display the Remove Book Menu and take User inputs
        public void RemoveBookMenu()
        {
            Console.WriteLine("\n=================================");
            Console.WriteLine("Remove a Book");
            Console.WriteLine("===================================");
            library.DisplayAvailableBooks();
            Console.Write("\nEnter ID of the Book to remove: ");
            string bookIDToRemove = Console.ReadLine();

            librarianmanager.RemoveABook(bookIDToRemove);
        }

        //Display Remove Memebr menu and take the user inputs
        public void RemoveMemberMenu()
        {
            Console.WriteLine("\n=================================");
            Console.WriteLine("Remove a Member");
            Console.WriteLine("===================================");
            library.DisplayMembers();
            Console.Write("\nEnter ID of the Member to Remove: ");
            string memberIDToRemove = Console.ReadLine();
            librarianmanager.RemoveMember(memberIDToRemove);
        }
    }
}
