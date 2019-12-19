using System;
using System.Collections.Generic;
using System.Text;

namespace TriviaApp.Core.Models
{
    public partial class Question
    {
        public long Response_Code { get; set; }
        public List<QuestionDetails> Results { get; set; }
    }

    public partial class QuestionDetails
    {
        public string Category { get; set; }
        public string Type { get; set; }
        public string Difficulty { get; set; }
        public string Question { get; set; }
        public string Correct_Answer { get; set; }
        public string[] Incorrect_Answers { get; set; }
    }
}
