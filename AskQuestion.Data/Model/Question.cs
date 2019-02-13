using AskQuestion.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AskQuestion.Data.Model
{
    public class Question
    {
        [Key]
        public int QuestionId { get; set; }

        [Required]
        [MinLength(10,ErrorMessage ="10 karakterden az olamaz")]
        public string QuestionTitle { get; set; }

        [Required(ErrorMessage ="Gereklidir.")]
        public string QuestionTime { get; set; }

        public int? CategoryId { get; set; }

        public Category Category { get; set; }

        public string Id { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

    }
}
