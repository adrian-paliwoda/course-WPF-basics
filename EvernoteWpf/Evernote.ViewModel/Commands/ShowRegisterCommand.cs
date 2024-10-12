using System;
using System.Windows.Input;

namespace Evernote.ViewModel.Commands
{
    public class ShowRegisterCommand : ICommand
    {
        public LoginViewModel LoginViewModel { get; set; }

        public ShowRegisterCommand(LoginViewModel loginViewModel)
        {
            LoginViewModel = loginViewModel;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            LoginViewModel.SwitchViews();
        }

        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}