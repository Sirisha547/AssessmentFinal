using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS1_Moq
{
    public interface IBooks
    {
        public bool AddBookDetails();
        public bool EditBookDetails();
        public bool DeleteBookDetails();
        public bool SearchBookbyAuthor();
        public bool IssueBookToStudent();
        public bool ReturnBook();

    }
}
