using System;
using System.Collections.Generic;
using System.Linq;
 
 
namespace EFUnit
{
    public class BookRepository
    {

        public void AddBook(Book[] books)
        {

            using (var db = new AppContext())
            {
                var bookAdd = books.Where(p => p.Id == GetBookId(p)).ToArray();

                db.Books.AddRange(bookAdd);
                db.SaveChanges();


            }
        }

        public void RemoveBook(Book[] books)
        {
            using (var db = new AppContext())
            {
                var bookRemove = books.Select(u => new Book { Id = GetBookId(u), Designation = u.Designation, 
                                                                PublicationYear = u.PublicationYear, 
                                                                AuthorId = u.AuthorId ,
                                                                StyleId = u.StyleId}).Where(u => u.Id > 0).ToArray();
                if (bookRemove.Length > 0)
                {
                    db.Books.RemoveRange(bookRemove);
                    db.SaveChanges();
                }

            }
        }

        public  int GetBookId(Book bookData)
        {
            int ret = 0;

            using (var db = new AppContext())
            {

                var find = db.Books.FirstOrDefault(p => p.Designation == bookData.Designation &&
                                                  p.PublicationYear == bookData.PublicationYear  &&
                                                  p.AuthorId == bookData.AuthorId &&
                                                  p.StyleId == bookData.StyleId);

                if (find != null)
                    ret = find.Id;

            }


            return ret;
        }

        public IEnumerable<Book> FindById(int id)
        {
            using (var db = new AppContext())
            {
                var find = db.Books.Where(p => p.Id == id).ToList();
                return find;
            }
        }

        public void SelectAllPublicationYearSort()
        { 
            using (var db = new AppContext())
            {
                
                var st = new StyleRepository();
                var styles = st.SelectAll();

                var at = new AuthorRepository();
                var autors = at.SelectAll();

                var ret = db.Books.Select(p => p);

                var result = autors.Join(ret, p => p.Id, u => u.AuthorId,  (p, u) => new
                {
                    Author = p.FirstName + " " + p.LastName,
                    Designation = u.Designation,
                    StyleId = u.StyleId,
                    PublicationYear = u.PublicationYear
                });

                var publ = styles.Join(result, p => p.Id, u => u.StyleId, (p, u) => new
                {
                    Author = u.Author,
                    Designation = u.Designation,
                    PublicationYear = u.PublicationYear,
                    Style = p.Name
                }).OrderByDescending(p => p.PublicationYear);

                foreach (var r in publ)
                {
                    Console.WriteLine(r.Author + " " + r.Designation + " " + " " + r.Style + " " + r.PublicationYear);
                }


            }
        }


        public void SelectAllDesignationSort()
        {
            using (var db = new AppContext())
            {
                
                var st = new StyleRepository();
                var styles = st.SelectAll();

                var at = new AuthorRepository();
                var autors = at.SelectAll();

                var ret = db.Books.Select(p => p);

                var result = autors.Join(ret, p => p.Id, u => u.AuthorId, (p, u) => new
                {
                    Author = p.FirstName + " " + p.LastName,
                    Designation = u.Designation,
                    StyleId = u.StyleId,
                    PublicationYear = u.PublicationYear
                });

                var publ = styles.Join(result, p => p.Id, u => u.StyleId, (p, u) => new
                {
                    Author = u.Author,
                    Designation = u.Designation,
                    PublicationYear = u.PublicationYear,
                    Style = p.Name
                }).OrderBy(p => p.Designation);

                foreach (var r in publ)
                {
                    Console.WriteLine(r.Author + " " + r.Designation + " " + " " + r.Style + " " + r.PublicationYear);
                }

            }
        }




        public int GetCountBookForStyle(Style style)
        {
            int ret = 0;
            var st =  new StyleRepository();

            

            using (var db = new AppContext())
            {
               ret = db.Books.Where(p => p.StyleId == st.GetStyleId(style)).Count();
              
            }


            return ret;
        }

        public int GetCountBookForAuthor(Author author)
        {
            int ret = 0;
            var st = new AuthorRepository();

            using (var db = new AppContext())
            {
                ret = db.Books.Where(p => p.AuthorId == st.GetAuthorId(author)).Count();

            }


            return ret;
        }

        public bool GetBookForAuthor(Author author, string designation)
        {
            bool ret = false;
            var st = new AuthorRepository();



            using (var db = new AppContext())
            {
                ret = db.Books.Any(p => p.AuthorId == st.GetAuthorId(author) && p.Designation.ToLower() == designation.ToLower());

            }


            return ret;
        }



        public void GetListBookForStyle(Style style, int yearStart, int yearEnd)
        {
            
            var st = new StyleRepository();
            var styles = st.SelectAll();

            var at = new AuthorRepository();
            var autors = at.SelectAll();


            using (var db = new AppContext())
            {
                var ret = db.Books.Where(p => p.StyleId == st.GetStyleId(style) && p.PublicationYear >= yearStart && p.PublicationYear <= yearEnd);

                var result = autors.Join(ret, p => p.Id, u => u.AuthorId, (p, u) => new
                {
                    Author = p.FirstName + " " + p.LastName,
                    Designation = u.Designation,
                    PublicationYear = u.PublicationYear
                }).OrderBy(p => p.PublicationYear);

                foreach (var r in result)
                {
                    Console.WriteLine(r.Author + " " + r.Designation + " " + r.PublicationYear);
                }

            }

        }

        public void GetBookForLastPublicationYear()
        {
            using (var db = new AppContext())
            {
                var ret = db.Books.OrderByDescending(p => p.PublicationYear).First();

                var at = new AuthorRepository();

                var author = at.FindById(ret.AuthorId).First();

                var st = new StyleRepository();

                var style = st.FindById(ret.StyleId).First();

                Console.WriteLine(author.FirstName + " " + author.LastName + " " + ret.Designation + " " + ret.PublicationYear + " " + style.Name);
                 
            }

        }
              

        public int GetCountBookAtUser(User user)
        {
            int ret = 0;

            var us = new UserRepository();

            using (var db = new AppContext())
            {
                ret = db.Books.Where(p => p.UserId == us.GetUserId(user)).Count();
            }

            return ret;
        }

        public bool GetBookAtUser(Author author, string designation)
        {
            bool ret = false;
            var st = new AuthorRepository();

            using (var db = new AppContext())
            {
                ret = db.Books.Any(p => p.AuthorId == st.GetAuthorId(author) && p.Designation.ToLower() == designation.ToLower() && p.UserId > 0);

            }

            return ret;
        }
    }  

}
