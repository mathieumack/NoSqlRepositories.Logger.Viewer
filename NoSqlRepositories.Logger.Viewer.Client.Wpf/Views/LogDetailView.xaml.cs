using NoSqlRepositories.Logger.Viewer.Client.Wpf.Attributes;
using System.Windows.Controls;
using MvvmCross.Wpf.Views;
using mshtml;
using System.Windows;
using System;
using NoSqlLogReader.Core;

namespace NoSqlRepositories.Logger.Viewer.Client.Wpf.Views
{
    /// <summary>
    /// Logique d'interaction pour LogDetailView.xaml
    /// </summary>
    [Region("RightPanel")]
    public partial class LogDetailView : MvxWpfView
    {
        public LogDetailView()
        {
            InitializeComponent();
        }


        private void webBrowserJson_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            webBrowserJson.Height = ((IHTMLElement2)((HTMLDocument)webBrowserJson.Document).body).scrollHeight * 0.80;
            webBrowserJson.IsEnabled = false;
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if(webBrowserJson.Document != null)
                Clipboard.SetText(((HTMLDocument)webBrowserJson.Document).body.innerText);
        }

        private void lbAttachments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Attachment attachment = (Attachment)((ListBox)sender).SelectedItem;
            if(attachment != null) { 
                string path = attachment.Path.Substring(1);
                System.Diagnostics.Process.Start(path.Insert(0, Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)));
            }
        }
    }
}
