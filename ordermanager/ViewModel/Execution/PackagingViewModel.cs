using ordermanager.DatabaseModel;
using ordermanager.Interfaces_And_Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager.ViewModel.Execution
{
    public class PackagingViewModel : JobExecutionViewModelBase
    {
        public PackagingViewModel()
        {
            base.PropertyChanged += PackagingViewModel_PropertyChanged;
        }

        void PackagingViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsReadOnly")
                OnPropertyChanged("CanModify");
        }

        public bool CanModify
        {
            get { return DBResources.Instance.CurrentUser.UserRole.CanModifyExecutionPackaging && !IsReadOnly; }
        }

        public override void AddNewRecord(DateTime date)
        {
            base.AddNewRecord(date);
        }

     

        public bool AddNewCartonBox(string boxNumber)
        {
            var existingCartonBox = (from cartonbox in this.Order.CartonBoxes
                                     where cartonbox.Number == boxNumber
                                     select cartonbox).FirstOrDefault();

            if (existingCartonBox == null)
            {
                CartonBox newCartonBox = DBResources.Instance.Context.CartonBoxes.Create();
                newCartonBox.Number = boxNumber;
                newCartonBox.Order = Order;
                this.Order.CartonBoxes.Add(newCartonBox);
                
                return true;
            }

            return false;
        }
    }
}
