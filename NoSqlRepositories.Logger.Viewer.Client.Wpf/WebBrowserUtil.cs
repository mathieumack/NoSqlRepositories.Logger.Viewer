using CefSharp;
using CefSharp.Wpf;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace NoSqlRepositories.Logger.Viewer.Client.Wpf
{
    public class WebBrowserUtil
    {
        public static readonly DependencyProperty InputStringProperty =
            DependencyProperty.RegisterAttached("InputString", typeof(string), typeof(WebBrowserUtil), new UIPropertyMetadata(default(string), new PropertyChangedCallback(OnInputStringChanged)));

        public static void SetInputString(UIElement element, string value)
        {
            element.SetValue(InputStringProperty, value);
        }


        public static string GetInputString(UIElement element)
        {
            return (string)element.GetValue(InputStringProperty);
        }

        public static void OnInputStringChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            ChromiumWebBrowser webBrowserJson = o as ChromiumWebBrowser;
            string content = e.NewValue as string;
            if (!string.IsNullOrWhiteSpace(content) && content != "rendering")
            {
                string styledContent = "<meta charset=\"UTF-8\" /><style>" + File.ReadAllText(@"./Stylesheets/ContentLogStyle.css") + "</style><pre>" + content + "</pre>";
                styledContent = styledContent.Replace("\\r\\n", "</br>");
                webBrowserJson.LoadHtml(styledContent, "http://rendering/", Encoding.UTF8);

                // Height calculation
                int lines = 0, n = 0;

                while ((n = content.IndexOf("\n", n, StringComparison.InvariantCulture)) != -1)
                {
                    n += "\n".Length;
                    ++lines;
                }
                n = 0;

                while((n = styledContent.IndexOf("</br>", n, StringComparison.InvariantCulture)) != -1)
                {
                    n += "</br>".Length;
                    ++lines;
                }
                ++lines;

                webBrowserJson.Height = lines * 15.5 + 10;

                // Width calculation
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(content);
                var json = doc.DocumentNode.InnerText;


                var result = Regex.Split(json, "\r\n|\r|\n|à|---");
                int maxLen = result.Max(l => l.Length);

                webBrowserJson.Width = maxLen * 8;
                
            }
        }
    }
}
