using MahApps.Metro.Controls;
using ordermanager.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ordermanager.Views.PopUps
{
    /// <summary>
    /// Interaction logic for PopupBox.xaml
    /// </summary>
    public partial class PopupBox
    {
        IMaskable maskableParent = null;
        public PopupBox()
        {
            InitializeComponent();
            this.IsVisibleChanged += PopupBox_IsVisibleChanged;
        }

        void PopupBox_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (maskableParent != null)
            {
                if (IsVisible)
                {
                    maskableParent.Mask();
                }
                else
                {
                    maskableParent.UnMask();
                }
            }
        }

        public PopupBox(Window parentWindow) : this()
        {
            maskableParent = parentWindow as IMaskable;
        }

        public string Message
        {
            get
            {
                return messageTextBlock.Text;
            }
            set
            {
                messageTextBlock.Text = value;
            }
        }

        private PopupButton m_PopupButton = PopupButton.None;
        public PopupButton PopupButton
        {
            get
            {
                return m_PopupButton;
            }
            set
            {
                m_PopupButton = value;
                switch (value)
                {
                    case MahApps.Metro.Controls.PopupButton.OK:
                        {
                            positiveDecisionBtn.Visibility = System.Windows.Visibility.Visible;
                        }
                        break;
                    case MahApps.Metro.Controls.PopupButton.OKCancel:
                        {
                            positiveDecisionBtn.Visibility = System.Windows.Visibility.Visible;
                            neutralDecisionBtn.Visibility = System.Windows.Visibility.Visible;
                        }
                        break;
                    case MahApps.Metro.Controls.PopupButton.YesNo:
                        {
                            positiveDecisionBtn.Visibility = System.Windows.Visibility.Visible;
                            negativeDecisionBtn.Visibility = System.Windows.Visibility.Visible;
                            positiveDecisionBtn.Content = "YES";
                        }
                        break;
                    case MahApps.Metro.Controls.PopupButton.YesNoCancel:
                        {
                            positiveDecisionBtn.Visibility = System.Windows.Visibility.Visible;
                            negativeDecisionBtn.Visibility = System.Windows.Visibility.Visible;
                            neutralDecisionBtn.Visibility = System.Windows.Visibility.Visible;
                            positiveDecisionBtn.Content = "YES";
                        }
                        break;
                    case MahApps.Metro.Controls.PopupButton.AddCancel:
                        {
                            positiveDecisionBtn.Visibility = System.Windows.Visibility.Visible;
                            neutralDecisionBtn.Visibility = System.Windows.Visibility.Visible;
                            positiveDecisionBtn.Content = "ADD";
                        }
                        break;
                }
            }
        }

        private void positiveDecisionBtn_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void negativeDecisionBtn_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void neutralDecisionBtn_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = null;
            this.Close();
        }
    }
}
