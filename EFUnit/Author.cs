using System;
using System.Collections.Generic;
using System.Text;

namespace EFUnit
{
    public class Author
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<Book> Book { get; set; } = new List<Book>();

    }
}
