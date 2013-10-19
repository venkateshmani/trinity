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
    /// Interaction logic for CommentBox.xaml
    /// </summary>
    public partial class CommentBox 
    {
        IMaskable maskableParent = null;
        public CommentBox()
        {
            InitializeComponent();
            this.IsVisibleChanged += CommentBox_IsVisibleChanged;
        }

        void CommentBox_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
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

        public CommentBox(Window parentWindow) : this()
        {
            maskableParent = parentWindow as IMaskable;
        }

        public string UpdateBtnText
        {
            get
            {
                return updateToDatabaseBtn.Content.ToString();
            }
            set
            {
                updateToDatabaseBtn.Content = value;
            }
        }

        public string Comment
        {
            get
            {
                return commentTextBox.Text;
            }
            set
            {
                commentTextBox.Text = value;
            }
        }

        private void updateToDatabaseBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.DialogResult = true;
            }
            catch
            {

            }
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.DialogResult = false;
            }
            catch
            {

            }
        }
    }
}
