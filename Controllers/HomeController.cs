
using Microsoft.AspNetCore.Mvc;
using BookStoree.Models;
using BookStoree.ViewModels;
using System.Collections.Generic;

namespace BookStoree.Controllers
{
    public class HomeController : Controller
    {
        private static List<Author> AuthorData = new List<Author>();

        public List<Author> Authors
        {
            get { return AuthorData; }
        }

        public static List<Book> Books = new List<Book>();

        public HomeController()
        {
            if (Authors.Count == 0)
            {
                Authors.Add(new Author
                {
                    Id = 1,
                    Name = "John Smith",
                    Country = "USA"
                });

                Authors.Add(new Author
                {
                    Id = 2,
                    Name = "Ali Ahmed",
                    Country = "Pakistan"
                });
            }

            if (Books.Count == 0)
            {
                Books.Add(new Book
                {
                    Id = 1,
                    Title = "C# Programming",
                    Price = 1500,
                    AuthorId = 1
                });

                Books.Add(new Book
                {
                    Id = 2,
                    Title = "ASP.NET Core MVC",
                    Price = 2000,
                    AuthorId = 2
                });

                Books.Add(new Book
                {
                    Id = 3,
                    Title = "Object Oriented Programming",
                    Price = 1800,
                    AuthorId = 1
                });
            }
        }

        public int GetNextAuthorId()
        {
            if (Authors.Count == 0)
            {
                return 1;
            }

            return Authors[Authors.Count - 1].Id + 1;
        }

        public static int GetNextBookId()
        {
            if (Books.Count == 0)
            {
                return 1;
            }

            return Books[Books.Count - 1].Id + 1;
        }

        public IActionResult Index()
        {
            HomeViewModel model = new HomeViewModel();

            model.Authors = Authors;

            model.Books = new List<BookCardViewModel>();

            foreach (Book book in Books)
            {
                Author author = Authors.Find(a => a.Id == book.AuthorId);

                model.Books.Add(new BookCardViewModel
                {
                    Id = book.Id,
                    Title = book.Title,
                    Price = book.Price,
                    AuthorName = author.Name
                });
            }

            return View(model);
        }
        [HttpPost]
        public IActionResult AddBook(string title, decimal price, int authorId)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return RedirectToAction("Index");
            }

            Author author = Authors.Find(a => a.Id == authorId);

            if (author == null)
            {
                return RedirectToAction("Index");
            }

            Book newBook = new Book
            {
                Id = GetNextBookId(),
                Title = title,
                Price = price,
                AuthorId = authorId
            };

            Books.Add(newBook);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult AddAuthor(string name, string country)
        {
            Author newAuthor = new Author
            {
                Id = GetNextAuthorId(),
                Name = name,
                Country = country
            };

            Authors.Add(newAuthor);

            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult RemoveBook(int id)
        {
            Book book = Books.Find(b => b.Id == id);

            if (book != null)
            {
                Books.Remove(book);
            }

            return RedirectToAction("Index");
        }
        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

    }
}
