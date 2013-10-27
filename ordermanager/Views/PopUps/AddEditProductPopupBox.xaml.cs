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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ordermanager.Views.PopUps
{
    /// <summary>
    /// Interaction logic for AddEditProductPopupBox.xaml
    /// </summary>
    public partial class AddEditProductPopupBox
    {
        IMaskable maskableParent = null;
        public AddEditProductPopupBox()
        {
            InitializeComponent();
        }

        public AddEditProductPopupBox(Window parentWindow)
            : this()
        {
            maskableParent = parentWindow as IMaskable;
        }


        public string ProductName
        {
            get
            {
                return productName.Text;
            }
            set
            {
                productName.Text = value;
            }
        }

        public string StyleId
        {
            get
            {
                return styleID.Text;
            }
            set
            {
                styleID.Text = value;
            }
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

        private void positiveDecisionBtn_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void neutralDecisionBtn_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = null;
            this.Close();
        }
    }
}
