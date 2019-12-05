using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace TriviaApp.Core.Models
{
    public partial class Question
    {
        public long ResponseCode { get; set; }
        public QuestionDetails[] Details { get; set; }
    }

    public partial class QuestionDetails
    {
        public string Category { get; set; }
        public string Type { get; set; }
        public string Difficulty { get; set; }
        public string Question { get; set; }
        public string CorrectAnswer { get; set; }
        public string[] IncorrectAnswers { get; set; }
    }
}
