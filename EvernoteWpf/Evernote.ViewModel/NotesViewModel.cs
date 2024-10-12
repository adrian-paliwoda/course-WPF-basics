using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using Evernote.Db;
using Evernote.Model;
using Evernote.ViewModel.Annotations;
using Evernote.ViewModel.Commands;

namespace Evernote.ViewModel
{
    public class NotesViewModel : INotifyPropertyChanged
    {
        public EditCommand EditCommand { get; set; }
        public NewNotebookCommand NewNotebookCommand { get; set; }
        public NewNoteCommand NewNoteCommand { get; set; }


        public Visibility NotebookNameVisibility
        {
            get => _notebookNameVisibility;
            set
            {
                _notebookNameVisibility = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Notebook> Notebooks { get; set; }
        public ObservableCollection<Note> Notes { get; set; }

        public Note SelectedNote
        {
            get => _selectedNote;
            set
            {
                _selectedNote = value;
                OnPropertyChanged(nameof(SelectedNote));
                SelectedNoteChanged?.Invoke(this, EventArgs.Empty);
            }
        }


        public Notebook SelectedNotebook
        {
            get => _selectedNotebook;
            set
            {
                _selectedNotebook = value;
                OnPropertyChanged(nameof(SelectedNotebook));
                GetNotes();
            }
        }

        public StopEditCommand StopEditCommand { get; set; }
        private Visibility _notebookNameVisibility;
        private Note _selectedNote;


        private Notebook _selectedNotebook;

        public NotesViewModel()
        {
            NewNotebookCommand = new NewNotebookCommand(this);
            NewNoteCommand = new NewNoteCommand(this);
            EditCommand = new EditCommand(this);
            StopEditCommand = new StopEditCommand(this);

            Notebooks = new ObservableCollection<Notebook>();
            Notes = new ObservableCollection<Note>();

            NotebookNameVisibility = Visibility.Collapsed;

            GetNotebooks();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public event EventHandler? SelectedNoteChanged;

        public async Task CreateNote(string notebookId)
        {
            var note = new Note
            {
                NotebookId = notebookId,
                Created = DateTime.Now,
                Updated = DateTime.Now,
                FileLocation = "",
                Title = "New note"
            };

            //DatabaseManager.Insert(note);
            FirebaseCloudDb.Insert(note);

            GetNotes();
        }

        public async Task CreateNoteForSql(int notebookId)
        {
            var note = new NoteToSqlite()
            {
                NotebookId = notebookId,
                Created = DateTime.Now,
                Updated = DateTime.Now,
                FileLocation = "",
                Title = "New note"
            };

            //DatabaseManager.Insert(note);
             FirebaseCloudDb.Insert(note);

            GetNotes();
        }

        public async Task CreateNotebook()
        {
            var notebook = new Notebook() {
                UserId = CurrentUser.UserId,
                Name = "New notebook"
            };

            // DatabaseManager.Insert(notebook);
             FirebaseCloudDb.Insert(notebook);
            
            GetNotebooks();
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void GetNotebooks()
        {
            //var notebooks = DatabaseManager.Read<Notebook>().Where(p => p.UserId == CurrentUser.UserId).ToList();
            var notebooks = FirebaseCloudDb.Read<Notebook>().Result.Where(p => p.UserId == CurrentUser.UserId).ToList();

            if (notebooks is {Count: > 0})
            {
                Notebooks.Clear();
                for (var i = 0; i < notebooks.Count; i++)
                {
                    Notebooks.Add(notebooks[i]);
                }
            }
        }

        private void GetNotes()
        {
            if (_selectedNotebook != null)
            {
                Notes.Clear();

                //var notes = DatabaseManager.Read<Note>().Where(p => p.NotebookId == _selectedNotebook.Id).ToList();
                var notes = FirebaseCloudDb.Read<Note>().Result.Where(p => p.NotebookId == _selectedNotebook.Id).ToList();
                
                if (notes is {Count: > 0})
                {
                    for (var i = 0; i < notes.Count; i++)
                    {
                        Notes.Add(notes[i]);
                    }
                }
            }
        }

        public void StartEditing()
        {
            NotebookNameVisibility = Visibility.Visible;
        }

        public async Task StopEditing(Notebook notebook)
        {
            NotebookNameVisibility = Visibility.Collapsed;
            //DatabaseManager.Update(notebook);
            await FirebaseCloudDb.Update(notebook);
            GetNotebooks();
        }
    }
}