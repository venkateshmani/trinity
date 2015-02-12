using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ordermanager.Views.UserControls.JobOrderControls;

namespace ordermanager.ViewModel.JobOrderControls
{
    public class PrintingJoViewModel : JobOrderBase, IActionButtons
    {


        #region IActionButtons

        public string PositiveButtonContent
        {
            get { throw new NotImplementedException(); }
        }

        public string NegativeButtonContent
        {
            get { throw new NotImplementedException(); }
        }

        public System.Windows.Visibility PositiveButtonVisibility
        {
            get { throw new NotImplementedException(); }
        }

        public System.Windows.Visibility NegativeButtonVisibility
        {
            get { throw new NotImplementedException(); }
        }

        public System.Windows.Visibility CommentsButtonVisibility
        {
            get { throw new NotImplementedException(); }
        }

        public System.Windows.Visibility AddRemoveButtonVisiblity
        {
            get { throw new NotImplementedException(); }
        }

        public void RefreshUIButtons()
        {
            throw new NotImplementedException();
        }

        #endregion 
    }
}
