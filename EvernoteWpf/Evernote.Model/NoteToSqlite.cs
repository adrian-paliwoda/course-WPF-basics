using System;
using SQLite;

namespace Evernote.Model
{
    public class NoteToSqlite
    {
        public DateTime Created { get; set; }
        public string FileLocation { get; set; }

        [AutoIncrement]
        [PrimaryKey]
        public int Id { get; set; }

        [Indexed]
        public int NotebookId { get; set; }

        public string Title { get; set; }
        public DateTime Updated { get; set; }

        public NoteToSqlite()
        {
            Title = string.Empty;
            FileLocation = string.Empty;
        }
    }
}