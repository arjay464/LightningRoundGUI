using System;
using System.Collections.ObjectModel;
using TriviaApp.Models;

namespace TriviaApp.ViewModels
{
    public class SetSelectionViewModel : ViewModelBase
    {
        public ObservableCollection<SetInfo> AvailableSets { get; }

        public SetSelectionViewModel()
        {
            AvailableSets = new ObservableCollection<SetInfo>
            {
                new SetInfo { Number = 1, Name = "Mountains" },
                new SetInfo { Number = 2, Name = "Also an NBA Team" },
                new SetInfo { Number = 3, Name = "Presidents" },
                new SetInfo { Number = 4, Name = "European Nations" },
                new SetInfo { Number = 5, Name = "Trees" },
                new SetInfo { Number = 6, Name = "Elements" },
                new SetInfo { Number = 7, Name = "Mathematicians" }
            };
        }

        public void SelectSet(int setNumber)
        {
            Console.WriteLine($"Selected set {setNumber}");
        }
    }

    public class SetInfo
    {
        public int Number { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}