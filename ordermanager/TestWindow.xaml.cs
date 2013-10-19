using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
namespace ordermanager
{
    /// <summary>
    /// Interaction logic for TestWindow.xaml
    /// </summary>
    public partial class TestWindow 
    {
        public TestWindow()
        {
            InitializeComponent();
            this.Loaded += TestWindow_Loaded;
        }

        void TestWindow_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void TextBoxPopup_OnClosed(object sender, PopupClosedEventArgs eventArgs)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TextBoxPopup.ShowDialog();
        }
    }
}
