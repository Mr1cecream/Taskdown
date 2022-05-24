using Microsoft.Data.Sqlite;
using System;
using System.Collections.ObjectModel;
using System.IO;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Taskdown
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ListPage : Page
    {
        private bool inEditingMode = false;
        private string listName;
        private ObservableCollection<ListViewItem> list = new ObservableCollection<ListViewItem>();
        private Guid currentTaskId;
        private bool currentTaskCompleted;
        private string currentTaskContent;
        public ListPage()
        {
            this.InitializeComponent();
            PageReferences.ListPage = this;
            MdTextbox.Text =
@"# Welcome to Taskdown!
This area is where you write and view your tasks in markdown form.
On the top, you will see a few buttons - those are controls for your task.
To switch to editing mode, press the 'Switch to Editing Mode' button on the top.
To switch back to reading mode, press the same button, now labeled
'Switch to Reading Mode'.
Select a task from the list on the left or create a new one on the top
to begin marking down your tasks!";
        }

        public void GenerateList(string listName)
        {
            this.listName = listName;
            GenerateList();
        }
        public void GenerateList()
        {
            SqliteCommand command = new SqliteCommand
            {
                CommandText = "SELECT guid, name, description, completed FROM tasks WHERE userguid=@UserGuid AND list=@List"
            };
            command.Parameters.AddWithValue("@UserGuid", PageReferences.MainPage.UserGuid);
            command.Parameters.AddWithValue("@List", listName);
            list.Clear();
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "database.db");
            using (SqliteConnection connection = new SqliteConnection($"Filename={dbpath}"))
            {
                connection.Open();
                command.Connection = connection;
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var task = BuildTask(reader.GetGuid(0), reader.GetString(1), reader.GetString(2), reader.GetBoolean(3));
                    list.Add(task);
                }
                reader.Close();
                connection.Close();
            }
            TaskList.ItemsSource = list;
        }

        private ListViewItem BuildTask(Guid id, string name, string description, bool completed)
        {
            var li = new ListViewItem()
            {
                Tag = id,
            };
            var sp = new StackPanel()
            {
                Orientation = Orientation.Vertical,
            };

            var tbName = new TextBlock()
            {
                Text = name,
                Foreground = new SolidColorBrush(Windows.UI.Colors.CornflowerBlue),
                FontSize = 16,
                FontFamily = new FontFamily("Comic Sans MS"),
            };
            var tbDesc = new TextBlock()
            {
                Text = description,
                Foreground = new SolidColorBrush(Windows.UI.Colors.DimGray),
                FontSize = 14,
                FontFamily = new FontFamily("Comic Sans MS"),
            };

            sp.Children.Add(tbName);
            sp.Children.Add(tbDesc);
            li.Content = sp;

            li.PointerPressed += TaskSelected;

            if (completed)
                li.Background = new SolidColorBrush(Windows.UI.Colors.LightGreen);

            return li;
        }

        private void SaveTask()
        {
            if (currentTaskId == null) return;
            if (inEditingMode)
                ReadingMode();
            if (currentTaskContent == MdTextbox.Text) return;
            var command = new SqliteCommand
            {
                CommandText = "UPDATE tasks SET markdown=@Markdown WHERE guid=@Guid"
            };
            command.Parameters.AddWithValue("@Markdown", MdTextbox.Text);
            command.Parameters.AddWithValue("@Guid", currentTaskId);
            DatabaseAccess.ExecuteNonQuery(command);
        }

        private void TaskSelected(object sender, RoutedEventArgs e)
        {
            SaveTask();
            if (!(TaskList.SelectedItem is ListViewItem li) || li.Tag == null) return;
            Guid taskId = (Guid)li.Tag;
            var command = new SqliteCommand
            {
                CommandText = "SELECT name, description, markdown, completed FROM tasks WHERE guid=@Guid"
            };
            command.Parameters.AddWithValue("@Guid", taskId);
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "database.db");
            using (SqliteConnection connection = new SqliteConnection($"Filename={dbpath}"))
            {
                connection.Open();
                command.Connection = connection;
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    TaskNameTextBox.Text = reader.GetString(0);
                    TaskDescTextBox.Text = reader.GetString(1);
                    if (reader.IsDBNull(2))
                        currentTaskContent = MdTextbox.Text = "";
                    else
                        currentTaskContent = MdTextbox.Text = reader.GetString(2);
                    currentTaskCompleted = reader.GetBoolean(3);
                    UpdateCompleteBtn();
                }
                reader.Close();
                connection.Close();
            }
            currentTaskId = taskId;
        }

        private void SwitchMode(object sender, RoutedEventArgs e)
        {
            if (inEditingMode)
            {
                ReadingMode();
            }
            else
            {
                EditingMode();
            }
            inEditingMode = !inEditingMode;
        }

        private void EditingMode()
        {
            LiteralTexbox.Text = MdTextbox.Text;
            MdTextbox.Visibility = Visibility.Collapsed;
            LiteralTexbox.Visibility = Visibility.Visible;
            SwitchModeBtn.Content = "Switch to Reading Mode";
        }

        private void ReadingMode()
        {
            MdTextbox.Text = LiteralTexbox.Text;
            LiteralTexbox.Visibility = Visibility.Collapsed;
            MdTextbox.Visibility = Visibility.Visible;
            SwitchModeBtn.Content = "Switch to Editing Mode";
        }

        private void NewTask(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NewTaskName.Text))
                return;
            var cmd = new SqliteCommand
            {
                CommandText = $"INSERT INTO tasks VALUES (@Guid, @UserGuid, @List, @Name, @Desc, NULL, FALSE)"
            };
            cmd.Parameters.AddWithValue("@Guid", Guid.NewGuid());
            cmd.Parameters.AddWithValue("@UserGuid", PageReferences.MainPage.UserGuid);
            cmd.Parameters.AddWithValue("@List", listName);
            cmd.Parameters.AddWithValue("@Name", NewTaskName.Text);
            cmd.Parameters.AddWithValue("@Desc", NewTaskDesc.Text);
            DatabaseAccess.ExecuteNonQuery(cmd);
            GenerateList();
            NewTaskName.Text = string.Empty; NewTaskDesc.Text = string.Empty;
        }

        private void DeleteTask(object sender, RoutedEventArgs e)
        {
            var command = new SqliteCommand
            {
                CommandText = "DELETE FROM tasks WHERE guid=@Guid"
            };
            command.Parameters.AddWithValue("@Guid", currentTaskId);
            DatabaseAccess.ExecuteNonQuery(command);
            GenerateList();
        }

        private void CompleteTask(object sender, RoutedEventArgs e)
        {
            var command = new SqliteCommand
            {
                CommandText = "UPDATE tasks SET completed=@Completed WHERE guid=@Guid"
            };
            command.Parameters.AddWithValue("@Completed", !currentTaskCompleted);
            command.Parameters.AddWithValue("@Guid", currentTaskId);
            DatabaseAccess.ExecuteNonQuery(command);
            foreach (var li in list)
            {
                if ((Guid)li.Tag == currentTaskId)
                {
                    if (currentTaskCompleted)
                        li.Background = new SolidColorBrush(Windows.UI.Colors.Transparent);
                    else
                        li.Background = new SolidColorBrush(Windows.UI.Colors.LightGreen);
                    break;
                }
            }
            currentTaskCompleted = !currentTaskCompleted;
            UpdateCompleteBtn();
        }

        private void UpdateCompleteBtn()
        {
            if (currentTaskCompleted)
            {
                CompleteBtn.Content = "Mark Task as Uncompleted";
            }
            else
            {
                CompleteBtn.Content = "Mark Task as Completed";
            }
        }

        private void SaveTask(object sender, RoutedEventArgs e)
        {
            SaveTask();
        }

        private void TaskChanged(object sender, TextChangedEventArgs e)
        {
            string columnChanged;
            string newContent;
            switch ((sender as TextBox).Tag.ToString())
            {
                case "Name":
                    columnChanged = "name";
                    newContent = TaskNameTextBox.Text;
                    break;
                case "Description":
                    columnChanged = "description";
                    newContent = TaskDescTextBox.Text;
                    break;
                default:
                    return;
            }
            var command = new SqliteCommand
            {
                CommandText = $"UPDATE tasks SET {columnChanged}=@NewContent WHERE guid=@Guid"
            };
            command.Parameters.AddWithValue("@NewContent", newContent);
            command.Parameters.AddWithValue("@Guid", currentTaskId);
            DatabaseAccess.ExecuteNonQuery(command);
            foreach (var li in list)
            {
                if ((Guid)li.Tag == currentTaskId)
                {
                    var sp = li.Content as StackPanel;
                    var tb = sp.Children[columnChanged == "name" ? 0 : 1] as TextBlock;
                    tb.Text = newContent;
                    break;
                }
            }
        }
    }
}
