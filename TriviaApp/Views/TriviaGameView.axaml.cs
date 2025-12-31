using System;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using TriviaApp.ViewModels;

namespace TriviaApp.Views
{
    public partial class TriviaGameView : UserControl
    {
        public event Action? BackToSelection;

        public TriviaGameView()
        {
            InitializeComponent();
            KeyDown += OnKeyDown;
            Loaded += OnLoaded;
        }

        private void OnLoaded(object? sender, RoutedEventArgs e)
        {
            Focus();
        }

        private void OnKeyDown(object? sender, KeyEventArgs e)
        {
            if (DataContext is not TriviaGameViewModel vm) return;

            switch (e.Key)
            {
                case Key.S:
                    vm.Skip();
                    e.Handled = true;
                    break;
                case Key.C:
                    _ = vm.RevealAndMarkCorrect();
                    e.Handled = true;
                    break;
                case Key.Escape:
                    BackToSelection?.Invoke();
                    e.Handled = true;
                    break;
            }
        }

        public void OnBackClick(object sender, RoutedEventArgs e)
        {
            BackToSelection?.Invoke();
        }
    }
}