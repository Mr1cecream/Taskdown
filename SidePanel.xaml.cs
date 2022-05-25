using Microsoft.Data.Sqlite;
using System;
using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Taskdown
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SidePanel : Page
    {
        private ObservableCollection<String> lists;
        public SidePanel()
        {
            this.InitializeComponent();
            PageReferences.SidePanel = this;
            Lists.Items.Clear();
            RefreshLists();
            Lists.ItemsSource = lists;
        }

        private ObservableCollection<String> GetLists(Guid userGuid)
        {
            ObservableCollection<String> list = new ObservableCollection<String>();
            var command = new SqliteCommand
            {
                CommandText = "SELECT list FROM tasks WHERE userguid=@UserGuid"
            };
            command.Parameters.AddWithValue("@UserGuid", userGuid);
            using (SqliteConnection connection = new SqliteConnection($"Filename={DatabaseAccess.dbPath}"))
            {
                connection.Open();
                command.Connection = connection;
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string listName = reader.GetString(0);
                    if (!list.Contains(listName))
                        list.Add(listName);
                }
                reader.Close();
                connection.Close();
            }
            return list;
        }

        private void RefreshLists() =>
            lists = GetLists((Guid)PageReferences.MainPage.UserGuid);

        public void ListSelected(object sender, RoutedEventArgs e)
        {
            var li = (string)Lists.SelectedItem;
            PageReferences.AppPage.ListSelected(li);
        }

        public void Quit(object sender, RoutedEventArgs e)
        {
            Application.Current.Exit();
        }

        public void Settings(object sender, RoutedEventArgs e)
        {
            //PageReferences.AppPage.NavigateTo(typeof(SettingsPage));
        }

        private void AddList(object sender, RoutedEventArgs e)
        {
            string name = NewListTextbox.Text;
            if (string.IsNullOrEmpty(name)) return;
            foreach (string item in lists)
                if (item == name)
                    return;
            lists.Add(name);
            NewListTextbox.Text = string.Empty;
        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            PageReferences.MainPage.Logout();
        }

        private void EnterPressed(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            if (e.Key != Windows.System.VirtualKey.Enter) return;
            AddList(null, null);
        }
    }
}
