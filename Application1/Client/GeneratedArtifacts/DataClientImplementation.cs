﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LightSwitchApplication.Implementation
{
    
    #region DyeingJO
    [global::System.Runtime.Serialization.DataContract(Namespace = "http://schemas.datacontract.org/2004/07/OrderManagerDBData.Implementation")]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.LightSwitch.BuildTasks.CodeGen", "11.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    public partial class DyeingJO :
        global::LightSwitchApplication.DyeingJO.DetailsClass.IImplementation
    {
        partial void OnDyeingJOIdChanged()
        {
            this.___OnPropertyChanged("DyeingJOId");
        }
        
        partial void OnSupplierIDChanged()
        {
            this.___OnPropertyChanged("SupplierID");
        }
        
        partial void OnOrderIDChanged()
        {
            this.___OnPropertyChanged("OrderID");
        }
        
        partial void OnPurchaseOrderIDChanged()
        {
            this.___OnPropertyChanged("PurchaseOrderID");
        }
        
        partial void OnJODateChanged()
        {
            this.___OnPropertyChanged("JODate");
        }
        
        partial void OnQuoteNoChanged()
        {
            this.___OnPropertyChanged("QuoteNo");
        }
        
        partial void OnQuoteDateChanged()
        {
            this.___OnPropertyChanged("QuoteDate");
        }
        
        partial void OnGRNRefNoChanged()
        {
            this.___OnPropertyChanged("GRNRefNo");
        }
        
        partial void OnOrderRefChanged()
        {
            this.___OnPropertyChanged("OrderRef");
        }
        
        partial void OnProcessChanged()
        {
            this.___OnPropertyChanged("Process");
        }
        
        partial void OnTermsAndConditionsChanged()
        {
            this.___OnPropertyChanged("TermsAndConditions");
        }
        
        partial void OnTotalValueChanged()
        {
            this.___OnPropertyChanged("TotalValue");
        }
        
        partial void OnApprovalIDChanged()
        {
            this.___OnPropertyChanged("ApprovalID");
        }
        
    }
    #endregion
    
    #region DyeingJO1
    [global::System.Runtime.Serialization.DataContract(Namespace = "http://schemas.datacontract.org/2004/07/OrderManagerDBData1.Implementation")]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.LightSwitch.BuildTasks.CodeGen", "11.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    public partial class DyeingJO1 :
        global::LightSwitchApplication.DyeingJO1.DetailsClass.IImplementation
    {
        partial void OnDyeingJOIdChanged()
        {
            this.___OnPropertyChanged("DyeingJOId");
        }
        
        partial void OnSupplierIDChanged()
        {
            this.___OnPropertyChanged("SupplierID");
        }
        
        partial void OnOrderIDChanged()
        {
            this.___OnPropertyChanged("OrderID");
        }
        
        partial void OnPurchaseOrderIDChanged()
        {
            this.___OnPropertyChanged("PurchaseOrderID");
        }
        
        partial void OnJODateChanged()
        {
            this.___OnPropertyChanged("JODate");
        }
        
        partial void OnQuoteNoChanged()
        {
            this.___OnPropertyChanged("QuoteNo");
        }
        
        partial void OnQuoteDateChanged()
        {
            this.___OnPropertyChanged("QuoteDate");
        }
        
        partial void OnGRNRefNoChanged()
        {
            this.___OnPropertyChanged("GRNRefNo");
        }
        
        partial void OnOrderRefChanged()
        {
            this.___OnPropertyChanged("OrderRef");
        }
        
        partial void OnProcessChanged()
        {
            this.___OnPropertyChanged("Process");
        }
        
        partial void OnTermsAndConditionsChanged()
        {
            this.___OnPropertyChanged("TermsAndConditions");
        }
        
        partial void OnTotalValueChanged()
        {
            this.___OnPropertyChanged("TotalValue");
        }
        
        partial void OnApprovalIDChanged()
        {
            this.___OnPropertyChanged("ApprovalID");
        }
        
        global::System.Collections.IEnumerable global::LightSwitchApplication.DyeingJO1.DetailsClass.IImplementation.DyeingJoItems
        {
            get
            {
                return this.DyeingJoItems;
            }
        }
        
        internal global::Microsoft.LightSwitch.ClientGenerated.Implementation.EntityRefCollection<global::LightSwitchApplication.Implementation.DyeingJoItem> __DyeingJoItems
        {
            get
            {
                if (this.___DyeingJoItems == null)
                {
                    this.___DyeingJoItems = new global::Microsoft.LightSwitch.ClientGenerated.Implementation.EntityRefCollection<global::LightSwitchApplication.Implementation.DyeingJoItem>(
                        this,
                        "DyeingJoItems",
                        () => this._DyeingJoItems,
                        e => global::System.Object.Equals(e.DyeingJOId, this.DyeingJOId));
                }
                return this.___DyeingJoItems;
            }
        }
        
        private global::Microsoft.LightSwitch.ClientGenerated.Implementation.EntityRefCollection<global::LightSwitchApplication.Implementation.DyeingJoItem> ___DyeingJoItems;
        
    }
    #endregion
    
    #region DyeingJoItem
    [global::System.Runtime.Serialization.DataContract(Namespace = "http://schemas.datacontract.org/2004/07/OrderManagerDBData1.Implementation")]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.LightSwitch.BuildTasks.CodeGen", "11.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    public partial class DyeingJoItem :
        global::LightSwitchApplication.DyeingJoItem.DetailsClass.IImplementation
    {
        partial void OnDyeingJOIdChanged()
        {
            this.___OnPropertyChanged("DyeingJOId");
            this.___OnPropertyChanged("DyeingJO1");
        }
        
        partial void OnDyeingJoItemsIDChanged()
        {
            this.___OnPropertyChanged("DyeingJoItemsID");
        }
        
        partial void OnColorChanged()
        {
            this.___OnPropertyChanged("Color");
        }
        
        partial void OnQualityDescriptionChanged()
        {
            this.___OnPropertyChanged("QualityDescription");
        }
        
        partial void OnReqGSMChanged()
        {
            this.___OnPropertyChanged("ReqGSM");
        }
        
        partial void OnReqWidthChanged()
        {
            this.___OnPropertyChanged("ReqWidth");
        }
        
        partial void OnNetQtyChanged()
        {
            this.___OnPropertyChanged("NetQty");
        }
        
        partial void OnRatePerKgChanged()
        {
            this.___OnPropertyChanged("RatePerKg");
        }
        
        partial void OnTotalAmountChanged()
        {
            this.___OnPropertyChanged("TotalAmount");
        }
        
        global::Microsoft.LightSwitch.Internal.IEntityImplementation global::LightSwitchApplication.DyeingJoItem.DetailsClass.IImplementation.DyeingJO1
        {
            get
            {
                return this.DyeingJO1;
            }
            set
            {
                this.DyeingJO1 = (global::LightSwitchApplication.Implementation.DyeingJO1)value;
            }
        }
        
        private global::Microsoft.LightSwitch.ClientGenerated.Implementation.EntityRef<global::LightSwitchApplication.Implementation.DyeingJO1> __DyeingJO1
        {
            get
            {
                if (this.___DyeingJO1 == null)
                {
                    this.___DyeingJO1 = new global::Microsoft.LightSwitch.ClientGenerated.Implementation.EntityRef<global::LightSwitchApplication.Implementation.DyeingJO1>(
                        this,
                        "DyeingJO1",
                        new string[] { "DyeingJOId" },
                        e => global::System.Object.Equals(e.DyeingJOId, this.DyeingJOId),
                        () => this._DyeingJO1,
                        e => this._DyeingJO1 = e);
                }
                return this.___DyeingJO1;
            }
        }
        
        private global::Microsoft.LightSwitch.ClientGenerated.Implementation.EntityRef<global::LightSwitchApplication.Implementation.DyeingJO1> ___DyeingJO1;
        
    }
    #endregion
    
    #region OrderManagerDBDataObjectContext
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.LightSwitch.BuildTasks.CodeGen", "11.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    public partial class OrderManagerDBDataObjectContext
    {
        protected override global::Microsoft.LightSwitch.Internal.IEntityImplementation CreateEntityImplementation<T>()
        {
            if (typeof(T) == typeof(global::LightSwitchApplication.DyeingJO))
            {
                return new global::LightSwitchApplication.Implementation.DyeingJO();
            }
            return null;
        }
    }
    #endregion
    
    #region OrderManagerDBData1ObjectContext
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.LightSwitch.BuildTasks.CodeGen", "11.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    public partial class OrderManagerDBData1ObjectContext
    {
        protected override global::Microsoft.LightSwitch.Internal.IEntityImplementation CreateEntityImplementation<T>()
        {
            if (typeof(T) == typeof(global::LightSwitchApplication.DyeingJO1))
            {
                return new global::LightSwitchApplication.Implementation.DyeingJO1();
            }
            if (typeof(T) == typeof(global::LightSwitchApplication.DyeingJoItem))
            {
                return new global::LightSwitchApplication.Implementation.DyeingJoItem();
            }
            return null;
        }
    }
    #endregion
    
    #region DataServiceImplementationFactory
    [global::System.ComponentModel.Composition.PartCreationPolicy(global::System.ComponentModel.Composition.CreationPolicy.Shared)]
    [global::System.ComponentModel.Composition.Export(typeof(global::Microsoft.LightSwitch.Internal.IDataServiceFactory))]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.LightSwitch.BuildTasks.CodeGen", "11.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    public class DataServiceFactory :
        global::Microsoft.LightSwitch.ClientGenerated.Implementation.DataServiceFactory
    {
    
        protected override global::Microsoft.LightSwitch.IDataService CreateDataService(global::System.Type dataServiceType)
        {
            if (dataServiceType == typeof(global::LightSwitchApplication.OrderManagerDBData))
            {
                return new global::LightSwitchApplication.OrderManagerDBData();
            }
            if (dataServiceType == typeof(global::LightSwitchApplication.OrderManagerDBData1))
            {
                return new global::LightSwitchApplication.OrderManagerDBData1();
            }
            return base.CreateDataService(dataServiceType);
        }
    
        protected override global::Microsoft.LightSwitch.Internal.IDataServiceImplementation CreateDataServiceImplementation<TDataService>(TDataService dataService)
        {
            if (typeof(TDataService) == typeof(global::LightSwitchApplication.OrderManagerDBData))
            {
                return new global::LightSwitchApplication.Implementation.OrderManagerDBDataObjectContext(global::Microsoft.LightSwitch.ClientGenerated.Implementation.DataServiceContext.CreateServiceUri("../OrderManagerDBData.svc"));
            }
            if (typeof(TDataService) == typeof(global::LightSwitchApplication.OrderManagerDBData1))
            {
                return new global::LightSwitchApplication.Implementation.OrderManagerDBData1ObjectContext(global::Microsoft.LightSwitch.ClientGenerated.Implementation.DataServiceContext.CreateServiceUri("../OrderManagerDBData1.svc"));
            }
            return base.CreateDataServiceImplementation(dataService);
        }
    }
    #endregion
    
    [global::System.ComponentModel.Composition.PartCreationPolicy(global::System.ComponentModel.Composition.CreationPolicy.Shared)]
    [global::System.ComponentModel.Composition.Export(typeof(global::Microsoft.LightSwitch.Internal.ITypeMappingProvider))]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.LightSwitch.BuildTasks.CodeGen", "11.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    public class __TypeMapping
        : global::Microsoft.LightSwitch.Internal.ITypeMappingProvider
    {
        global::System.Type global::Microsoft.LightSwitch.Internal.ITypeMappingProvider.GetImplementationType(global::System.Type definitionType)
        {
            if (typeof(global::LightSwitchApplication.DyeingJO) == definitionType)
            {
                return typeof(global::LightSwitchApplication.Implementation.DyeingJO);
            }
            if (typeof(global::LightSwitchApplication.DyeingJO1) == definitionType)
            {
                return typeof(global::LightSwitchApplication.Implementation.DyeingJO1);
            }
            if (typeof(global::LightSwitchApplication.DyeingJoItem) == definitionType)
            {
                return typeof(global::LightSwitchApplication.Implementation.DyeingJoItem);
            }
            return null;
        }
    }
}
