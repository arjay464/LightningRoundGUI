using System;
using System.IO;
using System.Text.Json;

namespace TriviaApp.Models
{
    public class QuestionLoader
    {
        public static QuestionSet LoadSet(int setNumber)
        {
            // Get the directory where the executable is running
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = Path.Combine(baseDirectory, "questions", $"set{setNumber}.json");

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Question set {setNumber} not found at {filePath}");
            }

            string jsonString = File.ReadAllText(filePath);
            var result = JsonSerializer.Deserialize<QuestionSet>(jsonString);

            if (result == null || result.Questions == null || result.Questions.Count == 0)
            {
                throw new InvalidDataException($"No questions loaded from set {setNumber}");
            }

            return result;
        }

        public static int GetAvailableSets()
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string questionsDir = Path.Combine(baseDirectory, "questions");

            if (!Directory.Exists(questionsDir))
            {
                return 0;
            }

            int count = 0;
            for (int i = 1; i <= 8; i++)
            {
                if (File.Exists(Path.Combine(questionsDir, $"set{i}.json")))
                {
                    count++;
                }
            }
            return count;
        }
    }
}