using AskQuestion.Core.Infrastructure;
using AskQuestion.Data.Model;
using AskQuestion.Service.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace AskQuestion.Service.Controllers
{
    public class QuestionController : ApiController
    {
        private readonly IQuestionRepository _questionRepository;
        
        public QuestionController(IQuestionRepository questionRepository)
        {
            _questionRepository = questionRepository;
        }
        [Authorize]
        [HttpPost]
        [Route("api/question/insert")]
        public HttpResponseMessage Insert(Question question)
        {
            try
            {
                var identityClaims = (ClaimsIdentity)User.Identity;
                IEnumerable<Claim> claims = identityClaims.Claims;
                question.Id = identityClaims.FindFirst("Id").Value;
                _questionRepository.Insert(question);
                _questionRepository.Save();
                return new HttpResponseMessage(HttpStatusCode.OK);

            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
        }
        [HttpGet]
        [Route("api/question/getQuestions/{CategoryId}")]
        public HttpResponseMessage GetQuestions(int CategoryId)

        {
            IEnumerable<QuestionInfo> questions = _questionRepository.GetMany(x => x.CategoryId == CategoryId).Select(x=>new QuestionInfo()
            {
                QuestionId =x.QuestionId,
                QuestionTime = x.QuestionTime,
                QuestionTitle = x.QuestionTitle,
                CategoryId = x.Category.CategoryId,
                CategoryName = x.Category.CategoryName,
                UserName = x.ApplicationUser.UserName
            });

            return Request.CreateResponse(HttpStatusCode.OK, questions);
        }
        [Authorize(Roles ="Admin")]
        [HttpGet]
        [Route("api/question/deleteQuestion/{QuestionId}")]
        public bool DeleteQuestion(int QuestionId)
        {
            Question question = _questionRepository.GetById(QuestionId);
            if(question!=null)
            {
                _questionRepository.Delete(QuestionId);
                _questionRepository.Save();
                return true;
            }
            else
            {
                return false;
            }

        }
        [HttpGet]
        [Route("api/question/get/{id}")]
        public HttpResponseMessage Get(int id)
        {
            try
            {
                Question question = _questionRepository.GetById(id);
                if (question!=null)
                {

                    QuestionInfo questionInfo = new QuestionInfo()
                    {
                        QuestionId = question.QuestionId,
                        QuestionTime = question.QuestionTime,
                        QuestionTitle = question.QuestionTitle,
                        CategoryId = question.Category.CategoryId,
                        CategoryName = question.Category.CategoryName,
                        UserName = question.ApplicationUser.UserName
                    };
                    return Request.CreateResponse(HttpStatusCode.OK, questionInfo);
                }
               else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Bulunamadı");
                }
            }catch(Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);
            }
        }
    }
}
