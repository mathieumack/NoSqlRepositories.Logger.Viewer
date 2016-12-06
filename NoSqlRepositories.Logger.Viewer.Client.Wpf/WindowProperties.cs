using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace NoSqlRepositories.Logger.Viewer.Client.Wpf
{
    /// <summary>
    /// WindowProperties class
    /// </summary>
    public static class WindowProperties
    {
        /// <summary>
        /// Window close event Dependecy property
        /// </summary>
        public static readonly DependencyProperty WindowClosingProperty =
           DependencyProperty.RegisterAttached("WindowClosing", typeof(ICommand), typeof(WindowProperties), new UIPropertyMetadata(null, WindowClosing));

        /// <summary>
        /// get the closing command
        /// </summary>
        /// <param name="depObj"></param>
        /// <returns></returns>
        public static object GetWindowClosing(DependencyObject depObj)
        {
            return (ICommand)depObj.GetValue(WindowClosingProperty);
        }

        /// <summary>
        /// Set the closing command
        /// </summary>
        /// <param name="depObj"></param>
        /// <param name="value"></param>
        public static void SetWindowClosing(DependencyObject depObj, ICommand value)
        {
            depObj.SetValue(WindowClosingProperty, value);
        }

        /// <summary>
        /// Window closing
        /// </summary>
        /// <param name="depObj"></param>
        /// <param name="e"></param>
        private static void WindowClosing(DependencyObject depObj, DependencyPropertyChangedEventArgs e)
        {
            var element = (Window)depObj;

            if (element != null)
                element.Closing += OnWindowClosing;

        }

        /// <summary>
        /// Window closing event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void OnWindowClosing(object sender, CancelEventArgs e)
        {   
            // IF there is a focused element
            if (Keyboard.FocusedElement != null)
            {
                //clear the focus
                Keyboard.ClearFocus();
            }

            ICommand command = (ICommand)GetWindowClosing((DependencyObject)sender);
            command.Execute((Window)sender);
        }
    }
}
