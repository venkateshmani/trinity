using ordermanager.Views.UserControls.JobOrderControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager.ViewModel.JobOrderControls
{
    public class CreateJOViewModel : ViewModelBase,  IActionButtons
    {
        private IActionButtons m_CurrentViewActionButtons = null;
        public IActionButtons CurrentViewActionButtons
        {
            get
            {
                return m_CurrentViewActionButtons;
            }
            set
            {
                m_CurrentViewActionButtons = value;
            }
        }

        public string PositiveButtonContent
        {
            get
            {
                return CurrentViewActionButtons.PositiveButtonContent;
            }
        }

        public string NegativeButtonContent
        {
            get
            {
                return CurrentViewActionButtons.NegativeButtonContent; 
            }
        }

        public System.Windows.Visibility PositiveButtonVisibility
        {
            get { return CurrentViewActionButtons.PositiveButtonVisibility; }
        }

        public System.Windows.Visibility NegativeButtonVisibility
        {
            get { return CurrentViewActionButtons.NegativeButtonVisibility; }
        }

        public System.Windows.Visibility CommentsButtonVisibility
        {
            get { return CurrentViewActionButtons.CommentsButtonVisibility; }
        }

        public System.Windows.Visibility AddRemoveButtonVisiblity
        {
            get { return CurrentViewActionButtons.AddRemoveButtonVisiblity; }
        }

        public void RefreshUIButtons()
        {
            CurrentViewActionButtons.RefreshUIButtons();
            foreach (var pInfo in typeof(IActionButtons).GetProperties())
            {
                OnPropertyChanged(pInfo.Name);
            }
        }
    }
}
