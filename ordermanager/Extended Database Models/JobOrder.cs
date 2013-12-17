using ordermanager.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ordermanager.DatabaseModel
{
    public partial class JobOrder : EntityBase
    {
        #region Wrappers

        private decimal m_Tolerance = -1M;
        private bool m_CanIssueToNextJob = false;
        private bool m_CanCreateNewJobForFailedQuantity = false;
        private bool m_SendToSpecialApproval = false;
        private string m_jobOrderNumber = string.Empty;
        private JobOrderReceipt m_JobOrderReceiptWrapper = null;

        public JobOrderReceipt JobOrderReceiptsWrapper
        {
            get
            {
                if (m_JobOrderReceiptWrapper == null)
                {
                    if (JobOrderReceipts.Count == 0)
                    {
                        m_JobOrderReceiptWrapper = new JobOrderReceipt();
                        this.JobOrderReceipts.Add(m_JobOrderReceiptWrapper);
                    }
                    m_JobOrderReceiptWrapper = JobOrderReceipts.FirstOrDefault<JobOrderReceipt>();
                    SetAccess();
                }
                return m_JobOrderReceiptWrapper;
            }
        }

        public JobOrderType JobOrderTypeWrapper
        {
            get
            {
                return JobOrderType;
            }
            set
            {
                JobOrderType = value;
            }
        }

        public decimal ChargesInINRWrapper
        {
            get
            {
                return ChargesInINR;
            }
            set
            {
                ChargesInINR = value;
            }
        }

        public DateTime RequiredDateWrapper
        {
            get
            {
                return RequiredDate;
            }
            set
            {
                RequiredDate = value;
            }
        }

        public Nullable<decimal> QualityPassedWrapper
        {
            get
            {
                return QualityPassed;
            }
            set
            {
                if (QualityPassed != value)
                {
                    QualityPassed = value;
                    OnPropertyChanged("QualityPassedWrapper");
                    QualityFailedWrapper = ReceivedQuantityWrapper - value;
                    SetAccess();
                }
            }
        }

        public Nullable<decimal> QualityFailedWrapper
        {
            get
            {
                return QualityFailed;
            }
            set
            {
                if (QualityFailed != value)
                {
                    QualityFailed = value;
                    OnPropertyChanged("QualityFailedWrapper");
                    if (value > 0)
                        CanCreateNewJobForFailedQuantity = true;
                    else
                        CanCreateNewJobForFailedQuantity = false;
                }
            }
        }

        public string JobOrderNumber
        {
            get
            {
                if (JobOrderTypeWrapper != null && m_jobOrderNumber == string.Empty)
                {
                    string jobOrderType = JobOrderTypeWrapper.Type.ToLower();
                    if (jobOrderType.Contains("knit"))
                    {
                        m_jobOrderNumber = "K" + this.JobOrderID.ToString();
                    }
                    else if (jobOrderType.Contains("dye"))
                    {
                        m_jobOrderNumber = "D" + this.JobOrderID.ToString();
                    }
                    else if (jobOrderType.Contains("print"))
                    {
                        m_jobOrderNumber = "P" + this.JobOrderID.ToString();
                    }
                    else if (jobOrderType.Contains("compact"))
                    {
                        m_jobOrderNumber = "C" + this.JobOrderID.ToString();
                    }
                    else if (jobOrderType.Contains("wash"))
                    {
                        m_jobOrderNumber = "W" + this.JobOrderID.ToString();
                    }
                    else if (jobOrderType.Contains("other"))
                    {
                        m_jobOrderNumber = "O" + this.JobOrderID.ToString();
                    }

                }
                return m_jobOrderNumber;
            }
        }

        public string DCNumberWrapper
        {
            get
            {
                return DCNumber;
            }
            set
            {
                if (DCNumber != value)
                {
                    DCNumber = value;
                    OnPropertyChanged("DCNumberWrapper");
                }
            }
        }

        public decimal ReceivedQuantityWrapper
        {
            get
            {
                return JobOrderReceiptsWrapper.ReceivedQuantity;
            }
            set
            {
                if (JobOrderReceiptsWrapper.ReceivedQuantity != value)
                {
                    JobOrderReceiptsWrapper.ReceivedQuantity = value;
                    OnPropertyChanged("ReceivedQuantityWrapper");
                    QualityFailedWrapper = value - QualityPassed;
                }
            }
        }

        public DateTime? ReceiptDateWrapper
        {
            get
            {
                if (JobOrderReceiptsWrapper.ReceiptDate == null)
                    return DateTime.Now;
                return JobOrderReceiptsWrapper.ReceiptDate;
            }
            set
            {
                if (JobOrderReceiptsWrapper.ReceiptDate != value)
                {
                    JobOrderReceiptsWrapper.ReceiptDate = value;
                    OnPropertyChanged("ReceiptDateWrapper");
                }
            }
        }

        public string CommentsWrapper
        {
            get
            {
                return JobOrderReceiptsWrapper.Comments;
            }
            set
            {
                if (JobOrderReceiptsWrapper.Comments != value)
                {
                    JobOrderReceiptsWrapper.Comments = value;
                    OnPropertyChanged("CommentsWrapper");
                }
            }
        }

        public Company Supplier
        {
            get
            {
                return Company;
            }
            set
            {
                Company = value;
            }
        }

        public bool CanIssueToNextJob
        {
            get
            { return m_CanIssueToNextJob; }
            set
            {
                if (m_CanIssueToNextJob != value)
                {
                    m_CanIssueToNextJob = value;
                    OnPropertyChanged("CanIssueToNextJob");
                }
            }
        }

        public bool CanCreateNewJobForFailedQuantity
        {
            get
            { return m_CanCreateNewJobForFailedQuantity; }
            set
            {
                if (m_CanCreateNewJobForFailedQuantity != value)
                {
                    m_CanCreateNewJobForFailedQuantity = value;
                    OnPropertyChanged("CanCreateNewJobForFailedQuantity");
                }
            }
        }

        public bool SendToSpecialApproval
        {
            get
            {
                return m_SendToSpecialApproval;
            }
            set
            {
                if (m_SendToSpecialApproval != value)
                {
                    m_SendToSpecialApproval = value;
                    OnPropertyChanged("SendToSpecialApproval");
                }
            }
        }

        public bool SpecialApprovalNeeded
        {
            get
            {
                if (IsWaitingForApproval && !HasApproved && (DBResources.Instance.CurrentUser.UserRole.AliasName.ToUpper() == "SPECIAL" || DBResources.Instance.CurrentUser.UserRole.AliasName.ToUpper() == "ROOT"))
                    return true;
                else
                    return false;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return (!DBResources.Instance.CurrentUser.UserRole.CanModifyJobOrder || IsIssued);
            }
        }

        string m_IssueStatus = string.Empty;
        public string IssueStatus
        {
            get { return m_IssueStatus; }
            set
            {
                if (m_IssueStatus != value)
                {
                    m_IssueStatus = value;
                    OnPropertyChanged("IssueStatus");
                }
            }
        }

        private void SetAccess()
        {
            if (DBResources.Instance.CurrentUser.UserRole.CanModifyJobOrder)
            {
                if (!IsIssued)
                {
                    IssueStatus = string.Empty;
                    if (QualityPassedWrapper >= JobQuantity * (1 - Tolerance) && QualityPassedWrapper <= JobQuantity)
                    {
                        CanIssueToNextJob = true;
                        SendToSpecialApproval = false;
                    }
                    else
                    {
                        if (IsWaitingForApproval)
                        {
                            CanIssueToNextJob = false;
                            SendToSpecialApproval = false;
                            IssueStatus = "Waiting For Special Approval";
                        }
                        else if (!HasApproved)
                        {
                            CanIssueToNextJob = false;
                            SendToSpecialApproval = true;
                        }
                        else
                        {
                            SendToSpecialApproval = false;
                            CanIssueToNextJob = true;
                        }
                    }
                }
                else
                {
                    SendToSpecialApproval = false;
                    CanIssueToNextJob = false;
                    IssueStatus = "Issued";
                }
                if (QualityFailedWrapper > 0 && !FailedQuantityIssued)
                    CanCreateNewJobForFailedQuantity = true;
                else
                    CanCreateNewJobForFailedQuantity = false;
                if (ReceivedQuantityWrapper <= 0 || QualityPassedWrapper == null || QualityPassedWrapper <= 0)
                {
                    SendToSpecialApproval = false;
                    CanIssueToNextJob = false;
                    CanCreateNewJobForFailedQuantity = false;
                }
            }
            else
            {
                SendToSpecialApproval = false;
                CanIssueToNextJob = false;
                CanCreateNewJobForFailedQuantity = false;
            }
            OnPropertyChanged("SpecialApprovalNeeded");
        }

        private decimal Tolerance
        {
            get
            {
                if (m_Tolerance == -1)
                {
                    List<JobOrderTolerance> jTol = GRNReciept.OrderedItem.PurchaseOrder.Order.JobOrderTolerances.Where(tol => tol.JobOrderToleranceType.Type == JobOrderType.Type).Select(tol => tol).ToList();
                    if (jTol == null || jTol.Count == 0)
                        m_Tolerance = 0;
                    else
                        m_Tolerance = jTol[0].ToleranceValueInPercentage;
                }
                return m_Tolerance;
            }
        }

        #endregion

        #region Validation

        public void Validate()
        {
            ValidateJobOrderType();
            if (JobOrderType != null && JobOrderType.Type.ToLower() != "stock")
            {
                ValidateChargesInINR();
                ValidateRequiredDate();
                ValidateSupplier();
            }
        }


        private void ValidateSupplier()
        {
            if (Supplier == null)
            {
                AddError("Supplier", "Choose a supplier to whom the job work should be sent", false);
            }
            else
            {
                RemoveError("Supplier", "Choose a supplier to whom the job work should be sent");
            }
        }

        private void ValidateRequiredDate()
        {
            if (RequiredDate == null || RequiredDate < DateTime.Now)
            {
                AddError("RequiredDateWrapper", "Select a date", false);
            }
            else
            {
                RemoveError("RequiredDateWrapper", "Select a date");
            }
        }

        private void ValidateChargesInINR()
        {
            if (ChargesInINR == 0)
            {
                AddError("ChargesInINRWrapper", "Charges can't be Zero", false);
            }
            else
            {
                RemoveError("ChargesInINRWrapper", "Charges can't be Zero");
            }
        }

        private void ValidateJobOrderType()
        {
            if (JobOrderType == null)
            {
                AddError("JobOrderTypeWrapper", "Select where to issue the material", false);
            }
            else
            {
                RemoveError("JobOrderTypeWrapper", "Select where to issue the material");
            }
        }

        public bool ValidateIssueAndReceiptDetails()
        {
            if (string.IsNullOrEmpty(DCNumberWrapper))
                AddError("DCNumberWrapper", "DC Number is required.", false);
            else
                RemoveError("DCNumberWrapper");

            if (ReceivedQuantityWrapper <= 0 || ReceivedQuantityWrapper > JobQuantity)
                AddError("ReceivedQuantityWrapper", string.Format("Received quantity should be greater than 0 and less than or equal to {0}.", JobQuantity), false);
            else
                RemoveError("ReceivedQuantityWrapper");
            if (ReceivedQuantityWrapper < QualityPassedWrapper)
                AddError("QualityPassedWrapper", "Quality passed cannot be more than Received Quantity.", false);
            else
                RemoveError("QualityPassedWrapper");

            if (ReceiptDateWrapper == null)
                AddError("ReceiptDateWrapper", "Select received date.", false);
            else
                RemoveError("ReceiptDateWrapper");

            return !HasErrors;

        }

        public void Refresh()
        {
            SetAccess();
        }

        #endregion
    }
}
