using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceApp.Entities
{
    public class DbInitializer : DropCreateDatabaseIfModelChanges<ShelfContext>
    {
        protected override void Seed(ShelfContext context)
        {
            var books = new List<Book>
            {
                new Book {Title = "Test-Driven Development", Author = "Kent Beck", AuthorGender = "Male"},
                new Book {Title = "The Healthy Programmer", Author = "Joe Kutner", AuthorGender = "Male"},
                new Book {Title = "Effective C#", Author = "Bill Wagner", AuthorGender = "Male"},
            };
            context.Books.AddRange(books);
            context.SaveChanges();
        }
    }
}
