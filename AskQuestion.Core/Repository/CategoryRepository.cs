using AskQuestion.Core.Infrastructure;
using AskQuestion.Data.Model;
using AskQuestion.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AskQuestion.Core.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        ApplicationDbContext _context = new ApplicationDbContext();
        public int Count()
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Category Get(Expression<Func<Category, bool>> expressions)
        {
            return _context.Category.FirstOrDefault(expressions);
        }

        public IEnumerable<Category> GetAll()
        {
            return _context.Category.Select(x => x);
        }

        public Category GetById(int id)
        {
           return _context.Category.FirstOrDefault(x => x.CategoryId == id);
        }

        public IQueryable<Category> GetMany(Expression<Func<Category, bool>> expression)
        {
            return _context.Category.Where(expression);
        }

        public void Insert(Category obj)
        {
                _context.Category.Add(obj);
        }

        public void Save()
        {
                _context.SaveChanges();
        }

        public void Update(Category obj)
        {
            throw new NotImplementedException();
        }
    }
}
