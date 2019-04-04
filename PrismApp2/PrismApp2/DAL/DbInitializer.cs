using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using PrismApp2.Entities;

namespace PrismApp2.DAL
{
    public class DbInitializer : DropCreateDatabaseIfModelChanges<ShelfContext>
    {
        protected override void Seed(ShelfContext context)
        {
            var exeDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var projDirectory = exeDirectory.Replace(@"\bin\Debug", "");
            var constraintPath = Path.Combine(projDirectory, "CheckConstraint.sql");
            var constraintSql = File.ReadAllText(constraintPath);
            context.Database.ExecuteSqlCommand(constraintSql);

            var books = new List<Book>
            {
                new Book {Title = "テスト駆動開発", Author = "Kent Beck", AuthorGender = "M"},
                new Book {Title = "スモール・リーダーシップ", Author = "和智右桂", AuthorGender = "M"},
                new Book {Title = "ヘルシープログラマ", Author = "Joe Kutner", AuthorGender = "M"},
            };
            context.Books.AddRange(books);
            context.SaveChanges();
        }
    }
}