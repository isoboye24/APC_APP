using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APC.BLL
{
    interface IBLL<K, T> where K : class where T : class
    {
        bool Insert(T entity);
        bool Update(T entity);
        bool Delete(T entity);
        bool GetBack(T entity);
        K Select();
    }
}
