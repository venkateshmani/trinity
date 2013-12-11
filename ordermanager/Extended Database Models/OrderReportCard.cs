﻿using ordermanager.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager.DatabaseModel
{
    public partial class OrderReportCard : EntityBase
    {
        #region UI Properties

            private decimal m_NumberOfDays = -1;
            public decimal NumberOfDays
            {
                get
                {
                    return m_NumberOfDays;
                }
                set
                {
                    m_NumberOfDays = value;
                    SetDatesThroughNumberOfDays();
                    OnPropertyChanged("NumberOfDays");
                }
            }


            private decimal m_NumberOfDaysInPercentage = -1;
            public decimal NumberOfDaysInPercentage
            {
                get
                {
                    if (m_NumberOfDaysInPercentage == -1)
                    {

                    }
                    return m_NumberOfDaysInPercentage;
                }
                set
                {
                    m_NumberOfDaysInPercentage = value;
                    SetDatesThroughNumberOfDaysInPercentage();
                    OnPropertyChanged("NumberOfDaysInPercentage");
                }
            }

            public DateTime? RequiredFinishDateWrapper
            {
                get
                {
                    return RequiredFinishDate;
                }
                set
                {
                    RequiredFinishDate = value;
                    OnPropertyChanged("RequiredFinishDateWrapper");
                }
            }

            bool m_IsReadOnly = false;
            public bool IsReadOnly
            {
                get
                {
                    if (OrderReportCardType.Type == "Shipment")
                        return true;

                    if (this.Order != null && this.Order.OrderApprovedDate != null && this.Order.OrderConfirmedDate == null)
                        return true;

                    return m_IsReadOnly;
                }
                set
                {
                    m_IsReadOnly = value;
                    OnPropertyChanged("IsReadOnly");
                }
            }

        #endregion 

        #region Proxy Properties

        private decimal NumberOfDaysProxy
            {
                get
                {
                    return m_NumberOfDays;
                }
                set
                {
                    m_NumberOfDays = value;
                    OnPropertyChanged("NumberOfDays");
                }
            }

            private decimal NumberOfDaysInPercetangeProxy
            {
                get
                {
                    return m_NumberOfDaysInPercentage;
                }
                set
                {
                    m_NumberOfDaysInPercentage = value;
                    OnPropertyChanged("NumberOfDaysInPercentage");
                }
            }

        #endregion 

        private void CalculateNumberOfDays()
        {
            NumberOfDaysProxy = (decimal)RequiredFinishDate.Value.Subtract(StartDate.Value).Days; 
        }

        private void SetDatesThroughNumberOfDays()
        {
            if (StartDate != null)
            {
                RequiredFinishDateWrapper = StartDate.Value.Add(new TimeSpan(Convert.ToInt32(NumberOfDays), 0, 0, 0, 0));
            }
        }

        private void SetDatesThroughNumberOfDaysInPercentage()
        {
            //TODO: Later :) 
        }


        private DateTime? StartDate
        {
            get
            {
                DateTime? startDate = null;

                switch (OrderReportCardType.Type)
                {
                    case "Sourcing":
                        if (this.Order.OrderConfirmedDate != null)
                        {
                            startDate = this.Order.OrderConfirmedDate.Value;
                        }
                        else 
                        {
                            startDate = DBResources.Instance.GetServerTime();
                        }
                        break;
                    case "Production":
                         startDate = this.Order.OrderReportCardsHelperDict["Sourcing"].RequiredFinishDate;
                        break;
                    case "Quality":
                        startDate = this.Order.OrderReportCardsHelperDict["Production"].RequiredFinishDate;
                        break;
                    case "Packaging":
                        startDate = this.Order.OrderReportCardsHelperDict["Quality"].RequiredFinishDate;
                        break;
                    case "Shipment":
                        startDate = this.Order.OrderReportCardsHelperDict["Packaging"].RequiredFinishDate;
                        break;
                }

                return startDate;
            }
        }

    }
}
