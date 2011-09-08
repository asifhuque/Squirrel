using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Phone.Controls;
using System.ComponentModel;

namespace Squirrel.Phone.Authentication
{
    public partial class MainPage : PhoneApplicationPage , INotifyPropertyChanged
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            const string fragment = "?token=";

            string url = e.Uri.ToString();

            int start = url.IndexOf(fragment);

            if (start > -1)
            {
                int index = start + fragment.Length;
                AccessToken = url.Substring(index, url.Length - index);
            }
        }

        public string AccessToken
        {
            get
            {
                return accessToken;
            }
            set
            {
                accessToken = value;
                OnPropertyChanged("AccessToken");
            }
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/Authenticate.xaml", UriKind.Relative));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string prop)
        {
            var e = new PropertyChangedEventArgs(prop);
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, e);
        }
     
        private string accessToken = "#Token#";
    }
}