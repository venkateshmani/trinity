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
            
            base.OnStartup(e);
        }

        private void TextBox_GotMouseCaptureEvent(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).SelectAll();
           
        }
    }
}
