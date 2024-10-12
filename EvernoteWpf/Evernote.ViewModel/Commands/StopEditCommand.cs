using System;
using System.Windows.Input;
using Evernote.Model;

namespace Evernote.ViewModel.Commands
{
    public class StopEditCommand : ICommand
    {
        public NotesViewModel NotesViewModel { get; set; }

        public StopEditCommand(NotesViewModel notesViewModel)
        {
            NotesViewModel = notesViewModel;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if (parameter is Notebook notebook)
            {
                NotesViewModel.StopEditing(notebook);
            }
        }

        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}