﻿
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.ApplicationSettings;
using WS.CFS.Common;
using WS.CFS.Data;

// The Blank Application template is documented at http://go.microsoft.com/fwlink/?LinkId=234227

namespace WS.CFS
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
           
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override async void OnLaunched(LaunchActivatedEventArgs e)
        {

#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                this.DebugSettings.EnableFrameRateCounter = true;
            }
#endif

            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                //Associate the frame with a SuspensionManager key                                
                SuspensionManager.RegisterFrame(rootFrame, "appFrame");

                // Set the default language
                rootFrame.Language = Windows.Globalization.ApplicationLanguages.Languages[0];

                rootFrame.NavigationFailed += OnNavigationFailed;

                var connectionProfile = Windows.Networking.Connectivity.NetworkInformation.GetInternetConnectionProfile();
                if (connectionProfile != null)
                {
                    var feedbackDataSource = (FeedbackDataSource)App.Current.Resources["feedbackDataSource"];
                    if (feedbackDataSource != null)
                    {
                        if (feedbackDataSource.Feedbacks.Count == 0)
                            await feedbackDataSource.GetFeedbacksAsync();
                    }
                }
                else
                {
                    var messageDialog = new Windows.UI.Popups.MessageDialog("An internet connection is needed to view the data. Please check your connection and restart the app.");
                    var result = messageDialog.ShowAsync();
                }


                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    // Restore the saved session state only when appropriate
                    try
                    {
                        await SuspensionManager.RestoreAsync();
                    }
                    catch (SuspensionManagerException)
                    {
                        //Something went wrong restoring state.
                        //Assume there is no state and continue
                    }
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                rootFrame.Navigate(typeof(LoginPage), e.Arguments);
            }
            // Ensure the current window is active
            Window.Current.Activate();

            //Windows.ApplicationModel.Search.SearchPane.GetForCurrentView().SuggestionsRequested += App_SuggestionsRequested;
        }
        private readonly string[] _suggestions = { "Apple", "Banana", "Orange" };
        void App_SuggestionsRequested(Windows.ApplicationModel.Search.SearchPane sender, Windows.ApplicationModel.Search.SearchPaneSuggestionsRequestedEventArgs args)
        {
            var request = args.Request;
            foreach (var suggestion in _suggestions)
            {
                if (suggestion.StartsWith(args.QueryText, StringComparison.CurrentCultureIgnoreCase))
                {
                    request.SearchSuggestionCollection.AppendQuerySuggestion(suggestion);
                }
            }
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private async void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            await SuspensionManager.SaveAsync();
            deferral.Complete();
        }

        protected override void OnWindowCreated(WindowCreatedEventArgs args)
        {
            SettingsPane.GetForCurrentView().CommandsRequested += App_CommandsRequested;
        }

        void App_CommandsRequested(SettingsPane sender, SettingsPaneCommandsRequestedEventArgs args)
        {
            // Add a preferences command to the settings pane
            var preferences = new SettingsCommand("preferences", "Preferences", (handler) => new PreferencesSettingsFlyout().Show());
            args.Request.ApplicationCommands.Add(preferences);
        }

        protected override void OnSearchActivated(SearchActivatedEventArgs args)
        {
            if (Window.Current.Content == null) return;
            var rootFrame = Window.Current.Content as Frame;
            if (rootFrame != null) rootFrame.Navigate(typeof(SearchResultsPage), args.QueryText);
        }
    }
}
