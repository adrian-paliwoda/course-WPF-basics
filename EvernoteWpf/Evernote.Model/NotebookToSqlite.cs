using SQLite;

namespace Evernote.Model
{
    public class NotebookToSqlite
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        [Indexed]
        public string UserId { get; set; }

        public NotebookToSqlite()
        {
            Name = string.Empty;
        }

        public NotebookToSqlite(int id, string userId, string name)
        {
            Id = id;
            UserId = userId;
            Name = name;
        }
    }
}