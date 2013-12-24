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
                CartonBox newCartonBox = new CartonBox();
                newCartonBox.Number = boxNumber;
                newCartonBox.OrderID = this.Order.OrderID;

                OrderManagerDBEntities newManager = new OrderManagerDBEntities();
                newManager.CartonBoxes.Add(newCartonBox);
                newManager.SaveChanges();
                newManager.Dispose();
                
                return true;
            }

            return false;
        }
    }
}
