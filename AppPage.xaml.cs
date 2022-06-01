using System;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Taskdown
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AppPage : Page
    {
        public AppPage()
        {
            this.InitializeComponent();
            PageReferences.AppPage = this;
            SidePanelFrame.Navigate(typeof(SidePanel));
        }
        /// <summary>
        /// Task list was selected in side panel
        /// </summary>
        /// <param name="listName"></param>
        public void ListSelected(string listName)
        {
            AppFrame.Navigate(typeof(ListPage));
            PageReferences.ListPage.GenerateList(listName);
        }
    }
}
