﻿#pragma checksum "..\..\..\..\..\Views\PopUps\NewEnquiryFormUserControl.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "CEE58F94FF19BC3A278A4CF10819C168"
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
using ordermanager.ViewModel;
using ordermanager.Views.CommonControls;
using ordermanager.Views.PopUps;


namespace ordermanager.Views.PopUps {
    
    
    /// <summary>
    /// NewEnquiryFormUserControl
    /// </summary>
    public partial class NewEnquiryFormUserControl : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 56 "..\..\..\..\..\Views\PopUps\NewEnquiryFormUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid rootLayout;
        
        #line default
        #line hidden
        
        
        #line 82 "..\..\..\..\..\Views\PopUps\NewEnquiryFormUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox customerComboBox;
        
        #line default
        #line hidden
        
        
        #line 96 "..\..\..\..\..\Views\PopUps\NewEnquiryFormUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button editExistingCustomerBtn;
        
        #line default
        #line hidden
        
        
        #line 113 "..\..\..\..\..\Views\PopUps\NewEnquiryFormUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button addNewCustomerBtn;
        
        #line default
        #line hidden
        
        
        #line 127 "..\..\..\..\..\Views\PopUps\NewEnquiryFormUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker orderDate;
        
        #line default
        #line hidden
        
        
        #line 137 "..\..\..\..\..\Views\PopUps\NewEnquiryFormUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker expectedDeliveryDate;
        
        #line default
        #line hidden
        
        
        #line 143 "..\..\..\..\..\Views\PopUps\NewEnquiryFormUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock numberOfDays;
        
        #line default
        #line hidden
        
        
        #line 148 "..\..\..\..\..\Views\PopUps\NewEnquiryFormUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker internalDeliveryDate;
        
        #line default
        #line hidden
        
        
        #line 154 "..\..\..\..\..\Views\PopUps\NewEnquiryFormUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock internalDeliveryDateSpan;
        
        #line default
        #line hidden
        
        
        #line 164 "..\..\..\..\..\Views\PopUps\NewEnquiryFormUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox agentComboxBox;
        
        #line default
        #line hidden
        
        
        #line 178 "..\..\..\..\..\Views\PopUps\NewEnquiryFormUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button editExistingAgentBtn;
        
        #line default
        #line hidden
        
        
        #line 195 "..\..\..\..\..\Views\PopUps\NewEnquiryFormUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button addNewAgentBtn;
        
        #line default
        #line hidden
        
        
        #line 213 "..\..\..\..\..\Views\PopUps\NewEnquiryFormUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel commisionInfo;
        
        #line default
        #line hidden
        
        
        #line 245 "..\..\..\..\..\Views\PopUps\NewEnquiryFormUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid targetDatesGrid;
        
        #line default
        #line hidden
        
        
        #line 285 "..\..\..\..\..\Views\PopUps\NewEnquiryFormUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button addNewItemBtn;
        
        #line default
        #line hidden
        
        
        #line 292 "..\..\..\..\..\Views\PopUps\NewEnquiryFormUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid productsRequired;
        
        #line default
        #line hidden
        
        
        #line 298 "..\..\..\..\..\Views\PopUps\NewEnquiryFormUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTemplateColumn productColumn;
        
        #line default
        #line hidden
        
        
        #line 360 "..\..\..\..\..\Views\PopUps\NewEnquiryFormUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTemplateColumn styleIDColumn;
        
        #line default
        #line hidden
        
        
        #line 368 "..\..\..\..\..\Views\PopUps\NewEnquiryFormUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTemplateColumn quantityColumn;
        
        #line default
        #line hidden
        
        
        #line 379 "..\..\..\..\..\Views\PopUps\NewEnquiryFormUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTemplateColumn unitOfMeasurementColumn;
        
        #line default
        #line hidden
        
        
        #line 418 "..\..\..\..\..\Views\PopUps\NewEnquiryFormUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTemplateColumn currencyColumn;
        
        #line default
        #line hidden
        
        
        #line 458 "..\..\..\..\..\Views\PopUps\NewEnquiryFormUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTemplateColumn currencyValueInINR;
        
        #line default
        #line hidden
        
        
        #line 481 "..\..\..\..\..\Views\PopUps\NewEnquiryFormUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTemplateColumn buyerTargetPriceColumn;
        
        #line default
        #line hidden
        
        
        #line 505 "..\..\..\..\..\Views\PopUps\NewEnquiryFormUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTemplateColumn ourPriceColumn;
        
        #line default
        #line hidden
        
        
        #line 528 "..\..\..\..\..\Views\PopUps\NewEnquiryFormUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTemplateColumn profitLoss;
        
        #line default
        #line hidden
        
        
        #line 550 "..\..\..\..\..\Views\PopUps\NewEnquiryFormUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTemplateColumn totalOurPriceColumn;
        
        #line default
        #line hidden
        
        
        #line 573 "..\..\..\..\..\Views\PopUps\NewEnquiryFormUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTemplateColumn totalProfitLossColumn;
        
        #line default
        #line hidden
        
        
        #line 596 "..\..\..\..\..\Views\PopUps\NewEnquiryFormUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTemplateColumn orderValueColumn;
        
