using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ordermanager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            EventManager.RegisterClassHandler(typeof(TextBox),
                                             TextBox.GotMouseCaptureEvent,
                                             new RoutedEventHandler(TextBox_GotMouseCaptureEvent));

            EventManager.RegisterClassHandler(typeof(TextBox),
                                             TextBox.GotFocusEvent,
                                             new RoutedEventHandler(TextBox_GotFocusEvent));

           
            
            base.OnStartup(e);
           
        }

        private void TextBox_GotFocusEvent(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).SelectAll();
        }

        private void TextBox_GotMouseCaptureEvent(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).SelectAll();
        }
    }
}
