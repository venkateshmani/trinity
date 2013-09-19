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
using System.Windows.Controls.Primitives;
using System.Collections.ObjectModel;

namespace ordermanager.Views.CommonControls
{
    /// <summary>
    /// Interaction logic for InLineComboBox.xaml
    /// </summary>
    public partial class InLineComboBox : UserControl
    {
        private ObservableCollection<string> SuggestableItems = null; 
        public InLineComboBox()
        {
            InitializeComponent();
            DefaultDisplayText = "Type Here";
            SuggestableItems = new ObservableCollection<string>() { "A", "B", "C"};
        }

        private void SetBindings()
        {
            
        }

        Button inlineAddButton = null;

        private void inlineAddBtn_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        public string SelectedItem
        {
            get
            {
                if (comboxBox.SelectedItem != null)
                {
                    return comboxBox.SelectedItem.ToString();    
                }

                return string.Empty;
            }
            set
            {
                comboxBox.SelectedItem = value;
            }
        }

        public string m_DefaultDisplayText = string.Empty;
        public string DefaultDisplayText
        {
            get
            {
                return m_DefaultDisplayText;
            }
            set
            {
                m_DefaultDisplayText = value;
                comboxBox.Text = value;
            }
        }

        private void comboxBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (inlineAddButton == null)
            {
                ToggleButton tgglwBtn = comboxBox.Template.FindName("ToggleButton", comboxBox) as ToggleButton;
                inlineAddButton = tgglwBtn.Template.FindName("inlineAddBtn", tgglwBtn) as Button;
            }
     
            if (inlineAddButton != null)
            {

                if (string.IsNullOrEmpty(comboxBox.Text) || comboxBox.Text.ToLower() == DefaultDisplayText.ToLower() || !SuggestableItems.Contains(comboxBox.Text))
                {
                    inlineAddButton.Visibility = System.Windows.Visibility.Collapsed;
                }
                else
                {
                    inlineAddButton.Visibility = System.Windows.Visibility.Visible;
                }
            }
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
