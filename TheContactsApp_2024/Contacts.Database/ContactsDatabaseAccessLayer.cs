using Contacts.Model;
using Microsoft.Data.Sqlite;
using static System.Int32;

namespace Contacts.Database;

public class ContactsDatabaseAccessLayer
{
    private readonly string _connectionString;

    public ContactsDatabaseAccessLayer(string connectionString = "Data Source=contacts.db")
    {
        _connectionString = connectionString;
        _ = PrepareDatabase();
    }

    private async Task<int> PrepareDatabase()
    {
        await using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        string prepareQuery = Queries.PrepareDatabase();
        await using var command = new SqliteCommand(prepareQuery, connection);
        return await command.ExecuteNonQueryAsync();
    }

    public async Task<List<Contact>> GetTable()
    {
        var list = new List<Contact>();
        await using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        var command = connection.CreateCommand();
        command.CommandText = Queries.GetAllContacts();
        
        await using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            var contact = new Contact();
            if (!string.IsNullOrEmpty(reader["Id"].ToString()))
            {
                TryParse(reader["Id"].ToString(), out var id);
                contact.Id = id;
            }
            else
            {
                contact.Id = 0;
            }
            contact.Name = reader["Name"].ToString() ?? string.Empty;
            contact.Email = reader["Email"].ToString() ?? string.Empty;
            contact.Phone = reader["Phone"].ToString() ?? string.Empty;
                    
            list.Add(contact);
        }

        return list;
    }
    
    public async Task<int> AddContact(Contact contact)
    {
        await using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        string insertQuery = Queries.InsertContact();
        await using var command = new SqliteCommand(insertQuery, connection);
        
        command.Parameters.AddWithValue("@Name", contact.Name);
        command.Parameters.AddWithValue("@Email", contact.Email);
        command.Parameters.AddWithValue("@Phone", contact.Phone);

        return await command.ExecuteNonQueryAsync();
    }
    
    public async Task<int> UpdateContact(Contact contact)
    {
        await using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        string insertQuery = Queries.UpdateContact();
        await using var command = new SqliteCommand(insertQuery, connection);
        
        command.Parameters.AddWithValue("@Id", contact.Id);
        command.Parameters.AddWithValue("@Name", contact.Name);
        command.Parameters.AddWithValue("@Email", contact.Email);
        command.Parameters.AddWithValue("@Phone", contact.Phone);

        return await command.ExecuteNonQueryAsync();
    }
    
    public async Task<int> RemoveContact(int contactId)
    {
        await using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        string insertQuery = Queries.RemoveContact();
        await using var command = new SqliteCommand(insertQuery, connection);
        
        command.Parameters.AddWithValue("@Id", contactId);

        return await command.ExecuteNonQueryAsync();
    }
}