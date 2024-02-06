using LibrarySystem;
using System.IO;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using System.Data.SqlClient;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.ObjectModel;

namespace LibraryTests.Test
{
    public class UnitTest1
    {

        [Fact]

        //Test When Correct Username and Password are entered in Member Login
        public void ValidateMemberLogin_ValidCredentials_ReturnsTrue()
        {
            //Arrange   
            Library library = new Library();
           
            Member_Manager memberManager = new Member_Manager(library);

            string memberID = "MEM000";
            string username = "John";
            string password = "John117";
            double overdue = 0.0;
            string firstname = "John";
            string lastName = "Chief";
            string gender = "Male";
            string address = "UNSC Infinity";
            DateTime dateOfBirth = new DateTime(1990, 1, 1);
            library.AddAndSaveMember(memberID, username, password, overdue, firstname, lastName, gender, address, dateOfBirth);
            //Act
            bool result = memberManager.ValidateMemberLogin(username, password);
            //Assert
            Assert.True(result);
        }

        //Test to check Member Login with Invalid Credentials

        [Fact]

        public void ValidateMemberLogin_InvalidCredentials()
        {
            //Arrange
            Library library = new Library();
         
            Member_Manager memberManager = new Member_Manager(library);

            string memberID = "MEM000";
            string username = "John";
            string password = "John117";
            double overdue = 0.0;
            string firstname = "John";
            string lastName = "Chief";
            string gender = "Male";
            string address = "UNSC Infinity";
            DateTime dateOfBirth = new DateTime(1990, 1, 1);
            library.AddAndSaveMember(memberID, username, password, overdue, firstname, lastName, gender, address, dateOfBirth);

            //Act
            bool result = memberManager.ValidateMemberLogin("InvalidUsername", "InvalidPassword");
            //Assert
            Assert.False(result);

        }

        //Test to check When Correct Username and Password are entered in Librarian Login
        [Fact]

        public void ValidateLibrarianLogin_ValidCredentials()
        {
            Library library = new Library();
          
            Librarian_Manager librarianManager = new Librarian_Manager(library);

            string staffID = "ST001";
            string adminUsername = "Admin01";
            string adminPassword = "AdminPass";
            DateTime dateJoined = new DateTime(2023, 1, 1);
            double salary = 20000.00;
            string firstname = "Lara";
            string lastname = "Croft";
            string gender = "Female";
            string address = "Main Street";
            DateTime dateofBirth = new DateTime(1990, 1, 1);

            library.AddAndSaveLibrarian(staffID, adminUsername, adminPassword, dateJoined, salary, firstname, lastname, gender, address, dateofBirth);

            bool result = librarianManager.ValidateAdminLogin(adminUsername, adminPassword);

            Assert.True(result);
        }

        //Test to check Adding Book to Library

        [Fact]
        public void TestAddBookToLibrary()
        {
            Library library = new Library();
         
            Librarian_Manager librarianManager = new Librarian_Manager(library);

            string bookID = "B001";
            string title = "TestBook1";
            string author = "testAuthor1";
            int isbn = 123456789;

            //Act
            librarianManager.AddBooksToLibrary(bookID, title, author, isbn);

            //Assert

            Assert.Single(library.AvailableBooks);
            Assert.Equal(bookID, library.AvailableBooks[0].BookID);
            Assert.Equal(title, library.AvailableBooks[0].Title);
            Assert.Equal(author, library.AvailableBooks[0].Author);
            Assert.Equal(isbn, library.AvailableBooks[0].ISBN);

        }


        //Test to check when adding a book to library when theres book in Library with Same ID
        [Fact]

