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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ordermanager.Views.CommonControls
{
    /// <summary>
    /// Interaction logic for InLineComboBox.xaml
    /// </summary>
    public partial class InLineComboBox : UserControl
    {
        public InLineComboBox()
        {
            InitializeComponent();
        }

        private void ComboBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            comboxBox.IsDropDownOpen = true;   
        }
    }

    public static class Watermark
    {
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.RegisterAttached("Text",
                                                 typeof(string),
                                                 typeof(Watermark),
                                                 new FrameworkPropertyMetadata());

        public static void SetText(UIElement element, string value)
        {
            element.SetValue(TextProperty, value);
        }

        public static Boolean GetText(UIElement element)
        {
            return (Boolean)element.GetValue(TextProperty);
        }
    }
}
