using SQLite;

namespace Evernote.Model
{
    public class User
    {
        public string ConfirmPassword { get; set; }

        // [PrimaryKey]
        // [AutoIncrement]
        public int Id { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        public string Password { get; set; }
        public string UserName { get; set; }

        public User()
        {
            Name = string.Empty;
            LastName = string.Empty;
            UserName = string.Empty;
            Password = string.Empty;
        }

        public User(int id, string name, string lastName, string userName, string password)
        {
            Id = id;
            Name = name;
            LastName = lastName;
            UserName = userName;
            Password = password;
        }
    }
}