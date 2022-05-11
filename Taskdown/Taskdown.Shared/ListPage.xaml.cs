using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Taskdown
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ListPage : Page
    {
        public ListPage()
        {
            this.InitializeComponent();
            PageReferences.ListPage = this;
        }

        public void GenerateList(string listName)
        {
            // TODO: call to api to get list of tasks

        }

        private ListViewItem BuildTask(Guid id, string name, string description)
        {
            var li = new ListViewItem()
            {
                Tag = id,
            };
            var sp = new StackPanel() 
            { 
                Orientation = Orientation.Vertical,
            };

            var tbName = new TextBox()
            {
                Text = name,
                Foreground = new SolidColorBrush(Windows.UI.Colors.CornflowerBlue),
                FontSize = 16,
                FontFamily = new FontFamily("Comic Sans MS"),
            };
            var tbDesc = new TextBox()
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

            return li;
        }

        private void TaskSelected(object sender, RoutedEventArgs e)
        {
            var li = TaskList.SelectedItem as ListViewItem;
            if (li == null || li.Tag == null) return;
            Guid taskId = (Guid) li.Tag;
            // TODO: call api to generate markdown text block
        }
    }
}