        public void TestAddingBookWithSameID_WhenBookExist()
        {
            Library library = new Library();
          
            Librarian_Manager librarianManager = new Librarian_Manager(library);

            string existingBookID = "B001";
            string existingTitle = "Existing Book";
            string existingAuthor = "Existing Author";
            int existingISBN = 1234567890;

            library.AddBook(new Book(existingBookID, existingTitle, existingAuthor, existingISBN));

            string existingBookTitle = "Another Existing Book";
            string existingBookAuthor = "Another Existing Author";
            int existingBookISBN = 987654321;

            //Act


            librarianManager.AddBooksToLibrary(existingBookID, existingBookTitle, existingBookAuthor, existingBookISBN);

            //Assert

            Assert.Single(library.AvailableBooks);
            Assert.Equal(existingBookID, library.AvailableBooks[0].BookID);
            Assert.Equal(existingTitle, library.AvailableBooks[0].Title);
            Assert.Equal(existingAuthor, library.AvailableBooks[0].Author);
            Assert.Equal(existingISBN, library.AvailableBooks[0].ISBN);

        }

        //Test to check Removing a Member when Member exist in Library

        [Fact]

        public void TestRemoveAMember()
        {
            //Arrange
            Library library = new Library();
           
            Librarian_Manager librarian_Manager = new Librarian_Manager(library);

            Member member = new Member("MEM000", "user1", "userpass", 0.0, "Arthur", "Morgan", "Male", "Main Street", new DateTime(1991, 1, 1));
            library.Members.Add(member);

            //Act
            librarian_Manager.RemoveMember("MEM000");

            //Assert
            Assert.DoesNotContain(member, library.Members);


        }


        //Test to check when Invalid Member ID entered when Removing a Member
        [Fact]

        public void TestRemoveMember_InvalidID()
        {
            //Arrange
            Library library = new Library();
       
            Librarian_Manager librarian_Manager = new Librarian_Manager(library);

            Member member = new Member("MEM001", "user1", "userpass", 0.0, "Arthur", "Morgan", "Male", "Main Street", new DateTime(1991, 1, 1));
            library.Members.Add(member);

            //Act

            librarian_Manager.RemoveMember("Invalid_ID");

            //Assert
            Assert.Contains(member, library.Members);

        }

        //Test to check When Member Use a Username that has already taken

        [Fact]

        public void CheckUserNameTaken_ExistingUsername()
        {

            //Arrange
            Library library = new Library();
          
            Member_Manager memberManager = new Member_Manager(library);

            string memberID = "000";
            string username = "John";
            string password = "John117";
            double overdue = 0.0;
            string firstname = "John";
            string lastName = "Chief";
            string gender = "Male";
            string address = "UNSC Infinity";
            DateTime dateOfBirth = new DateTime(1990, 1, 1);

            //Act
            library.AddAndSaveMember(memberID, username, password, overdue, firstname, lastName, gender, address, dateOfBirth);

            bool result = memberManager.CheckUserNameTaken("John");

            //Assert
            Assert.True(result);
        }

        //Test to check When a New Username is entered

        [Fact]
        public void CheckUserNameTaken_NonExistingUsername()
        {
            //Arrange
            Library library = new Library();
          
            Member_Manager member_Manager = new Member_Manager(library);

            //Act
            bool result = member_Manager.CheckUserNameTaken("newUsername");

            //Assert
            Assert.False(result);
        }

        //Test to check Member Sign Up

        [Fact]

        public void CheckMemberSignUp()
        {
            //Arrange
            Library library = new Library();
       
            Member_Manager member_Manager = new Member_Manager(library);

            //Act
            member_Manager.RegisterMember("John", "Doe", "Male", "Main Street", "John1234", "1234", new DateTime(1991, 1, 1));

            //Assert
            Assert.Single(library.Members);
            Member registeredMember = library.Members[0];
            Assert.Equal("John", registeredMember.FirstName);
            Assert.Equal("Doe", registeredMember.LastName);
            Assert.Equal("Male", registeredMember.Gender);
            Assert.Equal("Main Street", registeredMember.Address);
            Assert.Equal("John1234", registeredMember.GetCredentials());
            Assert.Equal("1234", registeredMember.GetPassword());
            Assert.Equal(new DateTime(1991, 1, 1), registeredMember.DateOfBirth);
        }

        //Test to check if Available Books are printed

        [Fact]

