﻿using ordermanager.Interfaces_And_Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager.DatabaseModel
{
    public partial class GRNReciept : IVisibilityManager
    {
        public void Show()
        {
            Visibility = System.Windows.Visibility.Visible;
        }

        public void Hide()
        {
            Visibility = System.Windows.Visibility.Collapsed;
        }

        private bool m_IsChecked = false;
        public bool IsChecked
        {
            get
            {
                return m_IsChecked;
            }
            set
            {
                m_IsChecked = value;
                OnPropertyChanged("IsChecked");
            }

        }

        private System.Windows.Visibility m_Visibility = System.Windows.Visibility.Visible;
        public System.Windows.Visibility Visibility
        {
            get
            {
                return m_Visibility;
            }
            set
            {
                m_Visibility = value;
                OnPropertyChanged("Visibility");
            }
        }
    }
}
