using MyShop.Core.Contacts;
using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.SQL
{
    public class SQLRepository<T> : IRepository<T> where T: BaseEntity
    {
        internal DataContext context;

        internal DbSet<T> dbSet;

        public SQLRepository(DataContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }

        public IQueryable<T> Collection()
        {
            return this.dbSet;
        }
        
        public void Commit()
        {
            this.context.SaveChanges();
        }

        public void Delete(string Id)
        {
            var t = Find(Id);
            if (this.context.Entry(t).State == EntityState.Detached)
            {
                this.dbSet.Attach(t);
            }

            this.dbSet.Remove(t);
        }

        public T Find(string Id)
        {
            return this.dbSet.Find(Id);
        }

        public void Insert(T t)
        {
            this.dbSet.Add(t);
        }

        public void Update(T t)
        {
            this.dbSet.Attach(t);
            this.context.Entry(t).State = EntityState.Modified;
        }
    }
}
