using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceApp.DAL
{
    public abstract class BaseRepository<TC> where TC : DbContext
    {
        protected abstract TC Context { get; }

        public virtual void Save()
        {
            this.Context.SaveChanges();
        }
    }
}
