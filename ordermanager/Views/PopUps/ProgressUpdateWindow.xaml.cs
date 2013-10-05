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
    /// Interaction logic for ProgressUpdateWindow.xaml
    /// </summary>
    public partial class ProgressUpdateWindow 
    {
        IMaskable maskableParent = null;

        public ProgressUpdateWindow()
        {
            InitializeComponent();
            this.IsVisibleChanged += ProgressUpdateWindow_IsVisibleChanged;
        }

        public ProgressUpdateWindow(Window parentWindow, string message) : this()
        {
            maskableParent = parentWindow as IMaskable;
            this.UpdateString = message;
        }

        void ProgressUpdateWindow_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
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

        public string UpdateString
        {
            get
            {
                return updateText.Text;
            }
            set
            {
                updateText.Text = value;
            }
        }
    }
}
