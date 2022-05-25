using Isopoh.Cryptography.Argon2;
using Microsoft.Data.Sqlite;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Taskdown
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        private bool createAccount = false;
        public LoginPage()
        {
            this.InitializeComponent();
        }

        private void LoginButton(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextbox.Text;
            string password = PasswordTextbox.Password;

            if (string.IsNullOrWhiteSpace(username))
            {
                ShowUsernameError("Username cannot be empty or whitespace!");
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                ShowPasswordError("Password cannot be empty or whitespace!");
            }

            if (createAccount)
            {
                if (!Validate(username))
                {
                    ShowUsernameError("Username contains prohibited characters!\nAllowed characters are English letters, numbers and underscores.");
                    return;
                }
                if (!Validate(password))
                {
                    ShowPasswordError("Password contains prohibited characters!\nAllowed characters are English letters, numbers and underscores.");
                    return;
                }

                if (password != ConfirmTextbox.Password)
                {
                    ShowPasswordError("Passwords do not match!");
                    return;
                }

                if (!CreateAccount(username, password))
                    return;
            }
            else
            {
                if (!Login(username, password)) return;
            }
            PageReferences.MainPage.Login();
        }

        private bool Login(string username, string password)
        {
            SqliteCommand getUser = new SqliteCommand
            {
                CommandText = "SELECT * FROM users WHERE username=@Username"
            };
            getUser.Parameters.AddWithValue("@Username", username);

            bool success = false;
            using (SqliteConnection connection = new SqliteConnection($"Filename={DatabaseAccess.dbPath}"))
            {
                connection.Open();
                getUser.Connection = connection;
                var reader = getUser.ExecuteReader();
                while (reader.Read())
                {
                    if (!Argon2.Verify(reader.GetString(2), password))
                    {
                        ShowPasswordError("Incorrect password!");
                        return false;
                    }
                    PageReferences.MainPage.UserGuid = reader.GetGuid(0);
                    success = true;
                }
                reader.Close();
                connection.Close();
            }
            if (!success)
                ShowUsernameError("User does not exist!");
            return success;
        }

        private bool CreateAccount(string username, string password)
        {
            SqliteCommand userExistsCmd = new SqliteCommand
            {
                CommandText = "SELECT EXISTS(SELECT 1 FROM users WHERE username=@Username LIMIT 1);"
            };
            userExistsCmd.Parameters.AddWithValue("@Username", username);
            long userExists = (long)DatabaseAccess.ExecuteScalar(userExistsCmd);
            if (userExists > 0)
            {
                ShowUsernameError("User already exists!");
                return false;
            }

            SqliteCommand createUserCmd = new SqliteCommand
            {
                CommandText = "INSERT INTO users VALUES(@Guid, @Username, @Passhash)"
            };
            createUserCmd.Parameters.AddWithValue("@Guid", Guid.NewGuid());
            createUserCmd.Parameters.AddWithValue("@Username", username);
            createUserCmd.Parameters.AddWithValue("@Passhash", Argon2.Hash(password));
            DatabaseAccess.ExecuteNonQuery(createUserCmd);

            return Login(username, password);
        }

        private void CreateAccount(object sender, RoutedEventArgs e)
        {
            if (!createAccount)
            {
                createAccount = true;
                ConfirmTextbox.Visibility = Visibility.Visible;
                CreateBtn.Content = "Existing Account";
                LoginBtn.Content = "Create";
            }
            else
            {
                createAccount = false;
                ConfirmTextbox.Visibility = Visibility.Collapsed;
                CreateBtn.Content = "Create Account";
                LoginBtn.Content = "Login";
            }
        }

        private bool Validate(string str)
        {
            foreach (char c in str)
                if (!((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || (c >= '0' && c <= '9') || c == '_'))
                    return false;
            return true;
        }

        private void ShowUsernameError(string errorText)
        {
            UsernameError.Text = errorText;
            UsernameError.Visibility = Visibility.Visible;
        }

        private void ShowPasswordError(string errorText)
        {
            PasswordError.Text = errorText;
            PasswordError.Visibility = Visibility.Visible;
        }
        private void UsernameChanged(object sender, TextChangedEventArgs e)
        {
            UsernameError.Visibility = Visibility.Collapsed;
        }

        private void PasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordError.Visibility = Visibility.Collapsed;
        }

        private void ReturnPressed(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            if (e.Key != Windows.System.VirtualKey.Enter) return;
            switch ((sender as Control).Tag.ToString())
            {
                case "Username":
                    PasswordTextbox.Focus(FocusState.Programmatic);
                    break;
                case "Password":
                    if (createAccount)
                        ConfirmTextbox.Focus(FocusState.Programmatic);
                    else
                        LoginButton(null, null);
                    break;
                case "Confirm":
                    LoginButton(null, null);
                    break;
                default:
                    break;
            }
        }
    }
}
