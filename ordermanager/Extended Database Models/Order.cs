﻿using ordermanager.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager.DatabaseModel    
{
    public partial class Order : EntityBase
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

            public DateTime? InternalDeliveryDateWrapper
            {
                get
                {
                    return InternalDeliveryDate;
                }
                set
                {
                    InternalDeliveryDate = value;
                    this.OrderReportCardsHelperDict["Shipment"].RequiredFinishDateWrapper = value;
                    this.OrderReportCardsHelperDict["Shipment"].CalculateNumberOfDays(); 
                    OnPropertyChanged("InternalDeliveryDateWrapper");
                    ValidateInternalDeliveryDate();
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

            public virtual decimal? CommissionValueWrapper
            {
                get
                {
                    return CommissionValue;
                }
                set
                {
                    CommissionValue = value;
                    OnPropertyChanged("CommissionValueWrapper");
                }
            }

            public virtual CommissionValueType CommissionValueTypeWrapper
            {
                get
                {
                    return CommissionValueType;
                }
                set
                {
                    CommissionValueType = value;
                    OnPropertyChanged("CommissionValueType");
                    ValidateCommissionType();
                }
            }


            public System.Windows.Visibility TargetDatesGridVisibility
            {
                get
                {
                    if (OrderStatusID >= (int)OrderStatusEnum.EnquiryApproved)
                    {
                        return System.Windows.Visibility.Visible;
                    }

                    return System.Windows.Visibility.Collapsed;
                }
            }
                 
    

        #endregion 

        #region Helpers


            private Dictionary<string, OrderReportCard> m_orderReportCardsDict = null;
            public Dictionary<string, OrderReportCard> OrderReportCardsHelperDict
            {
                get
                {
                    if (m_orderReportCardsDict == null)
                    {
                        m_orderReportCardsDict = new Dictionary<string, OrderReportCard>();
                        foreach (OrderReportCard reportCard in this.OrderReportCards)
                        {
                            m_orderReportCardsDict.Add(reportCard.OrderReportCardType.Type, reportCard);
                        }
                    }
                    return m_orderReportCardsDict;
                }
            }
    
            private ObservableCollection<Company> m_Suppliers = null;
            public ObservableCollection<Company> Suppliers
            {
                get
                {
                    if ((m_Suppliers == null || m_Suppliers.Count == 0) && this.OrderProducts != null)
                    {
                        m_Suppliers = new ObservableCollection<Company>();
                        foreach (OrderProduct product in this.OrderProducts)
                        {
                            if (product.ProductMaterials != null)
                            {
                                foreach (ProductMaterial material in product.ProductMaterials)
                                {
                                    if (material.ProductMaterialItems != null)
                                    {
                                        foreach (ProductMaterialItem materialItem in material.ProductMaterialItems)
                                        {
                                            if (!m_Suppliers.Contains(materialItem.Company))
                                            {
                                                m_Suppliers.Add(materialItem.Company);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    return m_Suppliers;
                }
                set
                {
                    m_Suppliers = value;
                }
            }

        #endregion 

        #region Data Validation

            //Add the property validation methods in to this method to ensure validation on create button click
        public void Validate()
        {
            ValidateCustomer();
            ValidateExpectedDeliveryDate();
            ValidateAgent();
            ValidateOrderThrough();
            ValidateCommissionType();
        }

        public void ValidateReportCards()
        {
            foreach (OrderReportCard reportCard in OrderReportCards)
            {
                reportCard.HasUserClickedSaveOrSubmit = true;
                reportCard.Validate();
            }
        }
             

        private void ValidateCommissionType()
        {
            if (CommissionValue == 0 || CommissionValue == null || CommissionValueType != null)
            {
                RemoveError("CommissionValueTypeWrapper", "Select a commission type");
            }
            else
            {
                AddError("CommissionValueTypeWrapper", "Select a commission type", false);
            }
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


        private void ValidateInternalDeliveryDate()
        {
            if (InternalDeliveryDate != null)
            {
                if (InternalDeliveryDate < OrderDate)
                {
                    AddError("InternalDeliveryDateWrapper", "Internal delivery date cannot be before order date", false);
                }
                else
                {
                    RemoveError("InternalDeliveryDateWrapper", "Internal delivery date cannot be before order date");
                }
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

        public override bool HasUserClickedSaveOrSubmit
        {
            get
            {
                return base.HasUserClickedSaveOrSubmit;
            }
            set
            {
                base.HasUserClickedSaveOrSubmit = value;
                foreach (OrderProduct product in OrderProducts)
                {
                    product.HasUserClickedSaveOrSubmit = value;
                }
            }
        }

        #endregion 
    }
}
