using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem
{
    public class Loan
    {
        public string LoanID { get; set; }
        public Member BorrowedMember { get; set; }
        public Book BorrowedBook { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime DueDate { get; set; }

        public static List<Loan> AllLoans { get; set; } = new List<Loan>();

        public Loan(Member borrowedMember, Book borrowedBook, DateTime issuedate, DateTime dueDate)
        {
            this.LoanID = "LOAN" + (AllLoans.Count + 1).ToString("D3");//Generates a new Loan ID Automatically
            BorrowedMember = borrowedMember;
            BorrowedBook = borrowedBook;
            IssueDate = issuedate;
            DueDate = dueDate;
        }
        public static void AddLoan(Loan loan)
        {
            AllLoans.Add(loan);
        }

    }
}
