using Evernote.Model.Interfaces;
using SQLite;

namespace Evernote.Model
{
    public class Notebook : IHasId
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string UserId { get; set; }

        public Notebook()
        {
        Id = string.Empty;
            Name = string.Empty;
        }

        public Notebook(string id, string userId, string name)
        {
            Id = id;
            UserId = userId;
            Name = name;
        }
    }
}