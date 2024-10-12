using System;
using System.Windows.Input;
using Evernote.Model;

namespace Evernote.ViewModel.Commands
{
    public class LoginCommand : ICommand
    {
        public LoginViewModel LoginViewModel { get; set; }

        public LoginCommand(LoginViewModel loginViewModel)
        {
            LoginViewModel = loginViewModel;
        }

        public bool CanExecute(object? parameter)
        {
            if (parameter is User user)
            {
                if (string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.Password))
                {
                    return false;
                }

                return true;
            }

            return false;
        }

        public void Execute(object? parameter)
        {
            LoginViewModel.Login();
        }

        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;

            remove => CommandManager.RequerySuggested -= value;
        }
    }
}