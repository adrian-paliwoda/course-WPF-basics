using System;
using System.Windows;
using Evernote.ViewModel;

namespace EvernoteWpf.View
{
    public partial class LoginWindow : Window
    {
        private readonly LoginViewModel _viewModel;

        public LoginWindow()
        {
            InitializeComponent();

            _viewModel = Resources["Vm"] as LoginViewModel;
            if (_viewModel != null)
            {
                _viewModel.CloseLoginWindow += ViewModel_OnCloseWindow;
            }
        }

        private void ViewModel_OnCloseWindow(object? sender, EventArgs e)
        {
            Close();
        }
    }
}