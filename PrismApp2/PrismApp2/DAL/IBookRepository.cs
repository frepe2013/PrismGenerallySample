using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrismApp2.Entities;

namespace PrismApp2.DAL
{
    public interface IBookRepository : ISavable
    {
        IList<Book> Search(string keyword);

        Book Insert(Book model);

        void Update(Book model);

        void Delete(Book model);
    }
}
