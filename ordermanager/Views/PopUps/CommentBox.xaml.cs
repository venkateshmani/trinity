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
        public CommentBox()
        {
            InitializeComponent();
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
