using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EFUnit
{
    public class AppContext : DbContext 
    {
       
       public DbSet<User> Users { get; set; }
       public DbSet<Book> Books { get; set; }
       public DbSet<Author> Authors { get; set; }
       public DbSet<Style> Styles { get; set; }

        //public AppContext()
        //{
        //    Database.EnsureDeleted();
        //    Database.EnsureCreated();
        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source = TERMSRV01;Database = EF;Trusted_Connection = True;");
        }
    }
}
