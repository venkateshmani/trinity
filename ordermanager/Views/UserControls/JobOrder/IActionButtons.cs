using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ordermanager.Views.UserControls.JobOrder
{
    interface IActionButtons
    {
        string PositiveBttonContent { get;  }
        string NegativeButtonContent { get; }

        Visibility PositiveButtonVisibility { get;  }
        Visibility NegativeButtonVisibility {get;}
        Visibility CommentsButtonVisibility { get;  }

        void RefreshUIButtons();
    }
}
