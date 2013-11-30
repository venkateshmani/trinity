using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ordermanager.Interfaces_And_Enums
{
    public interface IVisibilityManager
    {
        void Show();
        void Hide();

        Visibility Visibility { get; set; }
    }
}
