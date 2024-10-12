using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using Evernote.Authentication;
using Evernote.Model;
using Evernote.ViewModel.Annotations;
using Evernote.ViewModel.Commands;

namespace Evernote.ViewModel
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        public string ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                _confirmPassword = value;
                User = UserInstance();
                OnPropertyChanged(nameof(ConfirmPassword));
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                User = UserInstance();
                OnPropertyChanged(nameof(LastName));
            }
        }

        public LoginCommand LoginCommand { get; set; }

        public Visibility LoginVisibility
        {
            get => _loginVisibility;
            set
            {
                _loginVisibility = value;
                OnPropertyChanged(nameof(LoginVisibility));
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                User = UserInstance();
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                User = UserInstance();
                OnPropertyChanged(nameof(Password));
            }
        }

        public RegisterCommand RegisterCommand { get; set; }

        public Visibility RegisterVisibility
        {
            get => _registerVisibility;
            set
            {
                _registerVisibility = value;
                OnPropertyChanged(nameof(RegisterVisibility));
            }
        }

        public ShowRegisterCommand ShowRegisterCommand { get; set; }

        public User User
        {
            get => _user;
            set
            {
                _user = value;
                OnPropertyChanged(nameof(User));
            }
        }

        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                User = UserInstance();

                OnPropertyChanged(nameof(UserName));
            }
        }

        private string _confirmPassword;
        private bool _isShowingRegister;
        private string _lastName;
        private Visibility _loginVisibility;
        private string _name;
        private string _password;
        private Visibility _registerVisibility;

        private User _user;
        private string _userName;

        public LoginViewModel()
        {
            User = new User();
            _isShowingRegister = false;

            LoginVisibility = Visibility.Visible;
            RegisterVisibility = Visibility.Collapsed;

            RegisterCommand = new RegisterCommand(this);
            LoginCommand = new LoginCommand(this);
            ShowRegisterCommand = new ShowRegisterCommand(this);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public event EventHandler? CloseLoginWindow;

        public void SwitchViews()
        {
            _isShowingRegister = !_isShowingRegister;

            if (_isShowingRegister)
            {
                RegisterVisibility = Visibility.Visible;
                LoginVisibility = Visibility.Collapsed;
            }
            else
            {
                RegisterVisibility = Visibility.Collapsed;
                LoginVisibility = Visibility.Visible;
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task Login()
        {
            var localId = await FirebaseAuthentication.Login(User);

            if (localId.Item1 && !string.IsNullOrEmpty(localId.Item2))
            {
                CurrentUser.UserId = localId.Item2;
                CloseLoginWindow?.Invoke(this, EventArgs.Empty);
            }
        }

        public async Task Register()
        {
            var localId = await FirebaseAuthentication.Register(User);

            if (localId.Item1 && !string.IsNullOrEmpty(localId.Item2))
            {
                CurrentUser.UserId = localId.Item2;
                CloseLoginWindow?.Invoke(this, EventArgs.Empty);
            }
        }

        private User UserInstance()
        {
            return new User
            {
                Name = Name, ConfirmPassword = ConfirmPassword, Password = Password,
                LastName = LastName, UserName = UserName
            };
        }
    }
}