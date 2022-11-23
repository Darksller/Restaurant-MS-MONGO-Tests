using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.DAL.Interfaces
{
    public interface IBaseRepository <T>
    {
        bool Create(T entity);
        bool Update(T entity);
        bool Delete(int id);
        T Get(int id);
        IEnumerable<T> GetAll();
    }
}
