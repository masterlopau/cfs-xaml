﻿

#pragma checksum "C:\Users\Paulo\Source\Repos\cfs-xaml\WS.CFS.WindowsPhone\SearchResultsPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "459AA1E532EC1B6F686FC1CCA8F1985B"
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
    partial class SearchResultsPage : global::Windows.UI.Xaml.Controls.Page, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 87 "..\..\SearchResultsPage.xaml"
                ((global::Windows.UI.Xaml.Controls.ListViewBase)(target)).ItemClick += this.resultsGridView_ItemClick;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 67 "..\..\SearchResultsPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ToggleButton)(target)).Checked += this.Filter_Checked;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}

