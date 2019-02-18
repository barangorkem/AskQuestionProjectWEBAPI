using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskQuestion.Data.Model
{
    public class QuestionCategory
    {
        public int QuestionId { get; set; }
        public string QuestionTitle { get; set; }
        public string QuestionTime { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string UserName { get; set; }
    }
}
