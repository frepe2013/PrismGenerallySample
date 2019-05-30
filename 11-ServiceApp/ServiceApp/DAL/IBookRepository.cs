using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceApp.Entities;

namespace ServiceApp.DAL
{
    public interface IBookRepository
    {
        IList<Book> FindAll();

        Book Insert(Book model);

        void Update(Book model);

        void Save();
    }
}
