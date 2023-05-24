using System.Data;
using System.Data.SqlClient;
using System.Net;

namespace LMS1_Moq;
internal class Program
{
    static void Main(string[] args)
    {
        SqlConnection con = new SqlConnection("Server=IN-7G3K9S3; database=moq; Integrated Security=true");

        // Creating an instances
        Students s = new Students(con);
        Books b = new Books(con);
        Console.WriteLine("Welcome to Library Management System");
        Console.WriteLine("Enter Login Details");
        Console.WriteLine("Enter your username: ");
        string user_name = Console.ReadLine();
        Console.WriteLine("Enter your password: ");
        string password = Console.ReadLine();
        Authentication auth = new Authentication();
        var Success = auth.Authenticate(user_name, password);
        if (Success)
        {
            Console.WriteLine("Authentication Successful");
            while (true)
            {
                Console.WriteLine("***** Library Management System Menu *****");
                Console.WriteLine("1. AddBookDetails");
                Console.WriteLine("2. EditBookDetails");
                Console.WriteLine("3. DeleteBookDetails");
                Console.WriteLine("4. AddStudentDetails");
                Console.WriteLine("5. EditStudentDetails");
                Console.WriteLine("6. DeleteStudentDetails");
                Console.WriteLine("7. SearchBookbyAuthor");
                Console.WriteLine("8. SearchStudentbyID");
                Console.WriteLine("9. IssueBookToStudent");
                Console.WriteLine("10. ReturnBook");
                Console.WriteLine("11. GetStudentsWithBooksCount");

                int ch = 0;
                try
                {
                    Console.WriteLine("enter ur choice");
                    ch = Convert.ToInt16(Console.ReadLine());
                }
                catch (FormatException)
                {

                    Console.WriteLine("Enter only Numbers");
                }

                switch (ch)
                {
                    case 1:
                        b.AddBookDetails();
                        break;
                    case 2:
                        b.EditBookDetails();
                        break;
                    case 3:
                        b.DeleteBookDetails();
                        break;
                    case 4:
                        s.AddStudentDetails();
                        break;
                    case 5:
                        s.EditStudentDetails();
                        break;
                    case 6:
                        s.DeleteStudentDetails();
                        break;
                    case 7:
                        b.SearchBookbyAuthor();
                        break;
                    case 8:
                        s.SearchStudentbyId();
                        break;
                    case 9:
                        b.IssueBookToStudent();
                        break;
                    case 10:
                        b.ReturnBook();
                        break;
                    case 11:
                        s.GetStudentsWithBooksCount();
                        break;
                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }

                Console.WriteLine("Do you want to continue (Y/N)?");
                string Choice = Console.ReadLine();

                if (Choice.ToUpper() != "Y")
                    break;
            }
        }
        else
        {
            Console.WriteLine("Authentication Failed");
        }

    }

    
}

