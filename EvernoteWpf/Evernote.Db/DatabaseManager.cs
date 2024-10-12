using System;
using System.Collections.Generic;
using System.IO;
using SQLite;

namespace Evernote.Db
{
    public class DatabaseManager
    {
        private const string DbFileName = "evernoteDb.db3";
        private static readonly string DbFilePath = Path.Combine(Environment.CurrentDirectory, DbFileName);

        public static bool Insert<T>(T item)
        {
            using var connection = new SQLiteConnection(DbFilePath);
            connection.CreateTable<T>();
            return connection.Insert(item) > 0;
        }

        public static bool Update<T>(T item)
        {
            using var connection = new SQLiteConnection(DbFilePath);
            connection.CreateTable<T>();
            return connection.Update(item) > 0;
        }

        public static bool Delete<T>(T item)
        {
            using var connection = new SQLiteConnection(DbFilePath);
            connection.CreateTable<T>();
            return connection.Delete(item) > 0;
        }

        public static List<T> Read<T>() where T : new()
        {
            using var connection = new SQLiteConnection(DbFilePath);
            connection.CreateTable<T>();

            return connection.Table<T>().ToList();
        }
    }
}