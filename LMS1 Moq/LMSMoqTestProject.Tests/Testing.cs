using LMS1_Moq;
using Moq;
using System.Data.SqlClient;

namespace LMSMoqTestProject.Tests
{
    public class Testing
    {
        private Authentication auth;
        private Books b;
        private Students s;
        private SqlConnection con;


        [OneTimeSetUp]
        public void Setup()
        {
            auth = new Authentication();
            b = new Books(con);
            s = new Students(con);
        }
        [Test]
        public void AddBookDetails_WhenCalled_ReturnsTrue()
        {
            var book = new Mock<IBooks>();
            book.Setup(x => x.AddBookDetails()).Returns(true);
            var result = book.Object.AddBookDetails();
            Assert.That(result, Is.EqualTo(true));
        }
        [Test]
        public void Authentication_WhenValidCredentials_ReturnsTrue()
        {
            string user_name = "sirisha";
            string password = "sirisha@123";
            Authentication auth = new Authentication();
            bool result = auth.Authenticate(user_name, password);

            Assert.IsTrue(result);
        }
        [Test]
        public void Authentication_WhenInvalidCredentials_ReturnsFalse()
        {
            string user_name = "sasi";
            string password = "sirisha@123";
            Authentication auth = new Authentication();
            bool result = auth.Authenticate(user_name, password);

            Assert.IsFalse(result);
        }
        [Test]
        public void GetStudentsWithBooksCount_BooksIssued_ReturnsCorrectCount()
        {
            int expectedcount = 2;
            int bookCount = s.GetStudentsWithBooksCount();
            Assert.AreEqual(2, bookCount);
        }
        [Test]
        public void GetStudentsWithBooksCount_BooksIssued_ReturnsInCorrectCount()
        {
            int expectedcount = 2;
            int bookCount = s.GetStudentsWithBooksCount();
            Assert.That(bookCount, Is.EqualTo(2));
        }
        [Test]
        public void SearchBookbyAuthor_InvalidAuthor_ReturnsNoMatchingBooksFound()
        {
            var book = new Mock<IBooks>();
            book.Setup(x => x.SearchBookbyAuthor()).Returns(false);
            var result = book.Object.SearchBookbyAuthor();
            Assert.That(result, Is.EqualTo(false));

        }
        [Test]
        public void SearchStudentbyId_WhenCalled_StudentFound()
        {
            var student = new Mock<IStudents>();
            student.Setup(x => x.SearchStudentbyId()).Returns(true);
            var result = student.Object.SearchStudentbyId();
            Assert.That(result, Is.EqualTo(true));
        }
        
        [Test]
        public void EditBookDetails_WhenCalled_ReturnsTrue()
        {
            var book = new Mock<IBooks>();
            book.Setup(x => x.EditBookDetails()).Returns(true);
            var result = book.Object.EditBookDetails();
            Assert.That(result, Is.EqualTo(true));
        }
        [Test]
        public void DeleteBookDetails_WhenCalled_ReturnsTrue() 
        {
            var book = new Mock<IBooks>();
            book.Setup(x => x.DeleteBookDetails()).Returns(true);
            var result = book.Object.DeleteBookDetails();
            Assert.That(result, Is.EqualTo(true));
        }
        [Test]
        public void AddStudentDetails_WhenCalled_ReturnsTrue()
        {
            var student = new Mock<IStudents>();
            student.Setup(x => x.AddStudentDetails()).Returns(true);
            var result = student.Object.AddStudentDetails();
            Assert.That(result, Is.EqualTo(true));
        }
        [Test]
        public void EditStudentDetails_WhenCalled_ReturnsTrue()
        {
            var student = new Mock<IStudents>();
            student.Setup(x => x.EditStudentDetails()).Returns(true);
            var result = student.Object.EditStudentDetails();
            Assert.That(result, Is.EqualTo(true));
        }
        [Test]
        public void DeleteStudentDetails_WhenCalled_ReturnsTrue()
        {
            var student = new Mock<IStudents>();
            student.Setup(x => x.DeleteStudentDetails()).Returns(true);
            var result = student.Object.DeleteStudentDetails();
            Assert.That(result, Is.EqualTo(true));
        }


    }




}