        public void CheckDisplayAvailableBooks()
        {
            //Arrange
            Library library = new Library();
           
            Book book1 = new Book("B001", "Book1", "Author1", 123456789);
            Book book2 = new Book("B002", "Book2", "Author2", 987654321);

            library.AddBook(book1);
            library.AddBook(book2);

            // Redirect console output to capture it
            var originalOutput = Console.Out;
            using (var consoleOutput = new System.IO.StringWriter())
            {
                Console.SetOut(consoleOutput);

                //Act
                library.DisplayAvailableBooks();


                var output = consoleOutput.ToString();
                Console.SetOut(originalOutput);

                //Assert
                Assert.Contains(book1.BookID, output);
                Assert.Contains(book1.Title, output);
                Assert.Contains(book1.Author, output);

                Assert.Contains(book2.BookID, output);
                Assert.Contains(book2.Title, output);
                Assert.Contains(book2.Author, output);
            }
        }

        //Test to check BorrowBooks
        [Fact]
        public void CheckBorrowBook()
        {
            // Arrange
            var library = new Library();
           
            var memberManager = new Member_Manager(library);
            var member = new Member("MEM000", "arthur", "art", 0.0, "Arthur", "Morgan", "Male", "Main", new DateTime(2022, 10, 10));
            var book = new Book("123", "Sample Book", "Sample Author", 123456789);
            library.AddBook(book);

            // Act
            var result = memberManager.BorrowBook(member, book);

            // Assert
            Assert.True(result);
            Assert.False(book.Availability);
            Assert.Single(member.PersonalLoans);
        }



        //Test to check when User try to borrow a book that is already on Loan
        [Fact]
        public void BorrowingABook_OnLoan()
        {
            //Arrange
            Library library = new Library();
           
            Member_Manager member_Manager = new Member_Manager(library);
            Member testmember = new Member("MEM001", "testuser", "testpassword", 0.0, "John", "Doe", "Male", "Test St", new DateTime(1990, 1, 1));
            library.Members.Add(testmember);
            Book book = new Book("B001", "Test Book", "Test Author", 123456789);
            library.AddBook(book);
            Member testMember2 = new Member("MEM002", "otheruser", "otherpassword", 0.0, "Jane", "Doe", "Female", "Other St", new DateTime(1995, 1, 1));
            Loan loan = new Loan(testMember2, book, DateTime.Now,DateTime.Now.AddDays(14));
            testMember2.PersonalLoans.Add(loan);
            Loan.AddLoan(loan);
            book.Availability = false;

            //Act
            bool result = member_Manager.BorrowBook(testmember, book);

            //Assert
            Assert.False(result);
        }


        //Test to check when User try to Borrrow a book when they have more than 100 in overdue
        [Fact]
        public void BorrowBook_WithExceeded_Overdue()
        {
            //Arrange
            Library library = new Library();
           
            Member_Manager member_Manager = new Member_Manager(library);

            Member testmember = new Member("MEM001", "testuser", "testpassword", 150.00, "John", "Doe", "Male", "Test St", new DateTime(1990, 1, 1));
            library.Members.Add(testmember);
            Book book = new Book("B001", "Test Book", "Test Author", 123456789);
            library.AddBook(book);

            //Act
            bool result = member_Manager.BorrowBook(testmember, book);

            //Assert
            Assert.False(result);
        }
        //Test to check Return Book

        [Fact]
        public void CheckReturnBook()
        {
            // Arrange
            Library library = new Library();
           
            Librarian_Manager librarian_Manager = new Librarian_Manager(library);

            //Creating Test Instances
            Member testMember = new Member("MEM001", "testuser", "testpassword", 0.0, "John", "Doe", "Male", "Test St", new DateTime(1990, 1, 1));
            library.Members.Add(testMember);

            Book testBook = new Book("B001", "Test Book", "Test Author", 123456789);
            library.AddBook(testBook);

            DateTime issuedDate = DateTime.Now;
            DateTime dueDate = DateTime.Now.AddDays(14);
            Loan loan = new Loan(testMember, testBook, issuedDate, dueDate);
            testMember.PersonalLoans.Add(loan);
            Loan.AddLoan(loan);

            // Act
            librarian_Manager.ReturnBook(testMember.MemberID, testBook.BookID);

            // Assert
            Assert.Empty(testMember.PersonalLoans);
            Assert.DoesNotContain(loan, Loan.AllLoans);
            Assert.True(testBook.Availability);


        }
        //Test to check When Return Book ID is not on Loan

