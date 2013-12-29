﻿#pragma checksum "..\..\..\..\..\Views\UserControls\ProductMaterialsControl.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "F7927081DD6F88A4405C6088D82BF11F"
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
using ordermanager.Views.UserControls;


namespace ordermanager.Views.UserControls {
    
    
    /// <summary>
    /// ProductMaterialsControl
    /// </summary>
    public partial class ProductMaterialsControl : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 11 "..\..\..\..\..\Views\UserControls\ProductMaterialsControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal ordermanager.Views.UserControls.ProductMaterialsControl This;
        
        #line default
        #line hidden
        
        
        #line 68 "..\..\..\..\..\Views\UserControls\ProductMaterialsControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView productsList;
        
        #line default
        #line hidden
        
        
        #line 71 "..\..\..\..\..\Views\UserControls\ProductMaterialsControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.GridViewColumn productColumn;
        
        #line default
        #line hidden
        
        
        #line 112 "..\..\..\..\..\Views\UserControls\ProductMaterialsControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnGeneratePDF;
        
        #line default
        #line hidden
        
        
        #line 119 "..\..\..\..\..\Views\UserControls\ProductMaterialsControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid productDetailsGrid;
        
        #line default
        #line hidden
        
        
        #line 185 "..\..\..\..\..\Views\UserControls\ProductMaterialsControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TabControl tabControl;
        
        #line default
        #line hidden
        
        
        #line 186 "..\..\..\..\..\Views\UserControls\ProductMaterialsControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TabItem tabViewMaterialsDetails;
        
        #line default
        #line hidden
        
        
        #line 188 "..\..\..\..\..\Views\UserControls\ProductMaterialsControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid gridDetails;
        
        #line default
        #line hidden
        
        
        #line 194 "..\..\..\..\..\Views\UserControls\ProductMaterialsControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel spAddDeleteButtons;
        
        #line default
        #line hidden
        
        
        #line 195 "..\..\..\..\..\Views\UserControls\ProductMaterialsControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnAddNewItem;
        
        #line default
        #line hidden
        
        
        #line 200 "..\..\..\..\..\Views\UserControls\ProductMaterialsControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnDeleteItem;
        
        #line default
        #line hidden
        
        
        #line 207 "..\..\..\..\..\Views\UserControls\ProductMaterialsControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid materialsGrid;
        
        #line default
        #line hidden
        
        
        #line 213 "..\..\..\..\..\Views\UserControls\ProductMaterialsControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTemplateColumn materialColumn;
        
        #line default
        #line hidden
        
        
        #line 245 "..\..\..\..\..\Views\UserControls\ProductMaterialsControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTemplateColumn consumptionColumn;
        
        #line default
        #line hidden
        
        
        #line 254 "..\..\..\..\..\Views\UserControls\ProductMaterialsControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTemplateColumn unitOfMeasurementColumn;
        
        #line default
        #line hidden
        
        
        #line 281 "..\..\..\..\..\Views\UserControls\ProductMaterialsControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTemplateColumn costPerUnitColumn;
        
        #line default
        #line hidden
        
        
        #line 290 "..\..\..\..\..\Views\UserControls\ProductMaterialsControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTemplateColumn currencyColumn;
        
        #line default
        #line hidden
        
        
        #line 317 "..\..\..\..\..\Views\UserControls\ProductMaterialsControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTemplateColumn currencyValueInINR;
        
        #line default
        #line hidden
        
        
        #line 338 "..\..\..\..\..\Views\UserControls\ProductMaterialsControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTemplateColumn extraCostPerUnitColumn;
        
        #line default
        #line hidden
        
        
        #line 360 "..\..\..\..\..\Views\UserControls\ProductMaterialsControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTemplateColumn extraCostNameColumn;
        
        #line default
        #line hidden
        
        
        #line 381 "..\..\..\..\..\Views\UserControls\ProductMaterialsControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTemplateColumn consumptionCostColumn;
        
