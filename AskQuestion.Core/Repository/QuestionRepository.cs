using AskQuestion.Core.Infrastructure;
using AskQuestion.Data.Model;
using AskQuestion.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace AskQuestion.Core.Repository
{
    public class QuestionRepository : IQuestionRepository
    {
        ApplicationDbContext _context = new ApplicationDbContext();
        public int count()
        {
           return _context.Question.Count();
        }

        public void Delete(int id)
        {
            Question question = GetById(id);
            if(question!=null)
            {
                _context.Question.Remove(question);
            }
            save();
        }

        public Question Get(Expression<Func<Question, bool>> expressions)
        {
            return _context.Question.FirstOrDefault(expressions);
        }

        public IEnumerable<Question> GetAll()
        {
            return _context.Question.ToList();
        }

        public Question GetById(int id)
        {
            return _context.Question.FirstOrDefault(x => x.QuestionId == id);
        }

        public IQueryable<Question> GetMany(Expression<Func<Question, bool>> expression)
        {
            return _context.Question.Where(expression);
        }

        public bool Insert(Question obj)
        {
            try
            {
                _context.Question.Add(obj);
                save();
                return true;
            }catch(Exception e)
            {
                return false;
            }
           
        }

        public void save()
        {
            _context.SaveChanges();
        }

        public void Update(Question obj)
        {
            _context.Question.AddOrUpdate(obj);
            save();
        }
    }
}
