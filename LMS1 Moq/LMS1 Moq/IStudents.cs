using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS1_Moq
{
    public interface IStudents
    {
        public bool AddStudentDetails();
        public bool EditStudentDetails();
        public bool DeleteStudentDetails();
        public bool SearchStudentbyId();
        public int GetStudentsWithBooksCount();
    }
}
