﻿using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ordermanager.DatabaseModel;
using ordermanager.Views.UserControls.JobOrderControls;
using Reports;

namespace ordermanager.ViewModel.JobOrderControls
{
    public class CompactingJoViewModel : JobOrderBase, IActionButtons
    {
        public CompactingJoViewModel(Order order, decimal quantity, PurchaseOrder po, string grnRefNo, GRNReciept reciept, bool jobOrderIssued, JobOrder parentJo)
        {
            this.Order = order;
            JO = new CompactingJo();
            
            JO.Order = order;
            JO.QuoteDate = order.OrderDate;
            JO.JODate = DBResources.Instance.GetServerTime();
            JO.PurchaseOrder = po;
            JO.GRNRefNo = grnRefNo;

            JO.GRNReciept = reciept;
            JO.JobOrder = parentJo;
            JO.JoNo = "C" + (DBResources.Instance.Context.CompactingJoes.Count() + 1).ToString();

            JO.Add();
        }

        public CompactingJoViewModel(CompactingJo jo)
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


        private CompactingJo m_JO = null;
        public CompactingJo JO
        {
            get
            {
                return m_JO;
            }
            set
            {
                m_JO = value;
                OnPropertyChanged("JO");
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

        public JoGenericParameters GetReportParameters()
        {
            JoGenericParameters parameters = new JoGenericParameters();

            parameters.GRNRef = JO.GRNRefNo;
            parameters.OrderRef = JO.OrderRef;
            parameters.Date = JO.JODate.ToString("dd/MM/yyyy");
            parameters.Process = JO.Process;
            parameters.QuoteDate = JO.QuoteDate.ToString("dd/MM/yyyy");
            parameters.QuoteNo = JO.QuoteNo;
            parameters.SupplierInformation = Constants.GetSupplierInformation(JO.Supplier);
            parameters.TermsAndConditions = JO.TermsAndConditions;
            parameters.JoOrderNo = JO.JoNoWrapper;

            return parameters;
        }

        public void Add()
        {
            JO.Add();
        }

        public void Delete(CompactingJoItem item)
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

                if ( (JO.Approval != null && JO.Approval.IsApproved == null && DBResources.Instance.CurrentUser.UserRole.RoleName != "Owner") ||
                      DBResources.Instance.CurrentUser.UserRole.CanModifyJobOrder == false)
                    visiblity = System.Windows.Visibility.Collapsed;

                return visiblity;
               
            }
        }

        public System.Windows.Visibility NegativeButtonVisibility
        {
            get
            {
                System.Windows.Visibility visiblity = System.Windows.Visibility.Visible;

                if ((JO.Approval != null && JO.Approval.IsApproved == null && DBResources.Instance.CurrentUser.UserRole.RoleName != "Owner") ||
                    DBResources.Instance.CurrentUser.UserRole.CanModifyJobOrder == false || JO.Approval.IsApproved == true)
                    visiblity = System.Windows.Visibility.Collapsed;

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
