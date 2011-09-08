using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Squirrel.Requests;
using Squirrel.Services;
using Microsoft.Phone.Reactive;
using Squirrel.Services.Abstraction;
using System.ComponentModel;

namespace Squirrel.Phone.Authentication
{
    public partial class Authenticate : PhoneApplicationPage, INotifyPropertyChanged
    {
        public Authenticate()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(Authenticate_Loaded);
        }

        void Authenticate_Loaded(object sender, RoutedEventArgs e)
        {
            this.service = new AuthService(new FourSquareContext());
            this.authBrowser.Navigated += new EventHandler<System.Windows.Navigation.NavigationEventArgs>(authBrowser_Navigated);
            this.authBrowser.Navigate(service.GetAuthorizationUrl());
            IsIntermidiate = true;
        }

        void authBrowser_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            service.GetAccessToken(e.Uri).ObserveOnDispatcher().Subscribe( x => {
                this.NavigationService.Navigate(new Uri(string.Format("/MainPage.xaml?token={0}", x), UriKind.Relative));
            });

            IsIntermidiate = false;
        }


        public bool IsIntermidiate
        {
            get
            {
                return isIntermidiate;
            }
            set
            {
                isIntermidiate = value;
                OnPropertyChanged("IsIntermidiate");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string prop)
        {
            var e = new PropertyChangedEventArgs(prop);
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, e);
        }

        private bool isIntermidiate;
        private IAuthService service;

    }
}