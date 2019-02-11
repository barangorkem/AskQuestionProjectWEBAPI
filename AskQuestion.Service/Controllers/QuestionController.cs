using AskQuestion.Core.Infrastructure;
using AskQuestion.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
        [HttpPost]
        [Route("api/question/insert")]
        public bool Insert(Question question)
        {
            bool isTrue=_questionRepository.Insert(question);
            return isTrue;
        }
    }
}
