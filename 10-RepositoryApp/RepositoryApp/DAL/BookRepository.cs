using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryApp.Entities;

namespace RepositoryApp.DAL
{
    public class BookRepository : BaseRepository<ShelfContext>, IBookRepository
    {
        protected override ShelfContext Context { get; }

        public BookRepository(ShelfContext context)
        {
            Context = context;
        }

        public void Update(Book model)
        {
            var entry = Context.Entry(model);
            entry.State = EntityState.Modified;
        }
    }
}
