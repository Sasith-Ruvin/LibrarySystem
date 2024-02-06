using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem
{
    public class Library
    {
        public List<Book> AvailableBooks { get; set; }
        public List<Member> Members { get; set; }
        public List<Librarian> Librarians { get; set; }

        private const string MembersFileName = "members.txt";
        private const string BooksFileName = "books.txt";



        public Library()
        {
            AvailableBooks = new List<Book>();
            Members = new List<Member>();
            Librarians = new List<Librarian>();
            
        }

        public List<Member> GetMembers()
        {
            return Members;
        }

        public List<Librarian> GetStaff()
        {
            return Librarians;
        }


        //Saving Members to a Text File
        public void SaveMembersToFile()
        {
            List<string> existingMemberIDs = new List<string>();
            string filepath = Path.Combine(Directory.GetCurrentDirectory(), MembersFileName);

            if (File.Exists(filepath))
            {
                using (StreamReader reader = new StreamReader(MembersFileName))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] memberData = line.Split(',');
                        string memberID = memberData[0];
                        existingMemberIDs.Add(memberID);
                    }
                }
            }

            using (StreamWriter writer = new StreamWriter(MembersFileName, true))
            {
                foreach (Member member in Members)
                {
                    // Check if the member already exists in the file
                    if (!existingMemberIDs.Contains(member.MemberID))
                    {
                        writer.WriteLine($"{member.MemberID},{member.GetCredentials()},{member.GetPassword()},{member.Overdue},{member.FirstName},{member.LastName},{member.Gender},{member.Address},{member.DateOfBirth}");
                    }
                }
            }
        }

        //Loading Members from text file
        public void LoadMembersFromFile()
        {
            string filepath = Path.Combine(Directory.GetCurrentDirectory(), MembersFileName);
            if (File.Exists(filepath))
            {
                using (StreamReader reader = new StreamReader(MembersFileName))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] parts = line.Split(',');
                        string memberID = parts[0];
                        if (Members.Any(m => m.MemberID == memberID))
                        {
                            continue;
                        }

                        Members.Add(new Member(memberID, parts[1], parts[2], double.Parse(parts[3]), parts[4], parts[5], parts[6], parts[7], DateTime.Parse(parts[8])));
                    }
                }
            }
        }

        
        //Adding Books to Available Books List
        public void AddBook(Book newBook)
        {
            AvailableBooks.Add(newBook);
           
            
        }
        //Removing Books from Available Books
        public void RemoveBook(Book book)
        {
            AvailableBooks.Remove(book);
        }

        //Saving Members to the Members List
        public void AddAndSaveMember(string memberID, string username, string password, double overdue, string firstname, string lastName, string gender, string address, DateTime dateOfBirth)
        {
           
            Member newMember = new Member(memberID, username, password, overdue, firstname, lastName, gender, address, dateOfBirth);
            Members.Add(newMember);
            SaveMembersToFile();
        }


        //Saving Librarians to Librarian List
        public void AddAndSaveLibrarian(string staffID, string adminUserName, string adminPassword, DateTime dateJoined, double salary, string firstname, string lastName, string gender, string address, DateTime dateOfBirth)
        {
            Librarian newlibrarian = new Librarian(staffID, adminUserName, adminPassword, dateJoined, salary, firstname, lastName, gender, address, dateOfBirth);
            Librarians.Add(newlibrarian);
        }

        //Method to display all the books in Library
        public void DisplayAvailableBooks()
        {
            Console.WriteLine("\nAvailable Books");
            Console.WriteLine("==============================================================================");
            Console.WriteLine($"|{"Book ID",-25} | {"Title",-25} | {"Author",-20} |{"Availability",-15}");
            Console.WriteLine("==============================================================================");

            foreach (Book book in AvailableBooks)
            {
                Console.WriteLine($"|{book.BookID,-25} | {book.Title,-25} | {book.Author,-20} | {book.Status,-15}");
                Console.WriteLine("------------------------------------------------------------------------------");
            }
        }

        //Method to Display all the Members in Library
        public void DisplayMembers()
        {
            Console.WriteLine("\nLibrary Members");
            Console.WriteLine("==================================================================================");
            Console.WriteLine($"|{"ID",-10} | {"Name",-25} | {"Gender",-10} | {"Address",-25} | {"Overdue", -10}");
            Console.WriteLine("==================================================================================");

            foreach (Member member in Members)
            {
                Console.WriteLine($"|{member.MemberID, -10} | {member.FirstName + " " + member.LastName, -25} | {member.Gender, -10} | {member.Address, -25} | {member.Overdue, -10}");
                Console.WriteLine("--------------------------------------------------------------------------------");
            }

        }

        //Display all the Loaned Book details in Loans List
        public void DisplayAllLoans()
        {
            Console.WriteLine("\nAll Loans in the Library");
            Console.WriteLine("==============================================================================");
            Console.WriteLine($"{"LoanID",-10}|{"Member ID",-13} | {"Member Name",-20} | {"Book ID",-10} | {"Book Title",-20} | {"Issue Date", -10} | {"Due Date",-10}");
            Console.WriteLine("==============================================================================");

            foreach (Loan loan in Loan.AllLoans)
            {
                Console.WriteLine($"|{loan.LoanID,-10} | {loan.BorrowedMember.MemberID,-10} | {loan.BorrowedMember.FirstName + " " + loan.BorrowedMember.LastName,-20} | {loan.BorrowedBook.BookID,-10} | {loan.BorrowedBook.Title,-20} | {loan.IssueDate.ToShortDateString(), -10} | {loan.DueDate.ToShortDateString(),-10}");
                Console.WriteLine("------------------------------------------------------------------------------");
            }
        }

        //Method to Delete Members from text File
        public void DeleteMemberFromFile(string memberID)
        {
            string filepath = Path.Combine(Directory.GetCurrentDirectory(), MembersFileName);

            List<string> lines = File.ReadAllLines(filepath).ToList();

            int indexToRemove = -1;

            for (int i = 0; i < lines.Count; i++)
            {
                string[] parts = lines[i].Split(',');
                if (parts[0] == memberID)
                {
                    indexToRemove = i;
                    break;
                }
            }

            if (indexToRemove != -1)
            {
                lines.RemoveAt(indexToRemove);

                File.WriteAllLines(filepath, lines);
            }
        }




    }
}
