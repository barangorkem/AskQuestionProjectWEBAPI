using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AskQuestion.Core.Infrastructure
{
    public interface IRepository<T> where T:class
    {
        IEnumerable<T> GetAll();

        T GetById(int id);

        T Get(Expression<Func<T, bool>> expressions);

        IEnumerable<T> GetMany(Expression<Func<T, bool>> expression);

        void Insert(T obj);

        void Update(T obj);

        void Delete(int id);

        int Count();

        void Save();

        
    }
}
