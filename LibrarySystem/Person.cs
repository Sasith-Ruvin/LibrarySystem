using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem
{
    public abstract class Person
    {
        public string FirstName { get;}
        public string LastName { get;}
        public string Gender { get;}
        public string Address { get; set; }
        public DateTime DateOfBirth { get;}

        public Person(string firstname, string lastName, string gender, string address, DateTime dateOfBirth)
        {
            FirstName = firstname;
            LastName = lastName;
            Gender = gender;
            Address = address;
            DateOfBirth = dateOfBirth;

        }

        public abstract override string ToString();
    }
}
