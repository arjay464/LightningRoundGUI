using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using TriviaApp.ViewModels;

namespace TriviaApp.Views
{
    public partial class SetSelectionView : UserControl
    {
        public event Action<int>? SetSelected;

        public SetSelectionView()
        {
            InitializeComponent();
            DataContext = new SetSelectionViewModel();
        }

        public void OnSetButtonClick(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is SetInfo setInfo)
            {
                SetSelected?.Invoke(setInfo.Number);
            }
        }
    }
}