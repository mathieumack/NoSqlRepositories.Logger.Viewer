﻿using MahApps.Metro.Controls;
using MvvmCross.Wpf.Views;
using NoSqlRepositories.Logger.Viewer.Client.Wpf.Attributes;

namespace NoSqlLogReader.UI.Wpf.Views
{
    /// <summary>
    /// Logique d'interaction pour LogFilterView.xaml
    /// </summary>
    [Region("Connection")]
    public partial class ConnectionView : MvxWpfView
    {
        public ConnectionView()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Flyout flyout = ((Flyout)this.Parent.GetParentObject().GetParentObject().GetParentObject().GetParentObject().GetParentObject().GetParentObject());
            flyout.IsOpen = false;
        }
    }
}