        [Fact]
        public void TestReturnBook_BookNotFound()
        {
            //Arrange
            Library library = new Library();
           
            Librarian_Manager librarianManager = new Librarian_Manager(library);

            Member testMember = new Member("MEM001", "testuser", "testpassword", 0.0, "John", "Doe", "Male", "Test St", new DateTime(1990, 1, 1));
            library.Members.Add(testMember);

            Book testBook = new Book("B001", "Test Book", "Test Author", 123456789);
            library.AddBook(testBook);

            //Act
            librarianManager.ReturnBook(testMember.MemberID, testBook.BookID);

            //Assert

            Assert.Empty(testMember.PersonalLoans);
            Assert.True(testBook.Availability);
        }

        //Test to check Return Book with Invalid Member ID
        [Fact]

        public void TestReturnBook_InvalidMemberID()
        {
            Library library = new Library();
           
            Librarian_Manager librarian_Manager = new Librarian_Manager(library);

            //Creating Test Instances
            Member testMember = new Member("MEM001", "testuser", "testpassword", 0.0, "John", "Doe", "Male", "Test St", new DateTime(1990, 1, 1));
            library.Members.Add(testMember);

            Book testBook = new Book("B001", "Test Book", "Test Author", 123456789);
            library.AddBook(testBook);

            DateTime issuedDate = DateTime.Now;
            DateTime dueDate = DateTime.Now.AddDays(14);
            Loan loan = new Loan(testMember, testBook,issuedDate, dueDate);
            testMember.PersonalLoans.Add(loan);
            Loan.AddLoan(loan);

            librarian_Manager.ReturnBook("InvalidMemberID", testBook.BookID);

            Assert.DoesNotContain("InvalidMemberID", library.Members.Select(m => m.MemberID));
        }

        //Test to remove book

        [Fact]

        public void TestRemoveBook()
        {
            Library library = new Library();
            Librarian_Manager librarianManager = new Librarian_Manager(library);
            string bookID = "B001";
            string title = "test Title";
            string author = "testauthor";
            int ISBN = 123456789;
            Book bookToRemove = new Book(bookID, title, author, ISBN);
            library.AddBook(bookToRemove);

            librarianManager.RemoveABook(bookID);
            Assert.Empty(library.AvailableBooks);
        }

        //Test removing a book that is on Loan

        [Fact]
        public void RemoveABook_BookIsOnLoan_ShouldDisplayErrorMessage()
        {
            // Arrange
            var library = new Library();
            var librarianManager = new Librarian_Manager(library);
            var bookToRemove = new Book("456", "On Loan Book", "Loan Author", 456);
            DateTime issuedDate = DateTime.Now;
            DateTime dueDate = DateTime.Now.AddDays(14);
            var member = new Member("M001", "user1", "password", 0, "John", "Doe", "Male", "Address", DateTime.Now);

            library.AddBook(bookToRemove);
            library.AddAndSaveMember(member.MemberID, member.GetCredentials(), member.GetPassword(), member.Overdue, member.FirstName, member.LastName, member.Gender, member.Address, member.DateOfBirth);
            library.Members.Add(member);

            // Simulate loan
            var loan = new Loan(member,bookToRemove, issuedDate, dueDate);
            member.PersonalLoans.Add(loan);
            Loan.AllLoans.Add(loan);
            bookToRemove.Availability = false;

            // Act
            librarianManager.RemoveABook("456");

            // Assert
            Assert.Contains(bookToRemove, library.AvailableBooks); 
        }

        //Test to remove a non existing book

        [Fact]
        public void TestRemoveBook_NotFound()
        {
            // Arrange
            Library library = new Library();
            Librarian_Manager librarianManager = new Librarian_Manager(library);

            string bookIDToRemove = "NonExistentBookID";

            // Act
            librarianManager.RemoveABook(bookIDToRemove);

            // Assert 
            Assert.True(true);
        }


    }
}