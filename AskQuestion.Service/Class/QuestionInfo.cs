using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AskQuestion.Service.Class
{
    public class QuestionInfo
    {
        public int QuestionId { get; set; }
        public string QuestionTitle { get; set; }
        public string QuestionTime { get; set; }
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string UserName { get; set; }

     
    }
}