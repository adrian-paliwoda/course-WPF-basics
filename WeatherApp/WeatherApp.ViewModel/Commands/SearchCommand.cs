using System;
using System.Windows.Input;

namespace WeatherApp.ViewModel.Commands
{
    public class SearchCommand : ICommand
    {
        public WeatherViewModel WeatherViewModel { get; set; }

        public SearchCommand(WeatherViewModel weatherViewModel)
        {
            WeatherViewModel = weatherViewModel;
        }

        public bool CanExecute(object? parameter)
        {
            if (parameter is string s && !string.IsNullOrEmpty(s) && !string.IsNullOrWhiteSpace(s))
            {
                return true;
            }
            
            return false;
        }

        public async void Execute(object? parameter)
        {
            await WeatherViewModel.MakeQuery();
        }

        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}