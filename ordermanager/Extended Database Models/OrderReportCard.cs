using ordermanager.ViewModel;
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

            private decimal m_NumberOfDays = 0;
            public decimal NumberOfDays
            {
                get
                {
                    if (m_NumberOfDays == 0)
                    {
                        CalculateNumberOfDays();
                    }
                    return m_NumberOfDays;
                }
                set
                {
                    m_NumberOfDays = value;
                    SetDatesThroughNumberOfDays();
                    ValidateNumberOfDays();
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
                    CascadeDateCalculation();
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

                    if (this.Order != null && this.Order.OrderApprovedDate != null && this.Order.OrderConfirmedDate != null)
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
                    ValidateNumberOfDays();
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


        #region Validation

            public void Validate()
            {
                ValidateNumberOfDays();
            }

            private void ValidateNumberOfDays()
            {
                if (NumberOfDaysProxy <= 0 && StartDate != null)
                {
                    AddError("NumberOfDays", "This Value will set an invalid date, Number of Days should be greater than zero", false);
                }
                else
                {
                    RemoveError("NumberOfDays", "This Value will set an invalid date, Number of Days should be greater than zero");
                }
            }

        #endregion 

        public void CalculateNumberOfDays()
        {
            if (StartDate != null && RequiredFinishDate != null)
            {
                NumberOfDaysProxy = (decimal)RequiredFinishDate.Value.Subtract(StartDate.Value).Days;
            }
            else
            {
                NumberOfDaysProxy = 0;
            }
        }

        public void SetDatesThroughNumberOfDays()
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



        private void CascadeDateCalculation()
        {
            switch (OrderReportCardType.Type)
            {
                case "Sourcing":
                    this.Order.OrderReportCardsHelperDict["Production"].SetDatesThroughNumberOfDays();
                    break;
                case "Production":
                    this.Order.OrderReportCardsHelperDict["Quality"].SetDatesThroughNumberOfDays();
                    break;
                case "Quality":
                    this.Order.OrderReportCardsHelperDict["Packaging"].SetDatesThroughNumberOfDays();
                    break;
                case "Packaging":
                    this.Order.OrderReportCardsHelperDict["Shipment"].CalculateNumberOfDays();
                    break;
                case "Shipment":
                    break;
            }
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
