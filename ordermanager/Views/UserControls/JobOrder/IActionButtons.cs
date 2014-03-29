using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ordermanager.Views.UserControls.JobOrderControls
{
    public interface IActionButtons
    {
        string PositiveButtonContent { get;  }
        string NegativeButtonContent { get; }

        Visibility PositiveButtonVisibility { get;  }
        Visibility NegativeButtonVisibility {get;}
        Visibility CommentsButtonVisibility { get;  }
        Visibility AddRemoveButtonVisiblity { get; }

        void RefreshUIButtons();
    }
}
