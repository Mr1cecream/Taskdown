using Microsoft.Data.Sqlite;
using System;
using System.IO;
using Windows.Storage;

namespace Taskdown
{
    internal class DatabaseAccess
    {
        public static string dbPath { get; private set; }
        public async static void InitializeDatabase()
        {
            await ApplicationData.Current.LocalFolder.CreateFileAsync("database.db", CreationCollisionOption.OpenIfExists);
            dbPath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "database.db");
            using (SqliteConnection connection = new SqliteConnection($"Filename={dbPath}"))
            {
                connection.Open();
                string usersTableCmdStr =
@"CREATE TABLE IF NOT EXISTS users(
guid TEXT NOT NULL PRIMARY KEY,
username TEXT UNIQUE NOT NULL,
passHash TEXT NOT NULL);";
                string tasksTableCmdStr =
@"CREATE TABLE IF NOT EXISTS tasks(
guid TEXT NOT NULL PRIMARY KEY,
userguid TEXT NOT NULL,
list TEXT NOT NULL,
name TEXT NOT NULL,
description TEXT,
markdown TEXT,
completed INTEGER NOT NULL);";
                SqliteCommand usersTableCmd = new SqliteCommand(usersTableCmdStr, connection);
                SqliteCommand tasksTableCmd = new SqliteCommand(tasksTableCmdStr, connection);
                usersTableCmd.ExecuteNonQuery();
                tasksTableCmd.ExecuteNonQuery();

                connection.Close();
            }
        }

        // template only, do not call
        private static void ExecuteReader(SqliteCommand command)
        {
            using (SqliteConnection connection = new SqliteConnection($"Filename={dbPath}"))
            {
                connection.Open();
                command.Connection = connection;
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    // put thy code here
                }
                reader.Close();
                connection.Close();
            }
        }

        public static void ExecuteNonQuery(SqliteCommand command)
        {
            using (SqliteConnection connection = new SqliteConnection($"Filename={dbPath}"))
            {
                connection.Open();
                command.Connection = connection;
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public static object ExecuteScalar(SqliteCommand command)
        {
            object value = null;
            using (SqliteConnection connection = new SqliteConnection($"Filename={dbPath}"))
            {
                connection.Open();
                command.Connection = connection;
                value = command.ExecuteScalar();
                connection.Close();
            }
            return value;
        }
    }
}
