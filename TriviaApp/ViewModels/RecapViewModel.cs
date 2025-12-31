using System.Collections.Generic;
using System.Linq;
using TriviaApp.Models;

namespace TriviaApp.ViewModels
{
    public class RecapViewModel : ViewModelBase
    {
        public int SetNumber { get; }
        public int Score { get; }
        public int CorrectCount { get; }
        public int TotalQuestions { get; }
        public List<RecapQuestion> Questions { get; }

        public RecapViewModel(int setNumber, int score, List<QuestionState> questions)
        {
            SetNumber = setNumber;
            Score = score;
            Questions = new List<RecapQuestion>();

            foreach (var q in questions)
            {
                Questions.Add(new RecapQuestion
                {
                    QuestionText = q.Question.QuestionText,
                    Answer = q.Question.Answer,
                    WasCorrect = q.IsCorrect == true
                });
            }

            CorrectCount = questions.Count(q => q.IsCorrect == true);
            TotalQuestions = questions.Count;
        }
    }

    public class RecapQuestion
    {
        public string QuestionText { get; set; } = string.Empty;
        public string Answer { get; set; } = string.Empty;
        public bool WasCorrect { get; set; }
    }
}