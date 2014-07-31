﻿using ordermanager.DatabaseModel;
using ordermanager.Views.UserControls.JobOrderControls;
using Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ordermanager.ViewModel.JobOrderControls
{
    public class DyeingJoViewModel : ViewModelBase, IActionButtons
    {
        public DyeingJoViewModel(Order order, decimal quantity, PurchaseOrder po, string grnRefNo, GRNReciept reciept, bool jobOrderIssued, JobOrder parentJo)
        {
            this.Order = order;
            JO = new DyeingJO();
            JO.Order = order;
            JO.QuoteDate = order.OrderDate;
            JO.JODate = DBResources.Instance.GetServerTime();
            JO.PurchaseOrder = po;
            JO.GRNRefNo = grnRefNo;

            JO.GRNReciept = reciept;
            JO.JobOrder = parentJo;
            JO.JoNo = "D" + (DBResources.Instance.Context.DyeingJOes.Count() + 1).ToString();

            JO.Add();
        }

        public DyeingJoViewModel(DyeingJO jo)
        {
            JO = jo;
            this.Order = jo.Order;
        }

        public bool IsReadOnly
        {
            get
            {
                if(JO.Approval != null && (JO.Approval.IsApproved == null || JO.Approval.IsApproved == true))
                    return true;

                return false;
            }
        }


        private DyeingJO m_JO = null;
        public DyeingJO JO
        {
            get
            {
                return m_JO;
            }
            set
            {
                m_JO = value;
            }
        }

        public void Discard()
        {
            DBResources.Instance.DiscardChanges();
        }

        public string Save()
        {
            string message = string.Empty;

            if (JO.Order == null)
                JO.Order = this.Order;

            DBResources.Instance.Context.SaveChanges();

            return message;
        }

        public DyeingJoParameters GetReportParameters()
        {
            DyeingJoParameters parameters = new DyeingJoParameters();

            parameters.GRNRef = JO.GRNRefNo;
            parameters.OrderRef = JO.OrderRef;
            parameters.Date = JO.JODate.ToString("dd/MM/yyyy");
            parameters.Process = JO.Process;
            parameters.PurchaseOrderNumber = JO.PurchaseOrderNumber;
            parameters.QuoteDate = JO.QuoteDate.ToString("dd/MM/yyyy");
            parameters.QuoteNo = JO.QuoteNo;
            parameters.SupplierInformation = Constants.GetSupplierInformation(JO.Supplier);
            parameters.TermsAndConditions = JO.TermsAndConditions;

            return parameters;
        }

        public void Add()
        {
            JO.Add();
        }

        public void Delete(DyeingJoItem item)
        {
            JO.Remove(item);
        }

        public string PositiveButtonContent
        {
            get
            {
                string content = string.Empty;
                if (JO.Approval == null)
                {
                    content = "Generate";
                }
                else if (JO.Approval.IsApproved == null)
                {
                    content = "Approve";
                }
                else if (JO.Approval.IsApproved == false)
                {
                    content = "Submit";
                }
                else if (JO.Approval.IsApproved == true)
                {
                    content = "PDF";
                }

                return content;
            }
        }

        public string NegativeButtonContent
        {
            get
            {
                string content = string.Empty;
                if (JO.Approval == null)
                {
                    content = "Discard";
                }
                else if (JO.Approval.IsApproved == null)
                {
                    content = "Reject";
                }
                else if (JO.Approval.IsApproved == false)
                {
                    content = "Delete";
                }

                return content;
            }
        }

        public System.Windows.Visibility PositiveButtonVisibility
        {
            get
            {
                System.Windows.Visibility visiblity = System.Windows.Visibility.Visible;

                if (DBResources.Instance.CurrentUser.UserRole.CanApprovePurchaseOrder == false)
                {
                    visiblity = System.Windows.Visibility.Collapsed;
                }
                return visiblity;
               
            }
        }

        public System.Windows.Visibility NegativeButtonVisibility
        {
            get
            {
                System.Windows.Visibility visiblity = System.Windows.Visibility.Visible;

                if (DBResources.Instance.CurrentUser.UserRole.CanApprovePurchaseOrder == false || JO.Approval.IsApproved == true)
                {
                    visiblity = System.Windows.Visibility.Collapsed;
                }
                return visiblity;
               
            }
           
        }

        public System.Windows.Visibility CommentsButtonVisibility
        {
            get
            {
                System.Windows.Visibility visiblity = System.Windows.Visibility.Visible;

                if (DBResources.Instance.CurrentUser.UserRole.CanApprovePurchaseOrder == false || JO.Approval == null)
                {
                    visiblity = System.Windows.Visibility.Collapsed;
                }

                return visiblity;
            }
        }

        public void RefreshUIButtons()
        {
            foreach (var pInfo in typeof(IActionButtons).GetProperties())
            {
                OnPropertyChanged(pInfo.Name);
            }

            //Dirty Code
            OnPropertyChanged("IsReadOnly");
        }


        public System.Windows.Visibility AddRemoveButtonVisiblity
        {
            get
            {
                System.Windows.Visibility visiblity = System.Windows.Visibility.Visible;

                if ((JO.Approval != null) && (DBResources.Instance.CurrentUser.UserRole.CanApprovePurchaseOrder == false || JO.Approval.IsApproved == null || JO.Approval.IsApproved == true))
                {
                    visiblity = System.Windows.Visibility.Collapsed;
                }

                return visiblity;
            }
        }
    }
}
