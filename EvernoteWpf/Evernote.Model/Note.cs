using System;
using Evernote.Model.Interfaces;

namespace Evernote.Model
{
    public class Note : IHasId
    {
        public DateTime Created { get; set; }
        public string FileLocation { get; set; }

        public string Id { get; set; }

        public string NotebookId { get; set; }

        public string Title { get; set; }
        public DateTime Updated { get; set; }

        public Note()
        {
            Id = string.Empty;
            Title = string.Empty;
            FileLocation = string.Empty;
        }
    }
}