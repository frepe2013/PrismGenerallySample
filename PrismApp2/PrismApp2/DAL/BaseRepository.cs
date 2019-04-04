using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismApp2.DAL
{
    public abstract class BaseRepository<TC> : ISavable where TC : DbContext
    {
        protected abstract TC Context { get; }

        public virtual void Save()
        {
            this.Context.SaveChanges();
        }
    }
}
