using AskQuestion.Core.Infrastructure;
using AskQuestion.Data.Model;
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
        [Authorize(Roles ="Admin")]
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
    }
}
