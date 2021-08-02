using System;
using System.Collections.Generic;
using System.Text;

namespace EFUnit
{
    public class Book
    {
        public int Id { get; set; }
        public string Designation { get; set; }
        public int PublicationYear { get; set; }
       
        public int AuthorId { get; set; }
        public List<Author> Author { get; set; }

        public int StyleId { get; set; }
        public List<Style> Style { get; set; }

        public int UserId { get; set; }
        public List<User> User { get; set; }
    }
}
