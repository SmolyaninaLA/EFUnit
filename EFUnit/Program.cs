using System;

namespace EFUnit
{
    class Program
    {
        static void Main(string[] args)
        {

            User[] users = new User[]
            {
                new User { FirstName = "Петя", LastName = "Васечкин", Email = "gmail@mail.ru" },
                new User { FirstName = "Вася", LastName = "Петров", Email = "gmail2@mail.ru" },
                new User { FirstName = "Вася", LastName = "Пирожков", Email = "gmail3@mail.ru" },
                new User { FirstName = "Иван", LastName = "Сидоров", Email = "gmaill@mail.ru" },
                new User { FirstName = "Федор", LastName = "Сидоров", Email = "gmailk@mail.ru" }
            };


            //пользователм
            var db = new UserRepository();
            //добавление
            db.AddUser(users);

            //изменение
            var userOld = new User { FirstName = "Федор", LastName = "Сидоров", Email = "gmailk@mail.ru" };
            var userNew = new User { FirstName = "Федор", LastName = "Федоров", Email = "gmailk@mail.ru" };
            db.ChangeUser(userOld, userNew);

            //удаление
            db.RemoveUser(users);

            //авторы
            Author[] authors = new Author[]
            {
                new Author {FirstName = "Гарри",LastName = "Гаррисон"},
                new Author {FirstName = "Лоис", LastName = "Буджолд"},
                new Author {FirstName = "Лев", LastName ="Толстой"},
                new Author {FirstName = "Ольга", LastName = "Громыко"},
                new Author {FirstName = "Агата",LastName = "Кристи"},
                new Author {FirstName = "Александр",LastName = "Волков"}
            };

            var db1 = new AuthorRepository();
            db1.AddAuthor(authors);


            //жанры
            Style[] styles = new Style[]
            {
                new Style {Name = "Фантастика"},
                new Style {Name = "Фентези"},
                new Style {Name = "Классика"},
                new Style {Name = "Детектив"},
                new Style {Name = "Детская"}
            };

            var db2 = new StyleRepository();
            db2.AddStyle(styles);

            //книги
            var db3 = new BookRepository();
          

            Book[] books = new Book[]
           {
                new Book {Designation = "Стальная крыса",PublicationYear = 2000, AuthorId = 1, StyleId = 1, UserId = 1 },
                new Book {Designation = "Стальная крыса",PublicationYear = 2010, AuthorId = 1, StyleId = 1 },
                new Book {Designation = "Стальная крыса идет а ад",PublicationYear = 2001, AuthorId = 1, StyleId = 1, UserId = 4 },
                new Book {Designation = "Осколки чести",PublicationYear = 2000, AuthorId = 2, StyleId = 1, UserId = 2 },
                new Book {Designation = "Барраяр",PublicationYear = 2005, AuthorId = 2, StyleId = 1 },
                new Book {Designation = "Барраяр",PublicationYear = 2015, AuthorId = 2, StyleId = 1 },
                new Book {Designation = "Анна Каренина",PublicationYear = 2008, AuthorId = 3, StyleId = 3, UserId = 4},
                new Book {Designation = "Война и мир",PublicationYear = 2009, AuthorId = 3, StyleId = 3 },
                new Book {Designation = "Профессия ведьма",PublicationYear = 2008, AuthorId = 4, StyleId = 2, UserId = 2 },
                new Book {Designation = "Космобиолухи",PublicationYear = 2019, AuthorId = 4, StyleId = 1 ,UserId = 1},
                new Book {Designation = "Космобиолухи",PublicationYear = 2020, AuthorId = 4, StyleId = 1 },
                new Book {Designation = "Космопсихолухи",PublicationYear = 2018, AuthorId = 4, StyleId = 1 ,UserId = 3},
                new Book {Designation = "Смерть на Ниле",PublicationYear = 2016, AuthorId = 5, StyleId = 4 },
                new Book {Designation = "Волшебник Изумрудного города",PublicationYear = 2010, AuthorId = 6, StyleId = 5, UserId = 5 }
           };

            db3.AddBook(books);

            //1.список книг определенного жанра и вышедших между определенными годами. 
            var st = new Style { Name = "Фантастика" };
            db3.GetListBookForStyle(st, 2000, 2015);

            //2. количество книг определенного автора в библиотеке.
            Console.WriteLine();
            var at = new Author { FirstName = "Гарри", LastName = "Гаррисон" };
            var kol = db3.GetCountBookForAuthor(at);
            Console.WriteLine(kol);

            //3.количество книг определенного жанра в библиотеке.
            Console.WriteLine();
            var st = new Style { Name = "Фентези" };
            kol = db3.GetCountBookForStyle(st);
            Console.WriteLine(kol);

            //4. есть ли книга определенного автора и с определенным названием в библиотеке. 
            Console.WriteLine();
            var bl = db3.GetBookForAuthor(at, "Стальная крыса");   //"Рождение Стальной Крысы");
            Console.WriteLine(bl);

            //5. есть ли определенная книга на руках у пользователя. 
            Console.WriteLine();
            bl = db3.GetBookAtUser(at, "Стальная крыса идет а ад");
            Console.WriteLine(bl);

            //6. Количество книг на руках у пользователя. 
            Console.WriteLine();
            var us = new User { FirstName = "Иван", LastName = "Сидоров", Email = "gmaill@mail.ru" };
            kol = db3.GetCountBookAtUser(us);
            Console.WriteLine(kol);

            //7. последняя вышедшая книга
            Console.WriteLine();
            db3.GetBookForLastPublicationYear();

            //8. список всех книг, отсортированного в алфавитном порядке по названию. 
            Console.WriteLine();
            db3.SelectAllDesignationSort();

            //9. список всех книг, отсортированного в порядке убывания года их выхода. 
            Console.WriteLine();
            db3.SelectAllPublicationYearSort();


        }
    }
}
