using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TriviaApp.Models;

namespace TriviaApp.ViewModels
{
    public class TriviaGameViewModel : ViewModelBase
    {
        private int _setNumber;
        private List<QuestionState> _questions;
        private int _currentIndex;
        private int _timeRemaining;
        private CancellationTokenSource? _timerCancellation;

        public event Action? GameComplete;

        public int SetNumber => _setNumber;
        public string CurrentQuestion => _questions[_currentIndex].Question.QuestionText;
        public string CurrentAnswer => _questions[_currentIndex].Question.Answer;
        public bool IsRevealed => _questions[_currentIndex].IsRevealed;
        public int QuestionNumber => _currentIndex + 1;
        public int TotalQuestions => _questions.Count;
        public int CorrectCount => _questions.Count(q => q.IsCorrect == true);
        public int TimeRemaining => _timeRemaining;
        public string TimeDisplay => $"{_timeRemaining / 60}:{(_timeRemaining % 60):D2}";

        public int CalculateScore()
        {
            int score = CorrectCount * 200;

            // Bonus for completing all questions before timer expires
            if (CorrectCount == TotalQuestions && _timeRemaining > 0)
            {
                score += 500;
            }

            return score;
        }

        public List<QuestionState> GetAllQuestions() => _questions;

        public TriviaGameViewModel(int setNumber)
        {
            _setNumber = setNumber;
            _questions = new List<QuestionState>();
            _timeRemaining = 120; // 2 minutes
            LoadQuestions();
            _currentIndex = 0;
            StartTimer();
        }

        private void LoadQuestions()
        {
            var questionSet = QuestionLoader.LoadSet(_setNumber);
            foreach (var question in questionSet.Questions)
            {
                _questions.Add(new QuestionState { Question = question });
            }
        }

        private async void StartTimer()
        {
            _timerCancellation = new CancellationTokenSource();

            try
            {
                while (_timeRemaining > 0 && !_timerCancellation.Token.IsCancellationRequested)
                {
                    await Task.Delay(1000, _timerCancellation.Token);
                    _timeRemaining--;
                    OnPropertyChanged(nameof(TimeRemaining));
                    OnPropertyChanged(nameof(TimeDisplay));

                    if (_timeRemaining <= 0)
                    {
                        await EndGame();
                    }
                }
            }
            catch (TaskCanceledException)
            {
                // Timer was cancelled
            }
        }

        public void Skip()
        {
            // Move to next unrevealed question
            int startIndex = _currentIndex;
            do
            {
                _currentIndex = (_currentIndex + 1) % _questions.Count;
            } while (_questions[_currentIndex].IsRevealed && _currentIndex != startIndex);

            RefreshDisplay();
        }

        public void Reveal()
        {
            _questions[_currentIndex].IsRevealed = true;
            OnPropertyChanged(nameof(IsRevealed));
        }

        public async Task RevealAndMarkCorrect()
        {
            _questions[_currentIndex].IsRevealed = true;
            _questions[_currentIndex].IsCorrect = true;

            OnPropertyChanged(nameof(IsRevealed));
            OnPropertyChanged(nameof(CorrectCount));

            // Check if all questions are answered
            if (CorrectCount == TotalQuestions)
            {
                await Task.Delay(200);
                await EndGame();
                return;
            }

            // Wait 800ms before moving to next question
            await Task.Delay(800);

            Skip();
        }

        private async Task EndGame()
        {
            _timerCancellation?.Cancel();
            await Task.Delay(1000); // Wait 1 second
            GameComplete?.Invoke();
        }

        private void RefreshDisplay()
        {
            OnPropertyChanged(nameof(CurrentQuestion));
            OnPropertyChanged(nameof(CurrentAnswer));
            OnPropertyChanged(nameof(IsRevealed));
            OnPropertyChanged(nameof(QuestionNumber));
        }
    }

    public class QuestionState
    {
        public required Question Question { get; set; }
        public bool IsRevealed { get; set; }
        public bool? IsCorrect { get; set; }
    }
}