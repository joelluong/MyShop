using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MyShop.Core.Contracts;
using MyShop.Core.Models;

namespace MyShop.DataAccess.InMemory
{
    public class InMemoryRepository<T> : IRepository<T>
        where T : BaseEntity
    {
        ObjectCache cache = MemoryCache.Default;

        private List<T> items;

        private string className;

        public InMemoryRepository()
        {
            this.className = typeof(T).Name;
            this.items = this.cache[this.className] as List<T>;
            if (this.items == null)
            {
                this.items = new List<T>();
            }
        }

        public void Commit()
        {
            this.cache[this.className] = this.items;
        }


        public void Insert(T t)
        {
            this.items.Add(t);
        }

        public void Update(T t)
        {
            T tToUpdate = this.items.Find(i => i.Id == t.Id);

            if (tToUpdate != null)
            {
                tToUpdate = t;
            }
            else
            {
                throw new Exception(this.className + "Not Found");
            }
        }

        public T Find(string Id)
        {
            T t = this.items.Find(i => i.Id == Id);
            if (t != null)
            {
                return t;
            }
            else
            {
                throw new Exception(this.className + "Not Found");
            }
        }

        public IQueryable<T> Collection()
        {
            return this.items.AsQueryable();
        }

        public void Delete(string Id)
        {
            T tToDelete = this.items.Find(i => i.Id == Id);
            if (tToDelete != null)
            {
                this.items.Remove(tToDelete);
            }
            else
            {
                throw new Exception(this.className + "Not Found");
            }
        }


    }
}
