using System;
using System.Collections.Generic;
using System.Text;

namespace EFUnit 
{
   public class Style
    {
        public int Id { get; set; } 
        public string Name { get; set; }

        public List<Book> Book { get; set; } = new List<Book>();
    }
}
