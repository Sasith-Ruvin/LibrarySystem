using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem
{
    public class Member_UI
    {
        private Member_Manager memberManager;
        private Library library;


        public Member_UI(Member_Manager memberManager, Library library)
        {
            this.memberManager = memberManager;
            this.library = library;
        }

        //Display Main Login Options to Member
        public void MemberLoginMenu()
        {
            Console.WriteLine("\n============================");
            Console.WriteLine("Member Login Menu");
            Console.WriteLine("==============================");
            Console.WriteLine("\n1.Login");
            Console.WriteLine("2.Sign up");
            Console.WriteLine("3.Exit");
            Console.Write("Enter your Choice: ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case"1":
                    MemberLogin();
                    break;
                case"2":
                    MemberSignUp();
                    break;
                case"3":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid Input.. Please Only Select Options Available. Enter a Number between 1-3");
                    MemberLoginMenu();
                    break;
            }
        }

        //Display the Menu for Memebr to Enter Username and Password to Login
        public void MemberLogin()
        {
            Console.WriteLine("\n=================================");
            Console.WriteLine("Member Login");
            Console.WriteLine("===================================");
            Console.Write("\nEnter Your Username: ");
            string username = Console.ReadLine();

            Console.Write("Enter Your Password: ");
            string password = Console.ReadLine();

            bool loginResult = memberManager.ValidateMemberLogin(username, password);
            if(loginResult)
            {
                MemberMenu(memberManager.GetLoggedMember());
            }
            else
            {
                MemberLoginMenu();
            }         
        }

        //Display the Member Options
        public void MemberMenu(Member member)
        {
            while(true)
            {
                Console.WriteLine("\n=================================");
                Console.WriteLine("Member Menu");
                Console.WriteLine("===================================");
                Console.WriteLine($"WELCOME!! {member.FirstName}");
                Console.WriteLine("\n1.Your Details");
                Console.WriteLine("2.Display Books");
                Console.WriteLine("3.Display Your Loaned Books");
                Console.WriteLine("4.Borrow a Book");
                Console.WriteLine("5.Check Overdue");
                Console.WriteLine("6.Log out");
                Console.Write("Enter Your Choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        member.ToString(); 
                        break;
                    case "2":
                        library.DisplayAvailableBooks();
                        break;
                    case "3":
                        memberManager.DisplayPersonalLoans(member);
                        break;
                    case "4":
                        BorrowBookMenu(member);
                        break;
                    case "5":
                        DisplayMemberOverdue(member);
                        break;
                    case "6":
                        Console.WriteLine("Logging Out...");
                        Console.WriteLine("Thank you for using library system");
                        MemberLoginMenu();
                        break;
                    default: 
                        Console.WriteLine("Invalid Input.. Please enter a number between 1-7");
                        MemberMenu(member);
                        break;
                }
            }
        }

        //display the borrow book menu
        public void BorrowBookMenu(Member member)
        {
            library.DisplayAvailableBooks();
            Console.Write("\nEnter the ID of the Book you want: ");
            string bookIDBorrowed = Console.ReadLine();
            Book selectedBook = library.AvailableBooks.Find(b=>b.BookID==bookIDBorrowed);
            if (selectedBook != null)
            {
               memberManager.BorrowBook(member, selectedBook);
                
            }
            else
            {
                Console.WriteLine("\nInvalid Book ID. Please Select one from the list or check again");
                BorrowBookMenu(member);
            }
        }

        //Display Member's Overdue amount
        public void DisplayMemberOverdue(Member member)
        {
            Console.WriteLine("\nOverdue Amount");
            Console.WriteLine("==============================");
            Console.WriteLine($"Name: {member.FirstName} {member.LastName}");
            Console.WriteLine($"Overdue Amount: {member.Overdue}");
            Console.WriteLine("==============================");
        }

        //Display the Member Sign up menu
        public void MemberSignUp()
        {
            Console.WriteLine("\n============================");
            Console.WriteLine("Member Sign Up");
            Console.WriteLine("==============================");
            Console.Write("Enter First Name: ");
            string fname = Console.ReadLine();
            Console.Write("Enter Last Name: ");
            string lname = Console.ReadLine();
            Console.Write("Enter Your Gender: ");
            string gender = Console.ReadLine();
            Console.Write("Enter Your Address: ");
            string address = Console.ReadLine();
            Console.Write("Enter a Username: ");

            string userName;
            while(true)
            {
                userName = Console.ReadLine();
                if (memberManager.CheckUserNameTaken(userName))
                {
                    Console.WriteLine("Username is already taken. Please choose a different name");
                    Console.Write("Enter a Username: ");

                } else
                {
                    break;
                }

            }
           
             Console.Write("Enter a Password: ");
             string password = Console.ReadLine();
             Console.Write("Enter Date of Birth: ");
             DateTime dob;
             while(true )
             {
                Console.Write("Enter Date of Birth (YYYY-MM-DD): ");
                if (DateTime.TryParse(Console.ReadLine(), out dob))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("\nInvalid date format. Please enter the date of birth in (YYYY-MM-DD) format");
                    Console.Write("Enter Date of Birth: ");
                }
             }
            memberManager.RegisterMember(fname, lname, gender, address, userName, password, dob);
            Console.WriteLine("\nCongratulation!! You have Successfully created an Account. Proceed to Login to use the Library System");
            MemberLoginMenu();
        }        
    }
}
