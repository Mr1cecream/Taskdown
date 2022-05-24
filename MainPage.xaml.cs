﻿using System;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Taskdown
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public Guid? UserGuid { get; set; }
        public MainPage()
        {
            this.InitializeComponent();
            PageReferences.MainPage = this;
            TopFrame.Navigate(typeof(LoginPage));
        }

        public void Login()
        {
            TopFrame.Navigate(typeof(AppPage));
        }

        public void Logout()
        {
            UserGuid = null;
            TopFrame.Navigate(typeof(LoginPage));
        }
    }
}
