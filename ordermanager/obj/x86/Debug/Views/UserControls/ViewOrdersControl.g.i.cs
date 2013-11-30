﻿#pragma checksum "..\..\..\..\..\Views\UserControls\ViewOrdersControl.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "907B288CA5FF6354B2E600019478C693"
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
using ordermanager.ViewModel;


namespace ordermanager.Views.UserControls {
    
    
    /// <summary>
    /// ViewOrdersControl
    /// </summary>
    public partial class ViewOrdersControl : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 62 "..\..\..\..\..\Views\UserControls\ViewOrdersControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox companyNameFilter;
        
        #line default
        #line hidden
        
        
        #line 67 "..\..\..\..\..\Views\UserControls\ViewOrdersControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker fromDateFilter;
        
        #line default
        #line hidden
        
        
        #line 72 "..\..\..\..\..\Views\UserControls\ViewOrdersControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker toDateFilter;
        
        #line default
        #line hidden
        
        
        #line 76 "..\..\..\..\..\Views\UserControls\ViewOrdersControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox searchText;
        
        #line default
        #line hidden
        
        
        #line 90 "..\..\..\..\..\Views\UserControls\ViewOrdersControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button clearFilterBtn;
        
        #line default
        #line hidden
        
        
        #line 102 "..\..\..\..\..\Views\UserControls\ViewOrdersControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView lvOrders;
        
        #line default
        #line hidden
        
        
        #line 146 "..\..\..\..\..\Views\UserControls\ViewOrdersControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock noTasksInfoLabel;
        
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
            System.Uri resourceLocater = new System.Uri("/ordermanager;component/views/usercontrols/vieworderscontrol.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\Views\UserControls\ViewOrdersControl.xaml"
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
            
            #line 44 "..\..\..\..\..\Views\UserControls\ViewOrdersControl.xaml"
            ((System.Windows.Data.CollectionViewSource)(target)).Filter += new System.Windows.Data.FilterEventHandler(this.CollectionViewSource_Filter);
            
            #line default
            #line hidden
            return;
            case 2:
            this.companyNameFilter = ((System.Windows.Controls.ComboBox)(target));
            
            #line 65 "..\..\..\..\..\Views\UserControls\ViewOrdersControl.xaml"
            this.companyNameFilter.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.companyNameFilter_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.fromDateFilter = ((System.Windows.Controls.DatePicker)(target));
            
            #line 70 "..\..\..\..\..\Views\UserControls\ViewOrdersControl.xaml"
            this.fromDateFilter.SelectedDateChanged += new System.EventHandler<System.Windows.Controls.SelectionChangedEventArgs>(this.fromDateFilter_SelectedDateChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.toDateFilter = ((System.Windows.Controls.DatePicker)(target));
            
            #line 75 "..\..\..\..\..\Views\UserControls\ViewOrdersControl.xaml"
            this.toDateFilter.SelectedDateChanged += new System.EventHandler<System.Windows.Controls.SelectionChangedEventArgs>(this.toDateFilter_SelectedDateChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.searchText = ((System.Windows.Controls.TextBox)(target));
            
            #line 82 "..\..\..\..\..\Views\UserControls\ViewOrdersControl.xaml"
            this.searchText.TextInput += new System.Windows.Input.TextCompositionEventHandler(this.searchText_TextInput);
            
            #line default
            #line hidden
            return;
            case 6:
            this.clearFilterBtn = ((System.Windows.Controls.Button)(target));
            
            #line 92 "..\..\..\..\..\Views\UserControls\ViewOrdersControl.xaml"
            this.clearFilterBtn.Click += new System.Windows.RoutedEventHandler(this.clearFilterBtn_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.lvOrders = ((System.Windows.Controls.ListView)(target));
            
            #line 103 "..\..\..\..\..\Views\UserControls\ViewOrdersControl.xaml"
            this.lvOrders.MouseLeftButtonUp += new System.Windows.Input.MouseButtonEventHandler(this.lvOrders_MouseLeftButtonUp);
            
            #line default
            #line hidden
            return;
            case 8:
            this.noTasksInfoLabel = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