        #line default
        #line hidden
        
        
        #line 625 "..\..\..\..\..\Views\PopUps\NewEnquiryFormUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock orderValue;
        
        #line default
        #line hidden
        
        
        #line 633 "..\..\..\..\..\Views\PopUps\NewEnquiryFormUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button positiveDecisionBtn;
        
        #line default
        #line hidden
        
        
        #line 639 "..\..\..\..\..\Views\PopUps\NewEnquiryFormUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button negativeDecisionBtn;
        
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
            System.Uri resourceLocater = new System.Uri("/ordermanager;component/views/popups/newenquiryformusercontrol.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\Views\PopUps\NewEnquiryFormUserControl.xaml"
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
            this.rootLayout = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.customerComboBox = ((System.Windows.Controls.ComboBox)(target));
            
            #line 87 "..\..\..\..\..\Views\PopUps\NewEnquiryFormUserControl.xaml"
            this.customerComboBox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.customerComboBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.editExistingCustomerBtn = ((System.Windows.Controls.Button)(target));
            
            #line 98 "..\..\..\..\..\Views\PopUps\NewEnquiryFormUserControl.xaml"
            this.editExistingCustomerBtn.Click += new System.Windows.RoutedEventHandler(this.editExistingCustomerBtn_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.addNewCustomerBtn = ((System.Windows.Controls.Button)(target));
            
            #line 115 "..\..\..\..\..\Views\PopUps\NewEnquiryFormUserControl.xaml"
            this.addNewCustomerBtn.Click += new System.Windows.RoutedEventHandler(this.addNewCustomerBtn_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.orderDate = ((System.Windows.Controls.DatePicker)(target));
            
            #line 132 "..\..\..\..\..\Views\PopUps\NewEnquiryFormUserControl.xaml"
            this.orderDate.SelectedDateChanged += new System.EventHandler<System.Windows.Controls.SelectionChangedEventArgs>(this.orderDate_SelectedDateChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.expectedDeliveryDate = ((System.Windows.Controls.DatePicker)(target));
            
            #line 141 "..\..\..\..\..\Views\PopUps\NewEnquiryFormUserControl.xaml"
            this.expectedDeliveryDate.SelectedDateChanged += new System.EventHandler<System.Windows.Controls.SelectionChangedEventArgs>(this.expectedDeliveryDate_SelectedDateChanged);
            
            #line default
            #line hidden
            return;
            case 7:
            this.numberOfDays = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 8:
            this.internalDeliveryDate = ((System.Windows.Controls.DatePicker)(target));
            
            #line 152 "..\..\..\..\..\Views\PopUps\NewEnquiryFormUserControl.xaml"
            this.internalDeliveryDate.SelectedDateChanged += new System.EventHandler<System.Windows.Controls.SelectionChangedEventArgs>(this.internalDeliveryDate_SelectedDateChanged);
            
            #line default
            #line hidden
            return;
            case 9:
            this.internalDeliveryDateSpan = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 10:
            this.agentComboxBox = ((System.Windows.Controls.ComboBox)(target));
            
            #line 169 "..\..\..\..\..\Views\PopUps\NewEnquiryFormUserControl.xaml"
            this.agentComboxBox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.agentComboxBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 11:
            this.editExistingAgentBtn = ((System.Windows.Controls.Button)(target));
            
            #line 180 "..\..\..\..\..\Views\PopUps\NewEnquiryFormUserControl.xaml"
            this.editExistingAgentBtn.Click += new System.Windows.RoutedEventHandler(this.editExistingAgentBtn_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            this.addNewAgentBtn = ((System.Windows.Controls.Button)(target));
            
            #line 197 "..\..\..\..\..\Views\PopUps\NewEnquiryFormUserControl.xaml"
            this.addNewAgentBtn.Click += new System.Windows.RoutedEventHandler(this.addNewAgentBtn_Click);
            
            #line default
            #line hidden
            return;
            case 13:
            this.commisionInfo = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 14:
            
            #line 218 "..\..\..\..\..\Views\PopUps\NewEnquiryFormUserControl.xaml"
            ((System.Windows.Controls.TextBox)(target)).PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.TextBox_PreviewTextInput);
            
            #line default
            #line hidden
            
            #line 219 "..\..\..\..\..\Views\PopUps\NewEnquiryFormUserControl.xaml"
            ((System.Windows.Controls.TextBox)(target)).AddHandler(System.Windows.DataObject.PastingEvent, new System.Windows.DataObjectPastingEventHandler(this.PastingHandler));
            
            #line default
            #line hidden
            return;
            case 15:
            this.targetDatesGrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 16:
            this.addNewItemBtn = ((System.Windows.Controls.Button)(target));
            
            #line 286 "..\..\..\..\..\Views\PopUps\NewEnquiryFormUserControl.xaml"
            this.addNewItemBtn.PreviewMouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.addNewItemBtn_PreviewMouseLeftButtonUp);
            
            #line default
            #line hidden
            return;
            case 17:
            this.productsRequired = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 18:
            this.productColumn = ((System.Windows.Controls.DataGridTemplateColumn)(target));
            return;
            case 22:
            this.styleIDColumn = ((System.Windows.Controls.DataGridTemplateColumn)(target));
            return;
            case 23:
            this.quantityColumn = ((System.Windows.Controls.DataGridTemplateColumn)(target));
            return;
            case 25:
            this.unitOfMeasurementColumn = ((System.Windows.Controls.DataGridTemplateColumn)(target));
            return;
            case 26:
            this.currencyColumn = ((System.Windows.Controls.DataGridTemplateColumn)(target));
            return;
            case 28:
            this.currencyValueInINR = ((System.Windows.Controls.DataGridTemplateColumn)(target));
            return;
            case 30:
            this.buyerTargetPriceColumn = ((System.Windows.Controls.DataGridTemplateColumn)(target));
            return;
            case 32:
            this.ourPriceColumn = ((System.Windows.Controls.DataGridTemplateColumn)(target));
            return;
            case 33:
            this.profitLoss = ((System.Windows.Controls.DataGridTemplateColumn)(target));
            return;
            case 34:
            this.totalOurPriceColumn = ((System.Windows.Controls.DataGridTemplateColumn)(target));
            return;
            case 35:
            this.totalProfitLossColumn = ((System.Windows.Controls.DataGridTemplateColumn)(target));
            return;
            case 36:
            this.orderValueColumn = ((System.Windows.Controls.DataGridTemplateColumn)(target));
            return;
            case 37:
            this.orderValue = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 38:
            this.positiveDecisionBtn = ((System.Windows.Controls.Button)(target));
            
            #line 634 "..\..\..\..\..\Views\PopUps\NewEnquiryFormUserControl.xaml"
            this.positiveDecisionBtn.Click += new System.Windows.RoutedEventHandler(this.positiveDecisionBtn_Click);
            
            #line default
            #line hidden
            return;
            case 39:
            this.negativeDecisionBtn = ((System.Windows.Controls.Button)(target));
            
            #line 640 "..\..\..\..\..\Views\PopUps\NewEnquiryFormUserControl.xaml"
            this.negativeDecisionBtn.Click += new System.Windows.RoutedEventHandler(this.negativeDecisionBtn_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 19:
            
            #line 313 "..\..\..\..\..\Views\PopUps\NewEnquiryFormUserControl.xaml"
            ((System.Windows.Controls.ComboBox)(target)).AddHandler(System.Windows.Controls.Primitives.TextBoxBase.TextChangedEvent, new System.Windows.Controls.TextChangedEventHandler(this.comboBox_TextChanged));
            
            #line default
            #line hidden
            break;
            case 20:
            
            #line 325 "..\..\..\..\..\Views\PopUps\NewEnquiryFormUserControl.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.addNewProductBtn_Click);
            
            #line default
            #line hidden
            break;
            case 21:
            
            #line 347 "..\..\..\..\..\Views\PopUps\NewEnquiryFormUserControl.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.editStyleID_Click);
            
            #line default
            #line hidden
            break;
            case 24:
            
            #line 372 "..\..\..\..\..\Views\PopUps\NewEnquiryFormUserControl.xaml"
            ((System.Windows.Controls.TextBox)(target)).PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.TextBox_PreviewTextInput);
            
            #line default
            #line hidden
            
            #line 373 "..\..\..\..\..\Views\PopUps\NewEnquiryFormUserControl.xaml"
            ((System.Windows.Controls.TextBox)(target)).AddHandler(System.Windows.DataObject.PastingEvent, new System.Windows.DataObjectPastingEventHandler(this.PastingHandler));
            
            #line default
            #line hidden
            break;
            case 27:
            
            #line 433 "..\..\..\..\..\Views\PopUps\NewEnquiryFormUserControl.xaml"
            ((System.Windows.Controls.ComboBox)(target)).SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.currencyComboBox_SelectionChanged);
            
            #line default
            #line hidden
            break;
            case 29:
            
            #line 474 "..\..\..\..\..\Views\PopUps\NewEnquiryFormUserControl.xaml"
            ((System.Windows.Controls.TextBox)(target)).PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.TextBox_PreviewTextInput);
            
            #line default
            #line hidden
            
            #line 475 "..\..\..\..\..\Views\PopUps\NewEnquiryFormUserControl.xaml"
            ((System.Windows.Controls.TextBox)(target)).AddHandler(System.Windows.DataObject.PastingEvent, new System.Windows.DataObjectPastingEventHandler(this.PastingHandler));
            
            #line default
            #line hidden
            break;
            case 31:
            
            #line 497 "..\..\..\..\..\Views\PopUps\NewEnquiryFormUserControl.xaml"
            ((System.Windows.Controls.TextBox)(target)).PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.TextBox_PreviewTextInput);
            
            #line default
            #line hidden
            
            #line 498 "..\..\..\..\..\Views\PopUps\NewEnquiryFormUserControl.xaml"
            ((System.Windows.Controls.TextBox)(target)).AddHandler(System.Windows.DataObject.PastingEvent, new System.Windows.DataObjectPastingEventHandler(this.PastingHandler));
            
            #line default
            #line hidden
            break;
            }
        }
    }
}

