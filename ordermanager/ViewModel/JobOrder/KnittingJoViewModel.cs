using ordermanager.DatabaseModel;
using ordermanager.Views.UserControls.JobOrderControls;
using Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager.ViewModel.JobOrderControls
{
    public class KnittingJoViewModel: ViewModelBase, IActionButtons
    {
        public KnittingJoViewModel(Order order, decimal quantity, GRNReciept reciept, bool jobOrderIssued, JobOrder parentJO)
        {
            this.Order = order;
            JO = new KnittingJO();
            JO.Order = order;
            JO.QuoteDate = order.OrderDate;
            JO.JoDate = DBResources.Instance.GetServerTime();
            JO.JobOrder = parentJO;
            JO.GRNReciept = reciept;

            JO.JoNoWrapper = "K" + (DBResources.Instance.Context.KnittingJOes.Count() + 1).ToString();
            JO.Add();
        }

        public KnittingJoViewModel(KnittingJO jo)
        {
            JO = jo;
            this.Order = jo.Order;
        }

        private KnittingJO m_JO = null;
        public KnittingJO JO
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

        public bool IsReadOnly
        {
            get
            {
                if (JO.Approval != null && (JO.Approval.IsApproved == null || JO.Approval.IsApproved == true))
                    return true;

                return false;
            }
        }

        public KnittingJoParameters GetReportParameters()
        {
            KnittingJoParameters parameters = new KnittingJoParameters();

            parameters.JoOrderNo = JO.JoNoWrapper;
            parameters.QualitySpecification = JO.QualitySpecifications;
            parameters.JoDate = JO.JoDate.ToString("dd/MM/yyyy");
            parameters.QuoteDate = JO.QuoteDate.ToString("dd/MM/yyyy");
            parameters.QuoteNumber = JO.QuoteNo;
            parameters.SupplierInformation = Constants.GetSupplierInformation(JO.Company);
            parameters.TermsAndConditions = JO.TermsAndConditions;

            return parameters;
        }

        public void Add()
        {
            JO.Add();
        }

        public void Delete(KnittingJoItem item)
        {
            JO.Remove(item);
        }

        #region IActionButtons Implementation


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

                if (DBResources.Instance.CurrentUser.UserRole.CanModifyJobOrder == false)
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

                if (DBResources.Instance.CurrentUser.UserRole.CanModifyJobOrder == false || JO.Approval.IsApproved == true)
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

        #endregion
    }
}
