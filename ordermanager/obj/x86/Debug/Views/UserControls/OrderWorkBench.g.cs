﻿#pragma checksum "..\..\..\..\..\Views\UserControls\OrderWorkBench.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "7B69E866118BF960EDCA2FC0F311951A"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17929
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using MahApps.Metro.Controls;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using ordermanager.ValueConverters;
using ordermanager.Views.PopUps;
using ordermanager.Views.UserControls;
using ordermanager.Views.UserControls.Execution;
using ordermanager.Views.UserControls.Information;
using ordermanager.Views.UserControls.JobOrderControls;
using ordermanager.Views.UserControls.PurchaseOrderControls;
using ordermanager.Views.UserControls.Stock;


namespace ordermanager.Views.UserControls {
    
    
    /// <summary>
    /// OrderWorkBench
    /// </summary>
    public partial class OrderWorkBench : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 63 "..\..\..\..\..\Views\UserControls\OrderWorkBench.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock orderIDTxtBlk;
        
        #line default
        #line hidden
        
        
        #line 69 "..\..\..\..\..\Views\UserControls\OrderWorkBench.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock customerNameTxtBlk;
        
        #line default
        #line hidden
        
        
        #line 76 "..\..\..\..\..\Views\UserControls\OrderWorkBench.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TabControl tabControl;
        
        #line default
        #line hidden
        
        
        #line 77 "..\..\..\..\..\Views\UserControls\OrderWorkBench.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TabItem GoBackTab;
        
        #line default
        #line hidden
        
        
        #line 95 "..\..\..\..\..\Views\UserControls\OrderWorkBench.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TabItem tabViewEnquiry;
        
        #line default
        #line hidden
        
        
        #line 96 "..\..\..\..\..\Views\UserControls\OrderWorkBench.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border viewEnquiryHolder;
        
        #line default
        #line hidden
        
        
        #line 100 "..\..\..\..\..\Views\UserControls\OrderWorkBench.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TabItem tabMaterials;
        
        #line default
        #line hidden
        
        
        #line 101 "..\..\..\..\..\Views\UserControls\OrderWorkBench.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border materialsControlHolder;
        
        #line default
        #line hidden
        
        
        #line 105 "..\..\..\..\..\Views\UserControls\OrderWorkBench.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TabItem tabMaterialDetails;
        
        #line default
        #line hidden
        
        
        #line 106 "..\..\..\..\..\Views\UserControls\OrderWorkBench.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border bomHolder;
        
        #line default
        #line hidden
        
        
        #line 110 "..\..\..\..\..\Views\UserControls\OrderWorkBench.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TabItem tabPurchaseOrder;
        
        #line default
        #line hidden
        
        
        #line 111 "..\..\..\..\..\Views\UserControls\OrderWorkBench.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border purchaseOrderControlHolder;
        
        #line default
        #line hidden
        
        
        #line 116 "..\..\..\..\..\Views\UserControls\OrderWorkBench.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TabControl joTabControl;
        
        #line default
        #line hidden
        
        
        #line 117 "..\..\..\..\..\Views\UserControls\OrderWorkBench.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TabItem joManagerCtrlHolder;
        
        #line default
        #line hidden
        
        
        #line 120 "..\..\..\..\..\Views\UserControls\OrderWorkBench.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TabItem joCtrlHolder;
        
        #line default
        #line hidden
        
        
        #line 127 "..\..\..\..\..\Views\UserControls\OrderWorkBench.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TabItem tabExecution;
        
        #line default
        #line hidden
        
        
        #line 129 "..\..\..\..\..\Views\UserControls\OrderWorkBench.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TabControl tabExecutionDetailsControl;
        
        #line default
        #line hidden
        
        
        #line 130 "..\..\..\..\..\Views\UserControls\OrderWorkBench.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TabItem tabOverAllCompletion;
        
        #line default
        #line hidden
        
        
        #line 133 "..\..\..\..\..\Views\UserControls\OrderWorkBench.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TabItem tabCutting;
        
        #line default
        #line hidden
        
        
        #line 136 "..\..\..\..\..\Views\UserControls\OrderWorkBench.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TabItem tabProduction;
        
        #line default
        #line hidden
        
        
        #line 139 "..\..\..\..\..\Views\UserControls\OrderWorkBench.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TabItem tabQuality;
        
        #line default
        #line hidden
        
        
        #line 142 "..\..\..\..\..\Views\UserControls\OrderWorkBench.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TabItem tabPackaging;
        
        #line default
        #line hidden
        
        
        #line 145 "..\..\..\..\..\Views\UserControls\OrderWorkBench.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TabItem tabShipment;
        
