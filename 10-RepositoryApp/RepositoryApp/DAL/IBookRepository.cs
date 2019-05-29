using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryApp.Entities;

namespace RepositoryApp.DAL
{
    public interface IBookRepository
    {
        void Update(Book model);

        void Save();
    }
}
