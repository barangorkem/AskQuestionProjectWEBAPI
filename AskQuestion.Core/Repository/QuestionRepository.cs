using AskQuestion.Core.Infrastructure;
using AskQuestion.Data.Model;
using AskQuestion.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;


namespace AskQuestion.Core.Repository
{
    public class QuestionRepository : IQuestionRepository
    {
        

         ApplicationDbContext _context = new ApplicationDbContext();
        public int Count()
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
            IEnumerable<Question> questions = _context.Question.Where(x => x.QuestionId == id);
            var check= (from t in questions
                       join d in _context.Category
                    on t.CategoryId equals d.CategoryId
                   join x in _context.Users
                   on  t.Id equals x.Id 
                   select new Question
                   {
                      QuestionId=t.QuestionId,
                      QuestionTitle=t.QuestionTitle,
                      QuestionTime=t.QuestionTime,
                      Category=new Category
                      {
                          CategoryId= d.CategoryId,
                          CategoryName=d.CategoryName
                      },
                      ApplicationUser=new ApplicationUser
                      {
                          UserName=x.UserName
                      }
                      
                   }).ToList();
            return check.FirstOrDefault(x=>x.QuestionId==id);


        }

        public IEnumerable<Question> GetMany(Expression<Func<Question, bool>> expression)
        {
            IEnumerable<Question> question = _context.Question.Where(expression);
            var entryPoint = (from ep in question
                              join e in _context.Category on ep.CategoryId equals e.CategoryId
                              join t in _context.Users on ep.Id equals t.Id
                              select new Question
                              {
                                  QuestionId = ep.QuestionId,
                                  QuestionTitle = ep.QuestionTitle,
                                  QuestionTime = ep.QuestionTime,
                                  Category=new Category
                                  {
                                      CategoryId=e.CategoryId,
                                      CategoryName=e.CategoryName
                                  },
                                  ApplicationUser=new ApplicationUser
                                  {
                                      UserName=t.UserName,
                                      Id=null
                                  }
                              }).ToList();
            return entryPoint;
        }

       

        public void Insert(Question obj)
        {
          
                _context.Question.Add(obj);

           
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Question obj)
        {
            _context.Question.AddOrUpdate(obj);
          
        }
    }
}
