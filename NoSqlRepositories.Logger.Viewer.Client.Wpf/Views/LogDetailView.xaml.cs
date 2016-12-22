using NoSqlRepositories.Logger.Viewer.Client.Wpf.Attributes;
using System.Windows.Controls;
using MvvmCross.Wpf.Views;
using mshtml;
using System.Windows;
using System;
using NoSqlLogReader.Core;
using CefSharp.Wpf;
using MahApps.Metro;
using System.IO;
using CefSharp;
using System.Linq;
using System.Threading;
using HtmlAgilityPack;
using System.Text.RegularExpressions;

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

            webBrowserJson.Height = 200;
            webBrowserJson.Width = 200;
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            
            webBrowserJson.GetSourceAsync().ContinueWith(taskHtml =>
            {
                var html = taskHtml.Result;
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(html);
                var json = doc.DocumentNode
                              .Descendants("pre").First().InnerText;
                Thread thread = new Thread(() => Clipboard.SetText(json));
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
                thread.Join();
            });
                
        }

        private void lbAttachments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Attachment attachment = (Attachment)((ListBox)sender).SelectedItem;
            if(attachment != null) { 
                string path = attachment.Path.Substring(1);
                System.Diagnostics.Process.Start(path.Insert(0, Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)));
            }
        }

        private void webBrowserJson_TitleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            string content = ((ChromiumWebBrowser)sender).Title;
            if (!string.IsNullOrWhiteSpace(content) && content != "rendering")
            {
                string styledContent = "<style>" + File.ReadAllText(@"./Stylesheets/ContentLogStyle.css") + "</style><pre>" + content + "</pre>";
                WebBrowserExtensions.LoadHtml(webBrowserJson, styledContent, "http://rendering/");

                // Height calculation
                int lines = 0, n = 0;

                while ((n = content.IndexOf("\n", n, StringComparison.InvariantCulture)) != -1)
                {
                    n += "\n".Length;
                    ++lines;
                }
                ++lines;

                webBrowserJson.Height = lines * 15.5 + 10;

                // Width calculation
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(content);
                var json = doc.DocumentNode.InnerText;
                             

                var result = Regex.Split(json, "\r\n|\r|\n");
                int maxLen = result.Max(l => l.Length);

                webBrowserJson.Width = maxLen * 8;
            }
        }

    }
}
