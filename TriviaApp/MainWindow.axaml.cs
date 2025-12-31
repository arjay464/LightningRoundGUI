using Avalonia.Controls;
using TriviaApp.Views;
using TriviaApp.ViewModels;

namespace TriviaApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ShowSetSelection();
        }

        public void ShowSetSelection()
        {
            var setSelectionView = new SetSelectionView();
            setSelectionView.SetSelected += OnSetSelected;
            Content = setSelectionView;
        }

        private void OnSetSelected(int setNumber)
        {
            var gameViewModel = new TriviaGameViewModel(setNumber);
            gameViewModel.GameComplete += () => OnGameComplete(gameViewModel);

            var gameView = new TriviaGameView();
            gameView.DataContext = gameViewModel;
            gameView.BackToSelection += OnBackToSelection;
            Content = gameView;
        }

        private void OnGameComplete(TriviaGameViewModel gameViewModel)
        {
            var recapViewModel = new RecapViewModel(
                gameViewModel.SetNumber,
                gameViewModel.CalculateScore(),
                gameViewModel.GetAllQuestions()
            );

            var recapView = new RecapView();
            recapView.DataContext = recapViewModel;
            recapView.BackToSelection += OnBackToSelection;
            Content = recapView;
        }

        private void OnBackToSelection()
        {
            ShowSetSelection();
        }
    }
}