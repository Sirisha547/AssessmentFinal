using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Net;

namespace LMS1_Moq
{
    public class Books : IBooks
    {
        public SqlDataAdapter adp1;
        public SqlDataAdapter adp2;
        public SqlDataAdapter adp3;
        public DataSet ds;
        public SqlCommandBuilder builder1;
        public SqlCommandBuilder builder2;
        public SqlCommandBuilder builder3;
        
        public Books(SqlConnection con)
        {
            con = new SqlConnection("Server=IN-7G3K9S3; database=moq; Integrated Security=true");
            adp1 = new SqlDataAdapter("SELECT * FROM Books", con);
            adp2 = new SqlDataAdapter("SELECT * FROM Students", con);
            adp3 = new SqlDataAdapter("SELECT * FROM Issue", con);
            ds = new DataSet();
            builder1 = new SqlCommandBuilder(adp1);
            builder2 = new SqlCommandBuilder(adp2);
            builder3 = new SqlCommandBuilder(adp3);
            adp1.Fill(ds, "Books");
            adp2.Fill(ds, "Students");
            adp3.Fill(ds, "Issue");
           
        }

        public bool AddBookDetails()
        {
            try
            {
                var row = ds.Tables["Books"].NewRow();

                Console.WriteLine("Enter BookName: ");
                row["Book_Title"] = Convert.ToString(Console.ReadLine());

                Console.WriteLine("Enter Author: ");
                row["Book_Author"] = Convert.ToString(Console.ReadLine());

                Console.WriteLine("Enter Available Stock: ");
                row["Stock"] = Convert.ToInt32(Console.ReadLine());

                ds.Tables["Books"].Rows.Add(row);

                adp1.Update(ds, "Books");
                Console.WriteLine("Book Details added successfully");
                return true;
            }
            catch
            {
                Console.WriteLine("Error occured while adding the book");
                return false;
            }
        }

        public bool EditBookDetails()
        {
            try
            {
                Console.WriteLine("Enter bookid: ");
                int BookId = Convert.ToInt32(Console.ReadLine());

                DataRow[] rows = ds.Tables["Books"].Select($"Book_id = {BookId}");

                if (rows.Length > 0)
                {
                    Console.WriteLine("Enter the column name you want to update: ");
                    string colname = Console.ReadLine();

                    Console.WriteLine("Enter the updated value:");
                    string value = Console.ReadLine();

                    rows[0][colname] = value;

                    adp1.Update(ds, "Books");
                    Console.WriteLine("Book Details updated successfully");
                    return true;
                }
                else
                {
                    Console.WriteLine("Book not found");
                    return false;
                }
            }
            catch
            {
                Console.WriteLine("Error occured while editing the book");
                return false;
            }
        }

        public bool DeleteBookDetails()
        {
            try
            {
                Console.WriteLine("Enter the id you want to delete:");
                int BookId = Convert.ToInt32(Console.ReadLine());

                DataRow[] rows = ds.Tables["Books"].Select($"Book_id = {BookId}");

                if (rows.Length > 0)
                {
                    rows[0].Delete();

                    adp1.Update(ds, "Books");
                    Console.WriteLine("Book deleted successfully");
                    return true;
                }
                else
                {
                    Console.WriteLine("Book not found");
                    return false;
                }
            }
            catch
            {
                Console.WriteLine("Error occured while deleting the book");
                return false;
            }
        }

        public bool SearchBookbyAuthor()
        {
            try
            {
                Console.WriteLine("Enter the author you want to search:");
                string author = Console.ReadLine();

                DataRow[] FoundBooks = ds.Tables["Books"].Select($"Book_Author = '{author}'");

                Console.WriteLine("Search Results: ");

                if (FoundBooks.Length > 0)
                {
                    foreach (DataRow row in FoundBooks)
                    {
                        Console.WriteLine("Book_id | Book_Name | Book_Author | Stock");
                        Console.WriteLine($"{row["Book_id"]} | {row["Book_Title"]} | {row["Book_Author"]} | {row["Stock"]}");
                    }
                    return true;
                }
                else
                {
                    Console.WriteLine("Book Not Found");
                    return false;
                }
            }
            catch
            {
                Console.WriteLine("Error occured while searching the book");
                return false;
            }
        }
        public bool IssueBookToStudent()
        {
            try
            {
                Console.WriteLine("Enter the student ID: ");
                int studentId = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Enter the book ID: ");
                int bookId = Convert.ToInt32(Console.ReadLine());

                
                DataRow[] bookRows = ds.Tables["Books"].Select($"Book_id = {bookId}");

                if (bookRows.Length > 0)
                {
                    DataRow bookRow = bookRows[0];
                    int stock = Convert.ToInt32(bookRow["Stock"]);

                    if (stock != 0)
                    {
                        
                        DataRow issueRow = ds.Tables["Issue"].NewRow();
                        issueRow["Student_id"] = studentId;
                        issueRow["Book_id"] = bookId;
                        ds.Tables["Issue"].Rows.Add(issueRow);
                        
                        adp3.Update(ds, "Issue");
                       
                        bookRow["Stock"] = stock - 1;
                        adp1.Update(ds, "Books");

                        Console.WriteLine("Successfully issued the book");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Book stock is zero");
                        return false;
                    }
                }
                else
                {
                    Console.WriteLine("Book not found");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred while issuing the book: " + ex.Message);
                return false;
            }
        }
        
        public bool ReturnBook()
        {
            Console.WriteLine("Enter Student Id");
            int studentid = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter the book id");
            int bookid = Convert.ToInt32(Console.ReadLine());

            DataRow[] issueRows = ds.Tables["Issue"].Select($"Book_id='{bookid}' AND Student_id='{studentid}'");

            if (issueRows.Length > 0)
            {
                DataRow issueRow = issueRows[0];
                issueRow.Delete();

                adp3.Update(ds, "Issue");

                Console.WriteLine("Book Returned Successfully!");

                DataRow[] bookRows = ds.Tables["Books"].Select($"Book_Id='{bookid}'");
                if (bookRows.Length > 0)
                {
                    DataRow bookRow = bookRows[0];
                    int stock = Convert.ToInt32(bookRow["Stock"]);
                    bookRow["Stock"] = stock + 1;

                    adp1.Update(ds, "Books");
                }
                return true;
            }
            else
            {
                Console.WriteLine("Book is not issued!");
                return false;
            }
        }





    }
}
