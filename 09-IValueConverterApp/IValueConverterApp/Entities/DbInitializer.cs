using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IValueConverterApp.Entities
{
    public class DbInitializer : DropCreateDatabaseIfModelChanges<ShelfContext>
    {
        protected override void Seed(ShelfContext context)
        {
            var books = new List<Book>
            {
                new Book {Title = "Test-Driven Development", Author = "Kent Beck"},
                new Book {Title = "The Healthy Programmer", Author = "Joe Kutner"},
                new Book {Title = "Effective C#", Author = "Bill Wagner"},
            };
            context.Books.AddRange(books);
            context.SaveChanges();
        }
    }
}
