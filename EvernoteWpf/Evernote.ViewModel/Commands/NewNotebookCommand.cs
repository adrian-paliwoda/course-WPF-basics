using System;
using System.Windows.Input;

namespace Evernote.ViewModel.Commands
{
    public class NewNotebookCommand : ICommand
    {
        public NotesViewModel NotesViewModel { get; set; }

        public NewNotebookCommand(NotesViewModel notesViewModel)
        {
            NotesViewModel = notesViewModel;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            NotesViewModel.CreateNotebook().Wait();
        }

        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}