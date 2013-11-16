using ordermanager.DatabaseModel;
using ordermanager.Interfaces_And_Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager.ViewModel.Execution
{
    public class CuttingViewModel : JobExecutionViewModelBase
    {
        public CuttingViewModel()
        {
            base.PropertyChanged += CuttingViewModel_PropertyChanged;
        }

        void CuttingViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsReadOnly")
                 OnPropertyChanged("CanModify");
        }

        public bool CanModify
        {
            get 
            {
                return DBResources.Instance.CurrentUser.UserRole.CanModifyExecutionCutting && !IsReadOnly; 
            }
        }

        public override void AddNewRecord(DateTime date)
        {
            base.AddNewRecord(date);
        }
    }
}
