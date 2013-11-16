using ordermanager.Interfaces_And_Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager.ViewModel.Execution
{
    public class ShipmentViewModel : JobExecutionViewModelBase
    {
        public ShipmentViewModel()
        {
            base.PropertyChanged += ShipmentViewModel_PropertyChanged;
        }

        void ShipmentViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsReadOnly")
                OnPropertyChanged("CanModify");
        }

        public bool CanModify
        {
            get { return DBResources.Instance.CurrentUser.UserRole.CanModifyExecutionShipping && !IsReadOnly; }
        }

        public override void AddNewRecord(DateTime date)
        {
            base.AddNewRecord(date);
        }
    }
}