        #line default
        #line hidden
        
        
        #line 416 "..\..\..\..\..\Views\UserControls\ProductMaterialsControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TabItem tabExtraCost;
        
        #line default
        #line hidden
        
        
        #line 418 "..\..\..\..\..\Views\UserControls\ProductMaterialsControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid gridExtraCostDetails;
        
        #line default
        #line hidden
        
        
        #line 432 "..\..\..\..\..\Views\UserControls\ProductMaterialsControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid extraCostGrid;
        
        #line default
        #line hidden
        
        
        #line 438 "..\..\..\..\..\Views\UserControls\ProductMaterialsControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTemplateColumn extraTypeColumn;
        
        #line default
        #line hidden
        
        
        #line 470 "..\..\..\..\..\Views\UserControls\ProductMaterialsControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGridTemplateColumn extracost;
        
        #line default
        #line hidden
        
        
        #line 513 "..\..\..\..\..\Views\UserControls\ProductMaterialsControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid gridButtons;
        
        #line default
        #line hidden
        
        
        #line 515 "..\..\..\..\..\Views\UserControls\ProductMaterialsControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button sendBackBtn;
        
        #line default
        #line hidden
        
        
        #line 523 "..\..\..\..\..\Views\UserControls\ProductMaterialsControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button saveBtn;
        
        #line default
        #line hidden
        
        
        #line 529 "..\..\..\..\..\Views\UserControls\ProductMaterialsControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button submitBtn;
        
