﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17929
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Original file name:
// Generation date: 3/23/2014 7:55:39 PM
namespace LightSwitchApplication.Implementation
{
    
    /// <summary>
    /// There are no comments for OrderManagerDBData1ObjectContext in the schema.
    /// </summary>
    public partial class OrderManagerDBData1ObjectContext : global::Microsoft.LightSwitch.ClientGenerated.Implementation.DataServiceContext
    {
        /// <summary>
        /// Initialize a new OrderManagerDBData1ObjectContext object.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public OrderManagerDBData1ObjectContext(global::System.Uri serviceRoot) : 
                base(serviceRoot)
        {
            this.ResolveName = new global::System.Func<global::System.Type, string>(this.ResolveNameFromType);
            this.ResolveType = new global::System.Func<string, global::System.Type>(this.ResolveTypeFromName);
            this.OnContextCreated();
        }
        partial void OnContextCreated();
        /// <summary>
        /// Since the namespace configured for this service reference
        /// in Visual Studio is different from the one indicated in the
        /// server schema, use type-mappers to map between the two.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        protected global::System.Type ResolveTypeFromName(string typeName)
        {
            if (typeName.StartsWith("LightSwitchApplication", global::System.StringComparison.Ordinal))
            {
                return this.GetType().Assembly.GetType(string.Concat("LightSwitchApplication.Implementation", typeName.Substring(22)), false);
            }
            return null;
        }
        /// <summary>
        /// Since the namespace configured for this service reference
        /// in Visual Studio is different from the one indicated in the
        /// server schema, use type-mappers to map between the two.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        protected string ResolveNameFromType(global::System.Type clientType)
        {
            if (clientType.Namespace.Equals("LightSwitchApplication.Implementation", global::System.StringComparison.Ordinal))
            {
                return string.Concat("LightSwitchApplication.", clientType.Name);
            }
            return null;
        }
        /// <summary>
        /// There are no comments for DyeingJOes in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public global::System.Data.Services.Client.DataServiceQuery<DyeingJO1> DyeingJOes
        {
            get
            {
                if ((this._DyeingJOes == null))
                {
                    this._DyeingJOes = base.CreateQuery<DyeingJO1>("DyeingJOes");
                }
                return this._DyeingJOes;
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private global::System.Data.Services.Client.DataServiceQuery<DyeingJO1> _DyeingJOes;
        /// <summary>
        /// There are no comments for DyeingJoItems in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public global::System.Data.Services.Client.DataServiceQuery<DyeingJoItem> DyeingJoItems
        {
            get
            {
                if ((this._DyeingJoItems == null))
                {
                    this._DyeingJoItems = base.CreateQuery<DyeingJoItem>("DyeingJoItems");
                }
                return this._DyeingJoItems;
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private global::System.Data.Services.Client.DataServiceQuery<DyeingJoItem> _DyeingJoItems;
        /// <summary>
        /// There are no comments for DyeingJOes in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public void AddToDyeingJOes(DyeingJO1 dyeingJO1)
        {
            base.AddObject("DyeingJOes", dyeingJO1);
        }
        /// <summary>
        /// There are no comments for DyeingJoItems in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public void AddToDyeingJoItems(DyeingJoItem dyeingJoItem)
        {
            base.AddObject("DyeingJoItems", dyeingJoItem);
        }
    }
    /// <summary>
    /// There are no comments for LightSwitchApplication.DyeingJO1 in the schema.
    /// </summary>
    /// <KeyProperties>
    /// DyeingJOId
    /// </KeyProperties>
    [global::System.Data.Services.Common.EntitySetAttribute("DyeingJOes")]
    [global::System.Data.Services.Common.DataServiceKeyAttribute("DyeingJOId")]
    public partial class DyeingJO1 : global::Microsoft.LightSwitch.ClientGenerated.Implementation.EntityBase, global::System.ComponentModel.INotifyPropertyChanged
    {
        /// <summary>
        /// Create a new DyeingJO1 object.
        /// </summary>
        /// <param name="dyeingJOId">Initial value of DyeingJOId.</param>
        /// <param name="supplierID">Initial value of SupplierID.</param>
        /// <param name="orderID">Initial value of OrderID.</param>
        /// <param name="purchaseOrderID">Initial value of PurchaseOrderID.</param>
        /// <param name="jODate">Initial value of JODate.</param>
        /// <param name="quoteNo">Initial value of QuoteNo.</param>
        /// <param name="quoteDate">Initial value of QuoteDate.</param>
        /// <param name="gRNRefNo">Initial value of GRNRefNo.</param>
        /// <param name="orderRef">Initial value of OrderRef.</param>
        /// <param name="process">Initial value of Process.</param>
        /// <param name="termsAndConditions">Initial value of TermsAndConditions.</param>
        /// <param name="totalValue">Initial value of TotalValue.</param>
        /// <param name="approvalID">Initial value of ApprovalID.</param>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public static DyeingJO1 CreateDyeingJO1(long dyeingJOId, int supplierID, long orderID, long purchaseOrderID, global::System.DateTime jODate, string quoteNo, global::System.DateTime quoteDate, string gRNRefNo, string orderRef, string process, string termsAndConditions, decimal totalValue, long approvalID)
        {
            DyeingJO1 dyeingJO1 = new DyeingJO1();
            dyeingJO1.DyeingJOId = dyeingJOId;
            dyeingJO1.SupplierID = supplierID;
            dyeingJO1.OrderID = orderID;
            dyeingJO1.PurchaseOrderID = purchaseOrderID;
            dyeingJO1.JODate = jODate;
            dyeingJO1.QuoteNo = quoteNo;
            dyeingJO1.QuoteDate = quoteDate;
            dyeingJO1.GRNRefNo = gRNRefNo;
            dyeingJO1.OrderRef = orderRef;
            dyeingJO1.Process = process;
            dyeingJO1.TermsAndConditions = termsAndConditions;
            dyeingJO1.TotalValue = totalValue;
            dyeingJO1.ApprovalID = approvalID;
            return dyeingJO1;
        }
        /// <summary>
        /// There are no comments for Property DyeingJOId in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public long DyeingJOId
        {
            get
            {
                return this._DyeingJOId;
            }
            set
            {
                this.OnDyeingJOIdChanging(value);
                if (object.Equals(this.DyeingJOId, value))
                {
                    return;
                }
                this._DyeingJOId = value;
                this.OnDyeingJOIdChanged();
                this.OnPropertyChanged("DyeingJOId");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private long _DyeingJOId;
        partial void OnDyeingJOIdChanging(long value);
        partial void OnDyeingJOIdChanged();
        /// <summary>
        /// There are no comments for Property SupplierID in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public int SupplierID
        {
            get
            {
                return this._SupplierID;
            }
            set
            {
                this.OnSupplierIDChanging(value);
                if (object.Equals(this.SupplierID, value))
                {
                    return;
                }
                this._SupplierID = value;
                this.OnSupplierIDChanged();
                this.OnPropertyChanged("SupplierID");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private int _SupplierID;
        partial void OnSupplierIDChanging(int value);
        partial void OnSupplierIDChanged();
        /// <summary>
        /// There are no comments for Property OrderID in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public long OrderID
        {
            get
            {
                return this._OrderID;
            }
            set
            {
                this.OnOrderIDChanging(value);
                if (object.Equals(this.OrderID, value))
                {
                    return;
                }
                this._OrderID = value;
                this.OnOrderIDChanged();
                this.OnPropertyChanged("OrderID");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private long _OrderID;
        partial void OnOrderIDChanging(long value);
        partial void OnOrderIDChanged();
        /// <summary>
        /// There are no comments for Property PurchaseOrderID in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public long PurchaseOrderID
        {
            get
            {
                return this._PurchaseOrderID;
            }
            set
            {
                this.OnPurchaseOrderIDChanging(value);
                if (object.Equals(this.PurchaseOrderID, value))
                {
                    return;
                }
                this._PurchaseOrderID = value;
                this.OnPurchaseOrderIDChanged();
                this.OnPropertyChanged("PurchaseOrderID");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private long _PurchaseOrderID;
        partial void OnPurchaseOrderIDChanging(long value);
        partial void OnPurchaseOrderIDChanged();
        /// <summary>
        /// There are no comments for Property JODate in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public global::System.DateTime JODate
        {
            get
            {
                return this._JODate;
            }
            set
            {
                this.OnJODateChanging(value);
                if (object.Equals(this.JODate, value))
                {
                    return;
                }
                this._JODate = value;
                this.OnJODateChanged();
                this.OnPropertyChanged("JODate");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private global::System.DateTime _JODate;
        partial void OnJODateChanging(global::System.DateTime value);
        partial void OnJODateChanged();
        /// <summary>
        /// There are no comments for Property QuoteNo in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public string QuoteNo
        {
            get
            {
                return this._QuoteNo;
            }
            set
            {
                this.OnQuoteNoChanging(value);
                if (object.Equals(this.QuoteNo, value))
                {
                    return;
                }
                this._QuoteNo = value;
                this.OnQuoteNoChanged();
                this.OnPropertyChanged("QuoteNo");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private string _QuoteNo;
        partial void OnQuoteNoChanging(string value);
        partial void OnQuoteNoChanged();
        /// <summary>
        /// There are no comments for Property QuoteDate in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public global::System.DateTime QuoteDate
        {
            get
            {
                return this._QuoteDate;
            }
            set
            {
                this.OnQuoteDateChanging(value);
                if (object.Equals(this.QuoteDate, value))
                {
                    return;
                }
                this._QuoteDate = value;
                this.OnQuoteDateChanged();
                this.OnPropertyChanged("QuoteDate");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private global::System.DateTime _QuoteDate;
        partial void OnQuoteDateChanging(global::System.DateTime value);
        partial void OnQuoteDateChanged();
        /// <summary>
        /// There are no comments for Property GRNRefNo in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public string GRNRefNo
        {
            get
            {
                return this._GRNRefNo;
            }
            set
            {
                this.OnGRNRefNoChanging(value);
                if (object.Equals(this.GRNRefNo, value))
                {
                    return;
                }
                this._GRNRefNo = value;
                this.OnGRNRefNoChanged();
                this.OnPropertyChanged("GRNRefNo");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private string _GRNRefNo;
        partial void OnGRNRefNoChanging(string value);
        partial void OnGRNRefNoChanged();
        /// <summary>
        /// There are no comments for Property OrderRef in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public string OrderRef
        {
            get
            {
                return this._OrderRef;
            }
            set
            {
                this.OnOrderRefChanging(value);
                if (object.Equals(this.OrderRef, value))
                {
                    return;
                }
                this._OrderRef = value;
                this.OnOrderRefChanged();
                this.OnPropertyChanged("OrderRef");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private string _OrderRef;
        partial void OnOrderRefChanging(string value);
        partial void OnOrderRefChanged();
        /// <summary>
        /// There are no comments for Property Process in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public string Process
        {
            get
            {
                return this._Process;
            }
            set
            {
                this.OnProcessChanging(value);
                if (object.Equals(this.Process, value))
                {
                    return;
                }
                this._Process = value;
                this.OnProcessChanged();
                this.OnPropertyChanged("Process");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private string _Process;
        partial void OnProcessChanging(string value);
        partial void OnProcessChanged();
        /// <summary>
        /// There are no comments for Property TermsAndConditions in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public string TermsAndConditions
        {
            get
            {
                return this._TermsAndConditions;
            }
            set
            {
                this.OnTermsAndConditionsChanging(value);
                if (object.Equals(this.TermsAndConditions, value))
                {
                    return;
                }
                this._TermsAndConditions = value;
                this.OnTermsAndConditionsChanged();
                this.OnPropertyChanged("TermsAndConditions");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private string _TermsAndConditions;
        partial void OnTermsAndConditionsChanging(string value);
        partial void OnTermsAndConditionsChanged();
        /// <summary>
        /// There are no comments for Property TotalValue in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public decimal TotalValue
        {
            get
            {
                return this._TotalValue;
            }
            set
            {
                this.OnTotalValueChanging(value);
                if (object.Equals(this.TotalValue, value))
                {
                    return;
                }
                this._TotalValue = value;
                this.OnTotalValueChanged();
                this.OnPropertyChanged("TotalValue");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private decimal _TotalValue;
        partial void OnTotalValueChanging(decimal value);
        partial void OnTotalValueChanged();
        /// <summary>
        /// There are no comments for Property ApprovalID in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public long ApprovalID
        {
            get
            {
                return this._ApprovalID;
            }
            set
            {
                this.OnApprovalIDChanging(value);
                if (object.Equals(this.ApprovalID, value))
                {
                    return;
                }
                this._ApprovalID = value;
                this.OnApprovalIDChanged();
                this.OnPropertyChanged("ApprovalID");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private long _ApprovalID;
        partial void OnApprovalIDChanging(long value);
        partial void OnApprovalIDChanged();
        /// <summary>
        /// There are no comments for DyeingJoItems in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public global::System.Data.Services.Client.DataServiceCollection<DyeingJoItem> DyeingJoItems
        {
            get
            {
                this.@__DyeingJoItems.EnsureValueInitialized();
                return this._DyeingJoItems;
            }
            set
            {
                this._DyeingJoItems = value;
                this.OnPropertyChanged("DyeingJoItems");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private global::System.Data.Services.Client.DataServiceCollection<DyeingJoItem> _DyeingJoItems = new global::System.Data.Services.Client.DataServiceCollection<DyeingJoItem>(null, global::System.Data.Services.Client.TrackingMode.None);
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public event global::System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        protected virtual void OnPropertyChanged(string property)
        {
            if ((this.PropertyChanged != null))
            {
                this.PropertyChanged(this, new global::System.ComponentModel.PropertyChangedEventArgs(property));
            }
        }
    }
    /// <summary>
    /// There are no comments for LightSwitchApplication.DyeingJoItem in the schema.
    /// </summary>
    /// <KeyProperties>
    /// DyeingJoItemsID
    /// </KeyProperties>
    [global::System.Data.Services.Common.EntitySetAttribute("DyeingJoItems")]
    [global::System.Data.Services.Common.DataServiceKeyAttribute("DyeingJoItemsID")]
    public partial class DyeingJoItem : global::Microsoft.LightSwitch.ClientGenerated.Implementation.EntityBase, global::System.ComponentModel.INotifyPropertyChanged
    {
        /// <summary>
        /// Create a new DyeingJoItem object.
        /// </summary>
        /// <param name="dyeingJoItemsID">Initial value of DyeingJoItemsID.</param>
        /// <param name="color">Initial value of Color.</param>
        /// <param name="qualityDescription">Initial value of QualityDescription.</param>
        /// <param name="reqGSM">Initial value of ReqGSM.</param>
        /// <param name="reqWidth">Initial value of ReqWidth.</param>
        /// <param name="netQty">Initial value of NetQty.</param>
        /// <param name="ratePerKg">Initial value of RatePerKg.</param>
        /// <param name="totalAmount">Initial value of TotalAmount.</param>
        /// <param name="dyeingJOId">Initial value of DyeingJOId.</param>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public static DyeingJoItem CreateDyeingJoItem(long dyeingJoItemsID, string color, string qualityDescription, string reqGSM, string reqWidth, decimal netQty, decimal ratePerKg, decimal totalAmount, long dyeingJOId)
        {
            DyeingJoItem dyeingJoItem = new DyeingJoItem();
            dyeingJoItem.DyeingJoItemsID = dyeingJoItemsID;
            dyeingJoItem.Color = color;
            dyeingJoItem.QualityDescription = qualityDescription;
            dyeingJoItem.ReqGSM = reqGSM;
            dyeingJoItem.ReqWidth = reqWidth;
            dyeingJoItem.NetQty = netQty;
            dyeingJoItem.RatePerKg = ratePerKg;
            dyeingJoItem.TotalAmount = totalAmount;
            dyeingJoItem.DyeingJOId = dyeingJOId;
            return dyeingJoItem;
        }
        /// <summary>
        /// There are no comments for Property DyeingJoItemsID in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public long DyeingJoItemsID
        {
            get
            {
                return this._DyeingJoItemsID;
            }
            set
            {
                this.OnDyeingJoItemsIDChanging(value);
                if (object.Equals(this.DyeingJoItemsID, value))
                {
                    return;
                }
                this._DyeingJoItemsID = value;
                this.OnDyeingJoItemsIDChanged();
                this.OnPropertyChanged("DyeingJoItemsID");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private long _DyeingJoItemsID;
        partial void OnDyeingJoItemsIDChanging(long value);
        partial void OnDyeingJoItemsIDChanged();
        /// <summary>
        /// There are no comments for Property Color in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public string Color
        {
            get
            {
                return this._Color;
            }
            set
            {
                this.OnColorChanging(value);
                if (object.Equals(this.Color, value))
                {
                    return;
                }
                this._Color = value;
                this.OnColorChanged();
                this.OnPropertyChanged("Color");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private string _Color;
        partial void OnColorChanging(string value);
        partial void OnColorChanged();
        /// <summary>
        /// There are no comments for Property QualityDescription in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public string QualityDescription
        {
            get
            {
                return this._QualityDescription;
            }
            set
            {
                this.OnQualityDescriptionChanging(value);
                if (object.Equals(this.QualityDescription, value))
                {
                    return;
                }
                this._QualityDescription = value;
                this.OnQualityDescriptionChanged();
                this.OnPropertyChanged("QualityDescription");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private string _QualityDescription;
        partial void OnQualityDescriptionChanging(string value);
        partial void OnQualityDescriptionChanged();
        /// <summary>
        /// There are no comments for Property ReqGSM in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public string ReqGSM
        {
            get
            {
                return this._ReqGSM;
            }
            set
            {
                this.OnReqGSMChanging(value);
                if (object.Equals(this.ReqGSM, value))
                {
                    return;
                }
                this._ReqGSM = value;
                this.OnReqGSMChanged();
                this.OnPropertyChanged("ReqGSM");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private string _ReqGSM;
        partial void OnReqGSMChanging(string value);
        partial void OnReqGSMChanged();
        /// <summary>
        /// There are no comments for Property ReqWidth in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public string ReqWidth
        {
            get
            {
                return this._ReqWidth;
            }
            set
            {
                this.OnReqWidthChanging(value);
                if (object.Equals(this.ReqWidth, value))
                {
                    return;
                }
                this._ReqWidth = value;
                this.OnReqWidthChanged();
                this.OnPropertyChanged("ReqWidth");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private string _ReqWidth;
        partial void OnReqWidthChanging(string value);
        partial void OnReqWidthChanged();
        /// <summary>
        /// There are no comments for Property NetQty in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public decimal NetQty
        {
            get
            {
                return this._NetQty;
            }
            set
            {
                this.OnNetQtyChanging(value);
                if (object.Equals(this.NetQty, value))
                {
                    return;
                }
                this._NetQty = value;
                this.OnNetQtyChanged();
                this.OnPropertyChanged("NetQty");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private decimal _NetQty;
        partial void OnNetQtyChanging(decimal value);
        partial void OnNetQtyChanged();
        /// <summary>
        /// There are no comments for Property RatePerKg in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public decimal RatePerKg
        {
            get
            {
                return this._RatePerKg;
            }
            set
            {
                this.OnRatePerKgChanging(value);
                if (object.Equals(this.RatePerKg, value))
                {
                    return;
                }
                this._RatePerKg = value;
                this.OnRatePerKgChanged();
                this.OnPropertyChanged("RatePerKg");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private decimal _RatePerKg;
        partial void OnRatePerKgChanging(decimal value);
        partial void OnRatePerKgChanged();
        /// <summary>
        /// There are no comments for Property TotalAmount in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public decimal TotalAmount
        {
            get
            {
                return this._TotalAmount;
            }
            set
            {
                this.OnTotalAmountChanging(value);
                if (object.Equals(this.TotalAmount, value))
                {
                    return;
                }
                this._TotalAmount = value;
                this.OnTotalAmountChanged();
                this.OnPropertyChanged("TotalAmount");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private decimal _TotalAmount;
        partial void OnTotalAmountChanging(decimal value);
        partial void OnTotalAmountChanged();
        /// <summary>
        /// There are no comments for Property DyeingJOId in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public long DyeingJOId
        {
            get
            {
                return this._DyeingJOId;
            }
            set
            {
                this.OnDyeingJOIdChanging(value);
                if (object.Equals(this.DyeingJOId, value))
                {
                    return;
                }
                this._DyeingJOId = value;
                this.OnDyeingJOIdChanged();
                this.OnPropertyChanged("DyeingJOId");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private long _DyeingJOId;
        partial void OnDyeingJOIdChanging(long value);
        partial void OnDyeingJOIdChanged();
        /// <summary>
        /// There are no comments for DyeingJO1 in the schema.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public DyeingJO1 DyeingJO1
        {
            get
            {
                this.@__DyeingJO1.EnsureValueInitialized();
                return this._DyeingJO1;
            }
            set
            {
                DyeingJO1 previous = this.DyeingJO1;
                if ((previous == value))
                {
                    return;
                }
                if ((previous != null))
                {
                    this._DyeingJO1 = null;
                    this.@__DyeingJO1.OnValueSet();
                    previous.DyeingJoItems.Remove(this);
                }
                if ((this.___Host != null))
                {
                    if ((value != null))
                    {
                        this.DyeingJOId = value.DyeingJOId;
                    }
                    else
                    {
                        this.DyeingJOId = default(long);
                    }
                }
                this._DyeingJO1 = value;
                this.@__DyeingJO1.OnValueSet();
                if ((value != null))
                {
                    value.@__DyeingJoItems.Add(this);
                }
                this.___OnPropertyChanged("DyeingJO1");
                this.OnPropertyChanged("DyeingJO1");
            }
        }
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        private DyeingJO1 _DyeingJO1;
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        public event global::System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Data.Services.Design", "1.0.0")]
        protected virtual void OnPropertyChanged(string property)
        {
            if ((this.PropertyChanged != null))
            {
                this.PropertyChanged(this, new global::System.ComponentModel.PropertyChangedEventArgs(property));
            }
        }
    }
}
