using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem
{
    public class Librarian:Person
    {
        public string StaffID {  get; set; }
        private string AdminUserName {  get; set; }
        private string AdminPassword { get; set; }

        public DateTime DateJoined { get;}

        public double Salary {  get; set; }

        public Librarian(string staffID, string adminUserName, string adminPassword, DateTime dateJoined, double salary,string firstname, string lastName, string gender, string address, DateTime dateOfBirth):base(firstname, lastName, gender, address, dateOfBirth)
        {
            StaffID = staffID;
            AdminUserName = adminUserName;
            AdminPassword = adminPassword;
            DateJoined = dateJoined;
            Salary = salary;
        }
        public string GetAdminUsername()
        {
            return AdminUserName;
        }
        public string GetAdminPassword()
        {
            return AdminPassword;
        }
        public override string ToString()
        {
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine($"\nStaff ID: {StaffID}");
            Console.WriteLine($"{FirstName}:{LastName}");
            Console.WriteLine($"{Gender}");
            Console.WriteLine($"{DateOfBirth}");
            Console.WriteLine($"{Salary}");
            return base.FirstName + " " + base.LastName;
        }
    }
}
