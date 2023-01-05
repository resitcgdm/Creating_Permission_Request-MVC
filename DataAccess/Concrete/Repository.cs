using DataAccess.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public int Add(T entity)
        {
            using (Context context = new Context())
            {
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Added;
               return context.SaveChanges();
            }
        }

        public int Delete(T entity)
        {
            using (Context context = new Context())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                return context.SaveChanges();
            }
        }

        public T Get(int id)
        {
            using (Context context = new Context())
            {
                return context.Set<T>().Find(id);
            }
        }

        public List<T> GetList()
        {
            using (Context context = new Context())
            {
                return context.Set<T>().ToList();
            }
        }

        public int Update(T entity)
        {
            using (Context context = new Context())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                return context.SaveChanges();
            }
        }
    }
}
