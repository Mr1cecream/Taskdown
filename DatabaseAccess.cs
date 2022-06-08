using Microsoft.Data.Sqlite;
using System;
using System.IO;
using Windows.Storage;

namespace Taskdown
{
    internal class DatabaseAccess
    {
        /// <summary>
        /// Path to database
        /// </summary>
        public static string DbPath { get; private set; }

        /// <summary>
        /// Create (if doesn't exist) and connect to database
        /// </summary>
        public static async void InitializeDatabase()
        {
            await ApplicationData.Current.LocalFolder.CreateFileAsync("database.db", CreationCollisionOption.OpenIfExists);
            DbPath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "database.db");
            using (SqliteConnection connection = new SqliteConnection($"Filename={DbPath}"))
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

        /// <summary>
        /// Execute a non-query command
        /// </summary>
        /// <param name="command">Command to execute</param>
        // NOTE: template only, do not call
        private static void ExecuteReader(SqliteCommand command)
        {
            using (SqliteConnection connection = new SqliteConnection($"Filename={DbPath}"))
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

        /// <summary>
        /// Execute a non-query command
        /// </summary>
        /// <param name="command">Command to execute</param>
        public static void ExecuteNonQuery(SqliteCommand command)
        {
            using (SqliteConnection connection = new SqliteConnection($"Filename={DbPath}"))
            {
                connection.Open();
                command.Connection = connection;
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        /// <summary>
        /// Execute a scalar command
        /// </summary>
        /// <param name="command">Command to execute</param>
        /// <returns>Value from scalar</returns>
        public static object ExecuteScalar(SqliteCommand command)
        {
            object value = null;
            using (SqliteConnection connection = new SqliteConnection($"Filename={DbPath}"))
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