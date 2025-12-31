using System.Collections.Generic;

namespace TriviaApp.Models
{
    public class Question
    {
        public string QuestionText { get; set; } = string.Empty;
        public string Answer { get; set; } = string.Empty;
    }

    public class QuestionSet
    {
        public List<Question> Questions { get; set; } = new List<Question>();
    }
}