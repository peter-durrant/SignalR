#region Author: P Durrant

// This code was constructed as a learning exercise from publicly available documentation, online example code, and my own thoughts.

#endregion

using HDD.Utility;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Threading;

namespace HDD.WPF.Client
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<string> IpAddresses { get; private set; }
        public string IpAddress { get; set; }
        public string Port { get; set; }
        public string Uri { get; set; }
        private SignalR.Client.Client _client;

        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;
            IpAddresses = new ObservableCollection<string>(Network.GetHostIpAddresses());
            IpAddresses.Insert(0, "localhost");
            Port = "8080";
        }

        public void UpdateServerUri()
        {
            Uri = string.Format("http://{0}:{1}", IpAddress, Port);
            ServerUri.Content = Uri;
        }

        private void ServerIp_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            UpdateServerUri();
        }

        private void ServerPort_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            UpdateServerUri();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _client = new SignalR.Client.Client(new System.Uri(Uri));
            _client.OnMessage += (se, ev) =>
                Application.Current.Dispatcher.BeginInvoke(
                    DispatcherPriority.Normal,
                    new Action(() =>
                    {
                        Messages.Text += string.Format("\n{0}", ev.Value);
                    }));
            var connected = _client.Connect();
            Messages.Text = string.Format("Connected: {0}", connected);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_client != null)
            {
                _client.Dispose();
            }
        }
    }
}
