using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace EFUnit
{
    public class AuthorRepository
    {
        public void AddAuthor(Author[] authors)
        {

            using (var db = new AppContext())
            {
                var authorAdd = authors.Where(p => p.Id == GetAuthorId(p)).ToArray();
              
                db.Authors.AddRange(authorAdd);
                db.SaveChanges();
               

            }
        }

        public void RemoveAutor(Author[] authors)
        {
            using (var db = new AppContext())
            {
                var authorRemove = authors.Select(u => new Author { Id = GetAuthorId(u), FirstName = u.FirstName, LastName = u.LastName }).Where(u => u.Id > 0).ToArray();
                if (authorRemove.Length > 0)
                {
                    db.Authors.RemoveRange(authorRemove);
                    db.SaveChanges();
                }

            }
        }

        public int GetAuthorId(Author Data)
        {
            int ret = 0;

            using (var db = new AppContext())
            {
               
                var find = db.Authors.FirstOrDefault(p => p.FirstName == Data.FirstName &&
                                                 p.LastName == Data.LastName);
                if (find != null)
                    ret = find.Id;

            }


            return ret;
        }

        public IEnumerable<Author> FindById(int id)
        {
            using (var db = new AppContext())
            {
                var find = db.Authors.Where(p => p.Id == id).ToList();
                return find;
            }
        }

        public IEnumerable<Author> SelectAll()
        {
            using (var db = new AppContext())
            {
                var AllSelect = db.Authors.Select(p => p).ToList();
                return AllSelect;
            }
        }
    }
}
