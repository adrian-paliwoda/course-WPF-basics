using System;
using System.Windows.Input;
using Evernote.Model;

namespace Evernote.ViewModel.Commands
{
    public class RegisterCommand : ICommand
    {
        public LoginViewModel LoginViewModel { get; set; }

        public RegisterCommand(LoginViewModel loginViewModel)
        {
            LoginViewModel = loginViewModel;
        }

        public bool CanExecute(object? parameter)
        {
            if (parameter is User user)
            {
                if (string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.Password) ||
                    string.IsNullOrEmpty(user.ConfirmPassword))
                {
                    return false;
                }

                if (!user.Password.Equals(user.ConfirmPassword))
                {
                    return false;
                }

                return true;
            }

            return false;
        }

        public void Execute(object? parameter)
        {
            LoginViewModel.Register();
        }

        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;

            remove => CommandManager.RequerySuggested -= value;
        }
    }
}