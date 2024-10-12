namespace Contacts.Database;

public static class Queries
{
    public static string PrepareDatabase(string connectionString = "Data Source=contacts.db")
    {
        return "CREATE TABLE IF NOT EXISTS Contacts (\n    ID INTEGER PRIMARY KEY AUTOINCREMENT,\n    Name TEXT,\n    Email TEXT,\n    Phone TEXT\n);";
    }

    public static string GetAllContacts()
    {
        return
            @"
        SELECT *
        FROM contacts
    ";
    }

    public static string InsertContact()
    {
        return "INSERT INTO Contacts (Name, Email, Phone) VALUES (@Name, @Email, @Phone)";
    }

    public static string UpdateContact()
    {
        return "Update Contacts SET Name=@Name, Email=@Email, Phone=@Phone  WHERE ID = @Id";
    }
    
    public static string RemoveContact()
    {
        return "DELETE FROM Contacts WHERE ID = @Id";
    }

}