        #line default
        #line hidden
        
        
        #line 535 "..\..\..\..\..\Views\UserControls\ProductMaterialsControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button discardBtn;
        
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
            System.Uri resourceLocater = new System.Uri("/ordermanager;component/views/usercontrols/productmaterialscontrol.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\Views\UserControls\ProductMaterialsControl.xaml"
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
            this.This = ((ordermanager.Views.UserControls.ProductMaterialsControl)(target));
            
            #line 11 "..\..\..\..\..\Views\UserControls\ProductMaterialsControl.xaml"
            this.This.DataContextChanged += new System.Windows.DependencyPropertyChangedEventHandler(this.UserControl_DataContextChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.productsList = ((System.Windows.Controls.ListView)(target));
            
            #line 68 "..\..\..\..\..\Views\UserControls\ProductMaterialsControl.xaml"
            this.productsList.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.productsList_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.productColumn = ((System.Windows.Controls.GridViewColumn)(target));
            return;
            case 4:
            this.btnGeneratePDF = ((System.Windows.Controls.Button)(target));
            
            #line 112 "..\..\..\..\..\Views\UserControls\ProductMaterialsControl.xaml"
            this.btnGeneratePDF.Click += new System.Windows.RoutedEventHandler(this.btnGeneratePDF_Click_1);
            
            #line default
            #line hidden
            return;
            case 5:
            this.productDetailsGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 6:
            this.tabControl = ((System.Windows.Controls.TabControl)(target));
            return;
            case 7:
            this.tabViewMaterialsDetails = ((System.Windows.Controls.TabItem)(target));
            return;
            case 8:
            this.gridDetails = ((System.Windows.Controls.Grid)(target));
            return;
            case 9:
            this.spAddDeleteButtons = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 10:
            this.btnAddNewItem = ((System.Windows.Controls.Button)(target));
            
            #line 195 "..\..\..\..\..\Views\UserControls\ProductMaterialsControl.xaml"
            this.btnAddNewItem.Click += new System.Windows.RoutedEventHandler(this.AddNewItem_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.btnDeleteItem = ((System.Windows.Controls.Button)(target));
            
            #line 200 "..\..\..\..\..\Views\UserControls\ProductMaterialsControl.xaml"
            this.btnDeleteItem.Click += new System.Windows.RoutedEventHandler(this.DeleteItem_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            this.materialsGrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 13:
            this.materialColumn = ((System.Windows.Controls.DataGridTemplateColumn)(target));
            return;
            case 16:
            this.consumptionColumn = ((System.Windows.Controls.DataGridTemplateColumn)(target));
            return;
            case 17:
            this.unitOfMeasurementColumn = ((System.Windows.Controls.DataGridTemplateColumn)(target));
            return;
            case 18:
            this.costPerUnitColumn = ((System.Windows.Controls.DataGridTemplateColumn)(target));
            return;
            case 19:
            this.currencyColumn = ((System.Windows.Controls.DataGridTemplateColumn)(target));
            return;
            case 20:
            this.currencyValueInINR = ((System.Windows.Controls.DataGridTemplateColumn)(target));
            return;
            case 21:
            this.extraCostPerUnitColumn = ((System.Windows.Controls.DataGridTemplateColumn)(target));
            return;
            case 22:
            this.extraCostNameColumn = ((System.Windows.Controls.DataGridTemplateColumn)(target));
            return;
            case 23:
            this.consumptionCostColumn = ((System.Windows.Controls.DataGridTemplateColumn)(target));
            return;
            case 24:
            this.tabExtraCost = ((System.Windows.Controls.TabItem)(target));
            return;
            case 25:
            this.gridExtraCostDetails = ((System.Windows.Controls.Grid)(target));
            return;
            case 26:
            
            #line 425 "..\..\..\..\..\Views\UserControls\ProductMaterialsControl.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.AddNewExtraCostItem_Click);
            
            #line default
            #line hidden
            return;
            case 27:
            this.extraCostGrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 28:
            this.extraTypeColumn = ((System.Windows.Controls.DataGridTemplateColumn)(target));
            return;
            case 31:
            this.extracost = ((System.Windows.Controls.DataGridTemplateColumn)(target));
            return;
            case 32:
            this.gridButtons = ((System.Windows.Controls.Grid)(target));
            return;
            case 33:
            this.sendBackBtn = ((System.Windows.Controls.Button)(target));
            return;
            case 34:
            this.saveBtn = ((System.Windows.Controls.Button)(target));
            
            #line 524 "..\..\..\..\..\Views\UserControls\ProductMaterialsControl.xaml"
            this.saveBtn.Click += new System.Windows.RoutedEventHandler(this.Save_Click);
            
            #line default
            #line hidden
            return;
            case 35:
            this.submitBtn = ((System.Windows.Controls.Button)(target));
            
            #line 530 "..\..\..\..\..\Views\UserControls\ProductMaterialsControl.xaml"
            this.submitBtn.Click += new System.Windows.RoutedEventHandler(this.submitBtn_Click);
            
            #line default
            #line hidden
            return;
            case 36:
            this.discardBtn = ((System.Windows.Controls.Button)(target));
            
            #line 537 "..\..\..\..\..\Views\UserControls\ProductMaterialsControl.xaml"
            this.discardBtn.Click += new System.Windows.RoutedEventHandler(this.discardBtn_Click);
            
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
            case 14:
            
            #line 230 "..\..\..\..\..\Views\UserControls\ProductMaterialsControl.xaml"
            ((System.Windows.Controls.ComboBox)(target)).AddHandler(System.Windows.Controls.Primitives.TextBoxBase.TextChangedEvent, new System.Windows.Controls.TextChangedEventHandler(this.comboBox_TextChanged));
            
            #line default
            #line hidden
            break;
            case 15:
            
            #line 233 "..\..\..\..\..\Views\UserControls\ProductMaterialsControl.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.AddNewMaterial);
            
            #line default
            #line hidden
            break;
            case 29:
            
            #line 455 "..\..\..\..\..\Views\UserControls\ProductMaterialsControl.xaml"
            ((System.Windows.Controls.ComboBox)(target)).AddHandler(System.Windows.Controls.Primitives.TextBoxBase.TextChangedEvent, new System.Windows.Controls.TextChangedEventHandler(this.extraTypeComboBox_TextChanged));
            
            #line default
            #line hidden
            break;
            case 30:
            
            #line 458 "..\..\..\..\..\Views\UserControls\ProductMaterialsControl.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.AddNewExtraCostItem);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}

