using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS1_Moq
{
    public class Students : IStudents
    {
        public SqlDataAdapter adp2;
        public SqlDataAdapter adp3;
        public DataSet ds;
        public SqlCommandBuilder builder2;
        public SqlCommandBuilder builder3;

        public Students(SqlConnection con)
        {
            con = new SqlConnection("Server=IN-7G3K9S3; database=moq; Integrated Security=true");
            adp2 = new SqlDataAdapter("SELECT * FROM Students", con);
            adp3 = new SqlDataAdapter("SELECT * FROM Issue", con);
            builder2 = new SqlCommandBuilder(adp2);
            builder3 = new SqlCommandBuilder(adp3);
            ds = new DataSet();
            adp2.Fill(ds, "Students");
            adp3.Fill(ds, "Issue");
        }
        public bool AddStudentDetails()
        {
            try
            {
                var row = ds.Tables["Students"].NewRow();

                Console.WriteLine("Enter StudentName: ");
                row["Student_Name"] = Convert.ToString(Console.ReadLine());

                Console.WriteLine("Enter StudentDept: ");
                row["Student_Dept"] = Convert.ToString(Console.ReadLine());

                ds.Tables["Students"].Rows.Add(row);

                adp2.Update(ds, "Students");
                Console.WriteLine("Student Details added successfully");
                return true;
            }
            catch
            {
                Console.WriteLine("Error occured while adding the student");
                return true;
            }
        }
        public bool EditStudentDetails()
        {
            try
            {
                Console.WriteLine("Enter studentid: ");
                int StudentId = Convert.ToInt32(Console.ReadLine());

                DataRow[] rows = ds.Tables["Students"].Select($"Student_id = {StudentId}");

                if (rows.Length > 0)
                {
                    Console.WriteLine("Enter the column name you want to update: ");
                    string colname = Console.ReadLine();

                    Console.WriteLine("Enter the updated value:");
                    string value = Console.ReadLine();

                    rows[0][colname] = value;

                    adp2.Update(ds, "Students");
                    Console.WriteLine("Student Details updated successfully");
                    return true;
                }
                else
                {
                    Console.WriteLine("Student not found");
                    return false;
                }
            }
            catch
            {
                Console.WriteLine("Error occured while editing the student");
                return false;
            }
        }

        public bool DeleteStudentDetails()
        {
            try
            {
                Console.WriteLine("Enter the id you want to delete:");
                int StudentId = Convert.ToInt32(Console.ReadLine());

                DataRow[] rows = ds.Tables["Students"].Select($"Student_id = {StudentId}");

                if (rows.Length > 0)
                {
                    rows[0].Delete();

                    adp2.Update(ds, "Students");
                    Console.WriteLine("Student deleted successfully");
                    return true;
                }
                else
                {
                    Console.WriteLine("Student not found");
                    return false;
                }
            }
            catch
            {
                Console.WriteLine("Error occured while deleting the student");
                return false;
            }
        }

        public bool SearchStudentbyId()
        {
            try
            {
                Console.WriteLine("Enter the student id you want to search:");
                int StudentId = Convert.ToInt32(Console.ReadLine());

                DataRow[] FoundStudents = ds.Tables["Students"].Select($"Student_id = {StudentId}");

                Console.WriteLine("Search Results: ");

                if (FoundStudents.Length > 0)
                {
                    foreach (DataRow row in FoundStudents)
                    {
                        Console.WriteLine("Student_id | Student_Title | Student_Dept ");
                        Console.WriteLine($"{row["Student_id"]} | {row["Student_Name"]} | {row["Student_Dept"]}");
                    }
                    return true;
                }
                else
                {
                    Console.WriteLine("Student Not Found");
                    return false;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error occured while searching the student" + ex.Message);
                return false;
            }
        }
        public int GetStudentsWithBooksCount()
        {
            DataRow[] rows = ds.Tables["Issue"].Select($"Book_id IS NOT NULL");
            int Count = rows.Length;
            Console.WriteLine($"Total students with books: {Count}");
            if (rows.Length > 0)
            {
                Console.WriteLine("Issued_id | Student_id | Book_id");
                Console.Write($"{rows[0]["Issued_id"]} | ");
                Console.Write($"{rows[0]["Student_id"]} | ");
                Console.Write($"{rows[0]["Book_id"]} | ");
                Console.WriteLine();

            }
            
            return Count;
        }



    }
}
