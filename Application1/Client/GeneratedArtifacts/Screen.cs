﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LightSwitchApplication
{
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.LightSwitch.BuildTasks.CodeGen", "11.0.0.0")]
    public sealed partial class CreateNew
        : global::Microsoft.LightSwitch.Framework.Client.ScreenObject<global::LightSwitchApplication.CreateNew, global::LightSwitchApplication.CreateNew.DetailsClass>
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.LightSwitch.BuildTasks.CodeGen", "11.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private CreateNew() : base("LightSwitchApplication:CreateNew")
        {
            global::LightSwitchApplication.CreateNew.DetailsClass.Initialize(this);
        }

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.LightSwitch.BuildTasks.CodeGen", "11.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
        public static CreateNew CreateInstance()
        {
            return new global::LightSwitchApplication.CreateNew(
            );
        }

        [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        partial void CreateNew_InitializeDataWorkspace(global::System.Collections.Generic.List<global::Microsoft.LightSwitch.IDataService> saveChangesTo);
        [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        partial void CreateNew_Created();
        [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        partial void CreateNew_Activated();
        [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        partial void CreateNew_Saving(ref bool handled);
        [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        partial void CreateNew_Saved();
        [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        partial void CreateNew_Closing(ref bool cancel);
        [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        partial void CreateNew_SaveError(global::System.Exception exception, ref bool handled);
     
        #region Private Properties
        
        /// <summary>
        /// Gets the Application object for this application.  The Application object provides access to active screens, methods to open screens and access to the current user.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.LightSwitch.BuildTasks.CodeGen", "11.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private global::LightSwitchApplication.Application Application
        {
            get
            {
                return global::LightSwitchApplication.Application.Current;
            }
        }
        
        /// <summary>
        /// Gets the containing data workspace.  The data workspace provides access to all data sources in the application.
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.LightSwitch.BuildTasks.CodeGen", "11.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private global::LightSwitchApplication.DataWorkspace DataWorkspace
        {
            get
            {
                return (global::LightSwitchApplication.DataWorkspace)((global::Microsoft.LightSwitch.Details.Client.IScreenDetails)this.Details).DataWorkspace;
            }
        }
        
        #endregion
 
        [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.LightSwitch.BuildTasks.CodeGen", "11.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public sealed class DetailsClass
            : global::Microsoft.LightSwitch.Details.Framework.Client.ScreenDetails<global::LightSwitchApplication.CreateNew, global::LightSwitchApplication.CreateNew.DetailsClass, global::LightSwitchApplication.CreateNew.DetailsClass.PropertySet, global::LightSwitchApplication.CreateNew.DetailsClass.CommandSet, global::LightSwitchApplication.CreateNew.DetailsClass.MethodSet>
        {

            static DetailsClass()
            {
            }

            [global::System.Diagnostics.DebuggerBrowsable(global::System.Diagnostics.DebuggerBrowsableState.Never)]
            private static global::Microsoft.LightSwitch.Details.Framework.Client.ScreenDetails<global::LightSwitchApplication.CreateNew, global::LightSwitchApplication.CreateNew.DetailsClass>.Entry
                __CreateNewEntry = new global::Microsoft.LightSwitch.Details.Framework.Client.ScreenDetails<global::LightSwitchApplication.CreateNew, global::LightSwitchApplication.CreateNew.DetailsClass>.Entry(
                    global::LightSwitchApplication.CreateNew.DetailsClass.__CreateNew_InvokeInitializeDataWorkspace,
                    global::LightSwitchApplication.CreateNew.DetailsClass.__CreateNew_InvokeSavingEvent,
                    global::LightSwitchApplication.CreateNew.DetailsClass.__CreateNew_InvokeSavedEvent,
                    global::LightSwitchApplication.CreateNew.DetailsClass.__CreateNew_InvokeClosingEvent,
                    global::LightSwitchApplication.CreateNew.DetailsClass.__CreateNew_InvokeCreated,
                    global::LightSwitchApplication.CreateNew.DetailsClass.__CreateNew_InvokeActivated,
                    global::LightSwitchApplication.CreateNew.DetailsClass.__CreateNew_InvokeSaveErrorEvent);
            private static void __CreateNew_InvokeInitializeDataWorkspace(global::LightSwitchApplication.CreateNew s, global::System.Collections.Generic.List<global::Microsoft.LightSwitch.IDataService> saveChangesTo)
            {
                s.CreateNew_InitializeDataWorkspace(saveChangesTo);
            }
            private static bool __CreateNew_InvokeSavingEvent(global::LightSwitchApplication.CreateNew s)
            {
                bool handled = false;
                s.CreateNew_Saving(ref handled);
                return handled;
            }
            private static void __CreateNew_InvokeSavedEvent(global::LightSwitchApplication.CreateNew s)
            {
                s.CreateNew_Saved();
            }
            private static bool __CreateNew_InvokeClosingEvent(global::LightSwitchApplication.CreateNew s)
            {
                bool cancel = false;
                s.CreateNew_Closing(ref cancel);
                return cancel;
            }
            private static void __CreateNew_InvokeCreated(global::LightSwitchApplication.CreateNew s)
            {
                s.CreateNew_Created();
            }
            private static void __CreateNew_InvokeActivated(global::LightSwitchApplication.CreateNew s)
            {
                s.CreateNew_Activated();
            }
            private static bool __CreateNew_InvokeSaveErrorEvent(global::LightSwitchApplication.CreateNew s, global::System.Exception ex)
            {
                bool handled = false;
                s.CreateNew_SaveError(ex, ref handled);
                return handled;
            }

            public DetailsClass() : base()
            {
            }

            public new global::LightSwitchApplication.CreateNew.DetailsClass.PropertySet Properties
            {
                get
                {
                    return base.Properties;
                }
            }

            public new global::LightSwitchApplication.CreateNew.DetailsClass.CommandSet Commands
            {
                get
                {
                    return base.Commands;
                }
            }

            public new global::LightSwitchApplication.CreateNew.DetailsClass.MethodSet Methods
            {
                get
                {
                    return base.Methods;
                }
            }

            [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.LightSwitch.BuildTasks.CodeGen", "11.0.0.0")]
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public sealed class PropertySet
                : global::Microsoft.LightSwitch.Details.Framework.Client.ScreenPropertySet<global::LightSwitchApplication.CreateNew, global::LightSwitchApplication.CreateNew.DetailsClass>
            {
            }

            [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.LightSwitch.BuildTasks.CodeGen", "11.0.0.0")]
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public sealed class CommandSet
                : global::Microsoft.LightSwitch.Details.Framework.Client.ScreenCommandSet<global::LightSwitchApplication.CreateNew, global::LightSwitchApplication.CreateNew.DetailsClass>
            {
            }

            [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.LightSwitch.BuildTasks.CodeGen", "11.0.0.0")]
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            public sealed class MethodSet
                : global::Microsoft.LightSwitch.Details.Framework.Client.ScreenMethodSet<global::LightSwitchApplication.CreateNew, global::LightSwitchApplication.CreateNew.DetailsClass>
            {
            }

            [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.LightSwitch.BuildTasks.CodeGen", "11.0.0.0")]
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            internal sealed class PropertySetProperties
            {
            }

            [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.LightSwitch.BuildTasks.CodeGen", "11.0.0.0")]
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            internal sealed class CommandSetProperties
            {
            }

            [global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
            [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.LightSwitch.BuildTasks.CodeGen", "11.0.0.0")]
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
            internal sealed class MethodSetProperties
            {
            }
        }
    }
}
