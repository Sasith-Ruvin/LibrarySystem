using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem
{
    public class Librarian_Manager
    {
        private Library library;
        private Librarian currentLoggedAdmin;

        public Librarian GetLoggedAdmin()
        {
            return currentLoggedAdmin;
        }
        public Librarian_Manager(Library library) 
        { 
            this.library = library; 
        }

        //Validates the Login Details Provided by user and Proceed to Log in Process
        public bool ValidateAdminLogin(string adminUsername, string adminPassword)
        {
            List<Librarian> librarians = library.GetStaff();

            Librarian librarian = librarians.Find(l => l.GetAdminUsername() == adminUsername && l.GetAdminPassword() == adminPassword);
            if (librarian != null)
            {
                currentLoggedAdmin = librarian;
                return true;
            }
            else
            {
                Console.WriteLine("Error. Invalid Username or Password..Please try again");
                return false;
            }
        }

        //Validates User inputs and Handles Return Book Process
        public void ReturnBook(string returnMemberID, string returnBookID)
        {
            Member member = library.Members.Find(m=>m.MemberID == returnMemberID);

            if(member == null)
            {
                Console.WriteLine("Member with Entered ID is not Found!");
                return;
            }
            else
            {
                Console.WriteLine("\nPersonal Loans:");
                Console.WriteLine($"\nMember: {member.FirstName} {member.LastName}");
                Console.WriteLine("==============================================================================");
                Console.WriteLine($"|{"Book ID",-25} | {"Title",-25} | {"Due Date",-20}");
                Console.WriteLine("==============================================================================");

                foreach(Loan loan in member.PersonalLoans)
                {
                    Console.WriteLine($"{loan.BorrowedBook.BookID} | {loan.BorrowedBook.Title} | {loan.DueDate}");
                    Console.WriteLine("------------------------------------------------------------------------------");
                }

                Loan bookInPersonalLoan = member.PersonalLoans.Find(l=>l.BorrowedBook.BookID == returnBookID);
                if(bookInPersonalLoan == null)
                {
                    Console.WriteLine("Book is not on current member's loan list. Enter a different ID or check again");
                    return;
                }
                else
                {
                    DateTime returnDate = DateTime.Now;

                    if (returnDate > bookInPersonalLoan.DueDate)
                    {
                        double overdueDays = (returnDate - bookInPersonalLoan.DueDate).TotalDays;
                        double overdueFine = overdueDays * 50;
                        member.Overdue += overdueFine;
                        Console.WriteLine($"Overdue fine of {overdueFine} was added to member's account");
                    }

                    member.PersonalLoans.Remove(bookInPersonalLoan);
                    Loan.AllLoans.Remove(bookInPersonalLoan);
                    bookInPersonalLoan.BorrowedBook.Availability = true;
                    Console.WriteLine("Book returned successfully");
                }

            }
        }

        //Validates user inputs and handles Adding Book Process and Add the book to Library
        public void AddBooksToLibrary(string addedBookID, string addedBookTitle, string addedBookAuthor, int isbn)
        {
            if (library.AvailableBooks.Any(b => b.BookID == addedBookID))
            {
                Console.WriteLine("Error: A book with same ID already exist in Library");
            }
            else
            {
                Book newBook = new Book(addedBookID, addedBookTitle, addedBookAuthor, isbn);

                library.AddBook(newBook);
                Console.WriteLine($"{newBook.Title} by {newBook.Author} was successfully added to the library.");
            }
        }

        //Vaidate User inputs and Handles the process of Removing the book from Library 
        public void RemoveABook(string removedBookID) 
        {
            Book bookToRemove = library.AvailableBooks.Find(b=>b.BookID == removedBookID);
            if (bookToRemove != null)
            {
                if(bookToRemove.Availability)
                {
                    library.RemoveBook(bookToRemove);
                    Console.WriteLine($"{bookToRemove.Title}  ID:{bookToRemove.BookID}was removed from Library");
                }
                else
                {
                    Console.WriteLine($"Error!. the book with ID: {removedBookID} is on loan");
                    return;
                    
                }
                
            }
            else
            {
                Console.WriteLine($"Error! No book with ID: {removedBookID} was not found. Please check the ID and try again..");
                return;
            }
        }

        //Vaidate User inputs and Handles the process of Removing the Member from Library 
        public void RemoveMember(string removedMemberID)
        {
            Member memberToRemove = library.Members.Find(m => m.MemberID == removedMemberID);
            if (memberToRemove != null)
            {
                library.Members.Remove(memberToRemove);
                library.DeleteMemberFromFile(removedMemberID);
                Console.WriteLine($"Member with ID:{memberToRemove.MemberID} {memberToRemove.FirstName + " " + memberToRemove.LastName} was successfully removed from Database");
         
            }
            else
            {
                Console.WriteLine($"Error: A Member with provided ID:{removedMemberID} was not found");
                return;
            }
        }
    }
}
