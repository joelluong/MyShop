using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyShop.Core.Contacts;
using MyShop.Core.Models;

namespace MyShop.WebUI.Tests.Mocks
{
    using MyShop.Core.Models;

    public class MockContext<T> : IRepository<T> where T : BaseEntity
    {
        private List<T> items;

        private string className;

        public MockContext()
        {
            this.items = new List<T>();

        }

        public void Commit()
        {
            return;
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
