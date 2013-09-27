using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager.DatabaseModel
{
    public partial class Order : EntityBase,  INotifyPropertyChanged
    {

        #region Property Wrappers

            public virtual Company Customer
            {
                get
                {
                    return Company;
                }
                set
                {
                    Company = value;

                    if (value != null)
                    {
                        CustomerID = value.CompanyID;
                    }

                    OnPropertyChanged("Customer");
                    ValidateCustomer();
                }
            }

            public virtual Company Agent
            {
                get
                {
                    return Company1;
                }
                set
                {
                    Company1 = value;

                    if (value != null)
                    {
                        OrderByID = value.CompanyID;
                    }

                    OnPropertyChanged("Agent");
                    ValidateAgent();
                }
            }

            public virtual DateTime? ExpectedDeliveryDateWrapper
            {
                get
                {
                    return ExpectedDeliveryDate;
                }
                set
                {
                    ExpectedDeliveryDate = value;
                    OnPropertyChanged("ExpectedDeliveryDateWrapper");
                    ValidateExpectedDeliveryDate();
                }
            }

            public virtual OrderThrough OrderThroughWrapper
            {
                get
                {
                    return OrderThrough;
                }
                set
                {
                    OrderThrough = value;
                    OnPropertyChanged("OrderThroughWrapper");
                    ValidateOrderThrough();
                }
            }
    

        #endregion 

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #region Data Validation

        //Add the property validation methods in to this method to ensure validation on create button click
        public void Validate()
        {
            ValidateCustomer();
            ValidateExpectedDeliveryDate();
            ValidateAgent();
            ValidateOrderThrough();
        }

        private void ValidateOrderThrough()
        {
            if (OrderThrough == null)
            {
                AddError("OrderThroughWrapper", "Select order through", false);
            }
            else
            {
                RemoveError("OrderThroughWrapper", "Select order through");
            }
        }

        private void ValidateExpectedDeliveryDate()
        {
            if (OrderDate != null)
            {
                if (ExpectedDeliveryDate == null)
                {
                    AddError("ExpectedDeliveryDateWrapper", "Choose delivery date expected by customer",false);
                }
                else
                {
                    RemoveError("ExpectedDeliveryDateWrapper", "Choose delivery date expected by customer");
                    if (ExpectedDeliveryDate < OrderDate)
                    {
                        AddError("ExpectedDeliveryDateWrapper", "Delivery date cannot be before order date", false);
                    }
                    else
                    {
                        RemoveError("ExpectedDeliveryDateWrapper", "Delivery date cannot be before order date");
                    }
                }
            }
        }

        private void ValidateCustomer()
        {
            if (Customer == null)
            {
                AddError("Customer", "Select a customer", false);
            }
            else
            {
                RemoveError("Customer", "Select a customer");
            }
        }

        private void ValidateAgent()
        {
            if (Agent == null)
            {
                AddError("Agent", "Select through whom the order was placed", false);
            }
            else
            {
                RemoveError("Agent", "Select through whom the order was placed");
            }
        }

        #endregion 
    }
}
