﻿

#pragma checksum "C:\Users\Paulo\Source\Repos\cfs-xaml\WS.CFS\FeedbackListPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "8EA908C284D0B9E05338F39D86F3E30D"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WS.CFS
{
    partial class FeedbackListPage : global::Windows.UI.Xaml.Controls.Page, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 54 "..\..\FeedbackListPage.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).DoubleTapped += this.grdFeedback_DoubleTapped;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 46 "..\..\FeedbackListPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.AppBarButton_Click;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}


