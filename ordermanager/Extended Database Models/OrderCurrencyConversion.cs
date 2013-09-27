using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager.DatabaseModel
{
    public partial class OrderCurrencyConversion : EntityBase, INotifyPropertyChanged
    {
        
        public decimal ValueInINRWrapper
        {
            get
            {
                return ValueInINR;
            }
            set
            {
                ValueInINR = value;
                ValidateValueInINR();
                OnPropertyChanged("ValueInINRWrapper");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #region Validation

        //Add the property validation methods in to this method to ensure validation on create button click
        public void Validate()
        {
            ValidateValueInINR();
        }

        private void ValidateValueInINR()
        {
            if (ValueInINRWrapper == 0)
            {
                AddError("ValueInINRWrapper", "Value in INR can't be Zero",false);
            }
            else
            {
                RemoveError("ValueInINRWrapper", "Value in INR can't be Zero");
            }
        }

        #endregion 

    }
}
