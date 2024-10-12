using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Evernote.Model;

namespace Evernote.ViewModel.Commands
{
    public class NewNoteCommand : ICommand
    {
        public NotesViewModel NotesViewModel { get; set; }

        public NewNoteCommand(NotesViewModel notesViewModel)
        {
            NotesViewModel = notesViewModel;
        }

        public bool CanExecute(object? parameter)
        {
            if (parameter is Notebook notebook)
            {
                return true;
            }

            return false;
        }

        public async void Execute(object? parameter)
        {
            if (parameter is Notebook notebook)
            {
                var result = NotesViewModel.CreateNote(notebook.Id);
                result.Wait();
            }
        }

        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;

            remove => CommandManager.RequerySuggested -= value;
        }
    }
}