        #line default
        #line hidden
        
        
        #line 151 "..\..\..\..\..\Views\UserControls\OrderWorkBench.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TabItem tabInvoice;
        
        #line default
        #line hidden
        
        
        #line 154 "..\..\..\..\..\Views\UserControls\OrderWorkBench.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TabItem tabNewInvoice;
        
        #line default
        #line hidden
        
        
        #line 157 "..\..\..\..\..\Views\UserControls\OrderWorkBench.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TabItem tabGeneratedInvoice;
        
        #line default
        #line hidden
        
        
        #line 163 "..\..\..\..\..\Views\UserControls\OrderWorkBench.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TabItem tabChangeHistory;
        
        #line default
        #line hidden
        
        
        #line 164 "..\..\..\..\..\Views\UserControls\OrderWorkBench.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border changeHistoryHolder;
        
        #line default
        #line hidden
        
        
        #line 168 "..\..\..\..\..\Views\UserControls\OrderWorkBench.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TabItem tabStock;
        
        #line default
        #line hidden
        
        
        #line 169 "..\..\..\..\..\Views\UserControls\OrderWorkBench.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border stockerOrderHolder;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/ordermanager;component/views/usercontrols/orderworkbench.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\Views\UserControls\OrderWorkBench.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.orderIDTxtBlk = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 2:
            this.customerNameTxtBlk = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.tabControl = ((System.Windows.Controls.TabControl)(target));
            return;
            case 4:
            this.GoBackTab = ((System.Windows.Controls.TabItem)(target));
            return;
            case 5:
            
            #line 86 "..\..\..\..\..\Views\UserControls\OrderWorkBench.xaml"
            ((System.Windows.Controls.Button)(target)).PreviewMouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.Button_PreviewMouseLeftButtonUp_1);
            
            #line default
            #line hidden
            return;
            case 6:
            this.tabViewEnquiry = ((System.Windows.Controls.TabItem)(target));
            return;
            case 7:
            this.viewEnquiryHolder = ((System.Windows.Controls.Border)(target));
            return;
            case 8:
            this.tabMaterials = ((System.Windows.Controls.TabItem)(target));
            return;
            case 9:
            this.materialsControlHolder = ((System.Windows.Controls.Border)(target));
            return;
            case 10:
            this.tabMaterialDetails = ((System.Windows.Controls.TabItem)(target));
            return;
            case 11:
            this.bomHolder = ((System.Windows.Controls.Border)(target));
            return;
            case 12:
            this.tabPurchaseOrder = ((System.Windows.Controls.TabItem)(target));
            return;
            case 13:
            this.purchaseOrderControlHolder = ((System.Windows.Controls.Border)(target));
            return;
            case 14:
            this.joTabControl = ((System.Windows.Controls.TabControl)(target));
            return;
            case 15:
            this.joManagerCtrlHolder = ((System.Windows.Controls.TabItem)(target));
            return;
            case 16:
            this.joCtrlHolder = ((System.Windows.Controls.TabItem)(target));
            return;
            case 17:
            this.tabExecution = ((System.Windows.Controls.TabItem)(target));
            return;
            case 18:
            this.tabExecutionDetailsControl = ((System.Windows.Controls.TabControl)(target));
            return;
            case 19:
            this.tabOverAllCompletion = ((System.Windows.Controls.TabItem)(target));
            return;
            case 20:
            this.tabCutting = ((System.Windows.Controls.TabItem)(target));
            return;
            case 21:
            this.tabProduction = ((System.Windows.Controls.TabItem)(target));
            return;
            case 22:
            this.tabQuality = ((System.Windows.Controls.TabItem)(target));
            return;
            case 23:
            this.tabPackaging = ((System.Windows.Controls.TabItem)(target));
            return;
            case 24:
            this.tabShipment = ((System.Windows.Controls.TabItem)(target));
            return;
            case 25:
            this.tabInvoice = ((System.Windows.Controls.TabItem)(target));
            return;
            case 26:
            this.tabNewInvoice = ((System.Windows.Controls.TabItem)(target));
            return;
            case 27:
            this.tabGeneratedInvoice = ((System.Windows.Controls.TabItem)(target));
            return;
            case 28:
            this.tabChangeHistory = ((System.Windows.Controls.TabItem)(target));
            return;
            case 29:
            this.changeHistoryHolder = ((System.Windows.Controls.Border)(target));
            return;
            case 30:
            this.tabStock = ((System.Windows.Controls.TabItem)(target));
            return;
            case 31:
            this.stockerOrderHolder = ((System.Windows.Controls.Border)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

