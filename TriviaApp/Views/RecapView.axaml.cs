using System;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;

namespace TriviaApp.Views
{
    public partial class RecapView : UserControl
    {
        public event Action? BackToSelection;

        public RecapView()
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
            if (e.Key == Key.Escape)
            {
                BackToSelection?.Invoke();
                e.Handled = true;
            }
        }

        public void OnBackClick(object sender, RoutedEventArgs e)
        {
            BackToSelection?.Invoke();
        }
    }
}