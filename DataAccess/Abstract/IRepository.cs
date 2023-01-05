using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IRepository<T> where T : class
    {
        int Add(T entity);
        int Delete(T entity);
        int Update(T entity);
        List<T> GetList();
        T Get(int id);
    }
}
