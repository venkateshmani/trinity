﻿using System;
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

        decimal tolerance = 0.05M;
        bool m_CanIssueToNextJob = false;
        bool m_CanCreateNewJobForFailedQuantity = false;
        bool m_IsSpecialApprovalRequired = false;
        bool m_SendToSpecialApproval = false;

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
                    if (!IsIssued)
                    {
                        if (value >= JobQuantity * (1 - tolerance) && value <= JobQuantity)
                        {
                            CanIssueToNextJob = true;
                        }
                        else
                        {
                            if (!HasApproved)
                                CanIssueToNextJob = false;
                            else
                                CanIssueToNextJob = true;
                        }
                    }
                    QualityFailedWrapper = ReceivedQuantityWrapper - value;
                    if (QualityFailedWrapper > 0)
                        CanCreateNewJobForFailedQuantity = true;
                    else
                        CanCreateNewJobForFailedQuantity = false;
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
                }
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

        #endregion
    }
}
