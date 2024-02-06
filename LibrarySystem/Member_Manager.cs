using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem
{
    public class Member_Manager
    {

        private Library library;
        private Member currentLogMember;
        
        public Member_Manager(Library library)
        {
            this.library = library;
        }

        public Member GetLoggedMember()
        {
            return currentLogMember;
        }
        //Validating Username and Password and validate Member Login
        public bool ValidateMemberLogin(string username, string password)
        {
            List<Member> members = library.GetMembers();

            Member member = members.Find(m => m.GetCredentials() == username && m.GetPassword() == password);

            if (member != null)
            {
                currentLogMember = member;
                return true;
            }
            else
            {
                Console.WriteLine("\nError: Username or Password is incorrect.");
                return false;
            }
         }

        //Checks if the Username entered in Member Signup is already taken
        public bool CheckUserNameTaken(string userName)
        {
            return library.Members.Any(m => m.GetCredentials() == userName);
        }

        //Saving the Entered Details on Member signup in Members List
        public void RegisterMember(string fname, string lname, string gender, string address, string userName, string password, DateTime dob) 
        {
            double overdue = 0.0;
            string memberID = "MEM" + (library.Members.Count + 1).ToString("D3");
            library.AddAndSaveMember(memberID, userName, password, overdue, fname, lname, gender, address, dob);
        }

        //Display the Logged in Memebr's Personal Loans
        public void DisplayPersonalLoans(Member member)
        {
            if (member.PersonalLoans == null || member.PersonalLoans.Count == 0)
            {
                Console.WriteLine("\nPersonal Loans");
                Console.WriteLine("==============================================================================");
                Console.WriteLine("\nYou dont have Loaned Books");
            }
            else
            {
                Console.WriteLine("\nPersonal Loans");
                Console.WriteLine("==============================================================================");
                Console.WriteLine($"|{"Loan ID",-10} |{"Book ID",-10} | {"Title",-25} | {"Due Date",-20}");
                Console.WriteLine("==============================================================================");

                foreach (Loan loan in member.PersonalLoans)
                {
                    Console.WriteLine($"{loan.LoanID,-10}| {loan.BorrowedBook.BookID, -10} | {loan.BorrowedBook.Title, -25} | {loan.DueDate}");
                    Console.WriteLine("------------------------------------------------------------------------------");
                }

            }
        }

        //Method handles the Borrowing Book Process
        public bool BorrowBook(Member member, Book book)
        {
            if(!book.Availability)
            {
                Console.WriteLine($"\nError: The selected book ({book.Title}) is already on loan. Please choose another book.");
                return false;
            }
            if(member.PersonalLoans.Count > 5)
            {
                Console.WriteLine("\nCannot borrow books. Maximum number of personal loans reached.");
                return false;
            }

            if(member.Overdue > 100)
            {
                Console.WriteLine("\nCannot borrow books. Overdue amount exceeds the limit.");
                return false;
            }

            DateTime issuedate = DateTime.Now;

            DateTime duedate = DateTime.Now.AddDays(14);

            Loan newLoan = new Loan(member, book,issuedate, duedate);

            member.PersonalLoans.Add(newLoan);
            Loan.AddLoan(newLoan);
            book.Availability = false;

            Console.WriteLine($"\nBook {book.Title} was borrowed successfully.\nDue Date {duedate.ToShortDateString()}");
            return true;

        }
    }
}
